namespace sale
{
    partial class P_SA_ESTMT_REG_NEW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_ESTMT_REG_NEW));
            this.lbl상태 = new Duzon.Common.Controls.LabelExt();
            this.ctx영업그룹조회 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.ctx견적번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cbo상태 = new Duzon.Common.Controls.DropDownComboBox();
            this.ctx거래처조회 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl견적일자조회 = new Duzon.Common.Controls.LabelExt();
            this.img = new System.Windows.Forms.ImageList(this.components);
            this.tab = new Duzon.Common.Controls.TabControlExt();
            this.tpg공통 = new System.Windows.Forms.TabPage();
            this.lay공통 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl공통5 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem15 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl25 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo할인구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl할인구분 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl24 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl기준환종 = new Duzon.Common.Controls.LabelExt();
            this.cbo기준환종 = new Duzon.Common.Controls.DropDownComboBox();
            this.pnl공통4 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem14 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl23 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp확정일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl확정일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl20 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo확정여부 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl확정여부 = new Duzon.Common.Controls.LabelExt();
            this.pnl공통3 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem12 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl19 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl결제조건 = new Duzon.Common.Controls.LabelExt();
            this.cbo결제방법 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem13 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl22 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp납기일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl납기일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl21 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl유효일자국내 = new Duzon.Common.Controls.LabelExt();
            this.dtp유효일자국내 = new Duzon.Common.Controls.DatePicker();
            this.pnl공통2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem10 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl16 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur환율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.cbo환종 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl15 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl수주형태 = new Duzon.Common.Controls.LabelExt();
            this.ctx수주형태 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem11 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl18 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo부가세포함 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl부가세포함 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl17 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl과세구분 = new Duzon.Common.Controls.LabelExt();
            this.cur부가세율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo과세구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.pnl공통1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl차수 = new Duzon.Common.Controls.LabelExt();
            this.cur차수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl견적번호 = new Duzon.Common.Controls.LabelExt();
            this.txt견적번호 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl견적명 = new Duzon.Common.Controls.LabelExt();
            this.txt견적명 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.ctx거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl견적일자 = new Duzon.Common.Controls.LabelExt();
            this.dtp견적일자 = new Duzon.Common.Controls.DatePicker();
            this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl임시거래처 = new Duzon.Common.Controls.LabelExt();
            this.textBoxExt1 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl견적요청자 = new Duzon.Common.Controls.LabelExt();
            this.txt견적요청자 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem8 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.ctx영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem9 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl14 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.ctx사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl계약번호 = new Duzon.Common.Controls.LabelExt();
            this.txt계약번호 = new Duzon.Common.Controls.TextBoxExt();
            this.tpg해외 = new System.Windows.Forms.TabPage();
            this.lay해외 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl해외 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem16 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl27 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl제조자 = new Duzon.Common.Controls.LabelExt();
            this.ctx제조자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl26 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl수출자 = new Duzon.Common.Controls.LabelExt();
            this.ctx수출자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem17 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl29 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl검사기관 = new Duzon.Common.Controls.LabelExt();
            this.txt검사기관 = new Duzon.Common.Controls.TextBoxExt();
            this.bpPanelControl28 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl인도조건 = new Duzon.Common.Controls.LabelExt();
            this.textBoxExt2 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem18 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl31 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl결제일 = new Duzon.Common.Controls.LabelExt();
            this.cur결제일 = new Duzon.Common.Controls.CurrencyTextBox();
            this.bpPanelControl30 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl결제형태 = new Duzon.Common.Controls.LabelExt();
            this.cbo결제형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem19 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl33 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl유효일자해외 = new Duzon.Common.Controls.LabelExt();
            this.dtp유효일자해외 = new Duzon.Common.Controls.DatePicker();
            this.bpPanelControl32 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl포장형태 = new Duzon.Common.Controls.LabelExt();
            this.cbo포장형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem20 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl35 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl운송형태 = new Duzon.Common.Controls.LabelExt();
            this.cbo운송형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl34 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl운송방법 = new Duzon.Common.Controls.LabelExt();
            this.cbo운송방법 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem21 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl36 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl선적항 = new Duzon.Common.Controls.LabelExt();
            this.txt선적항 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem22 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl37 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl도착항 = new Duzon.Common.Controls.LabelExt();
            this.txt도착항 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem23 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl38 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl원산지 = new Duzon.Common.Controls.LabelExt();
            this.cbo원산지 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem24 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl39 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl목적지 = new Duzon.Common.Controls.LabelExt();
            this.txt목적지 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem25 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl40 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo가격조건 = new Duzon.Common.Controls.DropDownComboBox();
            this.labelExt4 = new Duzon.Common.Controls.LabelExt();
            this.tpg비고 = new System.Windows.Forms.TabPage();
            this.lay비고 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl비고 = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid3 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem26 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl41 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고1 = new Duzon.Common.Controls.LabelExt();
            this.txt비고1 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem27 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl42 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고2 = new Duzon.Common.Controls.LabelExt();
            this.txt비고2 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem28 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl43 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고3 = new Duzon.Common.Controls.LabelExt();
            this.txt비고3 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem29 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl44 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고4 = new Duzon.Common.Controls.LabelExt();
            this.txt비고4 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem30 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl45 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고5 = new Duzon.Common.Controls.LabelExt();
            this.txt비고5 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem31 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl46 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고6 = new Duzon.Common.Controls.LabelExt();
            this.txt비고6 = new Duzon.Common.Controls.TextBoxExt();
            this.flexibleRoundedCornerBox1 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
            this.lbl비고7 = new Duzon.Common.Controls.LabelExt();
            this.txt멀티비고 = new Duzon.Common.Controls.TextBoxExt();
            this.tpg기타 = new System.Windows.Forms.TabPage();
            this.lay기타 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl기타 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem33 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl52 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl날짜_사용자정의_2 = new Duzon.Common.Controls.LabelExt();
            this.dtp날짜_사용자정의_2 = new Duzon.Common.Controls.DatePicker();
            this.bpPanelControl47 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl날짜_사용자정의_1 = new Duzon.Common.Controls.LabelExt();
            this.dtp날짜_사용자정의_1 = new Duzon.Common.Controls.DatePicker();
            this.oneGridItem34 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl48 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_1 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_1 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem35 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl49 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_2 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_2 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem36 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl50 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_3 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_3 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem37 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl51 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_4 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_4 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem38 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl53 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_5 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_5 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem39 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl54 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl텍스트_사용자정의_6 = new Duzon.Common.Controls.LabelExt();
            this.txt텍스트_사용자정의_6 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem40 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl56 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_2 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_2 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl55 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_1 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_1 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem41 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl57 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_4 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_4 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl58 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_3 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_3 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem42 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl59 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_6 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_6 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl60 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl코드_사용자정의_5 = new Duzon.Common.Controls.LabelExt();
            this.cbo코드_사용자정의_5 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem43 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl61 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl숫자_사용자정의_2 = new Duzon.Common.Controls.LabelExt();
            this.cur숫자_사용자정의_2 = new Duzon.Common.Controls.CurrencyTextBox();
            this.bpPanelControl62 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl숫자_사용자정의_1 = new Duzon.Common.Controls.LabelExt();
            this.cur숫자_사용자정의_1 = new Duzon.Common.Controls.CurrencyTextBox();
            this.oneGridItem44 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl63 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl숫자_사용자정의_4 = new Duzon.Common.Controls.LabelExt();
            this.cur숫자_사용자정의_4 = new Duzon.Common.Controls.CurrencyTextBox();
            this.bpPanelControl64 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl숫자_사용자정의_3 = new Duzon.Common.Controls.LabelExt();
            this.cur숫자_사용자정의_3 = new Duzon.Common.Controls.CurrencyTextBox();
            this.oneGridItem45 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl65 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur숫자_사용자정의_6 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl숫자_사용자정의_6 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl66 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl숫자_사용자정의_5 = new Duzon.Common.Controls.LabelExt();
            this.cur숫자_사용자정의_5 = new Duzon.Common.Controls.CurrencyTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt21 = new Duzon.Common.Controls.PanelExt();
            this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.lbl할인율 = new Duzon.Common.Controls.LabelExt();
            this.btn할인율적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.cur할인율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.ctx담당자조회 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.periodPicker1 = new Duzon.Common.Controls.PeriodPicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn차수추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn차수삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확정취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확정 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn파일업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn메일전송 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt30 = new Duzon.Common.Controls.PanelExt();
            this.panelExt35 = new Duzon.Common.Controls.PanelExt();
            this.textBoxExt3 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt40 = new Duzon.Common.Controls.PanelExt();
            this.textBoxExt4 = new Duzon.Common.Controls.TextBoxExt();
            this.textBoxExt5 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt41 = new Duzon.Common.Controls.PanelExt();
            this.textBoxExt6 = new Duzon.Common.Controls.TextBoxExt();
            this.textBoxExt7 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt42 = new Duzon.Common.Controls.PanelExt();
            this.textBoxExt8 = new Duzon.Common.Controls.TextBoxExt();
            this.textBoxExt9 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt43 = new Duzon.Common.Controls.PanelExt();
            this.panelExt44 = new Duzon.Common.Controls.PanelExt();
            this.labelExt5 = new Duzon.Common.Controls.LabelExt();
            this.labelExt6 = new Duzon.Common.Controls.LabelExt();
            this.labelExt7 = new Duzon.Common.Controls.LabelExt();
            this.panelExt45 = new Duzon.Common.Controls.PanelExt();
            this.labelExt9 = new Duzon.Common.Controls.LabelExt();
            this.labelExt10 = new Duzon.Common.Controls.LabelExt();
            this.labelExt11 = new Duzon.Common.Controls.LabelExt();
            this.labelExt13 = new Duzon.Common.Controls.LabelExt();
            this.btn전자결재 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.oneGridItem32 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.m_FileDlg = new System.Windows.Forms.OpenFileDialog();
            this.mDataArea.SuspendLayout();
            this.tab.SuspendLayout();
            this.tpg공통.SuspendLayout();
            this.lay공통.SuspendLayout();
            this.oneGridItem15.SuspendLayout();
            this.bpPanelControl25.SuspendLayout();
            this.bpPanelControl24.SuspendLayout();
            this.oneGridItem14.SuspendLayout();
            this.bpPanelControl23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp확정일자)).BeginInit();
            this.bpPanelControl20.SuspendLayout();
            this.oneGridItem12.SuspendLayout();
            this.bpPanelControl19.SuspendLayout();
            this.oneGridItem13.SuspendLayout();
            this.bpPanelControl22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp납기일자)).BeginInit();
            this.bpPanelControl21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자국내)).BeginInit();
            this.oneGridItem10.SuspendLayout();
            this.bpPanelControl16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).BeginInit();
            this.bpPanelControl15.SuspendLayout();
            this.oneGridItem11.SuspendLayout();
            this.bpPanelControl18.SuspendLayout();
            this.bpPanelControl17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).BeginInit();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur차수)).BeginInit();
            this.bpPanelControl3.SuspendLayout();
            this.oneGridItem4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.oneGridItem5.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp견적일자)).BeginInit();
            this.oneGridItem6.SuspendLayout();
            this.bpPanelControl8.SuspendLayout();
            this.oneGridItem7.SuspendLayout();
            this.bpPanelControl10.SuspendLayout();
            this.bpPanelControl9.SuspendLayout();
            this.oneGridItem8.SuspendLayout();
            this.bpPanelControl12.SuspendLayout();
            this.bpPanelControl11.SuspendLayout();
            this.oneGridItem9.SuspendLayout();
            this.bpPanelControl14.SuspendLayout();
            this.bpPanelControl13.SuspendLayout();
            this.tpg해외.SuspendLayout();
            this.lay해외.SuspendLayout();
            this.oneGridItem16.SuspendLayout();
            this.bpPanelControl27.SuspendLayout();
            this.bpPanelControl26.SuspendLayout();
            this.oneGridItem17.SuspendLayout();
            this.bpPanelControl29.SuspendLayout();
            this.bpPanelControl28.SuspendLayout();
            this.oneGridItem18.SuspendLayout();
            this.bpPanelControl31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur결제일)).BeginInit();
            this.bpPanelControl30.SuspendLayout();
            this.oneGridItem19.SuspendLayout();
            this.bpPanelControl33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자해외)).BeginInit();
            this.bpPanelControl32.SuspendLayout();
            this.oneGridItem20.SuspendLayout();
            this.bpPanelControl35.SuspendLayout();
            this.bpPanelControl34.SuspendLayout();
            this.oneGridItem21.SuspendLayout();
            this.bpPanelControl36.SuspendLayout();
            this.oneGridItem22.SuspendLayout();
            this.bpPanelControl37.SuspendLayout();
            this.oneGridItem23.SuspendLayout();
            this.bpPanelControl38.SuspendLayout();
            this.oneGridItem24.SuspendLayout();
            this.bpPanelControl39.SuspendLayout();
            this.oneGridItem25.SuspendLayout();
            this.bpPanelControl40.SuspendLayout();
            this.tpg비고.SuspendLayout();
            this.lay비고.SuspendLayout();
            this.pnl비고.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.oneGridItem26.SuspendLayout();
            this.bpPanelControl41.SuspendLayout();
            this.oneGridItem27.SuspendLayout();
            this.bpPanelControl42.SuspendLayout();
            this.oneGridItem28.SuspendLayout();
            this.bpPanelControl43.SuspendLayout();
            this.oneGridItem29.SuspendLayout();
            this.bpPanelControl44.SuspendLayout();
            this.oneGridItem30.SuspendLayout();
            this.bpPanelControl45.SuspendLayout();
            this.oneGridItem31.SuspendLayout();
            this.bpPanelControl46.SuspendLayout();
            this.flexibleRoundedCornerBox1.SuspendLayout();
            this.tpg기타.SuspendLayout();
            this.lay기타.SuspendLayout();
            this.oneGridItem33.SuspendLayout();
            this.bpPanelControl52.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp날짜_사용자정의_2)).BeginInit();
            this.bpPanelControl47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp날짜_사용자정의_1)).BeginInit();
            this.oneGridItem34.SuspendLayout();
            this.bpPanelControl48.SuspendLayout();
            this.oneGridItem35.SuspendLayout();
            this.bpPanelControl49.SuspendLayout();
            this.oneGridItem36.SuspendLayout();
            this.bpPanelControl50.SuspendLayout();
            this.oneGridItem37.SuspendLayout();
            this.bpPanelControl51.SuspendLayout();
            this.oneGridItem38.SuspendLayout();
            this.bpPanelControl53.SuspendLayout();
            this.oneGridItem39.SuspendLayout();
            this.bpPanelControl54.SuspendLayout();
            this.oneGridItem40.SuspendLayout();
            this.bpPanelControl56.SuspendLayout();
            this.bpPanelControl55.SuspendLayout();
            this.oneGridItem41.SuspendLayout();
            this.bpPanelControl57.SuspendLayout();
            this.bpPanelControl58.SuspendLayout();
            this.oneGridItem42.SuspendLayout();
            this.bpPanelControl59.SuspendLayout();
            this.bpPanelControl60.SuspendLayout();
            this.oneGridItem43.SuspendLayout();
            this.bpPanelControl61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_2)).BeginInit();
            this.bpPanelControl62.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_1)).BeginInit();
            this.oneGridItem44.SuspendLayout();
            this.bpPanelControl63.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_4)).BeginInit();
            this.bpPanelControl64.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_3)).BeginInit();
            this.oneGridItem45.SuspendLayout();
            this.bpPanelControl65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_6)).BeginInit();
            this.bpPanelControl66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_5)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.panelExt21.SuspendLayout();
            this.panelExt4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur할인율)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt30.SuspendLayout();
            this.panelExt45.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel4);
            // 
            // lbl상태
            // 
            this.lbl상태.BackColor = System.Drawing.Color.Transparent;
            this.lbl상태.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl상태.Location = new System.Drawing.Point(0, 2);
            this.lbl상태.Name = "lbl상태";
            this.lbl상태.Resizeble = true;
            this.lbl상태.Size = new System.Drawing.Size(80, 16);
            this.lbl상태.TabIndex = 1;
            this.lbl상태.Tag = "";
            this.lbl상태.Text = "상태";
            this.lbl상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx영업그룹조회
            // 
            this.ctx영업그룹조회.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx영업그룹조회.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx영업그룹조회.ButtonImage")));
            this.ctx영업그룹조회.ChildMode = "";
            this.ctx영업그룹조회.CodeName = "";
            this.ctx영업그룹조회.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx영업그룹조회.CodeValue = "";
            this.ctx영업그룹조회.ComboCheck = true;
            this.ctx영업그룹조회.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.ctx영업그룹조회.IsCodeValueToUpper = true;
            this.ctx영업그룹조회.ItemBackColor = System.Drawing.Color.White;
            this.ctx영업그룹조회.LabelVisibled = true;
            this.ctx영업그룹조회.Location = new System.Drawing.Point(271, 1);
            this.ctx영업그룹조회.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx영업그룹조회.Name = "ctx영업그룹조회";
            this.ctx영업그룹조회.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx영업그룹조회.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx영업그룹조회.SearchCode = true;
            this.ctx영업그룹조회.SelectCount = 0;
            this.ctx영업그룹조회.SetDefaultValue = false;
            this.ctx영업그룹조회.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx영업그룹조회.Size = new System.Drawing.Size(267, 21);
            this.ctx영업그룹조회.TabIndex = 1;
            this.ctx영업그룹조회.TabStop = false;
            this.ctx영업그룹조회.Tag = "CD_BIZAREA;NM_BIZAREA";
            this.ctx영업그룹조회.Text = "영업그룹";
            // 
            // ctx견적번호
            // 
            this.ctx견적번호.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx견적번호.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx견적번호.ButtonImage")));
            this.ctx견적번호.ChildMode = "";
            this.ctx견적번호.CodeName = "";
            this.ctx견적번호.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx견적번호.CodeValue = "";
            this.ctx견적번호.ComboCheck = true;
            this.ctx견적번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx견적번호.IsCodeValueToUpper = true;
            this.ctx견적번호.ItemBackColor = System.Drawing.Color.White;
            this.ctx견적번호.LabelVisibled = true;
            this.ctx견적번호.Location = new System.Drawing.Point(2, 1);
            this.ctx견적번호.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx견적번호.Name = "ctx견적번호";
            this.ctx견적번호.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx견적번호.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx견적번호.SearchCode = true;
            this.ctx견적번호.SelectCount = 0;
            this.ctx견적번호.SetDefaultValue = true;
            this.ctx견적번호.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx견적번호.Size = new System.Drawing.Size(267, 21);
            this.ctx견적번호.TabIndex = 3;
            this.ctx견적번호.TabStop = false;
            this.ctx견적번호.Tag = "";
            this.ctx견적번호.Text = "견적번호";
            this.ctx견적번호.UserCodeName = "NO_EST_NM";
            this.ctx견적번호.UserCodeValue = "NO_EST";
            this.ctx견적번호.UserHelpID = "H_SA_ESTMT_SUB";
            // 
            // cbo상태
            // 
            this.cbo상태.AutoDropDown = true;
            this.cbo상태.BackColor = System.Drawing.Color.White;
            this.cbo상태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo상태.ItemHeight = 12;
            this.cbo상태.Location = new System.Drawing.Point(81, 1);
            this.cbo상태.Name = "cbo상태";
            this.cbo상태.ShowCheckBox = false;
            this.cbo상태.Size = new System.Drawing.Size(186, 20);
            this.cbo상태.TabIndex = 6;
            this.cbo상태.Tag = "FG_TRANS";
            this.cbo상태.UseKeyEnter = true;
            this.cbo상태.UseKeyF3 = true;
            // 
            // ctx거래처조회
            // 
            this.ctx거래처조회.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx거래처조회.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx거래처조회.ButtonImage")));
            this.ctx거래처조회.ChildMode = "";
            this.ctx거래처조회.CodeName = "";
            this.ctx거래처조회.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx거래처조회.CodeValue = "";
            this.ctx거래처조회.ComboCheck = true;
            this.ctx거래처조회.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx거래처조회.IsCodeValueToUpper = true;
            this.ctx거래처조회.ItemBackColor = System.Drawing.Color.White;
            this.ctx거래처조회.LabelVisibled = true;
            this.ctx거래처조회.Location = new System.Drawing.Point(2, 1);
            this.ctx거래처조회.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx거래처조회.Name = "ctx거래처조회";
            this.ctx거래처조회.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx거래처조회.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx거래처조회.SearchCode = true;
            this.ctx거래처조회.SelectCount = 0;
            this.ctx거래처조회.SetDefaultValue = true;
            this.ctx거래처조회.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx거래처조회.Size = new System.Drawing.Size(267, 21);
            this.ctx거래처조회.TabIndex = 0;
            this.ctx거래처조회.TabStop = false;
            this.ctx거래처조회.Tag = "";
            this.ctx거래처조회.Text = "거래처";
            // 
            // lbl견적일자조회
            // 
            this.lbl견적일자조회.BackColor = System.Drawing.Color.Transparent;
            this.lbl견적일자조회.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl견적일자조회.Location = new System.Drawing.Point(0, 2);
            this.lbl견적일자조회.Name = "lbl견적일자조회";
            this.lbl견적일자조회.Resizeble = true;
            this.lbl견적일자조회.Size = new System.Drawing.Size(80, 16);
            this.lbl견적일자조회.TabIndex = 12;
            this.lbl견적일자조회.Tag = "CD_BIZAREA";
            this.lbl견적일자조회.Text = "견적일자";
            this.lbl견적일자조회.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // img
            // 
            this.img.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img.ImageStream")));
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            this.img.Images.SetKeyName(0, "tab_icon_0004.gif");
            this.img.Images.SetKeyName(1, "tab_icon_02.gif");
            this.img.Images.SetKeyName(2, "tab_icon_hr001.gif");
            this.img.Images.SetKeyName(3, "tab_icon_fi_ze02.gif");
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tpg공통);
            this.tab.Controls.Add(this.tpg해외);
            this.tab.Controls.Add(this.tpg비고);
            this.tab.Controls.Add(this.tpg기타);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.ImageList = this.img;
            this.tab.ItemSize = new System.Drawing.Size(100, 20);
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(470, 404);
            this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab.TabIndex = 4;
            // 
            // tpg공통
            // 
            this.tpg공통.BackColor = System.Drawing.Color.White;
            this.tpg공통.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg공통.Controls.Add(this.lay공통);
            this.tpg공통.ImageIndex = 0;
            this.tpg공통.Location = new System.Drawing.Point(4, 24);
            this.tpg공통.Name = "tpg공통";
            this.tpg공통.Padding = new System.Windows.Forms.Padding(4);
            this.tpg공통.Size = new System.Drawing.Size(462, 376);
            this.tpg공통.TabIndex = 0;
            this.tpg공통.Tag = "BASE";
            this.tpg공통.Text = "공통";
            this.tpg공통.UseVisualStyleBackColor = true;
            // 
            // lay공통
            // 
            this.lay공통.ColumnCount = 1;
            this.lay공통.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay공통.Controls.Add(this.pnl공통5, 0, 4);
            this.lay공통.Controls.Add(this.pnl공통4, 0, 3);
            this.lay공통.Controls.Add(this.pnl공통3, 0, 2);
            this.lay공통.Controls.Add(this.pnl공통2, 0, 1);
            this.lay공통.Controls.Add(this.pnl공통1, 0, 0);
            this.lay공통.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay공통.Location = new System.Drawing.Point(4, 4);
            this.lay공통.Name = "lay공통";
            this.lay공통.RowCount = 5;
            this.lay공통.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay공통.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay공통.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay공통.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay공통.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay공통.Size = new System.Drawing.Size(450, 364);
            this.lay공통.TabIndex = 0;
            // 
            // pnl공통5
            // 
            this.pnl공통5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl공통5.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem15});
            this.pnl공통5.Location = new System.Drawing.Point(3, 367);
            this.pnl공통5.Name = "pnl공통5";
            this.pnl공통5.Size = new System.Drawing.Size(444, 39);
            this.pnl공통5.TabIndex = 0;
            this.pnl공통5.Visible = false;
            // 
            // oneGridItem15
            // 
            this.oneGridItem15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem15.Controls.Add(this.bpPanelControl25);
            this.oneGridItem15.Controls.Add(this.bpPanelControl24);
            this.oneGridItem15.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem15.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem15.Name = "oneGridItem15";
            this.oneGridItem15.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem15.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem15.TabIndex = 0;
            // 
            // bpPanelControl25
            // 
            this.bpPanelControl25.Controls.Add(this.cbo할인구분);
            this.bpPanelControl25.Controls.Add(this.lbl할인구분);
            this.bpPanelControl25.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl25.Name = "bpPanelControl25";
            this.bpPanelControl25.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl25.TabIndex = 1;
            this.bpPanelControl25.Text = "bpPanelControl25";
            // 
            // cbo할인구분
            // 
            this.cbo할인구분.AutoDropDown = true;
            this.cbo할인구분.BackColor = System.Drawing.Color.White;
            this.cbo할인구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo할인구분.ItemHeight = 12;
            this.cbo할인구분.Location = new System.Drawing.Point(81, 1);
            this.cbo할인구분.Name = "cbo할인구분";
            this.cbo할인구분.ShowCheckBox = false;
            this.cbo할인구분.Size = new System.Drawing.Size(133, 20);
            this.cbo할인구분.TabIndex = 157;
            this.cbo할인구분.Tag = "";
            this.cbo할인구분.UseKeyEnter = true;
            this.cbo할인구분.UseKeyF3 = true;
            // 
            // lbl할인구분
            // 
            this.lbl할인구분.Location = new System.Drawing.Point(0, 2);
            this.lbl할인구분.Name = "lbl할인구분";
            this.lbl할인구분.Resizeble = true;
            this.lbl할인구분.Size = new System.Drawing.Size(80, 16);
            this.lbl할인구분.TabIndex = 154;
            this.lbl할인구분.Tag = "";
            this.lbl할인구분.Text = "할인구분";
            this.lbl할인구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl24
            // 
            this.bpPanelControl24.Controls.Add(this.lbl기준환종);
            this.bpPanelControl24.Controls.Add(this.cbo기준환종);
            this.bpPanelControl24.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl24.Name = "bpPanelControl24";
            this.bpPanelControl24.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl24.TabIndex = 0;
            this.bpPanelControl24.Text = "bpPanelControl24";
            // 
            // lbl기준환종
            // 
            this.lbl기준환종.Location = new System.Drawing.Point(0, 2);
            this.lbl기준환종.Name = "lbl기준환종";
            this.lbl기준환종.Resizeble = true;
            this.lbl기준환종.Size = new System.Drawing.Size(80, 16);
            this.lbl기준환종.TabIndex = 175;
            this.lbl기준환종.Tag = "";
            this.lbl기준환종.Text = "기준환종";
            this.lbl기준환종.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo기준환종
            // 
            this.cbo기준환종.AutoDropDown = true;
            this.cbo기준환종.BackColor = System.Drawing.Color.White;
            this.cbo기준환종.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo기준환종.ItemHeight = 12;
            this.cbo기준환종.Location = new System.Drawing.Point(81, 1);
            this.cbo기준환종.Name = "cbo기준환종";
            this.cbo기준환종.ShowCheckBox = false;
            this.cbo기준환종.Size = new System.Drawing.Size(133, 20);
            this.cbo기준환종.TabIndex = 0;
            this.cbo기준환종.Tag = "";
            this.cbo기준환종.UseKeyEnter = true;
            this.cbo기준환종.UseKeyF3 = true;
            // 
            // pnl공통4
            // 
            this.pnl공통4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl공통4.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem14});
            this.pnl공통4.Location = new System.Drawing.Point(3, 322);
            this.pnl공통4.Name = "pnl공통4";
            this.pnl공통4.Size = new System.Drawing.Size(444, 39);
            this.pnl공통4.TabIndex = 0;
            // 
            // oneGridItem14
            // 
            this.oneGridItem14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem14.Controls.Add(this.bpPanelControl23);
            this.oneGridItem14.Controls.Add(this.bpPanelControl20);
            this.oneGridItem14.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem14.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem14.Name = "oneGridItem14";
            this.oneGridItem14.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem14.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem14.TabIndex = 0;
            // 
            // bpPanelControl23
            // 
            this.bpPanelControl23.Controls.Add(this.dtp확정일자);
            this.bpPanelControl23.Controls.Add(this.lbl확정일자);
            this.bpPanelControl23.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl23.Name = "bpPanelControl23";
            this.bpPanelControl23.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl23.TabIndex = 1;
            this.bpPanelControl23.Text = "bpPanelControl23";
            // 
            // dtp확정일자
            // 
            this.dtp확정일자.BackColor = System.Drawing.Color.White;
            this.dtp확정일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp확정일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp확정일자.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp확정일자.Enabled = false;
            this.dtp확정일자.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp확정일자.Location = new System.Drawing.Point(81, 1);
            this.dtp확정일자.Mask = "####/##/##";
            this.dtp확정일자.MaskBackColor = System.Drawing.Color.White;
            this.dtp확정일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp확정일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp확정일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp확정일자.Modified = false;
            this.dtp확정일자.Name = "dtp확정일자";
            this.dtp확정일자.NullCheck = false;
            this.dtp확정일자.PaddingCharacter = '_';
            this.dtp확정일자.PassivePromptCharacter = '_';
            this.dtp확정일자.PromptCharacter = '_';
            this.dtp확정일자.SelectedDayColor = System.Drawing.Color.White;
            this.dtp확정일자.ShowToDay = true;
            this.dtp확정일자.ShowTodayCircle = true;
            this.dtp확정일자.ShowUpDown = false;
            this.dtp확정일자.Size = new System.Drawing.Size(93, 21);
            this.dtp확정일자.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp확정일자.TabIndex = 1;
            this.dtp확정일자.Tag = "DT_CONT";
            this.dtp확정일자.TitleBackColor = System.Drawing.Color.White;
            this.dtp확정일자.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp확정일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp확정일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp확정일자.UseKeyF3 = false;
            this.dtp확정일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl확정일자
            // 
            this.lbl확정일자.Location = new System.Drawing.Point(0, 2);
            this.lbl확정일자.Name = "lbl확정일자";
            this.lbl확정일자.Resizeble = true;
            this.lbl확정일자.Size = new System.Drawing.Size(80, 16);
            this.lbl확정일자.TabIndex = 154;
            this.lbl확정일자.Tag = "YN_USE";
            this.lbl확정일자.Text = "확정일자";
            this.lbl확정일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl20
            // 
            this.bpPanelControl20.Controls.Add(this.cbo확정여부);
            this.bpPanelControl20.Controls.Add(this.lbl확정여부);
            this.bpPanelControl20.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl20.Name = "bpPanelControl20";
            this.bpPanelControl20.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl20.TabIndex = 0;
            this.bpPanelControl20.Text = "bpPanelControl20";
            // 
            // cbo확정여부
            // 
            this.cbo확정여부.AutoDropDown = true;
            this.cbo확정여부.BackColor = System.Drawing.Color.White;
            this.cbo확정여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo확정여부.Enabled = false;
            this.cbo확정여부.ItemHeight = 12;
            this.cbo확정여부.Location = new System.Drawing.Point(81, 1);
            this.cbo확정여부.Name = "cbo확정여부";
            this.cbo확정여부.ShowCheckBox = false;
            this.cbo확정여부.Size = new System.Drawing.Size(133, 20);
            this.cbo확정여부.TabIndex = 0;
            this.cbo확정여부.Tag = "STA_EST";
            this.cbo확정여부.UseKeyEnter = true;
            this.cbo확정여부.UseKeyF3 = true;
            // 
            // lbl확정여부
            // 
            this.lbl확정여부.Location = new System.Drawing.Point(0, 2);
            this.lbl확정여부.Name = "lbl확정여부";
            this.lbl확정여부.Resizeble = true;
            this.lbl확정여부.Size = new System.Drawing.Size(80, 16);
            this.lbl확정여부.TabIndex = 175;
            this.lbl확정여부.Tag = "STND_ITEM";
            this.lbl확정여부.Text = "확정여부";
            this.lbl확정여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl공통3
            // 
            this.pnl공통3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl공통3.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem12,
            this.oneGridItem13});
            this.pnl공통3.Location = new System.Drawing.Point(3, 254);
            this.pnl공통3.Name = "pnl공통3";
            this.pnl공통3.Size = new System.Drawing.Size(444, 62);
            this.pnl공통3.TabIndex = 3;
            // 
            // oneGridItem12
            // 
            this.oneGridItem12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem12.Controls.Add(this.bpPanelControl19);
            this.oneGridItem12.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem12.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem12.Name = "oneGridItem12";
            this.oneGridItem12.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem12.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem12.TabIndex = 0;
            // 
            // bpPanelControl19
            // 
            this.bpPanelControl19.Controls.Add(this.lbl결제조건);
            this.bpPanelControl19.Controls.Add(this.cbo결제방법);
            this.bpPanelControl19.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl19.Name = "bpPanelControl19";
            this.bpPanelControl19.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl19.TabIndex = 0;
            this.bpPanelControl19.Text = "bpPanelControl19";
            // 
            // lbl결제조건
            // 
            this.lbl결제조건.Location = new System.Drawing.Point(0, 2);
            this.lbl결제조건.Name = "lbl결제조건";
            this.lbl결제조건.Resizeble = true;
            this.lbl결제조건.Size = new System.Drawing.Size(80, 16);
            this.lbl결제조건.TabIndex = 1;
            this.lbl결제조건.Tag = "CD_ITEM";
            this.lbl결제조건.Text = "결제방법";
            this.lbl결제조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo결제방법
            // 
            this.cbo결제방법.AutoDropDown = true;
            this.cbo결제방법.BackColor = System.Drawing.Color.White;
            this.cbo결제방법.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo결제방법.ItemHeight = 12;
            this.cbo결제방법.Location = new System.Drawing.Point(81, 1);
            this.cbo결제방법.Name = "cbo결제방법";
            this.cbo결제방법.ShowCheckBox = false;
            this.cbo결제방법.Size = new System.Drawing.Size(133, 20);
            this.cbo결제방법.TabIndex = 0;
            this.cbo결제방법.Tag = "FG_BILL";
            this.cbo결제방법.UseKeyEnter = true;
            this.cbo결제방법.UseKeyF3 = true;
            // 
            // oneGridItem13
            // 
            this.oneGridItem13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem13.Controls.Add(this.bpPanelControl22);
            this.oneGridItem13.Controls.Add(this.bpPanelControl21);
            this.oneGridItem13.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem13.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem13.Name = "oneGridItem13";
            this.oneGridItem13.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem13.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem13.TabIndex = 1;
            // 
            // bpPanelControl22
            // 
            this.bpPanelControl22.Controls.Add(this.dtp납기일자);
            this.bpPanelControl22.Controls.Add(this.lbl납기일자);
            this.bpPanelControl22.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl22.Name = "bpPanelControl22";
            this.bpPanelControl22.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl22.TabIndex = 1;
            this.bpPanelControl22.Text = "bpPanelControl22";
            // 
            // dtp납기일자
            // 
            this.dtp납기일자.BackColor = System.Drawing.Color.White;
            this.dtp납기일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp납기일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp납기일자.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp납기일자.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp납기일자.Location = new System.Drawing.Point(81, 1);
            this.dtp납기일자.Mask = "####/##/##";
            this.dtp납기일자.MaskBackColor = System.Drawing.Color.White;
            this.dtp납기일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp납기일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp납기일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp납기일자.Modified = false;
            this.dtp납기일자.Name = "dtp납기일자";
            this.dtp납기일자.NullCheck = false;
            this.dtp납기일자.PaddingCharacter = '_';
            this.dtp납기일자.PassivePromptCharacter = '_';
            this.dtp납기일자.PromptCharacter = '_';
            this.dtp납기일자.SelectedDayColor = System.Drawing.Color.White;
            this.dtp납기일자.ShowToDay = true;
            this.dtp납기일자.ShowTodayCircle = true;
            this.dtp납기일자.ShowUpDown = false;
            this.dtp납기일자.Size = new System.Drawing.Size(93, 21);
            this.dtp납기일자.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp납기일자.TabIndex = 2;
            this.dtp납기일자.Tag = "DT_DUEDATE";
            this.dtp납기일자.TitleBackColor = System.Drawing.Color.White;
            this.dtp납기일자.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp납기일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp납기일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp납기일자.UseKeyF3 = false;
            this.dtp납기일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl납기일자
            // 
            this.lbl납기일자.Location = new System.Drawing.Point(0, 2);
            this.lbl납기일자.Name = "lbl납기일자";
            this.lbl납기일자.Resizeble = true;
            this.lbl납기일자.Size = new System.Drawing.Size(80, 16);
            this.lbl납기일자.TabIndex = 153;
            this.lbl납기일자.Tag = "YN_USE";
            this.lbl납기일자.Text = "납기일자";
            this.lbl납기일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl21
            // 
            this.bpPanelControl21.Controls.Add(this.lbl유효일자국내);
            this.bpPanelControl21.Controls.Add(this.dtp유효일자국내);
            this.bpPanelControl21.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl21.Name = "bpPanelControl21";
            this.bpPanelControl21.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl21.TabIndex = 0;
            this.bpPanelControl21.Text = "bpPanelControl21";
            // 
            // lbl유효일자국내
            // 
            this.lbl유효일자국내.Location = new System.Drawing.Point(0, 2);
            this.lbl유효일자국내.Name = "lbl유효일자국내";
            this.lbl유효일자국내.Resizeble = true;
            this.lbl유효일자국내.Size = new System.Drawing.Size(80, 16);
            this.lbl유효일자국내.TabIndex = 67;
            this.lbl유효일자국내.Tag = "NM_ITEM";
            this.lbl유효일자국내.Text = "유효일자";
            this.lbl유효일자국내.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp유효일자국내
            // 
            this.dtp유효일자국내.BackColor = System.Drawing.Color.White;
            this.dtp유효일자국내.CalendarBackColor = System.Drawing.Color.White;
            this.dtp유효일자국내.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp유효일자국내.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp유효일자국내.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp유효일자국내.Location = new System.Drawing.Point(81, 1);
            this.dtp유효일자국내.Mask = "####/##/##";
            this.dtp유효일자국내.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp유효일자국내.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp유효일자국내.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp유효일자국내.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp유효일자국내.Modified = false;
            this.dtp유효일자국내.Name = "dtp유효일자국내";
            this.dtp유효일자국내.NullCheck = false;
            this.dtp유효일자국내.PaddingCharacter = '_';
            this.dtp유효일자국내.PassivePromptCharacter = '_';
            this.dtp유효일자국내.PromptCharacter = '_';
            this.dtp유효일자국내.SelectedDayColor = System.Drawing.Color.White;
            this.dtp유효일자국내.ShowToDay = true;
            this.dtp유효일자국내.ShowTodayCircle = true;
            this.dtp유효일자국내.ShowUpDown = false;
            this.dtp유효일자국내.Size = new System.Drawing.Size(93, 21);
            this.dtp유효일자국내.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp유효일자국내.TabIndex = 1;
            this.dtp유효일자국내.Tag = "DT_VALID";
            this.dtp유효일자국내.TitleBackColor = System.Drawing.Color.White;
            this.dtp유효일자국내.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp유효일자국내.ToDayColor = System.Drawing.Color.Red;
            this.dtp유효일자국내.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp유효일자국내.UseKeyF3 = false;
            this.dtp유효일자국내.Value = new System.DateTime(((long)(0)));
            // 
            // pnl공통2
            // 
            this.pnl공통2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl공통2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem10,
            this.oneGridItem11});
            this.pnl공통2.Location = new System.Drawing.Point(3, 186);
            this.pnl공통2.Name = "pnl공통2";
            this.pnl공통2.Size = new System.Drawing.Size(444, 62);
            this.pnl공통2.TabIndex = 7;
            // 
            // oneGridItem10
            // 
            this.oneGridItem10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem10.Controls.Add(this.bpPanelControl16);
            this.oneGridItem10.Controls.Add(this.bpPanelControl15);
            this.oneGridItem10.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem10.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem10.Name = "oneGridItem10";
            this.oneGridItem10.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem10.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem10.TabIndex = 0;
            // 
            // bpPanelControl16
            // 
            this.bpPanelControl16.Controls.Add(this.cur환율);
            this.bpPanelControl16.Controls.Add(this.labelExt1);
            this.bpPanelControl16.Controls.Add(this.cbo환종);
            this.bpPanelControl16.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl16.Name = "bpPanelControl16";
            this.bpPanelControl16.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl16.TabIndex = 1;
            this.bpPanelControl16.Text = "bpPanelControl16";
            // 
            // cur환율
            // 
            this.cur환율.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cur환율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur환율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur환율.CurrencyDecimalDigits = 4;
            this.cur환율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur환율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur환율.Location = new System.Drawing.Point(139, 0);
            this.cur환율.Mask = null;
            this.cur환율.Name = "cur환율";
            this.cur환율.NullString = "0";
            this.cur환율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur환율.Size = new System.Drawing.Size(74, 21);
            this.cur환율.TabIndex = 6;
            this.cur환율.Tag = "RT_EXCH";
            this.cur환율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur환율.UseKeyEnter = true;
            this.cur환율.UseKeyF3 = true;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(0, 2);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(80, 16);
            this.labelExt1.TabIndex = 155;
            this.labelExt1.Tag = "CD_ITEM";
            this.labelExt1.Text = "환종";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo환종
            // 
            this.cbo환종.AutoDropDown = true;
            this.cbo환종.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo환종.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo환종.ItemHeight = 12;
            this.cbo환종.Location = new System.Drawing.Point(81, 1);
            this.cbo환종.Name = "cbo환종";
            this.cbo환종.ShowCheckBox = false;
            this.cbo환종.Size = new System.Drawing.Size(56, 20);
            this.cbo환종.TabIndex = 1;
            this.cbo환종.Tag = "CD_EXCH";
            this.cbo환종.UseKeyEnter = true;
            this.cbo환종.UseKeyF3 = true;
            // 
            // bpPanelControl15
            // 
            this.bpPanelControl15.Controls.Add(this.lbl수주형태);
            this.bpPanelControl15.Controls.Add(this.ctx수주형태);
            this.bpPanelControl15.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl15.Name = "bpPanelControl15";
            this.bpPanelControl15.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl15.TabIndex = 0;
            this.bpPanelControl15.Text = "bpPanelControl15";
            // 
            // lbl수주형태
            // 
            this.lbl수주형태.Location = new System.Drawing.Point(0, 2);
            this.lbl수주형태.Name = "lbl수주형태";
            this.lbl수주형태.Resizeble = true;
            this.lbl수주형태.Size = new System.Drawing.Size(80, 16);
            this.lbl수주형태.TabIndex = 154;
            this.lbl수주형태.Tag = "YN_USE";
            this.lbl수주형태.Text = "수주형태";
            this.lbl수주형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx수주형태
            // 
            this.ctx수주형태.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx수주형태.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx수주형태.ButtonImage")));
            this.ctx수주형태.ChildMode = "";
            this.ctx수주형태.CodeName = "";
            this.ctx수주형태.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx수주형태.CodeValue = "";
            this.ctx수주형태.ComboCheck = true;
            this.ctx수주형태.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB;
            this.ctx수주형태.IsCodeValueToUpper = true;
            this.ctx수주형태.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx수주형태.Location = new System.Drawing.Point(81, 1);
            this.ctx수주형태.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx수주형태.Name = "ctx수주형태";
            this.ctx수주형태.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx수주형태.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx수주형태.SearchCode = true;
            this.ctx수주형태.SelectCount = 0;
            this.ctx수주형태.SetDefaultValue = true;
            this.ctx수주형태.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx수주형태.Size = new System.Drawing.Size(133, 21);
            this.ctx수주형태.TabIndex = 0;
            this.ctx수주형태.TabStop = false;
            this.ctx수주형태.Tag = "TP_SO,NM_SO";
            this.ctx수주형태.Text = "bpCodeTextBox2";
            // 
            // oneGridItem11
            // 
            this.oneGridItem11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem11.Controls.Add(this.bpPanelControl18);
            this.oneGridItem11.Controls.Add(this.bpPanelControl17);
            this.oneGridItem11.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem11.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem11.Name = "oneGridItem11";
            this.oneGridItem11.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem11.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem11.TabIndex = 1;
            // 
            // bpPanelControl18
            // 
            this.bpPanelControl18.Controls.Add(this.cbo부가세포함);
            this.bpPanelControl18.Controls.Add(this.lbl부가세포함);
            this.bpPanelControl18.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl18.Name = "bpPanelControl18";
            this.bpPanelControl18.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl18.TabIndex = 1;
            this.bpPanelControl18.Text = "bpPanelControl18";
            // 
            // cbo부가세포함
            // 
            this.cbo부가세포함.AutoDropDown = true;
            this.cbo부가세포함.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo부가세포함.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo부가세포함.Enabled = false;
            this.cbo부가세포함.ItemHeight = 12;
            this.cbo부가세포함.Location = new System.Drawing.Point(81, 1);
            this.cbo부가세포함.Name = "cbo부가세포함";
            this.cbo부가세포함.ShowCheckBox = false;
            this.cbo부가세포함.Size = new System.Drawing.Size(133, 20);
            this.cbo부가세포함.TabIndex = 3;
            this.cbo부가세포함.Tag = "FG_VAT";
            this.cbo부가세포함.UseKeyEnter = true;
            this.cbo부가세포함.UseKeyF3 = true;
            // 
            // lbl부가세포함
            // 
            this.lbl부가세포함.Location = new System.Drawing.Point(0, 2);
            this.lbl부가세포함.Name = "lbl부가세포함";
            this.lbl부가세포함.Resizeble = true;
            this.lbl부가세포함.Size = new System.Drawing.Size(80, 16);
            this.lbl부가세포함.TabIndex = 154;
            this.lbl부가세포함.Tag = "YN_USE";
            this.lbl부가세포함.Text = "부가세포함";
            this.lbl부가세포함.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl17
            // 
            this.bpPanelControl17.Controls.Add(this.lbl과세구분);
            this.bpPanelControl17.Controls.Add(this.cur부가세율);
            this.bpPanelControl17.Controls.Add(this.cbo과세구분);
            this.bpPanelControl17.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl17.Name = "bpPanelControl17";
            this.bpPanelControl17.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl17.TabIndex = 0;
            this.bpPanelControl17.Text = "bpPanelControl17";
            // 
            // lbl과세구분
            // 
            this.lbl과세구분.Location = new System.Drawing.Point(0, 2);
            this.lbl과세구분.Name = "lbl과세구분";
            this.lbl과세구분.Resizeble = true;
            this.lbl과세구분.Size = new System.Drawing.Size(80, 16);
            this.lbl과세구분.TabIndex = 68;
            this.lbl과세구분.Tag = "EN_ITEM";
            this.lbl과세구분.Text = "과세구분";
            this.lbl과세구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur부가세율
            // 
            this.cur부가세율.BackColor = System.Drawing.Color.White;
            this.cur부가세율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur부가세율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur부가세율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur부가세율.Enabled = false;
            this.cur부가세율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur부가세율.Location = new System.Drawing.Point(172, 1);
            this.cur부가세율.Mask = "";
            this.cur부가세율.Name = "cur부가세율";
            this.cur부가세율.NullString = "0";
            this.cur부가세율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur부가세율.Size = new System.Drawing.Size(42, 21);
            this.cur부가세율.TabIndex = 167;
            this.cur부가세율.Tag = "RT_VAT";
            this.cur부가세율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur부가세율.UseKeyEnter = true;
            this.cur부가세율.UseKeyF3 = true;
            // 
            // cbo과세구분
            // 
            this.cbo과세구분.AutoDropDown = true;
            this.cbo과세구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo과세구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo과세구분.ItemHeight = 12;
            this.cbo과세구분.Location = new System.Drawing.Point(81, 1);
            this.cbo과세구분.Name = "cbo과세구분";
            this.cbo과세구분.ShowCheckBox = false;
            this.cbo과세구분.Size = new System.Drawing.Size(89, 20);
            this.cbo과세구분.TabIndex = 2;
            this.cbo과세구분.Tag = "TP_VAT";
            this.cbo과세구분.UseKeyEnter = true;
            this.cbo과세구분.UseKeyF3 = true;
            // 
            // pnl공통1
            // 
            this.pnl공통1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl공통1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem5,
            this.oneGridItem6,
            this.oneGridItem7,
            this.oneGridItem8,
            this.oneGridItem9});
            this.pnl공통1.Location = new System.Drawing.Point(3, 3);
            this.pnl공통1.Name = "pnl공통1";
            this.pnl공통1.Size = new System.Drawing.Size(444, 177);
            this.pnl공통1.TabIndex = 7;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl4);
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl차수);
            this.bpPanelControl4.Controls.Add(this.cur차수);
            this.bpPanelControl4.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl4.TabIndex = 1;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // lbl차수
            // 
            this.lbl차수.Location = new System.Drawing.Point(0, 2);
            this.lbl차수.Name = "lbl차수";
            this.lbl차수.Resizeble = true;
            this.lbl차수.Size = new System.Drawing.Size(80, 16);
            this.lbl차수.TabIndex = 153;
            this.lbl차수.Tag = "YN_USE";
            this.lbl차수.Text = "차수";
            this.lbl차수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur차수
            // 
            this.cur차수.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cur차수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur차수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur차수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur차수.Enabled = false;
            this.cur차수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur차수.Location = new System.Drawing.Point(81, 1);
            this.cur차수.Mask = null;
            this.cur차수.Name = "cur차수";
            this.cur차수.NullString = "0";
            this.cur차수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur차수.Size = new System.Drawing.Size(133, 21);
            this.cur차수.TabIndex = 1;
            this.cur차수.Tag = "NO_HST";
            this.cur차수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur차수.UseKeyEnter = true;
            this.cur차수.UseKeyF3 = true;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.lbl견적번호);
            this.bpPanelControl3.Controls.Add(this.txt견적번호);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl3.TabIndex = 0;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // lbl견적번호
            // 
            this.lbl견적번호.Location = new System.Drawing.Point(0, 2);
            this.lbl견적번호.Name = "lbl견적번호";
            this.lbl견적번호.Resizeble = true;
            this.lbl견적번호.Size = new System.Drawing.Size(80, 16);
            this.lbl견적번호.TabIndex = 1;
            this.lbl견적번호.Tag = "CD_ITEM";
            this.lbl견적번호.Text = "견적번호";
            this.lbl견적번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt견적번호
            // 
            this.txt견적번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt견적번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt견적번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt견적번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt견적번호.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt견적번호.Location = new System.Drawing.Point(81, 1);
            this.txt견적번호.MaxLength = 20;
            this.txt견적번호.Name = "txt견적번호";
            this.txt견적번호.SelectedAllEnabled = false;
            this.txt견적번호.Size = new System.Drawing.Size(133, 21);
            this.txt견적번호.TabIndex = 0;
            this.txt견적번호.Tag = "NO_EST";
            this.txt견적번호.UseKeyEnter = true;
            this.txt견적번호.UseKeyF3 = true;
            // 
            // oneGridItem4
            // 
            this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem4.Controls.Add(this.bpPanelControl5);
            this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem4.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem4.Name = "oneGridItem4";
            this.oneGridItem4.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem4.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.lbl견적명);
            this.bpPanelControl5.Controls.Add(this.txt견적명);
            this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(429, 23);
            this.bpPanelControl5.TabIndex = 0;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // lbl견적명
            // 
            this.lbl견적명.Location = new System.Drawing.Point(0, 2);
            this.lbl견적명.Name = "lbl견적명";
            this.lbl견적명.Resizeble = true;
            this.lbl견적명.Size = new System.Drawing.Size(80, 16);
            this.lbl견적명.TabIndex = 67;
            this.lbl견적명.Tag = "NM_ITEM";
            this.lbl견적명.Text = "견적명";
            this.lbl견적명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt견적명
            // 
            this.txt견적명.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt견적명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt견적명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt견적명.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt견적명.Location = new System.Drawing.Point(81, 1);
            this.txt견적명.MaxLength = 50;
            this.txt견적명.Name = "txt견적명";
            this.txt견적명.SelectedAllEnabled = false;
            this.txt견적명.Size = new System.Drawing.Size(348, 21);
            this.txt견적명.TabIndex = 2;
            this.txt견적명.Tag = "NO_EST_NM";
            this.txt견적명.UseKeyEnter = true;
            this.txt견적명.UseKeyF3 = true;
            // 
            // oneGridItem5
            // 
            this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem5.Controls.Add(this.bpPanelControl7);
            this.oneGridItem5.Controls.Add(this.bpPanelControl6);
            this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem5.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem5.Name = "oneGridItem5";
            this.oneGridItem5.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem5.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.lbl거래처);
            this.bpPanelControl7.Controls.Add(this.ctx거래처);
            this.bpPanelControl7.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl7.TabIndex = 1;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(0, 2);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(80, 16);
            this.lbl거래처.TabIndex = 71;
            this.lbl거래처.Tag = "NM_ITEM";
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx거래처
            // 
            this.ctx거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx거래처.ButtonImage")));
            this.ctx거래처.ChildMode = "";
            this.ctx거래처.CodeName = "";
            this.ctx거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx거래처.CodeValue = "";
            this.ctx거래처.ComboCheck = true;
            this.ctx거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx거래처.IsCodeValueToUpper = true;
            this.ctx거래처.ItemBackColor = System.Drawing.Color.White;
            this.ctx거래처.Location = new System.Drawing.Point(81, 1);
            this.ctx거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx거래처.Name = "ctx거래처";
            this.ctx거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx거래처.SearchCode = true;
            this.ctx거래처.SelectCount = 0;
            this.ctx거래처.SetDefaultValue = true;
            this.ctx거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx거래처.Size = new System.Drawing.Size(133, 21);
            this.ctx거래처.TabIndex = 4;
            this.ctx거래처.TabStop = false;
            this.ctx거래처.Tag = "CD_PARTNER,LN_PARTNER";
            this.ctx거래처.Text = "bpCodeTextBox2";
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.lbl견적일자);
            this.bpPanelControl6.Controls.Add(this.dtp견적일자);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl6.TabIndex = 0;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // lbl견적일자
            // 
            this.lbl견적일자.Location = new System.Drawing.Point(0, 2);
            this.lbl견적일자.Name = "lbl견적일자";
            this.lbl견적일자.Resizeble = true;
            this.lbl견적일자.Size = new System.Drawing.Size(80, 16);
            this.lbl견적일자.TabIndex = 68;
            this.lbl견적일자.Tag = "EN_ITEM";
            this.lbl견적일자.Text = "견적일자";
            this.lbl견적일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp견적일자
            // 
            this.dtp견적일자.BackColor = System.Drawing.Color.White;
            this.dtp견적일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp견적일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp견적일자.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp견적일자.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp견적일자.Location = new System.Drawing.Point(81, 1);
            this.dtp견적일자.Mask = "####/##/##";
            this.dtp견적일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp견적일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp견적일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp견적일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp견적일자.Modified = false;
            this.dtp견적일자.Name = "dtp견적일자";
            this.dtp견적일자.NullCheck = false;
            this.dtp견적일자.PaddingCharacter = '_';
            this.dtp견적일자.PassivePromptCharacter = '_';
            this.dtp견적일자.PromptCharacter = '_';
            this.dtp견적일자.SelectedDayColor = System.Drawing.Color.White;
            this.dtp견적일자.ShowToDay = true;
            this.dtp견적일자.ShowTodayCircle = true;
            this.dtp견적일자.ShowUpDown = false;
            this.dtp견적일자.Size = new System.Drawing.Size(93, 21);
            this.dtp견적일자.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp견적일자.TabIndex = 3;
            this.dtp견적일자.Tag = "DT_EST";
            this.dtp견적일자.TitleBackColor = System.Drawing.Color.White;
            this.dtp견적일자.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp견적일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp견적일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp견적일자.UseKeyF3 = false;
            this.dtp견적일자.Value = new System.DateTime(((long)(0)));
            // 
            // oneGridItem6
            // 
            this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem6.Controls.Add(this.bpPanelControl8);
            this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem6.Location = new System.Drawing.Point(0, 69);
            this.oneGridItem6.Name = "oneGridItem6";
            this.oneGridItem6.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem6.TabIndex = 3;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.lbl임시거래처);
            this.bpPanelControl8.Controls.Add(this.textBoxExt1);
            this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(429, 23);
            this.bpPanelControl8.TabIndex = 0;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // lbl임시거래처
            // 
            this.lbl임시거래처.Location = new System.Drawing.Point(0, 2);
            this.lbl임시거래처.Name = "lbl임시거래처";
            this.lbl임시거래처.Resizeble = true;
            this.lbl임시거래처.Size = new System.Drawing.Size(80, 16);
            this.lbl임시거래처.TabIndex = 154;
            this.lbl임시거래처.Tag = "YN_USE";
            this.lbl임시거래처.Text = "임시거래처";
            this.lbl임시거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BackColor = System.Drawing.Color.White;
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt1.Location = new System.Drawing.Point(81, 1);
            this.textBoxExt1.MaxLength = 50;
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.SelectedAllEnabled = false;
            this.textBoxExt1.Size = new System.Drawing.Size(348, 21);
            this.textBoxExt1.TabIndex = 5;
            this.textBoxExt1.Tag = "NM_PARTNER_IMSI";
            this.textBoxExt1.UseKeyEnter = true;
            this.textBoxExt1.UseKeyF3 = true;
            // 
            // oneGridItem7
            // 
            this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem7.Controls.Add(this.bpPanelControl10);
            this.oneGridItem7.Controls.Add(this.bpPanelControl9);
            this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem7.Location = new System.Drawing.Point(0, 92);
            this.oneGridItem7.Name = "oneGridItem7";
            this.oneGridItem7.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem7.TabIndex = 4;
            // 
            // bpPanelControl10
            // 
            this.bpPanelControl10.Controls.Add(this.ctx프로젝트);
            this.bpPanelControl10.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl10.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl10.Name = "bpPanelControl10";
            this.bpPanelControl10.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl10.TabIndex = 1;
            this.bpPanelControl10.Text = "bpPanelControl10";
            // 
            // ctx프로젝트
            // 
            this.ctx프로젝트.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx프로젝트.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx프로젝트.ButtonImage")));
            this.ctx프로젝트.ChildMode = "";
            this.ctx프로젝트.CodeName = "";
            this.ctx프로젝트.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx프로젝트.CodeValue = "";
            this.ctx프로젝트.ComboCheck = true;
            this.ctx프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx프로젝트.IsCodeValueToUpper = true;
            this.ctx프로젝트.ItemBackColor = System.Drawing.Color.White;
            this.ctx프로젝트.Location = new System.Drawing.Point(81, 1);
            this.ctx프로젝트.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx프로젝트.Name = "ctx프로젝트";
            this.ctx프로젝트.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx프로젝트.SearchCode = true;
            this.ctx프로젝트.SelectCount = 0;
            this.ctx프로젝트.SetDefaultValue = false;
            this.ctx프로젝트.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx프로젝트.Size = new System.Drawing.Size(133, 21);
            this.ctx프로젝트.TabIndex = 7;
            this.ctx프로젝트.TabStop = false;
            this.ctx프로젝트.Tag = "NO_PROJECT,NM_PROJECT";
            this.ctx프로젝트.UserCodeName = "NM_PROJECT";
            this.ctx프로젝트.UserCodeValue = "NO_PROJECT";
            this.ctx프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 2);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Resizeble = true;
            this.lbl프로젝트.Size = new System.Drawing.Size(80, 16);
            this.lbl프로젝트.TabIndex = 155;
            this.lbl프로젝트.Tag = "";
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Controls.Add(this.lbl견적요청자);
            this.bpPanelControl9.Controls.Add(this.txt견적요청자);
            this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl9.TabIndex = 0;
            this.bpPanelControl9.Text = "bpPanelControl9";
            // 
            // lbl견적요청자
            // 
            this.lbl견적요청자.Location = new System.Drawing.Point(0, 2);
            this.lbl견적요청자.Name = "lbl견적요청자";
            this.lbl견적요청자.Resizeble = true;
            this.lbl견적요청자.Size = new System.Drawing.Size(80, 16);
            this.lbl견적요청자.TabIndex = 155;
            this.lbl견적요청자.Tag = "";
            this.lbl견적요청자.Text = "견적요청자";
            this.lbl견적요청자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt견적요청자
            // 
            this.txt견적요청자.BackColor = System.Drawing.Color.White;
            this.txt견적요청자.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt견적요청자.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt견적요청자.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt견적요청자.Location = new System.Drawing.Point(81, 1);
            this.txt견적요청자.MaxLength = 50;
            this.txt견적요청자.Name = "txt견적요청자";
            this.txt견적요청자.SelectedAllEnabled = false;
            this.txt견적요청자.Size = new System.Drawing.Size(133, 21);
            this.txt견적요청자.TabIndex = 6;
            this.txt견적요청자.Tag = "NM_EMP_PARTNER";
            this.txt견적요청자.UseKeyEnter = true;
            this.txt견적요청자.UseKeyF3 = true;
            // 
            // oneGridItem8
            // 
            this.oneGridItem8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem8.Controls.Add(this.bpPanelControl12);
            this.oneGridItem8.Controls.Add(this.bpPanelControl11);
            this.oneGridItem8.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem8.Location = new System.Drawing.Point(0, 115);
            this.oneGridItem8.Name = "oneGridItem8";
            this.oneGridItem8.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem8.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem8.TabIndex = 5;
            // 
            // bpPanelControl12
            // 
            this.bpPanelControl12.Controls.Add(this.lbl담당자);
            this.bpPanelControl12.Controls.Add(this.ctx담당자);
            this.bpPanelControl12.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl12.Name = "bpPanelControl12";
            this.bpPanelControl12.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl12.TabIndex = 1;
            this.bpPanelControl12.Text = "bpPanelControl12";
            // 
            // lbl담당자
            // 
            this.lbl담당자.Location = new System.Drawing.Point(0, 2);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Resizeble = true;
            this.lbl담당자.Size = new System.Drawing.Size(80, 16);
            this.lbl담당자.TabIndex = 155;
            this.lbl담당자.Tag = "YN_USE";
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx담당자
            // 
            this.ctx담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx담당자.ButtonImage")));
            this.ctx담당자.ChildMode = "";
            this.ctx담당자.CodeName = "";
            this.ctx담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx담당자.CodeValue = "";
            this.ctx담당자.ComboCheck = true;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.IsCodeValueToUpper = true;
            this.ctx담당자.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx담당자.Location = new System.Drawing.Point(81, 1);
            this.ctx담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx담당자.SearchCode = true;
            this.ctx담당자.SelectCount = 0;
            this.ctx담당자.SetDefaultValue = true;
            this.ctx담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx담당자.Size = new System.Drawing.Size(133, 21);
            this.ctx담당자.TabIndex = 9;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Tag = "NO_EMP,NM_KOR";
            this.ctx담당자.Text = "bpCodeTextBox2";
            // 
            // bpPanelControl11
            // 
            this.bpPanelControl11.Controls.Add(this.lbl영업그룹);
            this.bpPanelControl11.Controls.Add(this.ctx영업그룹);
            this.bpPanelControl11.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl11.Name = "bpPanelControl11";
            this.bpPanelControl11.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl11.TabIndex = 0;
            this.bpPanelControl11.Text = "bpPanelControl11";
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Location = new System.Drawing.Point(0, 2);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Resizeble = true;
            this.lbl영업그룹.Size = new System.Drawing.Size(80, 16);
            this.lbl영업그룹.TabIndex = 67;
            this.lbl영업그룹.Tag = "NM_ITEM";
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx영업그룹
            // 
            this.ctx영업그룹.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx영업그룹.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx영업그룹.ButtonImage")));
            this.ctx영업그룹.ChildMode = "";
            this.ctx영업그룹.CodeName = "";
            this.ctx영업그룹.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx영업그룹.CodeValue = "";
            this.ctx영업그룹.ComboCheck = true;
            this.ctx영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.ctx영업그룹.IsCodeValueToUpper = true;
            this.ctx영업그룹.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx영업그룹.Location = new System.Drawing.Point(81, 1);
            this.ctx영업그룹.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx영업그룹.Name = "ctx영업그룹";
            this.ctx영업그룹.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx영업그룹.SearchCode = true;
            this.ctx영업그룹.SelectCount = 0;
            this.ctx영업그룹.SetDefaultValue = true;
            this.ctx영업그룹.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx영업그룹.Size = new System.Drawing.Size(133, 21);
            this.ctx영업그룹.TabIndex = 8;
            this.ctx영업그룹.TabStop = false;
            this.ctx영업그룹.Tag = "CD_SALEGRP,NM_SALEGRP";
            this.ctx영업그룹.Text = "bpCodeTextBox2";
            // 
            // oneGridItem9
            // 
            this.oneGridItem9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem9.Controls.Add(this.bpPanelControl14);
            this.oneGridItem9.Controls.Add(this.bpPanelControl13);
            this.oneGridItem9.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem9.Location = new System.Drawing.Point(0, 138);
            this.oneGridItem9.Name = "oneGridItem9";
            this.oneGridItem9.Size = new System.Drawing.Size(434, 23);
            this.oneGridItem9.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem9.TabIndex = 6;
            // 
            // bpPanelControl14
            // 
            this.bpPanelControl14.Controls.Add(this.lbl사업장);
            this.bpPanelControl14.Controls.Add(this.ctx사업장);
            this.bpPanelControl14.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl14.Name = "bpPanelControl14";
            this.bpPanelControl14.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl14.TabIndex = 1;
            this.bpPanelControl14.Text = "bpPanelControl14";
            // 
            // lbl사업장
            // 
            this.lbl사업장.Location = new System.Drawing.Point(0, 2);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Resizeble = true;
            this.lbl사업장.Size = new System.Drawing.Size(80, 16);
            this.lbl사업장.TabIndex = 156;
            this.lbl사업장.Tag = "YN_USE";
            this.lbl사업장.Text = "사업장";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx사업장
            // 
            this.ctx사업장.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx사업장.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx사업장.ButtonImage")));
            this.ctx사업장.ChildMode = "";
            this.ctx사업장.CodeName = "";
            this.ctx사업장.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx사업장.CodeValue = "";
            this.ctx사업장.ComboCheck = true;
            this.ctx사업장.Enabled = false;
            this.ctx사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.ctx사업장.IsCodeValueToUpper = true;
            this.ctx사업장.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx사업장.Location = new System.Drawing.Point(81, 1);
            this.ctx사업장.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx사업장.Name = "ctx사업장";
            this.ctx사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx사업장.SearchCode = true;
            this.ctx사업장.SelectCount = 0;
            this.ctx사업장.SetDefaultValue = true;
            this.ctx사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx사업장.Size = new System.Drawing.Size(133, 21);
            this.ctx사업장.TabIndex = 11;
            this.ctx사업장.TabStop = false;
            this.ctx사업장.Tag = "CD_BIZAREA,NM_BIZAREA";
            this.ctx사업장.Text = "bpCodeTextBox2";
            // 
            // bpPanelControl13
            // 
            this.bpPanelControl13.Controls.Add(this.lbl계약번호);
            this.bpPanelControl13.Controls.Add(this.txt계약번호);
            this.bpPanelControl13.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl13.Name = "bpPanelControl13";
            this.bpPanelControl13.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl13.TabIndex = 0;
            this.bpPanelControl13.Text = "bpPanelControl13";
            // 
            // lbl계약번호
            // 
            this.lbl계약번호.Location = new System.Drawing.Point(0, 2);
            this.lbl계약번호.Name = "lbl계약번호";
            this.lbl계약번호.Resizeble = true;
            this.lbl계약번호.Size = new System.Drawing.Size(80, 16);
            this.lbl계약번호.TabIndex = 69;
            this.lbl계약번호.Tag = "STND_ITEM";
            this.lbl계약번호.Text = "계약번호";
            this.lbl계약번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt계약번호
            // 
            this.txt계약번호.BackColor = System.Drawing.Color.White;
            this.txt계약번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt계약번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt계약번호.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt계약번호.Location = new System.Drawing.Point(81, 1);
            this.txt계약번호.MaxLength = 50;
            this.txt계약번호.Name = "txt계약번호";
            this.txt계약번호.SelectedAllEnabled = false;
            this.txt계약번호.Size = new System.Drawing.Size(133, 21);
            this.txt계약번호.TabIndex = 10;
            this.txt계약번호.Tag = "NO_PO";
            this.txt계약번호.UseKeyEnter = true;
            this.txt계약번호.UseKeyF3 = true;
            // 
            // tpg해외
            // 
            this.tpg해외.BackColor = System.Drawing.Color.White;
            this.tpg해외.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg해외.Controls.Add(this.lay해외);
            this.tpg해외.ImageIndex = 3;
            this.tpg해외.Location = new System.Drawing.Point(4, 24);
            this.tpg해외.Name = "tpg해외";
            this.tpg해외.Padding = new System.Windows.Forms.Padding(4);
            this.tpg해외.Size = new System.Drawing.Size(462, 376);
            this.tpg해외.TabIndex = 2;
            this.tpg해외.Text = "해외";
            this.tpg해외.UseVisualStyleBackColor = true;
            // 
            // lay해외
            // 
            this.lay해외.ColumnCount = 1;
            this.lay해외.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay해외.Controls.Add(this.pnl해외, 0, 0);
            this.lay해외.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay해외.Location = new System.Drawing.Point(4, 4);
            this.lay해외.Name = "lay해외";
            this.lay해외.RowCount = 2;
            this.lay해외.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay해외.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay해외.Size = new System.Drawing.Size(450, 364);
            this.lay해외.TabIndex = 181;
            // 
            // pnl해외
            // 
            this.pnl해외.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl해외.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem16,
            this.oneGridItem17,
            this.oneGridItem18,
            this.oneGridItem19,
            this.oneGridItem20,
            this.oneGridItem21,
            this.oneGridItem22,
            this.oneGridItem23,
            this.oneGridItem24,
            this.oneGridItem25});
            this.pnl해외.Location = new System.Drawing.Point(3, 3);
            this.pnl해외.Name = "pnl해외";
            this.pnl해외.Size = new System.Drawing.Size(444, 246);
            this.pnl해외.TabIndex = 1;
            // 
            // oneGridItem16
            // 
            this.oneGridItem16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem16.Controls.Add(this.bpPanelControl27);
            this.oneGridItem16.Controls.Add(this.bpPanelControl26);
            this.oneGridItem16.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem16.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem16.Name = "oneGridItem16";
            this.oneGridItem16.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem16.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem16.TabIndex = 0;
            // 
            // bpPanelControl27
            // 
            this.bpPanelControl27.Controls.Add(this.lbl제조자);
            this.bpPanelControl27.Controls.Add(this.ctx제조자);
            this.bpPanelControl27.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl27.Name = "bpPanelControl27";
            this.bpPanelControl27.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl27.TabIndex = 1;
            this.bpPanelControl27.Text = "bpPanelControl27";
            // 
            // lbl제조자
            // 
            this.lbl제조자.Location = new System.Drawing.Point(0, 2);
            this.lbl제조자.Name = "lbl제조자";
            this.lbl제조자.Resizeble = true;
            this.lbl제조자.Size = new System.Drawing.Size(80, 16);
            this.lbl제조자.TabIndex = 153;
            this.lbl제조자.Tag = "YN_USE";
            this.lbl제조자.Text = "제조자";
            this.lbl제조자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx제조자
            // 
            this.ctx제조자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx제조자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx제조자.ButtonImage")));
            this.ctx제조자.ChildMode = "";
            this.ctx제조자.CodeName = "";
            this.ctx제조자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx제조자.CodeValue = "";
            this.ctx제조자.ComboCheck = true;
            this.ctx제조자.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ctx제조자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx제조자.IsCodeValueToUpper = true;
            this.ctx제조자.ItemBackColor = System.Drawing.Color.White;
            this.ctx제조자.Location = new System.Drawing.Point(81, 1);
            this.ctx제조자.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx제조자.Name = "ctx제조자";
            this.ctx제조자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx제조자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx제조자.SearchCode = true;
            this.ctx제조자.SelectCount = 0;
            this.ctx제조자.SetDefaultValue = true;
            this.ctx제조자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx제조자.Size = new System.Drawing.Size(130, 21);
            this.ctx제조자.TabIndex = 1;
            this.ctx제조자.TabStop = false;
            this.ctx제조자.Tag = "CD_PRODUCT,NM_PRODUCT";
            this.ctx제조자.Text = "bpCodeTextBox2";
            // 
            // bpPanelControl26
            // 
            this.bpPanelControl26.Controls.Add(this.lbl수출자);
            this.bpPanelControl26.Controls.Add(this.ctx수출자);
            this.bpPanelControl26.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl26.Name = "bpPanelControl26";
            this.bpPanelControl26.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl26.TabIndex = 0;
            this.bpPanelControl26.Text = "bpPanelControl26";
            // 
            // lbl수출자
            // 
            this.lbl수출자.Location = new System.Drawing.Point(0, 2);
            this.lbl수출자.Name = "lbl수출자";
            this.lbl수출자.Resizeble = true;
            this.lbl수출자.Size = new System.Drawing.Size(80, 16);
            this.lbl수출자.TabIndex = 1;
            this.lbl수출자.Tag = "CD_ITEM";
            this.lbl수출자.Text = "수출자";
            this.lbl수출자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx수출자
            // 
            this.ctx수출자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx수출자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx수출자.ButtonImage")));
            this.ctx수출자.ChildMode = "";
            this.ctx수출자.CodeName = "";
            this.ctx수출자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx수출자.CodeValue = "";
            this.ctx수출자.ComboCheck = true;
            this.ctx수출자.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ctx수출자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx수출자.IsCodeValueToUpper = true;
            this.ctx수출자.ItemBackColor = System.Drawing.Color.White;
            this.ctx수출자.Location = new System.Drawing.Point(81, 1);
            this.ctx수출자.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx수출자.Name = "ctx수출자";
            this.ctx수출자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx수출자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx수출자.SearchCode = true;
            this.ctx수출자.SelectCount = 0;
            this.ctx수출자.SetDefaultValue = true;
            this.ctx수출자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx수출자.Size = new System.Drawing.Size(130, 21);
            this.ctx수출자.TabIndex = 0;
            this.ctx수출자.TabStop = false;
            this.ctx수출자.Tag = "CD_EXPORT,NM_EXPORT";
            this.ctx수출자.Text = "bpCodeTextBox2";
            // 
            // oneGridItem17
            // 
            this.oneGridItem17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem17.Controls.Add(this.bpPanelControl29);
            this.oneGridItem17.Controls.Add(this.bpPanelControl28);
            this.oneGridItem17.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem17.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem17.Name = "oneGridItem17";
            this.oneGridItem17.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem17.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem17.TabIndex = 1;
            // 
            // bpPanelControl29
            // 
            this.bpPanelControl29.Controls.Add(this.lbl검사기관);
            this.bpPanelControl29.Controls.Add(this.txt검사기관);
            this.bpPanelControl29.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl29.Name = "bpPanelControl29";
            this.bpPanelControl29.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl29.TabIndex = 1;
            this.bpPanelControl29.Text = "bpPanelControl29";
            // 
            // lbl검사기관
            // 
            this.lbl검사기관.Location = new System.Drawing.Point(0, 2);
            this.lbl검사기관.Name = "lbl검사기관";
            this.lbl검사기관.Resizeble = true;
            this.lbl검사기관.Size = new System.Drawing.Size(80, 16);
            this.lbl검사기관.TabIndex = 154;
            this.lbl검사기관.Tag = "YN_USE";
            this.lbl검사기관.Text = "검사기관";
            this.lbl검사기관.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt검사기관
            // 
            this.txt검사기관.BackColor = System.Drawing.Color.White;
            this.txt검사기관.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt검사기관.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt검사기관.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt검사기관.Location = new System.Drawing.Point(81, 1);
            this.txt검사기관.MaxLength = 50;
            this.txt검사기관.Name = "txt검사기관";
            this.txt검사기관.SelectedAllEnabled = false;
            this.txt검사기관.Size = new System.Drawing.Size(130, 21);
            this.txt검사기관.TabIndex = 3;
            this.txt검사기관.Tag = "NM_INSPECT";
            this.txt검사기관.UseKeyEnter = true;
            this.txt검사기관.UseKeyF3 = true;
            // 
            // bpPanelControl28
            // 
            this.bpPanelControl28.Controls.Add(this.lbl인도조건);
            this.bpPanelControl28.Controls.Add(this.textBoxExt2);
            this.bpPanelControl28.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl28.Name = "bpPanelControl28";
            this.bpPanelControl28.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl28.TabIndex = 0;
            this.bpPanelControl28.Text = "bpPanelControl28";
            // 
            // lbl인도조건
            // 
            this.lbl인도조건.Location = new System.Drawing.Point(0, 2);
            this.lbl인도조건.Name = "lbl인도조건";
            this.lbl인도조건.Resizeble = true;
            this.lbl인도조건.Size = new System.Drawing.Size(80, 16);
            this.lbl인도조건.TabIndex = 67;
            this.lbl인도조건.Tag = "NM_ITEM";
            this.lbl인도조건.Text = "인도조건";
            this.lbl인도조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxExt2
            // 
            this.textBoxExt2.BackColor = System.Drawing.Color.White;
            this.textBoxExt2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxExt2.Location = new System.Drawing.Point(81, 1);
            this.textBoxExt2.MaxLength = 50;
            this.textBoxExt2.Name = "textBoxExt2";
            this.textBoxExt2.SelectedAllEnabled = false;
            this.textBoxExt2.Size = new System.Drawing.Size(130, 21);
            this.textBoxExt2.TabIndex = 2;
            this.textBoxExt2.Tag = "COND_TRANS";
            this.textBoxExt2.UseKeyEnter = true;
            this.textBoxExt2.UseKeyF3 = true;
            // 
            // oneGridItem18
            // 
            this.oneGridItem18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem18.Controls.Add(this.bpPanelControl31);
            this.oneGridItem18.Controls.Add(this.bpPanelControl30);
            this.oneGridItem18.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem18.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem18.Name = "oneGridItem18";
            this.oneGridItem18.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem18.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem18.TabIndex = 2;
            // 
            // bpPanelControl31
            // 
            this.bpPanelControl31.Controls.Add(this.lbl결제일);
            this.bpPanelControl31.Controls.Add(this.cur결제일);
            this.bpPanelControl31.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl31.Name = "bpPanelControl31";
            this.bpPanelControl31.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl31.TabIndex = 1;
            this.bpPanelControl31.Text = "bpPanelControl31";
            // 
            // lbl결제일
            // 
            this.lbl결제일.Location = new System.Drawing.Point(0, 2);
            this.lbl결제일.Name = "lbl결제일";
            this.lbl결제일.Resizeble = true;
            this.lbl결제일.Size = new System.Drawing.Size(80, 16);
            this.lbl결제일.TabIndex = 153;
            this.lbl결제일.Tag = "YN_USE";
            this.lbl결제일.Text = "결제일";
            this.lbl결제일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur결제일
            // 
            this.cur결제일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur결제일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur결제일.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur결제일.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cur결제일.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur결제일.Location = new System.Drawing.Point(81, 1);
            this.cur결제일.Mask = null;
            this.cur결제일.Name = "cur결제일";
            this.cur결제일.NullString = "0";
            this.cur결제일.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur결제일.Size = new System.Drawing.Size(130, 21);
            this.cur결제일.TabIndex = 5;
            this.cur결제일.Tag = "COND_DAYS";
            this.cur결제일.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur결제일.UseKeyEnter = true;
            this.cur결제일.UseKeyF3 = true;
            // 
            // bpPanelControl30
            // 
            this.bpPanelControl30.Controls.Add(this.lbl결제형태);
            this.bpPanelControl30.Controls.Add(this.cbo결제형태);
            this.bpPanelControl30.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl30.Name = "bpPanelControl30";
            this.bpPanelControl30.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl30.TabIndex = 0;
            this.bpPanelControl30.Text = "bpPanelControl30";
            // 
            // lbl결제형태
            // 
            this.lbl결제형태.Location = new System.Drawing.Point(0, 2);
            this.lbl결제형태.Name = "lbl결제형태";
            this.lbl결제형태.Resizeble = true;
            this.lbl결제형태.Size = new System.Drawing.Size(80, 16);
            this.lbl결제형태.TabIndex = 68;
            this.lbl결제형태.Tag = "EN_ITEM";
            this.lbl결제형태.Text = "결제형태";
            this.lbl결제형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo결제형태
            // 
            this.cbo결제형태.AutoDropDown = true;
            this.cbo결제형태.BackColor = System.Drawing.Color.White;
            this.cbo결제형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo결제형태.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo결제형태.ItemHeight = 12;
            this.cbo결제형태.Location = new System.Drawing.Point(81, 1);
            this.cbo결제형태.Name = "cbo결제형태";
            this.cbo결제형태.ShowCheckBox = false;
            this.cbo결제형태.Size = new System.Drawing.Size(130, 20);
            this.cbo결제형태.TabIndex = 4;
            this.cbo결제형태.Tag = "COND_PAY";
            this.cbo결제형태.UseKeyEnter = true;
            this.cbo결제형태.UseKeyF3 = true;
            // 
            // oneGridItem19
            // 
            this.oneGridItem19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem19.Controls.Add(this.bpPanelControl33);
            this.oneGridItem19.Controls.Add(this.bpPanelControl32);
            this.oneGridItem19.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem19.Location = new System.Drawing.Point(0, 69);
            this.oneGridItem19.Name = "oneGridItem19";
            this.oneGridItem19.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem19.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem19.TabIndex = 3;
            // 
            // bpPanelControl33
            // 
            this.bpPanelControl33.Controls.Add(this.lbl유효일자해외);
            this.bpPanelControl33.Controls.Add(this.dtp유효일자해외);
            this.bpPanelControl33.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl33.Name = "bpPanelControl33";
            this.bpPanelControl33.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl33.TabIndex = 1;
            this.bpPanelControl33.Text = "bpPanelControl33";
            // 
            // lbl유효일자해외
            // 
            this.lbl유효일자해외.Location = new System.Drawing.Point(0, 2);
            this.lbl유효일자해외.Name = "lbl유효일자해외";
            this.lbl유효일자해외.Resizeble = true;
            this.lbl유효일자해외.Size = new System.Drawing.Size(80, 16);
            this.lbl유효일자해외.TabIndex = 155;
            this.lbl유효일자해외.Tag = "YN_USE";
            this.lbl유효일자해외.Text = "유효일자";
            this.lbl유효일자해외.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp유효일자해외
            // 
            this.dtp유효일자해외.BackColor = System.Drawing.Color.White;
            this.dtp유효일자해외.CalendarBackColor = System.Drawing.Color.White;
            this.dtp유효일자해외.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp유효일자해외.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp유효일자해외.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp유효일자해외.Location = new System.Drawing.Point(81, 1);
            this.dtp유효일자해외.Mask = "####/##/##";
            this.dtp유효일자해외.MaskBackColor = System.Drawing.Color.White;
            this.dtp유효일자해외.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp유효일자해외.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp유효일자해외.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp유효일자해외.Modified = false;
            this.dtp유효일자해외.Name = "dtp유효일자해외";
            this.dtp유효일자해외.NullCheck = false;
            this.dtp유효일자해외.PaddingCharacter = '_';
            this.dtp유효일자해외.PassivePromptCharacter = '_';
            this.dtp유효일자해외.PromptCharacter = '_';
            this.dtp유효일자해외.SelectedDayColor = System.Drawing.Color.White;
            this.dtp유효일자해외.ShowToDay = true;
            this.dtp유효일자해외.ShowTodayCircle = true;
            this.dtp유효일자해외.ShowUpDown = false;
            this.dtp유효일자해외.Size = new System.Drawing.Size(93, 21);
            this.dtp유효일자해외.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp유효일자해외.TabIndex = 7;
            this.dtp유효일자해외.Tag = "DT_EXPIRY";
            this.dtp유효일자해외.TitleBackColor = System.Drawing.Color.White;
            this.dtp유효일자해외.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp유효일자해외.ToDayColor = System.Drawing.Color.Red;
            this.dtp유효일자해외.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp유효일자해외.UseKeyF3 = false;
            this.dtp유효일자해외.Value = new System.DateTime(((long)(0)));
            // 
            // bpPanelControl32
            // 
            this.bpPanelControl32.Controls.Add(this.lbl포장형태);
            this.bpPanelControl32.Controls.Add(this.cbo포장형태);
            this.bpPanelControl32.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl32.Name = "bpPanelControl32";
            this.bpPanelControl32.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl32.TabIndex = 0;
            this.bpPanelControl32.Text = "bpPanelControl32";
            // 
            // lbl포장형태
            // 
            this.lbl포장형태.Location = new System.Drawing.Point(0, 2);
            this.lbl포장형태.Name = "lbl포장형태";
            this.lbl포장형태.Resizeble = true;
            this.lbl포장형태.Size = new System.Drawing.Size(80, 16);
            this.lbl포장형태.TabIndex = 71;
            this.lbl포장형태.Tag = "NM_ITEM";
            this.lbl포장형태.Text = "포장형태";
            this.lbl포장형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo포장형태
            // 
            this.cbo포장형태.AutoDropDown = true;
            this.cbo포장형태.BackColor = System.Drawing.Color.White;
            this.cbo포장형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo포장형태.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo포장형태.ItemHeight = 12;
            this.cbo포장형태.Location = new System.Drawing.Point(81, 1);
            this.cbo포장형태.Name = "cbo포장형태";
            this.cbo포장형태.ShowCheckBox = false;
            this.cbo포장형태.Size = new System.Drawing.Size(130, 20);
            this.cbo포장형태.TabIndex = 6;
            this.cbo포장형태.Tag = "TP_PACKING";
            this.cbo포장형태.UseKeyEnter = true;
            this.cbo포장형태.UseKeyF3 = true;
            // 
            // oneGridItem20
            // 
            this.oneGridItem20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem20.Controls.Add(this.bpPanelControl35);
            this.oneGridItem20.Controls.Add(this.bpPanelControl34);
            this.oneGridItem20.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem20.Location = new System.Drawing.Point(0, 92);
            this.oneGridItem20.Name = "oneGridItem20";
            this.oneGridItem20.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem20.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem20.TabIndex = 4;
            // 
            // bpPanelControl35
            // 
            this.bpPanelControl35.Controls.Add(this.lbl운송형태);
            this.bpPanelControl35.Controls.Add(this.cbo운송형태);
            this.bpPanelControl35.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl35.Name = "bpPanelControl35";
            this.bpPanelControl35.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl35.TabIndex = 1;
            this.bpPanelControl35.Text = "bpPanelControl35";
            // 
            // lbl운송형태
            // 
            this.lbl운송형태.Location = new System.Drawing.Point(0, 2);
            this.lbl운송형태.Name = "lbl운송형태";
            this.lbl운송형태.Resizeble = true;
            this.lbl운송형태.Size = new System.Drawing.Size(80, 16);
            this.lbl운송형태.TabIndex = 153;
            this.lbl운송형태.Tag = "YN_USE";
            this.lbl운송형태.Text = "운송형태";
            this.lbl운송형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo운송형태
            // 
            this.cbo운송형태.AutoDropDown = true;
            this.cbo운송형태.BackColor = System.Drawing.Color.White;
            this.cbo운송형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo운송형태.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo운송형태.ItemHeight = 12;
            this.cbo운송형태.Location = new System.Drawing.Point(81, 1);
            this.cbo운송형태.Name = "cbo운송형태";
            this.cbo운송형태.ShowCheckBox = false;
            this.cbo운송형태.Size = new System.Drawing.Size(130, 20);
            this.cbo운송형태.TabIndex = 9;
            this.cbo운송형태.Tag = "TP_TRANSPORT";
            this.cbo운송형태.UseKeyEnter = true;
            this.cbo운송형태.UseKeyF3 = true;
            // 
            // bpPanelControl34
            // 
            this.bpPanelControl34.Controls.Add(this.lbl운송방법);
            this.bpPanelControl34.Controls.Add(this.cbo운송방법);
            this.bpPanelControl34.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl34.Name = "bpPanelControl34";
            this.bpPanelControl34.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl34.TabIndex = 0;
            this.bpPanelControl34.Text = "bpPanelControl34";
            // 
            // lbl운송방법
            // 
            this.lbl운송방법.Location = new System.Drawing.Point(0, 2);
            this.lbl운송방법.Name = "lbl운송방법";
            this.lbl운송방법.Resizeble = true;
            this.lbl운송방법.Size = new System.Drawing.Size(80, 16);
            this.lbl운송방법.TabIndex = 67;
            this.lbl운송방법.Tag = "NM_ITEM";
            this.lbl운송방법.Text = "운송방법";
            this.lbl운송방법.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo운송방법
            // 
            this.cbo운송방법.AutoDropDown = true;
            this.cbo운송방법.BackColor = System.Drawing.Color.White;
            this.cbo운송방법.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo운송방법.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo운송방법.ItemHeight = 12;
            this.cbo운송방법.Location = new System.Drawing.Point(81, 1);
            this.cbo운송방법.Name = "cbo운송방법";
            this.cbo운송방법.ShowCheckBox = false;
            this.cbo운송방법.Size = new System.Drawing.Size(130, 20);
            this.cbo운송방법.TabIndex = 8;
            this.cbo운송방법.Tag = "TP_TRANS";
            this.cbo운송방법.UseKeyEnter = true;
            this.cbo운송방법.UseKeyF3 = true;
            // 
            // oneGridItem21
            // 
            this.oneGridItem21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem21.Controls.Add(this.bpPanelControl36);
            this.oneGridItem21.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem21.Location = new System.Drawing.Point(0, 115);
            this.oneGridItem21.Name = "oneGridItem21";
            this.oneGridItem21.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem21.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem21.TabIndex = 5;
            // 
            // bpPanelControl36
            // 
            this.bpPanelControl36.Controls.Add(this.lbl선적항);
            this.bpPanelControl36.Controls.Add(this.txt선적항);
            this.bpPanelControl36.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl36.Name = "bpPanelControl36";
            this.bpPanelControl36.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl36.TabIndex = 0;
            this.bpPanelControl36.Text = "bpPanelControl36";
            // 
            // lbl선적항
            // 
            this.lbl선적항.Location = new System.Drawing.Point(0, 2);
            this.lbl선적항.Name = "lbl선적항";
            this.lbl선적항.Resizeble = true;
            this.lbl선적항.Size = new System.Drawing.Size(80, 16);
            this.lbl선적항.TabIndex = 69;
            this.lbl선적항.Tag = "STND_ITEM";
            this.lbl선적항.Text = "선적항";
            this.lbl선적항.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt선적항
            // 
            this.txt선적항.BackColor = System.Drawing.Color.White;
            this.txt선적항.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt선적항.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt선적항.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt선적항.Location = new System.Drawing.Point(81, 1);
            this.txt선적항.MaxLength = 50;
            this.txt선적항.Name = "txt선적항";
            this.txt선적항.SelectedAllEnabled = false;
            this.txt선적항.Size = new System.Drawing.Size(346, 21);
            this.txt선적항.TabIndex = 10;
            this.txt선적항.Tag = "PORT_LOADING";
            this.txt선적항.UseKeyEnter = true;
            this.txt선적항.UseKeyF3 = true;
            // 
            // oneGridItem22
            // 
            this.oneGridItem22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem22.Controls.Add(this.bpPanelControl37);
            this.oneGridItem22.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem22.Location = new System.Drawing.Point(0, 138);
            this.oneGridItem22.Name = "oneGridItem22";
            this.oneGridItem22.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem22.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem22.TabIndex = 6;
            // 
            // bpPanelControl37
            // 
            this.bpPanelControl37.Controls.Add(this.lbl도착항);
            this.bpPanelControl37.Controls.Add(this.txt도착항);
            this.bpPanelControl37.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl37.Name = "bpPanelControl37";
            this.bpPanelControl37.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl37.TabIndex = 0;
            this.bpPanelControl37.Text = "bpPanelControl37";
            // 
            // lbl도착항
            // 
            this.lbl도착항.Location = new System.Drawing.Point(0, 2);
            this.lbl도착항.Name = "lbl도착항";
            this.lbl도착항.Resizeble = true;
            this.lbl도착항.Size = new System.Drawing.Size(80, 16);
            this.lbl도착항.TabIndex = 72;
            this.lbl도착항.Tag = "STND_ITEM";
            this.lbl도착항.Text = "도착항";
            this.lbl도착항.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt도착항
            // 
            this.txt도착항.BackColor = System.Drawing.Color.White;
            this.txt도착항.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt도착항.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt도착항.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt도착항.Location = new System.Drawing.Point(81, 1);
            this.txt도착항.MaxLength = 50;
            this.txt도착항.Name = "txt도착항";
            this.txt도착항.SelectedAllEnabled = false;
            this.txt도착항.Size = new System.Drawing.Size(346, 21);
            this.txt도착항.TabIndex = 11;
            this.txt도착항.Tag = "PORT_ARRIVER";
            this.txt도착항.UseKeyEnter = true;
            this.txt도착항.UseKeyF3 = true;
            // 
            // oneGridItem23
            // 
            this.oneGridItem23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem23.Controls.Add(this.bpPanelControl38);
            this.oneGridItem23.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem23.Location = new System.Drawing.Point(0, 161);
            this.oneGridItem23.Name = "oneGridItem23";
            this.oneGridItem23.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem23.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem23.TabIndex = 7;
            // 
            // bpPanelControl38
            // 
            this.bpPanelControl38.Controls.Add(this.lbl원산지);
            this.bpPanelControl38.Controls.Add(this.cbo원산지);
            this.bpPanelControl38.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl38.Name = "bpPanelControl38";
            this.bpPanelControl38.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl38.TabIndex = 0;
            this.bpPanelControl38.Text = "bpPanelControl38";
            // 
            // lbl원산지
            // 
            this.lbl원산지.Location = new System.Drawing.Point(0, 2);
            this.lbl원산지.Name = "lbl원산지";
            this.lbl원산지.Resizeble = true;
            this.lbl원산지.Size = new System.Drawing.Size(80, 16);
            this.lbl원산지.TabIndex = 73;
            this.lbl원산지.Tag = "STND_ITEM";
            this.lbl원산지.Text = "원산지";
            this.lbl원산지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo원산지
            // 
            this.cbo원산지.AutoDropDown = true;
            this.cbo원산지.BackColor = System.Drawing.Color.White;
            this.cbo원산지.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo원산지.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo원산지.ItemHeight = 12;
            this.cbo원산지.Location = new System.Drawing.Point(81, 1);
            this.cbo원산지.Name = "cbo원산지";
            this.cbo원산지.ShowCheckBox = false;
            this.cbo원산지.Size = new System.Drawing.Size(346, 20);
            this.cbo원산지.TabIndex = 12;
            this.cbo원산지.Tag = "CD_ORIGIN";
            this.cbo원산지.UseKeyEnter = true;
            this.cbo원산지.UseKeyF3 = true;
            // 
            // oneGridItem24
            // 
            this.oneGridItem24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem24.Controls.Add(this.bpPanelControl39);
            this.oneGridItem24.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem24.Location = new System.Drawing.Point(0, 184);
            this.oneGridItem24.Name = "oneGridItem24";
            this.oneGridItem24.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem24.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem24.TabIndex = 8;
            // 
            // bpPanelControl39
            // 
            this.bpPanelControl39.Controls.Add(this.lbl목적지);
            this.bpPanelControl39.Controls.Add(this.txt목적지);
            this.bpPanelControl39.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl39.Name = "bpPanelControl39";
            this.bpPanelControl39.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl39.TabIndex = 1;
            this.bpPanelControl39.Text = "bpPanelControl39";
            // 
            // lbl목적지
            // 
            this.lbl목적지.Location = new System.Drawing.Point(0, 2);
            this.lbl목적지.Name = "lbl목적지";
            this.lbl목적지.Resizeble = true;
            this.lbl목적지.Size = new System.Drawing.Size(80, 16);
            this.lbl목적지.TabIndex = 70;
            this.lbl목적지.Tag = "STND_ITEM";
            this.lbl목적지.Text = "목적지";
            this.lbl목적지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt목적지
            // 
            this.txt목적지.BackColor = System.Drawing.Color.White;
            this.txt목적지.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt목적지.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt목적지.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt목적지.Location = new System.Drawing.Point(81, 1);
            this.txt목적지.MaxLength = 50;
            this.txt목적지.Name = "txt목적지";
            this.txt목적지.SelectedAllEnabled = false;
            this.txt목적지.Size = new System.Drawing.Size(346, 21);
            this.txt목적지.TabIndex = 13;
            this.txt목적지.Tag = "DESTINATION";
            this.txt목적지.UseKeyEnter = true;
            this.txt목적지.UseKeyF3 = true;
            // 
            // oneGridItem25
            // 
            this.oneGridItem25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem25.Controls.Add(this.bpPanelControl40);
            this.oneGridItem25.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem25.Location = new System.Drawing.Point(0, 207);
            this.oneGridItem25.Name = "oneGridItem25";
            this.oneGridItem25.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem25.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem25.TabIndex = 9;
            // 
            // bpPanelControl40
            // 
            this.bpPanelControl40.Controls.Add(this.cbo가격조건);
            this.bpPanelControl40.Controls.Add(this.labelExt4);
            this.bpPanelControl40.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl40.Name = "bpPanelControl40";
            this.bpPanelControl40.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl40.TabIndex = 1;
            this.bpPanelControl40.Text = "bpPanelControl40";
            // 
            // cbo가격조건
            // 
            this.cbo가격조건.AutoDropDown = true;
            this.cbo가격조건.BackColor = System.Drawing.Color.White;
            this.cbo가격조건.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo가격조건.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo가격조건.ItemHeight = 12;
            this.cbo가격조건.Location = new System.Drawing.Point(81, 1);
            this.cbo가격조건.Name = "cbo가격조건";
            this.cbo가격조건.ShowCheckBox = false;
            this.cbo가격조건.Size = new System.Drawing.Size(346, 20);
            this.cbo가격조건.TabIndex = 197;
            this.cbo가격조건.Tag = "COND_PRICE";
            this.cbo가격조건.UseKeyEnter = true;
            this.cbo가격조건.UseKeyF3 = true;
            // 
            // labelExt4
            // 
            this.labelExt4.Location = new System.Drawing.Point(0, 2);
            this.labelExt4.Name = "labelExt4";
            this.labelExt4.Resizeble = true;
            this.labelExt4.Size = new System.Drawing.Size(80, 16);
            this.labelExt4.TabIndex = 74;
            this.labelExt4.Tag = "VESSEL";
            this.labelExt4.Text = "가격조건 (PAYMENT TERM)";
            this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tpg비고
            // 
            this.tpg비고.BackColor = System.Drawing.Color.White;
            this.tpg비고.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg비고.Controls.Add(this.lay비고);
            this.tpg비고.ImageIndex = 1;
            this.tpg비고.Location = new System.Drawing.Point(4, 24);
            this.tpg비고.Name = "tpg비고";
            this.tpg비고.Padding = new System.Windows.Forms.Padding(4);
            this.tpg비고.Size = new System.Drawing.Size(462, 376);
            this.tpg비고.TabIndex = 3;
            this.tpg비고.Text = "비고";
            // 
            // lay비고
            // 
            this.lay비고.ColumnCount = 1;
            this.lay비고.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay비고.Controls.Add(this.pnl비고, 0, 0);
            this.lay비고.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay비고.Location = new System.Drawing.Point(4, 4);
            this.lay비고.Name = "lay비고";
            this.lay비고.RowCount = 2;
            this.lay비고.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay비고.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay비고.Size = new System.Drawing.Size(450, 364);
            this.lay비고.TabIndex = 2;
            // 
            // pnl비고
            // 
            this.pnl비고.BackColor = System.Drawing.Color.White;
            this.pnl비고.Controls.Add(this.tableLayoutPanel3);
            this.pnl비고.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl비고.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pnl비고.Location = new System.Drawing.Point(3, 3);
            this.pnl비고.Name = "pnl비고";
            this.pnl비고.Size = new System.Drawing.Size(444, 326);
            this.pnl비고.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.oneGrid3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flexibleRoundedCornerBox1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(444, 326);
            this.tableLayoutPanel3.TabIndex = 74;
            // 
            // oneGrid3
            // 
            this.oneGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid3.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem26,
            this.oneGridItem27,
            this.oneGridItem28,
            this.oneGridItem29,
            this.oneGridItem30,
            this.oneGridItem31});
            this.oneGrid3.Location = new System.Drawing.Point(3, 3);
            this.oneGrid3.Name = "oneGrid3";
            this.oneGrid3.Size = new System.Drawing.Size(438, 154);
            this.oneGrid3.TabIndex = 2;
            // 
            // oneGridItem26
            // 
            this.oneGridItem26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem26.Controls.Add(this.bpPanelControl41);
            this.oneGridItem26.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem26.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem26.Name = "oneGridItem26";
            this.oneGridItem26.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem26.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem26.TabIndex = 0;
            // 
            // bpPanelControl41
            // 
            this.bpPanelControl41.Controls.Add(this.lbl비고1);
            this.bpPanelControl41.Controls.Add(this.txt비고1);
            this.bpPanelControl41.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl41.Name = "bpPanelControl41";
            this.bpPanelControl41.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl41.TabIndex = 0;
            this.bpPanelControl41.Text = "bpPanelControl41";
            // 
            // lbl비고1
            // 
            this.lbl비고1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고1.Location = new System.Drawing.Point(0, 2);
            this.lbl비고1.Name = "lbl비고1";
            this.lbl비고1.Resizeble = true;
            this.lbl비고1.Size = new System.Drawing.Size(80, 16);
            this.lbl비고1.TabIndex = 1;
            this.lbl비고1.Tag = "CD_ITEM";
            this.lbl비고1.Text = "비고1";
            this.lbl비고1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고1
            // 
            this.txt비고1.BackColor = System.Drawing.Color.White;
            this.txt비고1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고1.Location = new System.Drawing.Point(81, 1);
            this.txt비고1.MaxLength = 200;
            this.txt비고1.Name = "txt비고1";
            this.txt비고1.SelectedAllEnabled = false;
            this.txt비고1.Size = new System.Drawing.Size(342, 21);
            this.txt비고1.TabIndex = 0;
            this.txt비고1.Tag = "DC_RMK1";
            this.txt비고1.UseKeyEnter = true;
            this.txt비고1.UseKeyF3 = true;
            // 
            // oneGridItem27
            // 
            this.oneGridItem27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem27.Controls.Add(this.bpPanelControl42);
            this.oneGridItem27.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem27.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem27.Name = "oneGridItem27";
            this.oneGridItem27.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem27.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem27.TabIndex = 1;
            // 
            // bpPanelControl42
            // 
            this.bpPanelControl42.Controls.Add(this.lbl비고2);
            this.bpPanelControl42.Controls.Add(this.txt비고2);
            this.bpPanelControl42.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl42.Name = "bpPanelControl42";
            this.bpPanelControl42.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl42.TabIndex = 1;
            this.bpPanelControl42.Text = "bpPanelControl42";
            // 
            // lbl비고2
            // 
            this.lbl비고2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고2.Location = new System.Drawing.Point(0, 2);
            this.lbl비고2.Name = "lbl비고2";
            this.lbl비고2.Resizeble = true;
            this.lbl비고2.Size = new System.Drawing.Size(80, 16);
            this.lbl비고2.TabIndex = 67;
            this.lbl비고2.Tag = "NM_ITEM";
            this.lbl비고2.Text = "비고2";
            this.lbl비고2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고2
            // 
            this.txt비고2.BackColor = System.Drawing.Color.White;
            this.txt비고2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고2.Location = new System.Drawing.Point(81, 1);
            this.txt비고2.MaxLength = 200;
            this.txt비고2.Name = "txt비고2";
            this.txt비고2.SelectedAllEnabled = false;
            this.txt비고2.Size = new System.Drawing.Size(342, 21);
            this.txt비고2.TabIndex = 1;
            this.txt비고2.Tag = "DC_RMK2";
            this.txt비고2.UseKeyEnter = true;
            this.txt비고2.UseKeyF3 = true;
            // 
            // oneGridItem28
            // 
            this.oneGridItem28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem28.Controls.Add(this.bpPanelControl43);
            this.oneGridItem28.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem28.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem28.Name = "oneGridItem28";
            this.oneGridItem28.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem28.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem28.TabIndex = 2;
            // 
            // bpPanelControl43
            // 
            this.bpPanelControl43.Controls.Add(this.lbl비고3);
            this.bpPanelControl43.Controls.Add(this.txt비고3);
            this.bpPanelControl43.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl43.Name = "bpPanelControl43";
            this.bpPanelControl43.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl43.TabIndex = 1;
            this.bpPanelControl43.Text = "bpPanelControl43";
            // 
            // lbl비고3
            // 
            this.lbl비고3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고3.Location = new System.Drawing.Point(0, 2);
            this.lbl비고3.Name = "lbl비고3";
            this.lbl비고3.Resizeble = true;
            this.lbl비고3.Size = new System.Drawing.Size(80, 16);
            this.lbl비고3.TabIndex = 68;
            this.lbl비고3.Tag = "EN_ITEM";
            this.lbl비고3.Text = "비고3";
            this.lbl비고3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고3
            // 
            this.txt비고3.BackColor = System.Drawing.Color.White;
            this.txt비고3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고3.Location = new System.Drawing.Point(81, 1);
            this.txt비고3.MaxLength = 200;
            this.txt비고3.Name = "txt비고3";
            this.txt비고3.SelectedAllEnabled = false;
            this.txt비고3.Size = new System.Drawing.Size(342, 21);
            this.txt비고3.TabIndex = 2;
            this.txt비고3.Tag = "DC_RMK3";
            this.txt비고3.UseKeyEnter = true;
            this.txt비고3.UseKeyF3 = true;
            // 
            // oneGridItem29
            // 
            this.oneGridItem29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem29.Controls.Add(this.bpPanelControl44);
            this.oneGridItem29.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem29.Location = new System.Drawing.Point(0, 69);
            this.oneGridItem29.Name = "oneGridItem29";
            this.oneGridItem29.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem29.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem29.TabIndex = 3;
            // 
            // bpPanelControl44
            // 
            this.bpPanelControl44.Controls.Add(this.lbl비고4);
            this.bpPanelControl44.Controls.Add(this.txt비고4);
            this.bpPanelControl44.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl44.Name = "bpPanelControl44";
            this.bpPanelControl44.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl44.TabIndex = 1;
            this.bpPanelControl44.Text = "bpPanelControl44";
            // 
            // lbl비고4
            // 
            this.lbl비고4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고4.Location = new System.Drawing.Point(0, 2);
            this.lbl비고4.Name = "lbl비고4";
            this.lbl비고4.Resizeble = true;
            this.lbl비고4.Size = new System.Drawing.Size(80, 16);
            this.lbl비고4.TabIndex = 69;
            this.lbl비고4.Tag = "";
            this.lbl비고4.Text = "비고4";
            this.lbl비고4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고4
            // 
            this.txt비고4.BackColor = System.Drawing.Color.White;
            this.txt비고4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고4.Location = new System.Drawing.Point(81, 1);
            this.txt비고4.MaxLength = 200;
            this.txt비고4.Name = "txt비고4";
            this.txt비고4.SelectedAllEnabled = false;
            this.txt비고4.Size = new System.Drawing.Size(342, 21);
            this.txt비고4.TabIndex = 3;
            this.txt비고4.Tag = "DC_RMK4";
            this.txt비고4.UseKeyEnter = true;
            this.txt비고4.UseKeyF3 = true;
            // 
            // oneGridItem30
            // 
            this.oneGridItem30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem30.Controls.Add(this.bpPanelControl45);
            this.oneGridItem30.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem30.Location = new System.Drawing.Point(0, 92);
            this.oneGridItem30.Name = "oneGridItem30";
            this.oneGridItem30.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem30.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem30.TabIndex = 4;
            // 
            // bpPanelControl45
            // 
            this.bpPanelControl45.Controls.Add(this.lbl비고5);
            this.bpPanelControl45.Controls.Add(this.txt비고5);
            this.bpPanelControl45.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl45.Name = "bpPanelControl45";
            this.bpPanelControl45.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl45.TabIndex = 1;
            this.bpPanelControl45.Text = "bpPanelControl45";
            // 
            // lbl비고5
            // 
            this.lbl비고5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고5.Location = new System.Drawing.Point(0, 2);
            this.lbl비고5.Name = "lbl비고5";
            this.lbl비고5.Resizeble = true;
            this.lbl비고5.Size = new System.Drawing.Size(80, 16);
            this.lbl비고5.TabIndex = 70;
            this.lbl비고5.Tag = "";
            this.lbl비고5.Text = "비고5";
            this.lbl비고5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고5
            // 
            this.txt비고5.BackColor = System.Drawing.Color.White;
            this.txt비고5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고5.Location = new System.Drawing.Point(81, 1);
            this.txt비고5.MaxLength = 200;
            this.txt비고5.Name = "txt비고5";
            this.txt비고5.SelectedAllEnabled = false;
            this.txt비고5.Size = new System.Drawing.Size(342, 21);
            this.txt비고5.TabIndex = 4;
            this.txt비고5.Tag = "DC_RMK5";
            this.txt비고5.UseKeyEnter = true;
            this.txt비고5.UseKeyF3 = true;
            // 
            // oneGridItem31
            // 
            this.oneGridItem31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem31.Controls.Add(this.bpPanelControl46);
            this.oneGridItem31.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem31.Location = new System.Drawing.Point(0, 115);
            this.oneGridItem31.Name = "oneGridItem31";
            this.oneGridItem31.Size = new System.Drawing.Size(428, 23);
            this.oneGridItem31.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem31.TabIndex = 5;
            // 
            // bpPanelControl46
            // 
            this.bpPanelControl46.Controls.Add(this.lbl비고6);
            this.bpPanelControl46.Controls.Add(this.txt비고6);
            this.bpPanelControl46.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl46.Name = "bpPanelControl46";
            this.bpPanelControl46.Size = new System.Drawing.Size(422, 23);
            this.bpPanelControl46.TabIndex = 1;
            this.bpPanelControl46.Text = "bpPanelControl46";
            // 
            // lbl비고6
            // 
            this.lbl비고6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고6.Location = new System.Drawing.Point(0, 2);
            this.lbl비고6.Name = "lbl비고6";
            this.lbl비고6.Resizeble = true;
            this.lbl비고6.Size = new System.Drawing.Size(80, 16);
            this.lbl비고6.TabIndex = 71;
            this.lbl비고6.Tag = "EN_ITEM";
            this.lbl비고6.Text = "비고6";
            this.lbl비고6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고6
            // 
            this.txt비고6.BackColor = System.Drawing.Color.White;
            this.txt비고6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt비고6.Location = new System.Drawing.Point(81, 1);
            this.txt비고6.MaxLength = 200;
            this.txt비고6.Name = "txt비고6";
            this.txt비고6.SelectedAllEnabled = false;
            this.txt비고6.Size = new System.Drawing.Size(342, 21);
            this.txt비고6.TabIndex = 5;
            this.txt비고6.Tag = "DC_RMK6";
            this.txt비고6.UseKeyEnter = true;
            this.txt비고6.UseKeyF3 = true;
            // 
            // flexibleRoundedCornerBox1
            // 
            this.flexibleRoundedCornerBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.flexibleRoundedCornerBox1.Controls.Add(this.lbl비고7);
            this.flexibleRoundedCornerBox1.Controls.Add(this.txt멀티비고);
            this.flexibleRoundedCornerBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexibleRoundedCornerBox1.Location = new System.Drawing.Point(3, 163);
            this.flexibleRoundedCornerBox1.Name = "flexibleRoundedCornerBox1";
            this.flexibleRoundedCornerBox1.Size = new System.Drawing.Size(438, 160);
            this.flexibleRoundedCornerBox1.TabIndex = 73;
            // 
            // lbl비고7
            // 
            this.lbl비고7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl비고7.Location = new System.Drawing.Point(5, 5);
            this.lbl비고7.Name = "lbl비고7";
            this.lbl비고7.Resizeble = true;
            this.lbl비고7.Size = new System.Drawing.Size(80, 16);
            this.lbl비고7.TabIndex = 72;
            this.lbl비고7.Tag = "";
            this.lbl비고7.Text = "비고7";
            this.lbl비고7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt멀티비고
            // 
            this.txt멀티비고.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txt멀티비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt멀티비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt멀티비고.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt멀티비고.Location = new System.Drawing.Point(87, 4);
            this.txt멀티비고.Multiline = true;
            this.txt멀티비고.Name = "txt멀티비고";
            this.txt멀티비고.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt멀티비고.SelectedAllEnabled = false;
            this.txt멀티비고.Size = new System.Drawing.Size(343, 150);
            this.txt멀티비고.TabIndex = 6;
            this.txt멀티비고.Tag = "DC_RMK_TEXT";
            this.txt멀티비고.UseKeyEnter = false;
            this.txt멀티비고.UseKeyF3 = false;
            // 
            // tpg기타
            // 
            this.tpg기타.BackColor = System.Drawing.Color.White;
            this.tpg기타.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg기타.Controls.Add(this.lay기타);
            this.tpg기타.ImageIndex = 1;
            this.tpg기타.Location = new System.Drawing.Point(4, 24);
            this.tpg기타.Name = "tpg기타";
            this.tpg기타.Padding = new System.Windows.Forms.Padding(4);
            this.tpg기타.Size = new System.Drawing.Size(462, 376);
            this.tpg기타.TabIndex = 4;
            this.tpg기타.Text = "기타";
            // 
            // lay기타
            // 
            this.lay기타.ColumnCount = 1;
            this.lay기타.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay기타.Controls.Add(this.pnl기타, 0, 0);
            this.lay기타.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lay기타.Location = new System.Drawing.Point(4, 4);
            this.lay기타.Name = "lay기타";
            this.lay기타.RowCount = 2;
            this.lay기타.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay기타.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lay기타.Size = new System.Drawing.Size(450, 364);
            this.lay기타.TabIndex = 3;
            // 
            // pnl기타
            // 
            this.pnl기타.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl기타.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem33,
            this.oneGridItem34,
            this.oneGridItem35,
            this.oneGridItem36,
            this.oneGridItem37,
            this.oneGridItem38,
            this.oneGridItem39,
            this.oneGridItem40,
            this.oneGridItem41,
            this.oneGridItem42,
            this.oneGridItem43,
            this.oneGridItem44,
            this.oneGridItem45});
            this.pnl기타.Location = new System.Drawing.Point(3, 3);
            this.pnl기타.Name = "pnl기타";
            this.pnl기타.Size = new System.Drawing.Size(444, 315);
            this.pnl기타.TabIndex = 2;
            // 
            // oneGridItem33
            // 
            this.oneGridItem33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem33.Controls.Add(this.bpPanelControl52);
            this.oneGridItem33.Controls.Add(this.bpPanelControl47);
            this.oneGridItem33.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem33.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem33.Name = "oneGridItem33";
            this.oneGridItem33.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem33.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem33.TabIndex = 0;
            // 
            // bpPanelControl52
            // 
            this.bpPanelControl52.Controls.Add(this.lbl날짜_사용자정의_2);
            this.bpPanelControl52.Controls.Add(this.dtp날짜_사용자정의_2);
            this.bpPanelControl52.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl52.Name = "bpPanelControl52";
            this.bpPanelControl52.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl52.TabIndex = 1;
            this.bpPanelControl52.Text = "bpPanelControl52";
            // 
            // lbl날짜_사용자정의_2
            // 
            this.lbl날짜_사용자정의_2.Location = new System.Drawing.Point(0, 2);
            this.lbl날짜_사용자정의_2.Name = "lbl날짜_사용자정의_2";
            this.lbl날짜_사용자정의_2.Resizeble = true;
            this.lbl날짜_사용자정의_2.Size = new System.Drawing.Size(80, 16);
            this.lbl날짜_사용자정의_2.TabIndex = 1;
            this.lbl날짜_사용자정의_2.Tag = "";
            this.lbl날짜_사용자정의_2.Text = "날짜_사용자정의_2";
            this.lbl날짜_사용자정의_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl날짜_사용자정의_2.Visible = false;
            // 
            // dtp날짜_사용자정의_2
            // 
            this.dtp날짜_사용자정의_2.CalendarBackColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp날짜_사용자정의_2.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp날짜_사용자정의_2.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp날짜_사용자정의_2.Location = new System.Drawing.Point(81, 1);
            this.dtp날짜_사용자정의_2.Mask = "####/##/##";
            this.dtp날짜_사용자정의_2.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dtp날짜_사용자정의_2.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp날짜_사용자정의_2.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp날짜_사용자정의_2.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp날짜_사용자정의_2.Modified = true;
            this.dtp날짜_사용자정의_2.Name = "dtp날짜_사용자정의_2";
            this.dtp날짜_사용자정의_2.NullCheck = false;
            this.dtp날짜_사용자정의_2.PaddingCharacter = '_';
            this.dtp날짜_사용자정의_2.PassivePromptCharacter = '_';
            this.dtp날짜_사용자정의_2.PromptCharacter = '_';
            this.dtp날짜_사용자정의_2.SelectedDayColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_2.ShowToDay = true;
            this.dtp날짜_사용자정의_2.ShowTodayCircle = true;
            this.dtp날짜_사용자정의_2.ShowUpDown = false;
            this.dtp날짜_사용자정의_2.Size = new System.Drawing.Size(103, 21);
            this.dtp날짜_사용자정의_2.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp날짜_사용자정의_2.TabIndex = 2;
            this.dtp날짜_사용자정의_2.Tag = "DATE_USERDEF2";
            this.dtp날짜_사용자정의_2.TitleBackColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_2.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp날짜_사용자정의_2.ToDayColor = System.Drawing.Color.Red;
            this.dtp날짜_사용자정의_2.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp날짜_사용자정의_2.UseKeyF3 = false;
            this.dtp날짜_사용자정의_2.Value = new System.DateTime(((long)(0)));
            this.dtp날짜_사용자정의_2.Visible = false;
            // 
            // bpPanelControl47
            // 
            this.bpPanelControl47.Controls.Add(this.lbl날짜_사용자정의_1);
            this.bpPanelControl47.Controls.Add(this.dtp날짜_사용자정의_1);
            this.bpPanelControl47.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl47.Name = "bpPanelControl47";
            this.bpPanelControl47.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl47.TabIndex = 0;
            this.bpPanelControl47.Text = "bpPanelControl47";
            // 
            // lbl날짜_사용자정의_1
            // 
            this.lbl날짜_사용자정의_1.Location = new System.Drawing.Point(0, 2);
            this.lbl날짜_사용자정의_1.Name = "lbl날짜_사용자정의_1";
            this.lbl날짜_사용자정의_1.Resizeble = true;
            this.lbl날짜_사용자정의_1.Size = new System.Drawing.Size(80, 16);
            this.lbl날짜_사용자정의_1.TabIndex = 1;
            this.lbl날짜_사용자정의_1.Tag = "";
            this.lbl날짜_사용자정의_1.Text = "날짜_사용자정의_1";
            this.lbl날짜_사용자정의_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl날짜_사용자정의_1.Visible = false;
            // 
            // dtp날짜_사용자정의_1
            // 
            this.dtp날짜_사용자정의_1.CalendarBackColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp날짜_사용자정의_1.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp날짜_사용자정의_1.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp날짜_사용자정의_1.Location = new System.Drawing.Point(81, 1);
            this.dtp날짜_사용자정의_1.Mask = "####/##/##";
            this.dtp날짜_사용자정의_1.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dtp날짜_사용자정의_1.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp날짜_사용자정의_1.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp날짜_사용자정의_1.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp날짜_사용자정의_1.Modified = true;
            this.dtp날짜_사용자정의_1.Name = "dtp날짜_사용자정의_1";
            this.dtp날짜_사용자정의_1.NullCheck = false;
            this.dtp날짜_사용자정의_1.PaddingCharacter = '_';
            this.dtp날짜_사용자정의_1.PassivePromptCharacter = '_';
            this.dtp날짜_사용자정의_1.PromptCharacter = '_';
            this.dtp날짜_사용자정의_1.SelectedDayColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_1.ShowToDay = true;
            this.dtp날짜_사용자정의_1.ShowTodayCircle = true;
            this.dtp날짜_사용자정의_1.ShowUpDown = false;
            this.dtp날짜_사용자정의_1.Size = new System.Drawing.Size(103, 21);
            this.dtp날짜_사용자정의_1.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp날짜_사용자정의_1.TabIndex = 1;
            this.dtp날짜_사용자정의_1.Tag = "DATE_USERDEF1";
            this.dtp날짜_사용자정의_1.TitleBackColor = System.Drawing.Color.White;
            this.dtp날짜_사용자정의_1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp날짜_사용자정의_1.ToDayColor = System.Drawing.Color.Red;
            this.dtp날짜_사용자정의_1.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp날짜_사용자정의_1.UseKeyF3 = false;
            this.dtp날짜_사용자정의_1.Value = new System.DateTime(((long)(0)));
            this.dtp날짜_사용자정의_1.Visible = false;
            // 
            // oneGridItem34
            // 
            this.oneGridItem34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem34.Controls.Add(this.bpPanelControl48);
            this.oneGridItem34.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem34.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem34.Name = "oneGridItem34";
            this.oneGridItem34.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem34.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem34.TabIndex = 1;
            // 
            // bpPanelControl48
            // 
            this.bpPanelControl48.Controls.Add(this.lbl텍스트_사용자정의_1);
            this.bpPanelControl48.Controls.Add(this.txt텍스트_사용자정의_1);
            this.bpPanelControl48.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl48.Name = "bpPanelControl48";
            this.bpPanelControl48.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl48.TabIndex = 1;
            this.bpPanelControl48.Text = "bpPanelControl48";
            // 
            // lbl텍스트_사용자정의_1
            // 
            this.lbl텍스트_사용자정의_1.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_1.Name = "lbl텍스트_사용자정의_1";
            this.lbl텍스트_사용자정의_1.Resizeble = true;
            this.lbl텍스트_사용자정의_1.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_1.TabIndex = 2;
            this.lbl텍스트_사용자정의_1.Tag = "";
            this.lbl텍스트_사용자정의_1.Text = "텍스트_사용자정의_1";
            this.lbl텍스트_사용자정의_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_1.Visible = false;
            // 
            // txt텍스트_사용자정의_1
            // 
            this.txt텍스트_사용자정의_1.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_1.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_1.MaxLength = 200;
            this.txt텍스트_사용자정의_1.Name = "txt텍스트_사용자정의_1";
            this.txt텍스트_사용자정의_1.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_1.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_1.TabIndex = 3;
            this.txt텍스트_사용자정의_1.Tag = "TEXT_USERDEF1";
            this.txt텍스트_사용자정의_1.UseKeyEnter = true;
            this.txt텍스트_사용자정의_1.UseKeyF3 = true;
            this.txt텍스트_사용자정의_1.Visible = false;
            // 
            // oneGridItem35
            // 
            this.oneGridItem35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem35.Controls.Add(this.bpPanelControl49);
            this.oneGridItem35.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem35.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem35.Name = "oneGridItem35";
            this.oneGridItem35.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem35.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem35.TabIndex = 2;
            // 
            // bpPanelControl49
            // 
            this.bpPanelControl49.Controls.Add(this.lbl텍스트_사용자정의_2);
            this.bpPanelControl49.Controls.Add(this.txt텍스트_사용자정의_2);
            this.bpPanelControl49.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl49.Name = "bpPanelControl49";
            this.bpPanelControl49.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl49.TabIndex = 1;
            this.bpPanelControl49.Text = "bpPanelControl49";
            // 
            // lbl텍스트_사용자정의_2
            // 
            this.lbl텍스트_사용자정의_2.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_2.Name = "lbl텍스트_사용자정의_2";
            this.lbl텍스트_사용자정의_2.Resizeble = true;
            this.lbl텍스트_사용자정의_2.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_2.TabIndex = 2;
            this.lbl텍스트_사용자정의_2.Tag = "";
            this.lbl텍스트_사용자정의_2.Text = "텍스트_사용자정의_2";
            this.lbl텍스트_사용자정의_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_2.Visible = false;
            // 
            // txt텍스트_사용자정의_2
            // 
            this.txt텍스트_사용자정의_2.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_2.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_2.MaxLength = 200;
            this.txt텍스트_사용자정의_2.Name = "txt텍스트_사용자정의_2";
            this.txt텍스트_사용자정의_2.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_2.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_2.TabIndex = 4;
            this.txt텍스트_사용자정의_2.Tag = "TEXT_USERDEF2";
            this.txt텍스트_사용자정의_2.UseKeyEnter = true;
            this.txt텍스트_사용자정의_2.UseKeyF3 = true;
            this.txt텍스트_사용자정의_2.Visible = false;
            // 
            // oneGridItem36
            // 
            this.oneGridItem36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem36.Controls.Add(this.bpPanelControl50);
            this.oneGridItem36.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem36.Location = new System.Drawing.Point(0, 69);
            this.oneGridItem36.Name = "oneGridItem36";
            this.oneGridItem36.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem36.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem36.TabIndex = 3;
            // 
            // bpPanelControl50
            // 
            this.bpPanelControl50.Controls.Add(this.lbl텍스트_사용자정의_3);
            this.bpPanelControl50.Controls.Add(this.txt텍스트_사용자정의_3);
            this.bpPanelControl50.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl50.Name = "bpPanelControl50";
            this.bpPanelControl50.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl50.TabIndex = 2;
            this.bpPanelControl50.Text = "bpPanelControl50";
            // 
            // lbl텍스트_사용자정의_3
            // 
            this.lbl텍스트_사용자정의_3.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_3.Name = "lbl텍스트_사용자정의_3";
            this.lbl텍스트_사용자정의_3.Resizeble = true;
            this.lbl텍스트_사용자정의_3.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_3.TabIndex = 3;
            this.lbl텍스트_사용자정의_3.Tag = "";
            this.lbl텍스트_사용자정의_3.Text = "텍스트_사용자정의_3";
            this.lbl텍스트_사용자정의_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_3.Visible = false;
            // 
            // txt텍스트_사용자정의_3
            // 
            this.txt텍스트_사용자정의_3.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_3.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_3.MaxLength = 200;
            this.txt텍스트_사용자정의_3.Name = "txt텍스트_사용자정의_3";
            this.txt텍스트_사용자정의_3.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_3.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_3.TabIndex = 5;
            this.txt텍스트_사용자정의_3.Tag = "TEXT_USERDEF3";
            this.txt텍스트_사용자정의_3.UseKeyEnter = true;
            this.txt텍스트_사용자정의_3.UseKeyF3 = true;
            this.txt텍스트_사용자정의_3.Visible = false;
            // 
            // oneGridItem37
            // 
            this.oneGridItem37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem37.Controls.Add(this.bpPanelControl51);
            this.oneGridItem37.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem37.Location = new System.Drawing.Point(0, 92);
            this.oneGridItem37.Name = "oneGridItem37";
            this.oneGridItem37.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem37.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem37.TabIndex = 4;
            // 
            // bpPanelControl51
            // 
            this.bpPanelControl51.Controls.Add(this.lbl텍스트_사용자정의_4);
            this.bpPanelControl51.Controls.Add(this.txt텍스트_사용자정의_4);
            this.bpPanelControl51.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl51.Name = "bpPanelControl51";
            this.bpPanelControl51.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl51.TabIndex = 2;
            this.bpPanelControl51.Text = "bpPanelControl51";
            // 
            // lbl텍스트_사용자정의_4
            // 
            this.lbl텍스트_사용자정의_4.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_4.Name = "lbl텍스트_사용자정의_4";
            this.lbl텍스트_사용자정의_4.Resizeble = true;
            this.lbl텍스트_사용자정의_4.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_4.TabIndex = 4;
            this.lbl텍스트_사용자정의_4.Tag = "";
            this.lbl텍스트_사용자정의_4.Text = "텍스트_사용자정의_4";
            this.lbl텍스트_사용자정의_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_4.Visible = false;
            // 
            // txt텍스트_사용자정의_4
            // 
            this.txt텍스트_사용자정의_4.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_4.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_4.MaxLength = 200;
            this.txt텍스트_사용자정의_4.Name = "txt텍스트_사용자정의_4";
            this.txt텍스트_사용자정의_4.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_4.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_4.TabIndex = 6;
            this.txt텍스트_사용자정의_4.Tag = "TEXT_USERDEF4";
            this.txt텍스트_사용자정의_4.UseKeyEnter = true;
            this.txt텍스트_사용자정의_4.UseKeyF3 = true;
            this.txt텍스트_사용자정의_4.Visible = false;
            // 
            // oneGridItem38
            // 
            this.oneGridItem38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem38.Controls.Add(this.bpPanelControl53);
            this.oneGridItem38.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem38.Location = new System.Drawing.Point(0, 115);
            this.oneGridItem38.Name = "oneGridItem38";
            this.oneGridItem38.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem38.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem38.TabIndex = 5;
            // 
            // bpPanelControl53
            // 
            this.bpPanelControl53.Controls.Add(this.lbl텍스트_사용자정의_5);
            this.bpPanelControl53.Controls.Add(this.txt텍스트_사용자정의_5);
            this.bpPanelControl53.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl53.Name = "bpPanelControl53";
            this.bpPanelControl53.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl53.TabIndex = 0;
            this.bpPanelControl53.Text = "bpPanelControl53";
            // 
            // lbl텍스트_사용자정의_5
            // 
            this.lbl텍스트_사용자정의_5.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_5.Name = "lbl텍스트_사용자정의_5";
            this.lbl텍스트_사용자정의_5.Resizeble = true;
            this.lbl텍스트_사용자정의_5.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_5.TabIndex = 4;
            this.lbl텍스트_사용자정의_5.Tag = "";
            this.lbl텍스트_사용자정의_5.Text = "텍스트_사용자정의_5";
            this.lbl텍스트_사용자정의_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_5.Visible = false;
            // 
            // txt텍스트_사용자정의_5
            // 
            this.txt텍스트_사용자정의_5.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_5.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_5.MaxLength = 200;
            this.txt텍스트_사용자정의_5.Name = "txt텍스트_사용자정의_5";
            this.txt텍스트_사용자정의_5.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_5.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_5.TabIndex = 7;
            this.txt텍스트_사용자정의_5.Tag = "TEXT_USERDEF5";
            this.txt텍스트_사용자정의_5.UseKeyEnter = true;
            this.txt텍스트_사용자정의_5.UseKeyF3 = true;
            this.txt텍스트_사용자정의_5.Visible = false;
            // 
            // oneGridItem39
            // 
            this.oneGridItem39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem39.Controls.Add(this.bpPanelControl54);
            this.oneGridItem39.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem39.Location = new System.Drawing.Point(0, 138);
            this.oneGridItem39.Name = "oneGridItem39";
            this.oneGridItem39.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem39.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem39.TabIndex = 6;
            // 
            // bpPanelControl54
            // 
            this.bpPanelControl54.Controls.Add(this.lbl텍스트_사용자정의_6);
            this.bpPanelControl54.Controls.Add(this.txt텍스트_사용자정의_6);
            this.bpPanelControl54.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl54.Name = "bpPanelControl54";
            this.bpPanelControl54.Size = new System.Drawing.Size(428, 23);
            this.bpPanelControl54.TabIndex = 0;
            this.bpPanelControl54.Text = "bpPanelControl54";
            // 
            // lbl텍스트_사용자정의_6
            // 
            this.lbl텍스트_사용자정의_6.Location = new System.Drawing.Point(0, 2);
            this.lbl텍스트_사용자정의_6.Name = "lbl텍스트_사용자정의_6";
            this.lbl텍스트_사용자정의_6.Resizeble = true;
            this.lbl텍스트_사용자정의_6.Size = new System.Drawing.Size(80, 16);
            this.lbl텍스트_사용자정의_6.TabIndex = 5;
            this.lbl텍스트_사용자정의_6.Tag = "";
            this.lbl텍스트_사용자정의_6.Text = "텍스트_사용자정의_6";
            this.lbl텍스트_사용자정의_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl텍스트_사용자정의_6.Visible = false;
            // 
            // txt텍스트_사용자정의_6
            // 
            this.txt텍스트_사용자정의_6.BackColor = System.Drawing.Color.White;
            this.txt텍스트_사용자정의_6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt텍스트_사용자정의_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt텍스트_사용자정의_6.Location = new System.Drawing.Point(81, 1);
            this.txt텍스트_사용자정의_6.MaxLength = 200;
            this.txt텍스트_사용자정의_6.Name = "txt텍스트_사용자정의_6";
            this.txt텍스트_사용자정의_6.SelectedAllEnabled = false;
            this.txt텍스트_사용자정의_6.Size = new System.Drawing.Size(345, 21);
            this.txt텍스트_사용자정의_6.TabIndex = 8;
            this.txt텍스트_사용자정의_6.Tag = "TEXT_USERDEF6";
            this.txt텍스트_사용자정의_6.UseKeyEnter = true;
            this.txt텍스트_사용자정의_6.UseKeyF3 = true;
            this.txt텍스트_사용자정의_6.Visible = false;
            // 
            // oneGridItem40
            // 
            this.oneGridItem40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem40.Controls.Add(this.bpPanelControl56);
            this.oneGridItem40.Controls.Add(this.bpPanelControl55);
            this.oneGridItem40.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem40.Location = new System.Drawing.Point(0, 161);
            this.oneGridItem40.Name = "oneGridItem40";
            this.oneGridItem40.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem40.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem40.TabIndex = 7;
            // 
            // bpPanelControl56
            // 
            this.bpPanelControl56.Controls.Add(this.lbl코드_사용자정의_2);
            this.bpPanelControl56.Controls.Add(this.cbo코드_사용자정의_2);
            this.bpPanelControl56.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl56.Name = "bpPanelControl56";
            this.bpPanelControl56.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl56.TabIndex = 1;
            this.bpPanelControl56.Text = "bpPanelControl56";
            // 
            // lbl코드_사용자정의_2
            // 
            this.lbl코드_사용자정의_2.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_2.Name = "lbl코드_사용자정의_2";
            this.lbl코드_사용자정의_2.Resizeble = true;
            this.lbl코드_사용자정의_2.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_2.TabIndex = 6;
            this.lbl코드_사용자정의_2.Tag = "";
            this.lbl코드_사용자정의_2.Text = "코드_사용자정의_2";
            this.lbl코드_사용자정의_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_2.Visible = false;
            // 
            // cbo코드_사용자정의_2
            // 
            this.cbo코드_사용자정의_2.AutoDropDown = true;
            this.cbo코드_사용자정의_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_2.FormattingEnabled = true;
            this.cbo코드_사용자정의_2.ItemHeight = 12;
            this.cbo코드_사용자정의_2.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_2.Name = "cbo코드_사용자정의_2";
            this.cbo코드_사용자정의_2.ShowCheckBox = false;
            this.cbo코드_사용자정의_2.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_2.TabIndex = 10;
            this.cbo코드_사용자정의_2.Tag = "CD_USERDEF2";
            this.cbo코드_사용자정의_2.UseKeyEnter = true;
            this.cbo코드_사용자정의_2.UseKeyF3 = true;
            this.cbo코드_사용자정의_2.Visible = false;
            // 
            // bpPanelControl55
            // 
            this.bpPanelControl55.Controls.Add(this.lbl코드_사용자정의_1);
            this.bpPanelControl55.Controls.Add(this.cbo코드_사용자정의_1);
            this.bpPanelControl55.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl55.Name = "bpPanelControl55";
            this.bpPanelControl55.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl55.TabIndex = 0;
            this.bpPanelControl55.Text = "bpPanelControl55";
            // 
            // lbl코드_사용자정의_1
            // 
            this.lbl코드_사용자정의_1.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_1.Name = "lbl코드_사용자정의_1";
            this.lbl코드_사용자정의_1.Resizeble = true;
            this.lbl코드_사용자정의_1.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_1.TabIndex = 5;
            this.lbl코드_사용자정의_1.Tag = "";
            this.lbl코드_사용자정의_1.Text = "코드_사용자정의_1";
            this.lbl코드_사용자정의_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_1.Visible = false;
            // 
            // cbo코드_사용자정의_1
            // 
            this.cbo코드_사용자정의_1.AutoDropDown = true;
            this.cbo코드_사용자정의_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_1.FormattingEnabled = true;
            this.cbo코드_사용자정의_1.ItemHeight = 12;
            this.cbo코드_사용자정의_1.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_1.Name = "cbo코드_사용자정의_1";
            this.cbo코드_사용자정의_1.ShowCheckBox = false;
            this.cbo코드_사용자정의_1.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_1.TabIndex = 9;
            this.cbo코드_사용자정의_1.Tag = "CD_USERDEF1";
            this.cbo코드_사용자정의_1.UseKeyEnter = true;
            this.cbo코드_사용자정의_1.UseKeyF3 = true;
            this.cbo코드_사용자정의_1.Visible = false;
            // 
            // oneGridItem41
            // 
            this.oneGridItem41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem41.Controls.Add(this.bpPanelControl57);
            this.oneGridItem41.Controls.Add(this.bpPanelControl58);
            this.oneGridItem41.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem41.Location = new System.Drawing.Point(0, 184);
            this.oneGridItem41.Name = "oneGridItem41";
            this.oneGridItem41.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem41.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem41.TabIndex = 8;
            // 
            // bpPanelControl57
            // 
            this.bpPanelControl57.Controls.Add(this.lbl코드_사용자정의_4);
            this.bpPanelControl57.Controls.Add(this.cbo코드_사용자정의_4);
            this.bpPanelControl57.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl57.Name = "bpPanelControl57";
            this.bpPanelControl57.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl57.TabIndex = 3;
            this.bpPanelControl57.Text = "bpPanelControl57";
            // 
            // lbl코드_사용자정의_4
            // 
            this.lbl코드_사용자정의_4.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_4.Name = "lbl코드_사용자정의_4";
            this.lbl코드_사용자정의_4.Resizeble = true;
            this.lbl코드_사용자정의_4.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_4.TabIndex = 7;
            this.lbl코드_사용자정의_4.Tag = "";
            this.lbl코드_사용자정의_4.Text = "코드_사용자정의_4";
            this.lbl코드_사용자정의_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_4.Visible = false;
            // 
            // cbo코드_사용자정의_4
            // 
            this.cbo코드_사용자정의_4.AutoDropDown = true;
            this.cbo코드_사용자정의_4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_4.FormattingEnabled = true;
            this.cbo코드_사용자정의_4.ItemHeight = 12;
            this.cbo코드_사용자정의_4.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_4.Name = "cbo코드_사용자정의_4";
            this.cbo코드_사용자정의_4.ShowCheckBox = false;
            this.cbo코드_사용자정의_4.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_4.TabIndex = 12;
            this.cbo코드_사용자정의_4.Tag = "CD_USERDEF4";
            this.cbo코드_사용자정의_4.UseKeyEnter = true;
            this.cbo코드_사용자정의_4.UseKeyF3 = true;
            this.cbo코드_사용자정의_4.Visible = false;
            // 
            // bpPanelControl58
            // 
            this.bpPanelControl58.Controls.Add(this.lbl코드_사용자정의_3);
            this.bpPanelControl58.Controls.Add(this.cbo코드_사용자정의_3);
            this.bpPanelControl58.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl58.Name = "bpPanelControl58";
            this.bpPanelControl58.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl58.TabIndex = 2;
            this.bpPanelControl58.Text = "bpPanelControl58";
            // 
            // lbl코드_사용자정의_3
            // 
            this.lbl코드_사용자정의_3.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_3.Name = "lbl코드_사용자정의_3";
            this.lbl코드_사용자정의_3.Resizeble = true;
            this.lbl코드_사용자정의_3.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_3.TabIndex = 6;
            this.lbl코드_사용자정의_3.Tag = "";
            this.lbl코드_사용자정의_3.Text = "코드_사용자정의_3";
            this.lbl코드_사용자정의_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_3.Visible = false;
            // 
            // cbo코드_사용자정의_3
            // 
            this.cbo코드_사용자정의_3.AutoDropDown = true;
            this.cbo코드_사용자정의_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_3.FormattingEnabled = true;
            this.cbo코드_사용자정의_3.ItemHeight = 12;
            this.cbo코드_사용자정의_3.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_3.Name = "cbo코드_사용자정의_3";
            this.cbo코드_사용자정의_3.ShowCheckBox = false;
            this.cbo코드_사용자정의_3.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_3.TabIndex = 11;
            this.cbo코드_사용자정의_3.Tag = "CD_USERDEF3";
            this.cbo코드_사용자정의_3.UseKeyEnter = true;
            this.cbo코드_사용자정의_3.UseKeyF3 = true;
            this.cbo코드_사용자정의_3.Visible = false;
            // 
            // oneGridItem42
            // 
            this.oneGridItem42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem42.Controls.Add(this.bpPanelControl59);
            this.oneGridItem42.Controls.Add(this.bpPanelControl60);
            this.oneGridItem42.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem42.Location = new System.Drawing.Point(0, 207);
            this.oneGridItem42.Name = "oneGridItem42";
            this.oneGridItem42.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem42.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem42.TabIndex = 9;
            // 
            // bpPanelControl59
            // 
            this.bpPanelControl59.Controls.Add(this.lbl코드_사용자정의_6);
            this.bpPanelControl59.Controls.Add(this.cbo코드_사용자정의_6);
            this.bpPanelControl59.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl59.Name = "bpPanelControl59";
            this.bpPanelControl59.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl59.TabIndex = 3;
            this.bpPanelControl59.Text = "bpPanelControl59";
            // 
            // lbl코드_사용자정의_6
            // 
            this.lbl코드_사용자정의_6.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_6.Name = "lbl코드_사용자정의_6";
            this.lbl코드_사용자정의_6.Resizeble = true;
            this.lbl코드_사용자정의_6.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_6.TabIndex = 8;
            this.lbl코드_사용자정의_6.Tag = "";
            this.lbl코드_사용자정의_6.Text = "코드_사용자정의_6";
            this.lbl코드_사용자정의_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_6.Visible = false;
            // 
            // cbo코드_사용자정의_6
            // 
            this.cbo코드_사용자정의_6.AutoDropDown = true;
            this.cbo코드_사용자정의_6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_6.FormattingEnabled = true;
            this.cbo코드_사용자정의_6.ItemHeight = 12;
            this.cbo코드_사용자정의_6.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_6.Name = "cbo코드_사용자정의_6";
            this.cbo코드_사용자정의_6.ShowCheckBox = false;
            this.cbo코드_사용자정의_6.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_6.TabIndex = 14;
            this.cbo코드_사용자정의_6.Tag = "CD_USERDEF6";
            this.cbo코드_사용자정의_6.UseKeyEnter = true;
            this.cbo코드_사용자정의_6.UseKeyF3 = true;
            this.cbo코드_사용자정의_6.Visible = false;
            // 
            // bpPanelControl60
            // 
            this.bpPanelControl60.Controls.Add(this.lbl코드_사용자정의_5);
            this.bpPanelControl60.Controls.Add(this.cbo코드_사용자정의_5);
            this.bpPanelControl60.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl60.Name = "bpPanelControl60";
            this.bpPanelControl60.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl60.TabIndex = 2;
            this.bpPanelControl60.Text = "bpPanelControl60";
            // 
            // lbl코드_사용자정의_5
            // 
            this.lbl코드_사용자정의_5.Location = new System.Drawing.Point(0, 2);
            this.lbl코드_사용자정의_5.Name = "lbl코드_사용자정의_5";
            this.lbl코드_사용자정의_5.Resizeble = true;
            this.lbl코드_사용자정의_5.Size = new System.Drawing.Size(80, 16);
            this.lbl코드_사용자정의_5.TabIndex = 7;
            this.lbl코드_사용자정의_5.Tag = "";
            this.lbl코드_사용자정의_5.Text = "코드_사용자정의_5";
            this.lbl코드_사용자정의_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl코드_사용자정의_5.Visible = false;
            // 
            // cbo코드_사용자정의_5
            // 
            this.cbo코드_사용자정의_5.AutoDropDown = true;
            this.cbo코드_사용자정의_5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo코드_사용자정의_5.FormattingEnabled = true;
            this.cbo코드_사용자정의_5.ItemHeight = 12;
            this.cbo코드_사용자정의_5.Location = new System.Drawing.Point(81, 1);
            this.cbo코드_사용자정의_5.Name = "cbo코드_사용자정의_5";
            this.cbo코드_사용자정의_5.ShowCheckBox = false;
            this.cbo코드_사용자정의_5.Size = new System.Drawing.Size(130, 20);
            this.cbo코드_사용자정의_5.TabIndex = 13;
            this.cbo코드_사용자정의_5.Tag = "CD_USERDEF5";
            this.cbo코드_사용자정의_5.UseKeyEnter = true;
            this.cbo코드_사용자정의_5.UseKeyF3 = true;
            this.cbo코드_사용자정의_5.Visible = false;
            // 
            // oneGridItem43
            // 
            this.oneGridItem43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem43.Controls.Add(this.bpPanelControl61);
            this.oneGridItem43.Controls.Add(this.bpPanelControl62);
            this.oneGridItem43.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem43.Location = new System.Drawing.Point(0, 230);
            this.oneGridItem43.Name = "oneGridItem43";
            this.oneGridItem43.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem43.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem43.TabIndex = 10;
            // 
            // bpPanelControl61
            // 
            this.bpPanelControl61.Controls.Add(this.lbl숫자_사용자정의_2);
            this.bpPanelControl61.Controls.Add(this.cur숫자_사용자정의_2);
            this.bpPanelControl61.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl61.Name = "bpPanelControl61";
            this.bpPanelControl61.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl61.TabIndex = 3;
            this.bpPanelControl61.Text = "bpPanelControl61";
            // 
            // lbl숫자_사용자정의_2
            // 
            this.lbl숫자_사용자정의_2.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_2.Name = "lbl숫자_사용자정의_2";
            this.lbl숫자_사용자정의_2.Resizeble = true;
            this.lbl숫자_사용자정의_2.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_2.TabIndex = 9;
            this.lbl숫자_사용자정의_2.Tag = "";
            this.lbl숫자_사용자정의_2.Text = "숫자_사용자정의_2";
            this.lbl숫자_사용자정의_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_2.Visible = false;
            // 
            // cur숫자_사용자정의_2
            // 
            this.cur숫자_사용자정의_2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_2.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_2.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_2.Mask = null;
            this.cur숫자_사용자정의_2.Name = "cur숫자_사용자정의_2";
            this.cur숫자_사용자정의_2.NullString = "0";
            this.cur숫자_사용자정의_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_2.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_2.TabIndex = 16;
            this.cur숫자_사용자정의_2.Tag = "NUM_USERDEF2";
            this.cur숫자_사용자정의_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_2.UseKeyEnter = true;
            this.cur숫자_사용자정의_2.UseKeyF3 = true;
            this.cur숫자_사용자정의_2.Visible = false;
            // 
            // bpPanelControl62
            // 
            this.bpPanelControl62.Controls.Add(this.lbl숫자_사용자정의_1);
            this.bpPanelControl62.Controls.Add(this.cur숫자_사용자정의_1);
            this.bpPanelControl62.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl62.Name = "bpPanelControl62";
            this.bpPanelControl62.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl62.TabIndex = 2;
            this.bpPanelControl62.Text = "bpPanelControl62";
            // 
            // lbl숫자_사용자정의_1
            // 
            this.lbl숫자_사용자정의_1.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_1.Name = "lbl숫자_사용자정의_1";
            this.lbl숫자_사용자정의_1.Resizeble = true;
            this.lbl숫자_사용자정의_1.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_1.TabIndex = 8;
            this.lbl숫자_사용자정의_1.Tag = "";
            this.lbl숫자_사용자정의_1.Text = "숫자_사용자정의_1";
            this.lbl숫자_사용자정의_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_1.Visible = false;
            // 
            // cur숫자_사용자정의_1
            // 
            this.cur숫자_사용자정의_1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_1.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_1.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_1.Mask = null;
            this.cur숫자_사용자정의_1.Name = "cur숫자_사용자정의_1";
            this.cur숫자_사용자정의_1.NullString = "0";
            this.cur숫자_사용자정의_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_1.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_1.TabIndex = 15;
            this.cur숫자_사용자정의_1.Tag = "NUM_USERDEF1";
            this.cur숫자_사용자정의_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_1.UseKeyEnter = true;
            this.cur숫자_사용자정의_1.UseKeyF3 = true;
            this.cur숫자_사용자정의_1.Visible = false;
            // 
            // oneGridItem44
            // 
            this.oneGridItem44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem44.Controls.Add(this.bpPanelControl63);
            this.oneGridItem44.Controls.Add(this.bpPanelControl64);
            this.oneGridItem44.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem44.Location = new System.Drawing.Point(0, 253);
            this.oneGridItem44.Name = "oneGridItem44";
            this.oneGridItem44.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem44.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem44.TabIndex = 11;
            // 
            // bpPanelControl63
            // 
            this.bpPanelControl63.Controls.Add(this.lbl숫자_사용자정의_4);
            this.bpPanelControl63.Controls.Add(this.cur숫자_사용자정의_4);
            this.bpPanelControl63.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl63.Name = "bpPanelControl63";
            this.bpPanelControl63.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl63.TabIndex = 3;
            this.bpPanelControl63.Text = "bpPanelControl63";
            // 
            // lbl숫자_사용자정의_4
            // 
            this.lbl숫자_사용자정의_4.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_4.Name = "lbl숫자_사용자정의_4";
            this.lbl숫자_사용자정의_4.Resizeble = true;
            this.lbl숫자_사용자정의_4.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_4.TabIndex = 10;
            this.lbl숫자_사용자정의_4.Tag = "";
            this.lbl숫자_사용자정의_4.Text = "숫자_사용자정의_4";
            this.lbl숫자_사용자정의_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_4.Visible = false;
            // 
            // cur숫자_사용자정의_4
            // 
            this.cur숫자_사용자정의_4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_4.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_4.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_4.Mask = null;
            this.cur숫자_사용자정의_4.Name = "cur숫자_사용자정의_4";
            this.cur숫자_사용자정의_4.NullString = "0";
            this.cur숫자_사용자정의_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_4.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_4.TabIndex = 18;
            this.cur숫자_사용자정의_4.Tag = "NUM_USERDEF4";
            this.cur숫자_사용자정의_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_4.UseKeyEnter = true;
            this.cur숫자_사용자정의_4.UseKeyF3 = true;
            this.cur숫자_사용자정의_4.Visible = false;
            // 
            // bpPanelControl64
            // 
            this.bpPanelControl64.Controls.Add(this.lbl숫자_사용자정의_3);
            this.bpPanelControl64.Controls.Add(this.cur숫자_사용자정의_3);
            this.bpPanelControl64.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl64.Name = "bpPanelControl64";
            this.bpPanelControl64.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl64.TabIndex = 2;
            this.bpPanelControl64.Text = "bpPanelControl64";
            // 
            // lbl숫자_사용자정의_3
            // 
            this.lbl숫자_사용자정의_3.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_3.Name = "lbl숫자_사용자정의_3";
            this.lbl숫자_사용자정의_3.Resizeble = true;
            this.lbl숫자_사용자정의_3.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_3.TabIndex = 9;
            this.lbl숫자_사용자정의_3.Tag = "";
            this.lbl숫자_사용자정의_3.Text = "숫자_사용자정의_3";
            this.lbl숫자_사용자정의_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_3.Visible = false;
            // 
            // cur숫자_사용자정의_3
            // 
            this.cur숫자_사용자정의_3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_3.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_3.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_3.Mask = null;
            this.cur숫자_사용자정의_3.Name = "cur숫자_사용자정의_3";
            this.cur숫자_사용자정의_3.NullString = "0";
            this.cur숫자_사용자정의_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_3.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_3.TabIndex = 17;
            this.cur숫자_사용자정의_3.Tag = "NUM_USERDEF3";
            this.cur숫자_사용자정의_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_3.UseKeyEnter = true;
            this.cur숫자_사용자정의_3.UseKeyF3 = true;
            this.cur숫자_사용자정의_3.Visible = false;
            // 
            // oneGridItem45
            // 
            this.oneGridItem45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem45.Controls.Add(this.bpPanelControl65);
            this.oneGridItem45.Controls.Add(this.bpPanelControl66);
            this.oneGridItem45.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem45.Location = new System.Drawing.Point(0, 276);
            this.oneGridItem45.Name = "oneGridItem45";
            this.oneGridItem45.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem45.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem45.TabIndex = 12;
            // 
            // bpPanelControl65
            // 
            this.bpPanelControl65.Controls.Add(this.cur숫자_사용자정의_6);
            this.bpPanelControl65.Controls.Add(this.lbl숫자_사용자정의_6);
            this.bpPanelControl65.Location = new System.Drawing.Point(218, 1);
            this.bpPanelControl65.Name = "bpPanelControl65";
            this.bpPanelControl65.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl65.TabIndex = 3;
            this.bpPanelControl65.Text = "bpPanelControl65";
            // 
            // cur숫자_사용자정의_6
            // 
            this.cur숫자_사용자정의_6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_6.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_6.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_6.Mask = null;
            this.cur숫자_사용자정의_6.Name = "cur숫자_사용자정의_6";
            this.cur숫자_사용자정의_6.NullString = "0";
            this.cur숫자_사용자정의_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_6.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_6.TabIndex = 20;
            this.cur숫자_사용자정의_6.Tag = "NUM_USERDEF6";
            this.cur숫자_사용자정의_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_6.UseKeyEnter = true;
            this.cur숫자_사용자정의_6.UseKeyF3 = true;
            this.cur숫자_사용자정의_6.Visible = false;
            // 
            // lbl숫자_사용자정의_6
            // 
            this.lbl숫자_사용자정의_6.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_6.Name = "lbl숫자_사용자정의_6";
            this.lbl숫자_사용자정의_6.Resizeble = true;
            this.lbl숫자_사용자정의_6.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_6.TabIndex = 11;
            this.lbl숫자_사용자정의_6.Tag = "";
            this.lbl숫자_사용자정의_6.Text = "숫자_사용자정의_6";
            this.lbl숫자_사용자정의_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_6.Visible = false;
            // 
            // bpPanelControl66
            // 
            this.bpPanelControl66.Controls.Add(this.lbl숫자_사용자정의_5);
            this.bpPanelControl66.Controls.Add(this.cur숫자_사용자정의_5);
            this.bpPanelControl66.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl66.Name = "bpPanelControl66";
            this.bpPanelControl66.Size = new System.Drawing.Size(214, 23);
            this.bpPanelControl66.TabIndex = 2;
            this.bpPanelControl66.Text = "bpPanelControl66";
            // 
            // lbl숫자_사용자정의_5
            // 
            this.lbl숫자_사용자정의_5.Location = new System.Drawing.Point(0, 2);
            this.lbl숫자_사용자정의_5.Name = "lbl숫자_사용자정의_5";
            this.lbl숫자_사용자정의_5.Resizeble = true;
            this.lbl숫자_사용자정의_5.Size = new System.Drawing.Size(80, 16);
            this.lbl숫자_사용자정의_5.TabIndex = 11;
            this.lbl숫자_사용자정의_5.Tag = "";
            this.lbl숫자_사용자정의_5.Text = "숫자_사용자정의_5";
            this.lbl숫자_사용자정의_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl숫자_사용자정의_5.Visible = false;
            // 
            // cur숫자_사용자정의_5
            // 
            this.cur숫자_사용자정의_5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur숫자_사용자정의_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur숫자_사용자정의_5.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur숫자_사용자정의_5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur숫자_사용자정의_5.Location = new System.Drawing.Point(81, 1);
            this.cur숫자_사용자정의_5.Mask = null;
            this.cur숫자_사용자정의_5.Name = "cur숫자_사용자정의_5";
            this.cur숫자_사용자정의_5.NullString = "0";
            this.cur숫자_사용자정의_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur숫자_사용자정의_5.Size = new System.Drawing.Size(130, 21);
            this.cur숫자_사용자정의_5.TabIndex = 19;
            this.cur숫자_사용자정의_5.Tag = "NUM_USERDEF5";
            this.cur숫자_사용자정의_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur숫자_사용자정의_5.UseKeyEnter = true;
            this.cur숫자_사용자정의_5.UseKeyF3 = true;
            this.cur숫자_사용자정의_5.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flexH);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tab);
            this.splitContainer2.Size = new System.Drawing.Size(821, 404);
            this.splitContainer2.SplitterDistance = 347;
            this.splitContainer2.TabIndex = 8;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(347, 404);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 1;
            this._flexH.UseGridCalculator = true;
            // 
            // panelExt21
            // 
            this.panelExt21.Controls.Add(this.btn엑셀업로드);
            this.panelExt21.Controls.Add(this.panelExt4);
            this.panelExt21.Controls.Add(this.btn할인율적용);
            this.panelExt21.Controls.Add(this.cur할인율);
            this.panelExt21.Controls.Add(this.btn추가);
            this.panelExt21.Controls.Add(this.btn삭제);
            this.panelExt21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt21.Location = new System.Drawing.Point(3, 0);
            this.panelExt21.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panelExt21.Name = "panelExt21";
            this.panelExt21.Size = new System.Drawing.Size(815, 29);
            this.panelExt21.TabIndex = 157;
            // 
            // btn엑셀업로드
            // 
            this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
            this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn엑셀업로드.Location = new System.Drawing.Point(593, 5);
            this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn엑셀업로드.Name = "btn엑셀업로드";
            this.btn엑셀업로드.Size = new System.Drawing.Size(83, 19);
            this.btn엑셀업로드.TabIndex = 152;
            this.btn엑셀업로드.TabStop = false;
            this.btn엑셀업로드.Text = "엑셀업로드";
            this.btn엑셀업로드.UseVisualStyleBackColor = false;
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt4.Controls.Add(this.labelExt3);
            this.panelExt4.Controls.Add(this.lbl할인율);
            this.panelExt4.Location = new System.Drawing.Point(375, 2);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(80, 24);
            this.panelExt4.TabIndex = 151;
            // 
            // labelExt3
            // 
            this.labelExt3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt3.Location = new System.Drawing.Point(2, 182);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(90, 18);
            this.labelExt3.TabIndex = 70;
            this.labelExt3.Tag = "STND_ITEM";
            this.labelExt3.Text = "거래처";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl할인율
            // 
            this.lbl할인율.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl할인율.Location = new System.Drawing.Point(1, 4);
            this.lbl할인율.Name = "lbl할인율";
            this.lbl할인율.Resizeble = true;
            this.lbl할인율.Size = new System.Drawing.Size(75, 18);
            this.lbl할인율.TabIndex = 175;
            this.lbl할인율.Tag = "";
            this.lbl할인율.Text = "할인율";
            this.lbl할인율.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn할인율적용
            // 
            this.btn할인율적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn할인율적용.BackColor = System.Drawing.Color.White;
            this.btn할인율적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn할인율적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn할인율적용.Location = new System.Drawing.Point(525, 5);
            this.btn할인율적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn할인율적용.Name = "btn할인율적용";
            this.btn할인율적용.Size = new System.Drawing.Size(62, 19);
            this.btn할인율적용.TabIndex = 150;
            this.btn할인율적용.TabStop = false;
            this.btn할인율적용.Text = "적용";
            this.btn할인율적용.UseVisualStyleBackColor = false;
            // 
            // cur할인율
            // 
            this.cur할인율.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cur할인율.BackColor = System.Drawing.Color.White;
            this.cur할인율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur할인율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur할인율.CurrencyDecimalDigits = 2;
            this.cur할인율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur할인율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur할인율.Location = new System.Drawing.Point(458, 5);
            this.cur할인율.Mask = null;
            this.cur할인율.Name = "cur할인율";
            this.cur할인율.NullString = "0";
            this.cur할인율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur할인율.Size = new System.Drawing.Size(61, 21);
            this.cur할인율.TabIndex = 149;
            this.cur할인율.Tag = "NO_HST";
            this.cur할인율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur할인율.UseKeyEnter = true;
            this.cur할인율.UseKeyF3 = true;
            // 
            // btn추가
            // 
            this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn추가.BackColor = System.Drawing.Color.White;
            this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가.Location = new System.Drawing.Point(682, 4);
            this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가.Name = "btn추가";
            this.btn추가.Size = new System.Drawing.Size(62, 19);
            this.btn추가.TabIndex = 148;
            this.btn추가.TabStop = false;
            this.btn추가.Text = "추가";
            this.btn추가.UseVisualStyleBackColor = false;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(750, 4);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(62, 19);
            this.btn삭제.TabIndex = 147;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 62);
            this.oneGrid1.TabIndex = 6;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.ctx담당자조회);
            this.oneGridItem1.Controls.Add(this.ctx영업그룹조회);
            this.oneGridItem1.Controls.Add(this.ctx거래처조회);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // ctx담당자조회
            // 
            this.ctx담당자조회.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx담당자조회.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx담당자조회.ButtonImage")));
            this.ctx담당자조회.ChildMode = "";
            this.ctx담당자조회.CodeName = "";
            this.ctx담당자조회.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx담당자조회.CodeValue = "";
            this.ctx담당자조회.ComboCheck = true;
            this.ctx담당자조회.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자조회.IsCodeValueToUpper = true;
            this.ctx담당자조회.ItemBackColor = System.Drawing.Color.White;
            this.ctx담당자조회.LabelVisibled = true;
            this.ctx담당자조회.Location = new System.Drawing.Point(540, 1);
            this.ctx담당자조회.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx담당자조회.Name = "ctx담당자조회";
            this.ctx담당자조회.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx담당자조회.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx담당자조회.SearchCode = true;
            this.ctx담당자조회.SelectCount = 0;
            this.ctx담당자조회.SetDefaultValue = true;
            this.ctx담당자조회.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx담당자조회.Size = new System.Drawing.Size(267, 21);
            this.ctx담당자조회.TabIndex = 2;
            this.ctx담당자조회.TabStop = false;
            this.ctx담당자조회.Tag = "CD_BIZAREA;NM_BIZAREA";
            this.ctx담당자조회.Text = "담당자";
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.Controls.Add(this.bpPanelControl1);
            this.oneGridItem2.Controls.Add(this.ctx견적번호);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl상태);
            this.bpPanelControl2.Controls.Add(this.cbo상태);
            this.bpPanelControl2.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 5;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.periodPicker1);
            this.bpPanelControl1.Controls.Add(this.lbl견적일자조회);
            this.bpPanelControl1.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 4;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // periodPicker1
            // 
            this.periodPicker1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.periodPicker1.IsNecessaryCondition = true;
            this.periodPicker1.Location = new System.Drawing.Point(81, 1);
            this.periodPicker1.Mask = "####/##/##";
            this.periodPicker1.MaximumSize = new System.Drawing.Size(185, 21);
            this.periodPicker1.MinimumSize = new System.Drawing.Size(185, 21);
            this.periodPicker1.Name = "periodPicker1";
            this.periodPicker1.Size = new System.Drawing.Size(185, 21);
            this.periodPicker1.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 71);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(821, 505);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this._flexD, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelExt21, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(821, 97);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(3, 32);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(815, 62);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 156;
            this._flexD.UseGridCalculator = true;
            // 
            // btn차수추가
            // 
            this.btn차수추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn차수추가.BackColor = System.Drawing.Color.White;
            this.btn차수추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn차수추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn차수추가.Location = new System.Drawing.Point(313, 3);
            this.btn차수추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn차수추가.Name = "btn차수추가";
            this.btn차수추가.Size = new System.Drawing.Size(64, 19);
            this.btn차수추가.TabIndex = 149;
            this.btn차수추가.TabStop = false;
            this.btn차수추가.Text = "차수추가";
            this.btn차수추가.UseVisualStyleBackColor = false;
            // 
            // btn차수삭제
            // 
            this.btn차수삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn차수삭제.BackColor = System.Drawing.Color.White;
            this.btn차수삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn차수삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn차수삭제.Location = new System.Drawing.Point(243, 3);
            this.btn차수삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn차수삭제.Name = "btn차수삭제";
            this.btn차수삭제.Size = new System.Drawing.Size(64, 19);
            this.btn차수삭제.TabIndex = 150;
            this.btn차수삭제.TabStop = false;
            this.btn차수삭제.Text = "차수삭제";
            this.btn차수삭제.UseVisualStyleBackColor = false;
            // 
            // btn확정취소
            // 
            this.btn확정취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확정취소.BackColor = System.Drawing.Color.White;
            this.btn확정취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확정취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확정취소.Location = new System.Drawing.Point(105, 3);
            this.btn확정취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확정취소.Name = "btn확정취소";
            this.btn확정취소.Size = new System.Drawing.Size(64, 19);
            this.btn확정취소.TabIndex = 152;
            this.btn확정취소.TabStop = false;
            this.btn확정취소.Text = "확정취소";
            this.btn확정취소.UseVisualStyleBackColor = false;
            // 
            // btn확정
            // 
            this.btn확정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확정.BackColor = System.Drawing.Color.White;
            this.btn확정.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확정.Location = new System.Drawing.Point(175, 3);
            this.btn확정.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확정.Name = "btn확정";
            this.btn확정.Size = new System.Drawing.Size(62, 19);
            this.btn확정.TabIndex = 151;
            this.btn확정.TabStop = false;
            this.btn확정.Text = "확정";
            this.btn확정.UseVisualStyleBackColor = false;
            // 
            // btn파일업로드
            // 
            this.btn파일업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn파일업로드.BackColor = System.Drawing.Color.White;
            this.btn파일업로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn파일업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn파일업로드.Location = new System.Drawing.Point(383, 3);
            this.btn파일업로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn파일업로드.Name = "btn파일업로드";
            this.btn파일업로드.Size = new System.Drawing.Size(78, 19);
            this.btn파일업로드.TabIndex = 153;
            this.btn파일업로드.TabStop = false;
            this.btn파일업로드.Text = "파일업로드";
            this.btn파일업로드.UseVisualStyleBackColor = false;
            this.btn파일업로드.Click += new System.EventHandler(this.btn파일업로드_Click);
            // 
            // btn메일전송
            // 
            this.btn메일전송.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn메일전송.BackColor = System.Drawing.Color.White;
            this.btn메일전송.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn메일전송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn메일전송.Location = new System.Drawing.Point(467, 3);
            this.btn메일전송.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn메일전송.Name = "btn메일전송";
            this.btn메일전송.Size = new System.Drawing.Size(64, 19);
            this.btn메일전송.TabIndex = 154;
            this.btn메일전송.TabStop = false;
            this.btn메일전송.Text = "메일전송";
            this.btn메일전송.UseVisualStyleBackColor = false;
            this.btn메일전송.Visible = false;
            this.btn메일전송.Click += new System.EventHandler(this.btn메일전송_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt30, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(430, 332);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panelExt30
            // 
            this.panelExt30.BackColor = System.Drawing.Color.White;
            this.panelExt30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt30.Controls.Add(this.panelExt35);
            this.panelExt30.Controls.Add(this.textBoxExt3);
            this.panelExt30.Controls.Add(this.panelExt40);
            this.panelExt30.Controls.Add(this.textBoxExt4);
            this.panelExt30.Controls.Add(this.textBoxExt5);
            this.panelExt30.Controls.Add(this.panelExt41);
            this.panelExt30.Controls.Add(this.textBoxExt6);
            this.panelExt30.Controls.Add(this.textBoxExt7);
            this.panelExt30.Controls.Add(this.panelExt42);
            this.panelExt30.Controls.Add(this.textBoxExt8);
            this.panelExt30.Controls.Add(this.textBoxExt9);
            this.panelExt30.Controls.Add(this.panelExt43);
            this.panelExt30.Controls.Add(this.panelExt44);
            this.panelExt30.Controls.Add(this.labelExt5);
            this.panelExt30.Controls.Add(this.labelExt6);
            this.panelExt30.Controls.Add(this.labelExt7);
            this.panelExt30.Controls.Add(this.panelExt45);
            this.panelExt30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt30.Location = new System.Drawing.Point(3, 3);
            this.panelExt30.Name = "panelExt30";
            this.panelExt30.Size = new System.Drawing.Size(424, 326);
            this.panelExt30.TabIndex = 1;
            // 
            // panelExt35
            // 
            this.panelExt35.BackColor = System.Drawing.Color.Transparent;
            this.panelExt35.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt35.BackgroundImage")));
            this.panelExt35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt35.Location = new System.Drawing.Point(5, 144);
            this.panelExt35.Name = "panelExt35";
            this.panelExt35.Size = new System.Drawing.Size(535, 1);
            this.panelExt35.TabIndex = 139;
            // 
            // textBoxExt3
            // 
            this.textBoxExt3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt3.Location = new System.Drawing.Point(83, 147);
            this.textBoxExt3.Multiline = true;
            this.textBoxExt3.Name = "textBoxExt3";
            this.textBoxExt3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExt3.SelectedAllEnabled = false;
            this.textBoxExt3.Size = new System.Drawing.Size(366, 174);
            this.textBoxExt3.TabIndex = 175;
            this.textBoxExt3.Tag = "DC_RMK_TEXT";
            this.textBoxExt3.UseKeyEnter = false;
            this.textBoxExt3.UseKeyF3 = false;
            // 
            // panelExt40
            // 
            this.panelExt40.BackColor = System.Drawing.Color.Transparent;
            this.panelExt40.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt40.BackgroundImage")));
            this.panelExt40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt40.Location = new System.Drawing.Point(5, 72);
            this.panelExt40.Name = "panelExt40";
            this.panelExt40.Size = new System.Drawing.Size(535, 1);
            this.panelExt40.TabIndex = 138;
            // 
            // textBoxExt4
            // 
            this.textBoxExt4.BackColor = System.Drawing.Color.White;
            this.textBoxExt4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt4.Location = new System.Drawing.Point(83, 122);
            this.textBoxExt4.MaxLength = 50;
            this.textBoxExt4.Name = "textBoxExt4";
            this.textBoxExt4.SelectedAllEnabled = false;
            this.textBoxExt4.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt4.TabIndex = 136;
            this.textBoxExt4.Tag = "DC_RMK6";
            this.textBoxExt4.UseKeyEnter = true;
            this.textBoxExt4.UseKeyF3 = true;
            // 
            // textBoxExt5
            // 
            this.textBoxExt5.BackColor = System.Drawing.Color.White;
            this.textBoxExt5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt5.Location = new System.Drawing.Point(83, 50);
            this.textBoxExt5.MaxLength = 50;
            this.textBoxExt5.Name = "textBoxExt5";
            this.textBoxExt5.SelectedAllEnabled = false;
            this.textBoxExt5.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt5.TabIndex = 2;
            this.textBoxExt5.Tag = "DC_RMK3";
            this.textBoxExt5.UseKeyEnter = true;
            this.textBoxExt5.UseKeyF3 = true;
            // 
            // panelExt41
            // 
            this.panelExt41.BackColor = System.Drawing.Color.Transparent;
            this.panelExt41.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt41.BackgroundImage")));
            this.panelExt41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt41.Location = new System.Drawing.Point(5, 96);
            this.panelExt41.Name = "panelExt41";
            this.panelExt41.Size = new System.Drawing.Size(535, 1);
            this.panelExt41.TabIndex = 137;
            // 
            // textBoxExt6
            // 
            this.textBoxExt6.BackColor = System.Drawing.Color.White;
            this.textBoxExt6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt6.Location = new System.Drawing.Point(83, 98);
            this.textBoxExt6.MaxLength = 50;
            this.textBoxExt6.Name = "textBoxExt6";
            this.textBoxExt6.SelectedAllEnabled = false;
            this.textBoxExt6.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt6.TabIndex = 135;
            this.textBoxExt6.Tag = "DC_RMK5";
            this.textBoxExt6.UseKeyEnter = true;
            this.textBoxExt6.UseKeyF3 = true;
            // 
            // textBoxExt7
            // 
            this.textBoxExt7.BackColor = System.Drawing.Color.White;
            this.textBoxExt7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt7.Location = new System.Drawing.Point(83, 26);
            this.textBoxExt7.MaxLength = 50;
            this.textBoxExt7.Name = "textBoxExt7";
            this.textBoxExt7.SelectedAllEnabled = false;
            this.textBoxExt7.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt7.TabIndex = 1;
            this.textBoxExt7.Tag = "DC_RMK2";
            this.textBoxExt7.UseKeyEnter = true;
            this.textBoxExt7.UseKeyF3 = true;
            // 
            // panelExt42
            // 
            this.panelExt42.BackColor = System.Drawing.Color.Transparent;
            this.panelExt42.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt42.BackgroundImage")));
            this.panelExt42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt42.Location = new System.Drawing.Point(5, 120);
            this.panelExt42.Name = "panelExt42";
            this.panelExt42.Size = new System.Drawing.Size(535, 1);
            this.panelExt42.TabIndex = 138;
            // 
            // textBoxExt8
            // 
            this.textBoxExt8.BackColor = System.Drawing.Color.White;
            this.textBoxExt8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt8.Location = new System.Drawing.Point(83, 2);
            this.textBoxExt8.MaxLength = 50;
            this.textBoxExt8.Name = "textBoxExt8";
            this.textBoxExt8.SelectedAllEnabled = false;
            this.textBoxExt8.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt8.TabIndex = 0;
            this.textBoxExt8.Tag = "DC_RMK1";
            this.textBoxExt8.UseKeyEnter = true;
            this.textBoxExt8.UseKeyF3 = true;
            // 
            // textBoxExt9
            // 
            this.textBoxExt9.BackColor = System.Drawing.Color.White;
            this.textBoxExt9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxExt9.Location = new System.Drawing.Point(83, 74);
            this.textBoxExt9.MaxLength = 50;
            this.textBoxExt9.Name = "textBoxExt9";
            this.textBoxExt9.SelectedAllEnabled = false;
            this.textBoxExt9.Size = new System.Drawing.Size(366, 21);
            this.textBoxExt9.TabIndex = 134;
            this.textBoxExt9.Tag = "DC_RMK4";
            this.textBoxExt9.UseKeyEnter = true;
            this.textBoxExt9.UseKeyF3 = true;
            // 
            // panelExt43
            // 
            this.panelExt43.BackColor = System.Drawing.Color.Transparent;
            this.panelExt43.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt43.BackgroundImage")));
            this.panelExt43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt43.Location = new System.Drawing.Point(5, 48);
            this.panelExt43.Name = "panelExt43";
            this.panelExt43.Size = new System.Drawing.Size(535, 1);
            this.panelExt43.TabIndex = 133;
            // 
            // panelExt44
            // 
            this.panelExt44.BackColor = System.Drawing.Color.Transparent;
            this.panelExt44.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt44.BackgroundImage")));
            this.panelExt44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt44.Location = new System.Drawing.Point(5, 24);
            this.panelExt44.Name = "panelExt44";
            this.panelExt44.Size = new System.Drawing.Size(535, 1);
            this.panelExt44.TabIndex = 118;
            // 
            // labelExt5
            // 
            this.labelExt5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt5.Location = new System.Drawing.Point(2, 29);
            this.labelExt5.Name = "labelExt5";
            this.labelExt5.Resizeble = true;
            this.labelExt5.Size = new System.Drawing.Size(75, 18);
            this.labelExt5.TabIndex = 67;
            this.labelExt5.Tag = "NM_ITEM";
            this.labelExt5.Text = "비고2";
            this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt6
            // 
            this.labelExt6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt6.Location = new System.Drawing.Point(2, 52);
            this.labelExt6.Name = "labelExt6";
            this.labelExt6.Resizeble = true;
            this.labelExt6.Size = new System.Drawing.Size(75, 18);
            this.labelExt6.TabIndex = 68;
            this.labelExt6.Tag = "EN_ITEM";
            this.labelExt6.Text = "비고3";
            this.labelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt7
            // 
            this.labelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt7.Location = new System.Drawing.Point(2, 5);
            this.labelExt7.Name = "labelExt7";
            this.labelExt7.Resizeble = true;
            this.labelExt7.Size = new System.Drawing.Size(75, 18);
            this.labelExt7.TabIndex = 1;
            this.labelExt7.Tag = "CD_ITEM";
            this.labelExt7.Text = "비고1";
            this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt45
            // 
            this.panelExt45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt45.Controls.Add(this.labelExt9);
            this.panelExt45.Controls.Add(this.labelExt10);
            this.panelExt45.Controls.Add(this.labelExt11);
            this.panelExt45.Controls.Add(this.labelExt13);
            this.panelExt45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panelExt45.Location = new System.Drawing.Point(1, 1);
            this.panelExt45.Name = "panelExt45";
            this.panelExt45.Size = new System.Drawing.Size(80, 322);
            this.panelExt45.TabIndex = 114;
            // 
            // labelExt9
            // 
            this.labelExt9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt9.Location = new System.Drawing.Point(2, 147);
            this.labelExt9.Name = "labelExt9";
            this.labelExt9.Resizeble = true;
            this.labelExt9.Size = new System.Drawing.Size(75, 18);
            this.labelExt9.TabIndex = 72;
            this.labelExt9.Tag = "";
            this.labelExt9.Text = "비고7";
            this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt10
            // 
            this.labelExt10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt10.Location = new System.Drawing.Point(2, 99);
            this.labelExt10.Name = "labelExt10";
            this.labelExt10.Resizeble = true;
            this.labelExt10.Size = new System.Drawing.Size(75, 18);
            this.labelExt10.TabIndex = 70;
            this.labelExt10.Tag = "";
            this.labelExt10.Text = "비고5";
            this.labelExt10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt11
            // 
            this.labelExt11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt11.Location = new System.Drawing.Point(2, 122);
            this.labelExt11.Name = "labelExt11";
            this.labelExt11.Resizeble = true;
            this.labelExt11.Size = new System.Drawing.Size(75, 18);
            this.labelExt11.TabIndex = 71;
            this.labelExt11.Tag = "EN_ITEM";
            this.labelExt11.Text = "비고6";
            this.labelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt13
            // 
            this.labelExt13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelExt13.Location = new System.Drawing.Point(2, 75);
            this.labelExt13.Name = "labelExt13";
            this.labelExt13.Resizeble = true;
            this.labelExt13.Size = new System.Drawing.Size(75, 18);
            this.labelExt13.TabIndex = 69;
            this.labelExt13.Tag = "";
            this.labelExt13.Text = "비고4";
            this.labelExt13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn전자결재
            // 
            this.btn전자결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전자결재.BackColor = System.Drawing.Color.Transparent;
            this.btn전자결재.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전자결재.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전자결재.Location = new System.Drawing.Point(537, 3);
            this.btn전자결재.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전자결재.Name = "btn전자결재";
            this.btn전자결재.Size = new System.Drawing.Size(64, 19);
            this.btn전자결재.TabIndex = 155;
            this.btn전자결재.TabStop = false;
            this.btn전자결재.Text = "전자결재";
            this.btn전자결재.UseVisualStyleBackColor = true;
            this.btn전자결재.Click += new System.EventHandler(this.btn전자결재_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn전자결재);
            this.flowLayoutPanel1.Controls.Add(this.btn메일전송);
            this.flowLayoutPanel1.Controls.Add(this.btn파일업로드);
            this.flowLayoutPanel1.Controls.Add(this.btn차수추가);
            this.flowLayoutPanel1.Controls.Add(this.btn차수삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn확정);
            this.flowLayoutPanel1.Controls.Add(this.btn확정취소);
            this.flowLayoutPanel1.Controls.Add(this.btn복사);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(220, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(604, 25);
            this.flowLayoutPanel1.TabIndex = 156;
            // 
            // btn복사
            // 
            this.btn복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn복사.BackColor = System.Drawing.Color.White;
            this.btn복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn복사.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn복사.Location = new System.Drawing.Point(28, 3);
            this.btn복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn복사.Name = "btn복사";
            this.btn복사.Size = new System.Drawing.Size(71, 19);
            this.btn복사.TabIndex = 157;
            this.btn복사.TabStop = false;
            this.btn복사.Text = "복사";
            this.btn복사.UseVisualStyleBackColor = false;
            // 
            // oneGridItem32
            // 
            this.oneGridItem32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem32.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem32.Location = new System.Drawing.Point(0, 138);
            this.oneGridItem32.Name = "oneGridItem32";
            this.oneGridItem32.Size = new System.Drawing.Size(417, 23);
            this.oneGridItem32.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem32.TabIndex = 6;
            // 
            // P_SA_ESTMT_REG_NEW
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_SA_ESTMT_REG_NEW";
            this.TitleText = "견적등록";
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.mDataArea.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.tpg공통.ResumeLayout(false);
            this.lay공통.ResumeLayout(false);
            this.oneGridItem15.ResumeLayout(false);
            this.bpPanelControl25.ResumeLayout(false);
            this.bpPanelControl24.ResumeLayout(false);
            this.oneGridItem14.ResumeLayout(false);
            this.bpPanelControl23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp확정일자)).EndInit();
            this.bpPanelControl20.ResumeLayout(false);
            this.oneGridItem12.ResumeLayout(false);
            this.bpPanelControl19.ResumeLayout(false);
            this.oneGridItem13.ResumeLayout(false);
            this.bpPanelControl22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp납기일자)).EndInit();
            this.bpPanelControl21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자국내)).EndInit();
            this.oneGridItem10.ResumeLayout(false);
            this.bpPanelControl16.ResumeLayout(false);
            this.bpPanelControl16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).EndInit();
            this.bpPanelControl15.ResumeLayout(false);
            this.oneGridItem11.ResumeLayout(false);
            this.bpPanelControl18.ResumeLayout(false);
            this.bpPanelControl17.ResumeLayout(false);
            this.bpPanelControl17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).EndInit();
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur차수)).EndInit();
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.oneGridItem4.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            this.oneGridItem5.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp견적일자)).EndInit();
            this.oneGridItem6.ResumeLayout(false);
            this.bpPanelControl8.ResumeLayout(false);
            this.bpPanelControl8.PerformLayout();
            this.oneGridItem7.ResumeLayout(false);
            this.bpPanelControl10.ResumeLayout(false);
            this.bpPanelControl9.ResumeLayout(false);
            this.bpPanelControl9.PerformLayout();
            this.oneGridItem8.ResumeLayout(false);
            this.bpPanelControl12.ResumeLayout(false);
            this.bpPanelControl11.ResumeLayout(false);
            this.oneGridItem9.ResumeLayout(false);
            this.bpPanelControl14.ResumeLayout(false);
            this.bpPanelControl13.ResumeLayout(false);
            this.bpPanelControl13.PerformLayout();
            this.tpg해외.ResumeLayout(false);
            this.lay해외.ResumeLayout(false);
            this.oneGridItem16.ResumeLayout(false);
            this.bpPanelControl27.ResumeLayout(false);
            this.bpPanelControl26.ResumeLayout(false);
            this.oneGridItem17.ResumeLayout(false);
            this.bpPanelControl29.ResumeLayout(false);
            this.bpPanelControl29.PerformLayout();
            this.bpPanelControl28.ResumeLayout(false);
            this.bpPanelControl28.PerformLayout();
            this.oneGridItem18.ResumeLayout(false);
            this.bpPanelControl31.ResumeLayout(false);
            this.bpPanelControl31.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur결제일)).EndInit();
            this.bpPanelControl30.ResumeLayout(false);
            this.oneGridItem19.ResumeLayout(false);
            this.bpPanelControl33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자해외)).EndInit();
            this.bpPanelControl32.ResumeLayout(false);
            this.oneGridItem20.ResumeLayout(false);
            this.bpPanelControl35.ResumeLayout(false);
            this.bpPanelControl34.ResumeLayout(false);
            this.oneGridItem21.ResumeLayout(false);
            this.bpPanelControl36.ResumeLayout(false);
            this.bpPanelControl36.PerformLayout();
            this.oneGridItem22.ResumeLayout(false);
            this.bpPanelControl37.ResumeLayout(false);
            this.bpPanelControl37.PerformLayout();
            this.oneGridItem23.ResumeLayout(false);
            this.bpPanelControl38.ResumeLayout(false);
            this.oneGridItem24.ResumeLayout(false);
            this.bpPanelControl39.ResumeLayout(false);
            this.bpPanelControl39.PerformLayout();
            this.oneGridItem25.ResumeLayout(false);
            this.bpPanelControl40.ResumeLayout(false);
            this.tpg비고.ResumeLayout(false);
            this.lay비고.ResumeLayout(false);
            this.pnl비고.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.oneGridItem26.ResumeLayout(false);
            this.bpPanelControl41.ResumeLayout(false);
            this.bpPanelControl41.PerformLayout();
            this.oneGridItem27.ResumeLayout(false);
            this.bpPanelControl42.ResumeLayout(false);
            this.bpPanelControl42.PerformLayout();
            this.oneGridItem28.ResumeLayout(false);
            this.bpPanelControl43.ResumeLayout(false);
            this.bpPanelControl43.PerformLayout();
            this.oneGridItem29.ResumeLayout(false);
            this.bpPanelControl44.ResumeLayout(false);
            this.bpPanelControl44.PerformLayout();
            this.oneGridItem30.ResumeLayout(false);
            this.bpPanelControl45.ResumeLayout(false);
            this.bpPanelControl45.PerformLayout();
            this.oneGridItem31.ResumeLayout(false);
            this.bpPanelControl46.ResumeLayout(false);
            this.bpPanelControl46.PerformLayout();
            this.flexibleRoundedCornerBox1.ResumeLayout(false);
            this.flexibleRoundedCornerBox1.PerformLayout();
            this.tpg기타.ResumeLayout(false);
            this.lay기타.ResumeLayout(false);
            this.oneGridItem33.ResumeLayout(false);
            this.bpPanelControl52.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp날짜_사용자정의_2)).EndInit();
            this.bpPanelControl47.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp날짜_사용자정의_1)).EndInit();
            this.oneGridItem34.ResumeLayout(false);
            this.bpPanelControl48.ResumeLayout(false);
            this.bpPanelControl48.PerformLayout();
            this.oneGridItem35.ResumeLayout(false);
            this.bpPanelControl49.ResumeLayout(false);
            this.bpPanelControl49.PerformLayout();
            this.oneGridItem36.ResumeLayout(false);
            this.bpPanelControl50.ResumeLayout(false);
            this.bpPanelControl50.PerformLayout();
            this.oneGridItem37.ResumeLayout(false);
            this.bpPanelControl51.ResumeLayout(false);
            this.bpPanelControl51.PerformLayout();
            this.oneGridItem38.ResumeLayout(false);
            this.bpPanelControl53.ResumeLayout(false);
            this.bpPanelControl53.PerformLayout();
            this.oneGridItem39.ResumeLayout(false);
            this.bpPanelControl54.ResumeLayout(false);
            this.bpPanelControl54.PerformLayout();
            this.oneGridItem40.ResumeLayout(false);
            this.bpPanelControl56.ResumeLayout(false);
            this.bpPanelControl55.ResumeLayout(false);
            this.oneGridItem41.ResumeLayout(false);
            this.bpPanelControl57.ResumeLayout(false);
            this.bpPanelControl58.ResumeLayout(false);
            this.oneGridItem42.ResumeLayout(false);
            this.bpPanelControl59.ResumeLayout(false);
            this.bpPanelControl60.ResumeLayout(false);
            this.oneGridItem43.ResumeLayout(false);
            this.bpPanelControl61.ResumeLayout(false);
            this.bpPanelControl61.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_2)).EndInit();
            this.bpPanelControl62.ResumeLayout(false);
            this.bpPanelControl62.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_1)).EndInit();
            this.oneGridItem44.ResumeLayout(false);
            this.bpPanelControl63.ResumeLayout(false);
            this.bpPanelControl63.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_4)).EndInit();
            this.bpPanelControl64.ResumeLayout(false);
            this.bpPanelControl64.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_3)).EndInit();
            this.oneGridItem45.ResumeLayout(false);
            this.bpPanelControl65.ResumeLayout(false);
            this.bpPanelControl65.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_6)).EndInit();
            this.bpPanelControl66.ResumeLayout(false);
            this.bpPanelControl66.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur숫자_사용자정의_5)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.panelExt21.ResumeLayout(false);
            this.panelExt21.PerformLayout();
            this.panelExt4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cur할인율)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt30.ResumeLayout(false);
            this.panelExt30.PerformLayout();
            this.panelExt45.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.DropDownComboBox cbo상태;
        private Duzon.Common.BpControls.BpCodeTextBox ctx견적번호;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹조회;
        private Duzon.Common.BpControls.BpCodeTextBox ctx거래처조회;
        private Duzon.Common.Controls.LabelExt lbl견적일자조회;
        private Duzon.Common.Controls.LabelExt lbl상태;
        private Dass.FlexGrid.FlexGrid _flexH;
        private System.Windows.Forms.ImageList img;
        private Duzon.Common.Controls.TabControlExt tab;
        private System.Windows.Forms.TabPage tpg공통;
        private System.Windows.Forms.TabPage tpg해외;
        private Duzon.Common.Controls.TextBoxExt txt선적항;
        private Duzon.Common.Controls.LabelExt lbl인도조건;
        private Duzon.Common.Controls.LabelExt lbl결제형태;
        private Duzon.Common.Controls.LabelExt lbl수출자;
        private Duzon.Common.Controls.LabelExt lbl포장형태;
        private Duzon.Common.Controls.LabelExt lbl목적지;
        private Duzon.Common.Controls.LabelExt lbl운송방법;
        private Duzon.Common.Controls.LabelExt lbl선적항;
        private Duzon.Common.Controls.LabelExt lbl제조자;
        private Duzon.Common.Controls.LabelExt lbl운송형태;
        private Duzon.Common.Controls.LabelExt lbl검사기관;
        private Duzon.Common.Controls.LabelExt lbl결제일;
        private Duzon.Common.Controls.DropDownComboBox cbo결제형태;
        private Duzon.Common.Controls.TextBoxExt txt검사기관;
        private Duzon.Common.BpControls.BpCodeTextBox ctx제조자;
        private Duzon.Common.BpControls.BpCodeTextBox ctx수출자;
        private Duzon.Common.Controls.LabelExt lbl도착항;
        private Duzon.Common.Controls.LabelExt lbl유효일자해외;
        private Duzon.Common.Controls.TextBoxExt txt목적지;
        private Duzon.Common.Controls.TextBoxExt txt도착항;
        private Duzon.Common.Controls.DropDownComboBox cbo운송형태;
        private Duzon.Common.Controls.DropDownComboBox cbo운송방법;
        private Duzon.Common.Controls.DropDownComboBox cbo포장형태;
        private Duzon.Common.Controls.DatePicker dtp유효일자해외;
        private Duzon.Common.Controls.LabelExt lbl원산지;
        private System.Windows.Forms.TableLayoutPanel lay해외;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Duzon.Common.Controls.PanelExt panelExt21;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Dass.FlexGrid.FlexGrid _flexD;
        private System.Windows.Forms.TabPage tpg비고;
        private Duzon.Common.Controls.PanelExt pnl비고;
        private Duzon.Common.Controls.TextBoxExt txt비고3;
        private Duzon.Common.Controls.TextBoxExt txt비고2;
        private Duzon.Common.Controls.TextBoxExt txt비고1;
        private Duzon.Common.Controls.LabelExt lbl비고2;
        private Duzon.Common.Controls.LabelExt lbl비고3;
        private Duzon.Common.Controls.LabelExt lbl비고1;
        private System.Windows.Forms.TableLayoutPanel lay비고;
        private Duzon.Common.Controls.TextBoxExt textBoxExt2;
        private Duzon.Common.Controls.CurrencyTextBox cur결제일;
        private Duzon.Common.Controls.RoundedButton btn차수추가;
        private Duzon.Common.Controls.RoundedButton btn차수삭제;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Duzon.Common.Controls.RoundedButton btn확정취소;
        private Duzon.Common.Controls.RoundedButton btn확정;
        private Duzon.Common.Controls.DropDownComboBox cbo원산지;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.Controls.LabelExt lbl할인율;
        private Duzon.Common.Controls.RoundedButton btn할인율적용;
        private Duzon.Common.Controls.CurrencyTextBox cur할인율;
        private Duzon.Common.Controls.RoundedButton btn파일업로드;
        private Duzon.Common.Controls.TextBoxExt txt비고6;
        private Duzon.Common.Controls.TextBoxExt txt비고5;
        private Duzon.Common.Controls.TextBoxExt txt비고4;
        private Duzon.Common.Controls.LabelExt lbl비고5;
        private Duzon.Common.Controls.LabelExt lbl비고6;
        private Duzon.Common.Controls.LabelExt lbl비고4;
        private Duzon.Common.Controls.TextBoxExt txt멀티비고;
        private Duzon.Common.Controls.LabelExt lbl비고7;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Common.Controls.DropDownComboBox cbo가격조건;
        private Duzon.Common.Controls.RoundedButton btn메일전송;
        private System.Windows.Forms.TabPage tpg기타;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt30;
        private Duzon.Common.Controls.PanelExt panelExt35;
        private Duzon.Common.Controls.TextBoxExt textBoxExt3;
        private Duzon.Common.Controls.PanelExt panelExt40;
        private Duzon.Common.Controls.TextBoxExt textBoxExt4;
        private Duzon.Common.Controls.TextBoxExt textBoxExt5;
        private Duzon.Common.Controls.PanelExt panelExt41;
        private Duzon.Common.Controls.TextBoxExt textBoxExt6;
        private Duzon.Common.Controls.TextBoxExt textBoxExt7;
        private Duzon.Common.Controls.PanelExt panelExt42;
        private Duzon.Common.Controls.TextBoxExt textBoxExt8;
        private Duzon.Common.Controls.TextBoxExt textBoxExt9;
        private Duzon.Common.Controls.PanelExt panelExt43;
        private Duzon.Common.Controls.PanelExt panelExt44;
        private Duzon.Common.Controls.LabelExt labelExt5;
        private Duzon.Common.Controls.LabelExt labelExt6;
        private Duzon.Common.Controls.LabelExt labelExt7;
        private Duzon.Common.Controls.PanelExt panelExt45;
        private Duzon.Common.Controls.LabelExt labelExt9;
        private Duzon.Common.Controls.LabelExt labelExt10;
        private Duzon.Common.Controls.LabelExt labelExt11;
        private Duzon.Common.Controls.LabelExt labelExt13;
        private System.Windows.Forms.TableLayoutPanel lay기타;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_3;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_1;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_5;
        private Duzon.Common.Controls.LabelExt lbl날짜_사용자정의_1;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_1;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_2;
        private Duzon.Common.Controls.LabelExt lbl날짜_사용자정의_2;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_3;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_4;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_5;
        private Duzon.Common.Controls.LabelExt lbl텍스트_사용자정의_6;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_1;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_3;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_5;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_1;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_3;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_4;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_2;
        private Duzon.Common.Controls.TextBoxExt txt텍스트_사용자정의_6;
        private Duzon.Common.Controls.DatePicker dtp날짜_사용자정의_2;
        private Duzon.Common.Controls.DatePicker dtp날짜_사용자정의_1;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_4;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_3;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_2;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_1;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_6;
        private Duzon.Common.Controls.DropDownComboBox cbo코드_사용자정의_5;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_4;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_2;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_3;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_1;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_5;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_5;
        private Duzon.Common.Controls.CurrencyTextBox cur숫자_사용자정의_6;
        private Duzon.Common.Controls.RoundedButton btn전자결재;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel lay공통;
        private Duzon.Common.Controls.DropDownComboBox cbo확정여부;
        private Duzon.Common.Controls.DatePicker dtp확정일자;
        private Duzon.Common.Controls.LabelExt lbl확정여부;
        private Duzon.Common.Controls.LabelExt lbl확정일자;
        private Duzon.Common.Controls.DatePicker dtp납기일자;
        private Duzon.Common.Controls.DatePicker dtp유효일자국내;
        private Duzon.Common.Controls.LabelExt lbl납기일자;
        private Duzon.Common.Controls.LabelExt lbl결제조건;
        private Duzon.Common.Controls.LabelExt lbl유효일자국내;
        private Duzon.Common.Controls.DropDownComboBox cbo결제방법;
        private Duzon.Common.BpControls.BpCodeTextBox ctx프로젝트;
        private Duzon.Common.Controls.TextBoxExt txt견적요청자;
        private Duzon.Common.Controls.TextBoxExt textBoxExt1;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Common.BpControls.BpCodeTextBox ctx사업장;
        private Duzon.Common.Controls.TextBoxExt txt계약번호;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox ctx거래처;
        private Duzon.Common.Controls.DatePicker dtp견적일자;
        private Duzon.Common.Controls.CurrencyTextBox cur차수;
        private Duzon.Common.Controls.LabelExt lbl차수;
        private Duzon.Common.Controls.TextBoxExt txt견적번호;
        private Duzon.Common.Controls.LabelExt lbl견적명;
        private Duzon.Common.Controls.TextBoxExt txt견적명;
        private Duzon.Common.Controls.LabelExt lbl견적일자;
        private Duzon.Common.Controls.LabelExt lbl견적번호;
        private Duzon.Common.Controls.LabelExt lbl견적요청자;
        private Duzon.Common.Controls.LabelExt lbl영업그룹;
        private Duzon.Common.Controls.LabelExt lbl계약번호;
        private Duzon.Common.Controls.LabelExt lbl임시거래처;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.LabelExt lbl담당자;
        private Duzon.Common.Controls.CurrencyTextBox cur환율;
        private Duzon.Common.Controls.DropDownComboBox cbo환종;
        private Duzon.Common.Controls.DropDownComboBox cbo부가세포함;
        private Duzon.Common.Controls.CurrencyTextBox cur부가세율;
        private Duzon.Common.BpControls.BpCodeTextBox ctx수주형태;
        private Duzon.Common.Controls.DropDownComboBox cbo과세구분;
        private Duzon.Common.Controls.LabelExt lbl수주형태;
        private Duzon.Common.Controls.LabelExt lbl과세구분;
        private Duzon.Common.Controls.LabelExt lbl부가세포함;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DropDownComboBox cbo기준환종;
        private Duzon.Common.Controls.LabelExt lbl할인구분;
        private Duzon.Common.Controls.DropDownComboBox cbo할인구분;
        private Duzon.Common.Controls.LabelExt lbl기준환종;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker periodPicker1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl공통1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem4;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem5;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem6;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem7;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem8;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl10;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl11;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl14;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl13;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl공통2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem10;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem11;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl16;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl15;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl18;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl17;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl공통3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem12;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem13;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl19;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl22;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl21;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl공통4;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem14;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl23;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl20;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl공통5;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem15;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl25;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl24;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl해외;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem16;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem17;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem18;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem19;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem20;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem21;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem22;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem23;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem24;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem25;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl27;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl26;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl29;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl28;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl31;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl30;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl33;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl32;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl34;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl35;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl36;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl37;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl38;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl39;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl40;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem26;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem27;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem28;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem29;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem30;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem31;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl41;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl42;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl43;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl44;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl45;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl46;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem32;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl기타;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem33;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem34;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem35;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem36;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem37;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem38;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem39;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem40;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem41;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem42;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem43;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem44;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem45;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl47;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl48;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl49;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl50;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl51;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl52;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl53;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl54;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_6;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_4;
        private Duzon.Common.Controls.LabelExt lbl숫자_사용자정의_2;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_6;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_4;
        private Duzon.Common.Controls.LabelExt lbl코드_사용자정의_2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl56;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl55;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl57;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl58;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl59;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl60;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl61;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl62;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl63;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl64;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl65;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl66;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Duzon.Common.Controls.FlexibleRoundedCornerBox flexibleRoundedCornerBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Duzon.Common.Controls.RoundedButton btn복사;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
        private System.Windows.Forms.OpenFileDialog m_FileDlg;
    }
}
