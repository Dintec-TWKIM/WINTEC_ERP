//********************************************************************
// ��   ��   �� : ���̿�
// ��   ��   �� : 2002-07-31
// ��   ��   �� : ����
// �� ��  �� �� : ��������
// ����ý��۸� : ����
// �� �� ��  �� : ��� ���
// ������Ʈ  �� : P_TR_TO_IN
//********************************************************************
//using System;
//using System.Data;
//using System.Drawing;
//using System.Threading;
//using System.Collections;
//using System.Windows.Forms;
//using System.ComponentModel;

//using Duzon.Common.Util;
//using Duzon.Common.Forms;
//using Duzon.Common.Controls;
//using Duzon.Common.Forms.Help;


//using System.Diagnostics;
//namespace trade
//{
//     <summary>
//     P_TR_TO_IN�� ���� ��� �����Դϴ�.
//     </summary>
//    public class P_TR_TO_IN_OLD : Duzon.Common.Forms.PageBase
//    {
//        private Duzon.Common.Controls.PanelExt panel_main;
//        private Duzon.Common.Controls.LabelExt m_lblToDate;
//        private Duzon.Common.Controls.LabelExt m_lblAmtLicense;
//        private Duzon.Common.Controls.LabelExt m_lblRemark;
//        private Duzon.Common.Controls.LabelExt m_lblNoInspect;
//        private Duzon.Common.Controls.LabelExt m_lblKorAmt;
//        private Duzon.Common.Controls.LabelExt m_lblNoMedical;
//        private Duzon.Common.Controls.LabelExt m_lblCondPrice;
//        private Duzon.Common.Controls.LabelExt m_lblNoTo;
//        private Duzon.Common.Controls.LabelExt m_lblCondCustom;
//        private Duzon.Common.Controls.LabelExt m_lblDtInspect;
//        private Duzon.Common.Controls.LabelExt m_lblRateLicense;
//        private Duzon.Common.Controls.LabelExt m_lblNoBl;
//        private Duzon.Common.Controls.LabelExt m_lblFgTrans;
//        private Duzon.Common.Controls.LabelExt m_lblCdCurrency;
//        private Duzon.Common.Controls.LabelExt m_lblImCost;
//        private Duzon.Common.Controls.LabelExt m_lblOpenBank;
//        private Duzon.Common.Controls.LabelExt m_lblNmEmp;
//        private Duzon.Common.Controls.LabelExt m_lblCdTrans;
//        private Duzon.Common.Controls.LabelExt m_lblGroupRcv;
//        private Duzon.Common.Controls.LabelExt m_lblAmtEx;
//        private Duzon.Common.Controls.LabelExt m_lblStDistribut;
//        private Duzon.Common.Controls.TextBoxExt m_txtNoTo;
//        private Duzon.Common.Controls.TextBoxExt m_txtNoBl;
//        private Duzon.Common.Controls.TextBoxExt m_txtStDistribut;
//        private Duzon.Common.Controls.TextBoxExt m_txtRemark;
//        private Duzon.Common.Controls.TextBoxExt m_txtNoMedical;
//        private Duzon.Common.Controls.TextBoxExt m_txtNoInspect;
//        private Duzon.Common.Controls.CurrencyTextBox m_txtAmtEx;
//        private Duzon.Common.Controls.CurrencyTextBox m_txtAmtLicense;
//        private Duzon.Common.Controls.CurrencyTextBox m_txtKorAmt;
//        private Duzon.Common.Controls.CurrencyTextBox m_txtRateLicense;
//        private Duzon.Common.Controls.CurrencyTextBox m_txtImCost;
//        private Duzon.Common.Controls.DropDownComboBox m_comCdCurrency;
//        private Duzon.Common.Controls.DropDownComboBox m_comFgTrans;
//        private Duzon.Common.Controls.DropDownComboBox m_comCondPrice;
//        private Duzon.Common.Controls.ButtonExt m_btnNoBl;
//        private Duzon.Common.Controls.RoundedButton m_btnImportCost;
//        private Duzon.Common.Controls.RoundedButton m_btnConfirmItem;
//        private Duzon.Common.Controls.LabelExt m_lblDtMedical;
//        private System.ComponentModel.IContainer components;
//        private Duzon.Common.Controls.PanelExt panel4;
//        private Duzon.Common.Controls.PanelExt panel5;
//        private Duzon.Common.Controls.PanelExt panel6;
//        private Duzon.Common.Controls.PanelExt panel7;
//        private Duzon.Common.Controls.PanelExt panel15;
//        private Duzon.Common.Controls.PanelExt panel16;
//        private Duzon.Common.Controls.PanelExt panel17;
//        private Duzon.Common.Controls.PanelExt panel18;
//        private Duzon.Common.Controls.PanelExt panel9;
//        private Duzon.Common.Controls.PanelExt panel10;
//        private Duzon.Common.Controls.PanelExt panel12;
//        private Duzon.Common.Controls.PanelExt panel13;
//        private Duzon.Common.Controls.PanelExt panel14;
//        private Duzon.Common.Controls.PanelExt panel19;

//        private BindingManagerBase m_Manager;
//        private CurrencyManager m_CurrencyManager;

//        private DataTable m_dtTOHead;

//        private string m_CdTrans = null, m_NmTrans = null;		//�ŷ�ó����
//        private string m_CdCond = null, m_NmCond = null;		//���Ҽ�������
//        private string m_CdEmp = null, m_NmEmp = null;			//����ں���
//        private string m_CdPurGrp = null, m_NmPurGrp = null;	//���ű׷캯��
//        private string m_CdBank = null, m_NmBank = null;		//���ຯ��
//        private string m_CdCC = null, m_NmCC = null;
//        private Duzon.Common.Controls.DatePicker m_txtToDate;
//        private Duzon.Common.Controls.DatePicker m_txtDtInspect;
//        private Duzon.Common.Controls.DatePicker m_txtDtMedical;
//        private Duzon.Common.BpControls.BpCodeTextBox m_txtCdTrans;
//        private Duzon.Common.BpControls.BpCodeTextBox m_txtOpenBank;
//        private Duzon.Common.BpControls.BpCodeTextBox m_txtGroupRcv;
//        private Duzon.Common.BpControls.BpCodeTextBox m_txtNmEmp;
//        private Duzon.Common.BpControls.BpCodeTextBox m_txtCondCustom;			//CC����
//         ����, ���� ���ϴ� ����
//        private string m_DeleteCheck = "N";

//        public P_TR_TO_IN_OLD()
//        {
//             �� ȣ���� Windows.Forms Form �����̳ʿ� �ʿ��մϴ�.
//            InitializeComponent();

//             TODO: InitForm�� ȣ���� ���� �ʱ�ȭ �۾��� �߰��մϴ�.
//        }

//         <summary> 
//         ��� ���� ��� ���ҽ��� �����մϴ�.
//         </summary>
//        protected override void Dispose(bool disposing)
//        {
//            if ( disposing )
//            {
//                if ( components != null )
//                {
//                    components.Dispose();
//                }
//            }
//            base.Dispose( disposing );
//        }

//        #region Component Designer generated code
//         <summary> 
//         �����̳� ������ �ʿ��� �޼����Դϴ�. 
//         �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
//         </summary>
//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( P_TR_TO_IN_OLD ) );
//            this.m_lblToDate = new Duzon.Common.Controls.LabelExt();
//            this.m_lblAmtLicense = new Duzon.Common.Controls.LabelExt();
//            this.m_lblRemark = new Duzon.Common.Controls.LabelExt();
//            this.m_lblNoInspect = new Duzon.Common.Controls.LabelExt();
//            this.m_lblKorAmt = new Duzon.Common.Controls.LabelExt();
//            this.m_lblNoMedical = new Duzon.Common.Controls.LabelExt();
//            this.m_lblCondPrice = new Duzon.Common.Controls.LabelExt();
//            this.m_lblNoTo = new Duzon.Common.Controls.LabelExt();
//            this.m_lblCondCustom = new Duzon.Common.Controls.LabelExt();
//            this.m_lblDtInspect = new Duzon.Common.Controls.LabelExt();
//            this.m_lblRateLicense = new Duzon.Common.Controls.LabelExt();
//            this.m_lblNoBl = new Duzon.Common.Controls.LabelExt();
//            this.m_lblDtMedical = new Duzon.Common.Controls.LabelExt();
//            this.m_lblFgTrans = new Duzon.Common.Controls.LabelExt();
//            this.m_lblCdCurrency = new Duzon.Common.Controls.LabelExt();
//            this.m_lblImCost = new Duzon.Common.Controls.LabelExt();
//            this.m_lblOpenBank = new Duzon.Common.Controls.LabelExt();
//            this.m_lblNmEmp = new Duzon.Common.Controls.LabelExt();
//            this.m_lblCdTrans = new Duzon.Common.Controls.LabelExt();
//            this.m_lblGroupRcv = new Duzon.Common.Controls.LabelExt();
//            this.m_lblAmtEx = new Duzon.Common.Controls.LabelExt();
//            this.m_lblStDistribut = new Duzon.Common.Controls.LabelExt();
//            this.m_txtNoTo = new Duzon.Common.Controls.TextBoxExt();
//            this.m_txtNoBl = new Duzon.Common.Controls.TextBoxExt();
//            this.m_txtStDistribut = new Duzon.Common.Controls.TextBoxExt();
//            this.m_txtRemark = new Duzon.Common.Controls.TextBoxExt();
//            this.m_txtNoMedical = new Duzon.Common.Controls.TextBoxExt();
//            this.m_txtNoInspect = new Duzon.Common.Controls.TextBoxExt();
//            this.m_comCdCurrency = new Duzon.Common.Controls.DropDownComboBox();
//            this.m_comFgTrans = new Duzon.Common.Controls.DropDownComboBox();
//            this.m_comCondPrice = new Duzon.Common.Controls.DropDownComboBox();
//            this.m_btnNoBl = new Duzon.Common.Controls.ButtonExt();
//            this.panel_main = new Duzon.Common.Controls.PanelExt();
//            this.m_txtImCost = new Duzon.Common.Controls.CurrencyTextBox();
//            this.m_txtRateLicense = new Duzon.Common.Controls.CurrencyTextBox();
//            this.m_txtKorAmt = new Duzon.Common.Controls.CurrencyTextBox();
//            this.m_txtAmtLicense = new Duzon.Common.Controls.CurrencyTextBox();
//            this.m_txtAmtEx = new Duzon.Common.Controls.CurrencyTextBox();
//            this.panel13 = new Duzon.Common.Controls.PanelExt();
//            this.panel14 = new Duzon.Common.Controls.PanelExt();
//            this.panel19 = new Duzon.Common.Controls.PanelExt();
//            this.panel12 = new Duzon.Common.Controls.PanelExt();
//            this.panel9 = new Duzon.Common.Controls.PanelExt();
//            this.panel10 = new Duzon.Common.Controls.PanelExt();
//            this.panel15 = new Duzon.Common.Controls.PanelExt();
//            this.panel16 = new Duzon.Common.Controls.PanelExt();
//            this.panel17 = new Duzon.Common.Controls.PanelExt();
//            this.panel18 = new Duzon.Common.Controls.PanelExt();
//            this.panel6 = new Duzon.Common.Controls.PanelExt();
//            this.panel5 = new Duzon.Common.Controls.PanelExt();
//            this.panel4 = new Duzon.Common.Controls.PanelExt();
//            this.panel7 = new Duzon.Common.Controls.PanelExt();
//            this.m_txtToDate = new Duzon.Common.Controls.DatePicker();
//            this.m_txtDtInspect = new Duzon.Common.Controls.DatePicker();
//            this.m_txtDtMedical = new Duzon.Common.Controls.DatePicker();
//            this.m_txtCdTrans = new Duzon.Common.BpControls.BpCodeTextBox();
//            this.m_txtOpenBank = new Duzon.Common.BpControls.BpCodeTextBox();
//            this.m_txtGroupRcv = new Duzon.Common.BpControls.BpCodeTextBox();
//            this.m_txtNmEmp = new Duzon.Common.BpControls.BpCodeTextBox();
//            this.m_txtCondCustom = new Duzon.Common.BpControls.BpCodeTextBox();
//            this.m_btnImportCost = new Duzon.Common.Controls.RoundedButton( this.components );
//            this.m_btnConfirmItem = new Duzon.Common.Controls.RoundedButton( this.components );
//            this.panel_main.SuspendLayout();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtImCost ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtRateLicense ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtKorAmt ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtAmtLicense ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtAmtEx ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtToDate ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtDtInspect ) ).BeginInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtDtMedical ) ).BeginInit();
//            this.SuspendLayout();

//             m_lblToDate

//            this.m_lblToDate.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblToDate.Location = new System.Drawing.Point( 263, 6 );
//            this.m_lblToDate.Name = "m_lblToDate";
//            this.m_lblToDate.Resizeble = true;
//            this.m_lblToDate.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblToDate.TabIndex = 0;
//            this.m_lblToDate.Tag = "TO_DATE";
//            this.m_lblToDate.Text = "�Ű���";
//            this.m_lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblAmtLicense

//            this.m_lblAmtLicense.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblAmtLicense.Location = new System.Drawing.Point( 525, 85 );
//            this.m_lblAmtLicense.Name = "m_lblAmtLicense";
//            this.m_lblAmtLicense.Resizeble = true;
//            this.m_lblAmtLicense.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblAmtLicense.TabIndex = 1;
//            this.m_lblAmtLicense.Tag = "AMT_LICENSE";
//            this.m_lblAmtLicense.Text = "����ݾ�";
//            this.m_lblAmtLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblRemark

//            this.m_lblRemark.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblRemark.Location = new System.Drawing.Point( 3, 188 );
//            this.m_lblRemark.Name = "m_lblRemark";
//            this.m_lblRemark.Resizeble = true;
//            this.m_lblRemark.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblRemark.TabIndex = 2;
//            this.m_lblRemark.Tag = "REMARK";
//            this.m_lblRemark.Text = "���";
//            this.m_lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblNoInspect

//            this.m_lblNoInspect.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblNoInspect.Location = new System.Drawing.Point( 3, 136 );
//            this.m_lblNoInspect.Name = "m_lblNoInspect";
//            this.m_lblNoInspect.Resizeble = true;
//            this.m_lblNoInspect.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblNoInspect.TabIndex = 3;
//            this.m_lblNoInspect.Tag = "NO_INSPECT";
//            this.m_lblNoInspect.Text = "�˻�����ȣ";
//            this.m_lblNoInspect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblKorAmt

//            this.m_lblKorAmt.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblKorAmt.Location = new System.Drawing.Point( 525, 110 );
//            this.m_lblKorAmt.Name = "m_lblKorAmt";
//            this.m_lblKorAmt.Resizeble = true;
//            this.m_lblKorAmt.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblKorAmt.TabIndex = 4;
//            this.m_lblKorAmt.Tag = "KOR_AMT";
//            this.m_lblKorAmt.Text = "��ȭ�ݾ�";
//            this.m_lblKorAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblNoMedical

//            this.m_lblNoMedical.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblNoMedical.Location = new System.Drawing.Point( 3, 162 );
//            this.m_lblNoMedical.Name = "m_lblNoMedical";
//            this.m_lblNoMedical.Resizeble = true;
//            this.m_lblNoMedical.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblNoMedical.TabIndex = 5;
//            this.m_lblNoMedical.Tag = "NO_MEDICAL";
//            this.m_lblNoMedical.Text = "�˿�����ȣ";
//            this.m_lblNoMedical.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblCondPrice

//            this.m_lblCondPrice.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblCondPrice.Location = new System.Drawing.Point( 3, 110 );
//            this.m_lblCondPrice.Name = "m_lblCondPrice";
//            this.m_lblCondPrice.Resizeble = true;
//            this.m_lblCondPrice.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblCondPrice.TabIndex = 6;
//            this.m_lblCondPrice.Tag = "COND_PRICE";
//            this.m_lblCondPrice.Text = "��������";
//            this.m_lblCondPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblNoTo

//            this.m_lblNoTo.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblNoTo.Location = new System.Drawing.Point( 3, 6 );
//            this.m_lblNoTo.Name = "m_lblNoTo";
//            this.m_lblNoTo.Resizeble = true;
//            this.m_lblNoTo.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblNoTo.TabIndex = 7;
//            this.m_lblNoTo.Tag = "NO_TO";
//            this.m_lblNoTo.Text = "�Ű� ��ȣ";
//            this.m_lblNoTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblCondCustom

//            this.m_lblCondCustom.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblCondCustom.Location = new System.Drawing.Point( 263, 110 );
//            this.m_lblCondCustom.Name = "m_lblCondCustom";
//            this.m_lblCondCustom.Resizeble = true;
//            this.m_lblCondCustom.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblCondCustom.TabIndex = 9;
//            this.m_lblCondCustom.Tag = "COND_CUSTOM";
//            this.m_lblCondCustom.Text = "���Ҽ���";
//            this.m_lblCondCustom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblDtInspect

//            this.m_lblDtInspect.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblDtInspect.Location = new System.Drawing.Point( 263, 136 );
//            this.m_lblDtInspect.Name = "m_lblDtInspect";
//            this.m_lblDtInspect.Resizeble = true;
//            this.m_lblDtInspect.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblDtInspect.TabIndex = 10;
//            this.m_lblDtInspect.Tag = "DT_INSPECT";
//            this.m_lblDtInspect.Text = "�˻���";
//            this.m_lblDtInspect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblRateLicense

//            this.m_lblRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblRateLicense.Location = new System.Drawing.Point( 263, 85 );
//            this.m_lblRateLicense.Name = "m_lblRateLicense";
//            this.m_lblRateLicense.Resizeble = true;
//            this.m_lblRateLicense.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblRateLicense.TabIndex = 11;
//            this.m_lblRateLicense.Tag = "RATE_LICENSE";
//            this.m_lblRateLicense.Text = "����ȯ��";
//            this.m_lblRateLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblNoBl

//            this.m_lblNoBl.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblNoBl.Location = new System.Drawing.Point( 525, 6 );
//            this.m_lblNoBl.Name = "m_lblNoBl";
//            this.m_lblNoBl.Resizeble = true;
//            this.m_lblNoBl.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblNoBl.TabIndex = 12;
//            this.m_lblNoBl.Tag = "NO_BL";
//            this.m_lblNoBl.Text = "B/L��ȣ";
//            this.m_lblNoBl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblDtMedical

//            this.m_lblDtMedical.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblDtMedical.Location = new System.Drawing.Point( 263, 162 );
//            this.m_lblDtMedical.Name = "m_lblDtMedical";
//            this.m_lblDtMedical.Resizeble = true;
//            this.m_lblDtMedical.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblDtMedical.TabIndex = 13;
//            this.m_lblDtMedical.Tag = "DT_MEDICAL";
//            this.m_lblDtMedical.Text = "�˿���";
//            this.m_lblDtMedical.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblFgTrans

//            this.m_lblFgTrans.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblFgTrans.Location = new System.Drawing.Point( 263, 32 );
//            this.m_lblFgTrans.Name = "m_lblFgTrans";
//            this.m_lblFgTrans.Resizeble = true;
//            this.m_lblFgTrans.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblFgTrans.TabIndex = 14;
//            this.m_lblFgTrans.Tag = "FG_LC";
//            this.m_lblFgTrans.Text = "L/C ����";
//            this.m_lblFgTrans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblCdCurrency

//            this.m_lblCdCurrency.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblCdCurrency.Location = new System.Drawing.Point( 3, 85 );
//            this.m_lblCdCurrency.Name = "m_lblCdCurrency";
//            this.m_lblCdCurrency.Resizeble = true;
//            this.m_lblCdCurrency.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblCdCurrency.TabIndex = 15;
//            this.m_lblCdCurrency.Tag = "CD_CURRENCY";
//            this.m_lblCdCurrency.Text = "��ȭ";
//            this.m_lblCdCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblImCost

//            this.m_lblImCost.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblImCost.Location = new System.Drawing.Point( 263, 214 );
//            this.m_lblImCost.Name = "m_lblImCost";
//            this.m_lblImCost.Resizeble = true;
//            this.m_lblImCost.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblImCost.TabIndex = 16;
//            this.m_lblImCost.Tag = "IM_COST";
//            this.m_lblImCost.Text = "�δ���";
//            this.m_lblImCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblOpenBank

//            this.m_lblOpenBank.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblOpenBank.Location = new System.Drawing.Point( 525, 32 );
//            this.m_lblOpenBank.Name = "m_lblOpenBank";
//            this.m_lblOpenBank.Resizeble = true;
//            this.m_lblOpenBank.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblOpenBank.TabIndex = 17;
//            this.m_lblOpenBank.Tag = "OPEN_BANK";
//            this.m_lblOpenBank.Text = "��������";
//            this.m_lblOpenBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblNmEmp

//            this.m_lblNmEmp.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblNmEmp.Location = new System.Drawing.Point( 263, 58 );
//            this.m_lblNmEmp.Name = "m_lblNmEmp";
//            this.m_lblNmEmp.Resizeble = true;
//            this.m_lblNmEmp.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblNmEmp.TabIndex = 18;
//            this.m_lblNmEmp.Tag = "NM_EMP";
//            this.m_lblNmEmp.Text = "�����";
//            this.m_lblNmEmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblCdTrans

//            this.m_lblCdTrans.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblCdTrans.Location = new System.Drawing.Point( 3, 32 );
//            this.m_lblCdTrans.Name = "m_lblCdTrans";
//            this.m_lblCdTrans.Resizeble = true;
//            this.m_lblCdTrans.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblCdTrans.TabIndex = 19;
//            this.m_lblCdTrans.Tag = "CD_TRANS";
//            this.m_lblCdTrans.Text = "�ŷ�ó";
//            this.m_lblCdTrans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblGroupRcv

//            this.m_lblGroupRcv.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblGroupRcv.Location = new System.Drawing.Point( 3, 58 );
//            this.m_lblGroupRcv.Name = "m_lblGroupRcv";
//            this.m_lblGroupRcv.Resizeble = true;
//            this.m_lblGroupRcv.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblGroupRcv.TabIndex = 21;
//            this.m_lblGroupRcv.Tag = "GROUP_RCV";
//            this.m_lblGroupRcv.Text = "���ű׷�";
//            this.m_lblGroupRcv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblAmtEx

//            this.m_lblAmtEx.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblAmtEx.Location = new System.Drawing.Point( 525, 58 );
//            this.m_lblAmtEx.Name = "m_lblAmtEx";
//            this.m_lblAmtEx.Resizeble = true;
//            this.m_lblAmtEx.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblAmtEx.TabIndex = 22;
//            this.m_lblAmtEx.Tag = "AMT_EX";
//            this.m_lblAmtEx.Text = "��ȭ�ݾ�";
//            this.m_lblAmtEx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_lblStDistribut

//            this.m_lblStDistribut.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.m_lblStDistribut.Location = new System.Drawing.Point( 3, 214 );
//            this.m_lblStDistribut.Name = "m_lblStDistribut";
//            this.m_lblStDistribut.Resizeble = true;
//            this.m_lblStDistribut.Size = new System.Drawing.Size( 80, 18 );
//            this.m_lblStDistribut.TabIndex = 23;
//            this.m_lblStDistribut.Tag = "ST_DISTRIBUT";
//            this.m_lblStDistribut.Text = "�������";
//            this.m_lblStDistribut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

//             m_txtNoTo

//            this.m_txtNoTo.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtNoTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
//            this.m_txtNoTo.ImeMode = System.Windows.Forms.ImeMode.Alpha;
//            this.m_txtNoTo.Location = new System.Drawing.Point( 91, 4 );
//            this.m_txtNoTo.MaxLength = 20;
//            this.m_txtNoTo.Name = "m_txtNoTo";
//            this.m_txtNoTo.SelectedAllEnabled = false;
//            this.m_txtNoTo.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtNoTo.TabIndex = 0;
//            this.m_txtNoTo.UseKeyEnter = true;
//            this.m_txtNoTo.UseKeyF3 = false;

//             m_txtNoBl

//            this.m_txtNoBl.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtNoBl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
//            this.m_txtNoBl.Location = new System.Drawing.Point( 611, 4 );
//            this.m_txtNoBl.MaxLength = 20;
//            this.m_txtNoBl.Name = "m_txtNoBl";
//            this.m_txtNoBl.ReadOnly = true;
//            this.m_txtNoBl.SelectedAllEnabled = false;
//            this.m_txtNoBl.Size = new System.Drawing.Size( 135, 21 );
//            this.m_txtNoBl.TabIndex = 2;
//            this.m_txtNoBl.TabStop = false;
//            this.m_txtNoBl.UseKeyEnter = true;
//            this.m_txtNoBl.UseKeyF3 = false;

//             m_txtStDistribut

//            this.m_txtStDistribut.Location = new System.Drawing.Point( 91, 212 );
//            this.m_txtStDistribut.MaxLength = 10;
//            this.m_txtStDistribut.Name = "m_txtStDistribut";
//            this.m_txtStDistribut.ReadOnly = true;
//            this.m_txtStDistribut.SelectedAllEnabled = false;
//            this.m_txtStDistribut.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtStDistribut.TabIndex = 20;
//            this.m_txtStDistribut.TabStop = false;
//            this.m_txtStDistribut.UseKeyEnter = true;
//            this.m_txtStDistribut.UseKeyF3 = true;
//            this.m_txtStDistribut.TextChanged += new System.EventHandler( this.m_txtStDistribut_TextChanged );

//             m_txtRemark

//            this.m_txtRemark.ImeMode = System.Windows.Forms.ImeMode.Hangul;
//            this.m_txtRemark.Location = new System.Drawing.Point( 91, 186 );
//            this.m_txtRemark.MaxLength = 100;
//            this.m_txtRemark.Name = "m_txtRemark";
//            this.m_txtRemark.SelectedAllEnabled = false;
//            this.m_txtRemark.Size = new System.Drawing.Size( 689, 21 );
//            this.m_txtRemark.TabIndex = 19;
//            this.m_txtRemark.UseKeyEnter = true;
//            this.m_txtRemark.UseKeyF3 = false;

//             m_txtNoMedical

//            this.m_txtNoMedical.Location = new System.Drawing.Point( 91, 160 );
//            this.m_txtNoMedical.MaxLength = 20;
//            this.m_txtNoMedical.Name = "m_txtNoMedical";
//            this.m_txtNoMedical.SelectedAllEnabled = false;
//            this.m_txtNoMedical.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtNoMedical.TabIndex = 17;
//            this.m_txtNoMedical.UseKeyEnter = true;
//            this.m_txtNoMedical.UseKeyF3 = false;

//             m_txtNoInspect

//            this.m_txtNoInspect.Location = new System.Drawing.Point( 91, 134 );
//            this.m_txtNoInspect.MaxLength = 20;
//            this.m_txtNoInspect.Name = "m_txtNoInspect";
//            this.m_txtNoInspect.SelectedAllEnabled = false;
//            this.m_txtNoInspect.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtNoInspect.TabIndex = 15;
//            this.m_txtNoInspect.UseKeyEnter = true;
//            this.m_txtNoInspect.UseKeyF3 = false;

//             m_comCdCurrency

//            this.m_comCdCurrency.AutoDropDown = true;
//            this.m_comCdCurrency.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_comCdCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.m_comCdCurrency.Location = new System.Drawing.Point( 91, 82 );
//            this.m_comCdCurrency.Name = "m_comCdCurrency";
//            this.m_comCdCurrency.ShowCheckBox = false;
//            this.m_comCdCurrency.Size = new System.Drawing.Size( 100, 20 );
//            this.m_comCdCurrency.TabIndex = 9;
//            this.m_comCdCurrency.UseKeyEnter = true;
//            this.m_comCdCurrency.UseKeyF3 = false;
//            this.m_comCdCurrency.SelectionChangeCommitted += new System.EventHandler( this.m_comCdCurrency_SelectionChangeCommitted );

//             m_comFgTrans

//            this.m_comFgTrans.AutoDropDown = true;
//            this.m_comFgTrans.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_comFgTrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.m_comFgTrans.Location = new System.Drawing.Point( 351, 30 );
//            this.m_comFgTrans.Name = "m_comFgTrans";
//            this.m_comFgTrans.ShowCheckBox = false;
//            this.m_comFgTrans.Size = new System.Drawing.Size( 165, 20 );
//            this.m_comFgTrans.TabIndex = 4;
//            this.m_comFgTrans.UseKeyEnter = true;
//            this.m_comFgTrans.UseKeyF3 = false;

//             m_comCondPrice

//            this.m_comCondPrice.AutoDropDown = true;
//            this.m_comCondPrice.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_comCondPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.m_comCondPrice.Location = new System.Drawing.Point( 91, 108 );
//            this.m_comCondPrice.Name = "m_comCondPrice";
//            this.m_comCondPrice.ShowCheckBox = false;
//            this.m_comCondPrice.Size = new System.Drawing.Size( 165, 20 );
//            this.m_comCondPrice.TabIndex = 12;
//            this.m_comCondPrice.UseKeyEnter = true;
//            this.m_comCondPrice.UseKeyF3 = false;

//             m_btnNoBl

//            this.m_btnNoBl.BackColor = System.Drawing.SystemColors.Control;
//            this.m_btnNoBl.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.m_btnNoBl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.m_btnNoBl.Image = ( ( System.Drawing.Image )( resources.GetObject( "m_btnNoBl.Image" ) ) );
//            this.m_btnNoBl.Location = new System.Drawing.Point( 747, 3 );
//            this.m_btnNoBl.Name = "m_btnNoBl";
//            this.m_btnNoBl.Size = new System.Drawing.Size( 30, 21 );
//            this.m_btnNoBl.TabIndex = 48;
//            this.m_btnNoBl.TabStop = false;
//            this.m_btnNoBl.UseVisualStyleBackColor = false;
//            this.m_btnNoBl.Click += new System.EventHandler( this.m_btnNoBl_Click );

//             panel_main

//            this.panel_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.panel_main.Controls.Add( this.m_txtImCost );
//            this.panel_main.Controls.Add( this.m_txtRateLicense );
//            this.panel_main.Controls.Add( this.m_txtKorAmt );
//            this.panel_main.Controls.Add( this.m_txtAmtLicense );
//            this.panel_main.Controls.Add( this.m_txtAmtEx );
//            this.panel_main.Controls.Add( this.panel13 );
//            this.panel_main.Controls.Add( this.panel14 );
//            this.panel_main.Controls.Add( this.panel19 );
//            this.panel_main.Controls.Add( this.panel12 );
//            this.panel_main.Controls.Add( this.panel9 );
//            this.panel_main.Controls.Add( this.panel10 );
//            this.panel_main.Controls.Add( this.panel15 );
//            this.panel_main.Controls.Add( this.panel16 );
//            this.panel_main.Controls.Add( this.panel17 );
//            this.panel_main.Controls.Add( this.panel18 );
//            this.panel_main.Controls.Add( this.m_btnNoBl );
//            this.panel_main.Controls.Add( this.m_txtNoTo );
//            this.panel_main.Controls.Add( this.m_lblNoTo );
//            this.panel_main.Controls.Add( this.m_lblNoBl );
//            this.panel_main.Controls.Add( this.m_txtNoBl );
//            this.panel_main.Controls.Add( this.m_lblToDate );
//            this.panel_main.Controls.Add( this.m_lblCdTrans );
//            this.panel_main.Controls.Add( this.m_lblOpenBank );
//            this.panel_main.Controls.Add( this.m_lblFgTrans );
//            this.panel_main.Controls.Add( this.m_comFgTrans );
//            this.panel_main.Controls.Add( this.m_lblGroupRcv );
//            this.panel_main.Controls.Add( this.m_lblNmEmp );
//            this.panel_main.Controls.Add( this.m_lblAmtEx );
//            this.panel_main.Controls.Add( this.m_lblCdCurrency );
//            this.panel_main.Controls.Add( this.m_comCdCurrency );
//            this.panel_main.Controls.Add( this.m_lblAmtLicense );
//            this.panel_main.Controls.Add( this.m_lblRateLicense );
//            this.panel_main.Controls.Add( this.m_lblCondPrice );
//            this.panel_main.Controls.Add( this.m_lblCondCustom );
//            this.panel_main.Controls.Add( this.m_lblKorAmt );
//            this.panel_main.Controls.Add( this.m_comCondPrice );
//            this.panel_main.Controls.Add( this.m_txtNoInspect );
//            this.panel_main.Controls.Add( this.m_lblDtInspect );
//            this.panel_main.Controls.Add( this.m_lblNoInspect );
//            this.panel_main.Controls.Add( this.m_lblNoMedical );
//            this.panel_main.Controls.Add( this.m_txtNoMedical );
//            this.panel_main.Controls.Add( this.m_lblDtMedical );
//            this.panel_main.Controls.Add( this.m_lblRemark );
//            this.panel_main.Controls.Add( this.m_txtRemark );
//            this.panel_main.Controls.Add( this.m_lblStDistribut );
//            this.panel_main.Controls.Add( this.m_lblImCost );
//            this.panel_main.Controls.Add( this.m_txtStDistribut );
//            this.panel_main.Controls.Add( this.panel6 );
//            this.panel_main.Controls.Add( this.panel5 );
//            this.panel_main.Controls.Add( this.panel4 );
//            this.panel_main.Controls.Add( this.panel7 );
//            this.panel_main.Controls.Add( this.m_txtToDate );
//            this.panel_main.Controls.Add( this.m_txtDtInspect );
//            this.panel_main.Controls.Add( this.m_txtDtMedical );
//            this.panel_main.Controls.Add( this.m_txtCdTrans );
//            this.panel_main.Controls.Add( this.m_txtOpenBank );
//            this.panel_main.Controls.Add( this.m_txtGroupRcv );
//            this.panel_main.Controls.Add( this.m_txtNmEmp );
//            this.panel_main.Controls.Add( this.m_txtCondCustom );
//            this.panel_main.Location = new System.Drawing.Point( 10, 88 );
//            this.panel_main.Name = "panel_main";
//            this.panel_main.Size = new System.Drawing.Size( 785, 237 );
//            this.panel_main.TabIndex = 0;

//             m_txtImCost

//            this.m_txtImCost.BackColor = System.Drawing.SystemColors.Control;
//            this.m_txtImCost.CurrencyDecimalDigits = 4;
//            this.m_txtImCost.DecimalValue = new decimal( new int [] {
//            0,
//            0,
//            0,
//            0} );
//            this.m_txtImCost.ForeColor = System.Drawing.SystemColors.ControlText;
//            this.m_txtImCost.Location = new System.Drawing.Point( 351, 212 );
//            this.m_txtImCost.Mask = null;
//            this.m_txtImCost.MaxLength = 22;
//            this.m_txtImCost.Name = "m_txtImCost";
//            this.m_txtImCost.NullString = "0";
//            this.m_txtImCost.PositiveColor = System.Drawing.Color.Black;
//            this.m_txtImCost.ReadOnly = true;
//            this.m_txtImCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
//            this.m_txtImCost.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtImCost.TabIndex = 21;
//            this.m_txtImCost.TabStop = false;
//            this.m_txtImCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.m_txtImCost.UseKeyEnter = true;
//            this.m_txtImCost.UseKeyF3 = true;

//             m_txtRateLicense

//            this.m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtRateLicense.CurrencyDecimalDigits = 4;
//            this.m_txtRateLicense.DecimalValue = new decimal( new int [] {
//            0,
//            0,
//            0,
//            0} );
//            this.m_txtRateLicense.ForeColor = System.Drawing.SystemColors.ControlText;
//            this.m_txtRateLicense.Location = new System.Drawing.Point( 351, 82 );
//            this.m_txtRateLicense.Mask = null;
//            this.m_txtRateLicense.MaxLength = 20;
//            this.m_txtRateLicense.Name = "m_txtRateLicense";
//            this.m_txtRateLicense.NullString = "0";
//            this.m_txtRateLicense.PositiveColor = System.Drawing.Color.Black;
//            this.m_txtRateLicense.RightToLeft = System.Windows.Forms.RightToLeft.No;
//            this.m_txtRateLicense.Size = new System.Drawing.Size( 167, 21 );
//            this.m_txtRateLicense.TabIndex = 10;
//            this.m_txtRateLicense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.m_txtRateLicense.UseKeyEnter = true;
//            this.m_txtRateLicense.UseKeyF3 = false;

//             m_txtKorAmt

//            this.m_txtKorAmt.BackColor = System.Drawing.SystemColors.Control;
//            this.m_txtKorAmt.CurrencyDecimalDigits = 4;
//            this.m_txtKorAmt.DecimalValue = new decimal( new int [] {
//            0,
//            0,
//            0,
//            0} );
//            this.m_txtKorAmt.ForeColor = System.Drawing.SystemColors.ControlText;
//            this.m_txtKorAmt.Location = new System.Drawing.Point( 611, 108 );
//            this.m_txtKorAmt.Mask = null;
//            this.m_txtKorAmt.MaxLength = 22;
//            this.m_txtKorAmt.Name = "m_txtKorAmt";
//            this.m_txtKorAmt.NullString = "0";
//            this.m_txtKorAmt.PositiveColor = System.Drawing.Color.Black;
//            this.m_txtKorAmt.ReadOnly = true;
//            this.m_txtKorAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
//            this.m_txtKorAmt.Size = new System.Drawing.Size( 167, 21 );
//            this.m_txtKorAmt.TabIndex = 14;
//            this.m_txtKorAmt.TabStop = false;
//            this.m_txtKorAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.m_txtKorAmt.UseKeyEnter = true;
//            this.m_txtKorAmt.UseKeyF3 = false;

//             m_txtAmtLicense

//            this.m_txtAmtLicense.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtAmtLicense.CurrencyDecimalDigits = 4;
//            this.m_txtAmtLicense.DecimalValue = new decimal( new int [] {
//            0,
//            0,
//            0,
//            0} );
//            this.m_txtAmtLicense.ForeColor = System.Drawing.SystemColors.ControlText;
//            this.m_txtAmtLicense.Location = new System.Drawing.Point( 611, 82 );
//            this.m_txtAmtLicense.Mask = null;
//            this.m_txtAmtLicense.MaxLength = 22;
//            this.m_txtAmtLicense.Name = "m_txtAmtLicense";
//            this.m_txtAmtLicense.NullString = "0";
//            this.m_txtAmtLicense.PositiveColor = System.Drawing.Color.Black;
//            this.m_txtAmtLicense.RightToLeft = System.Windows.Forms.RightToLeft.No;
//            this.m_txtAmtLicense.Size = new System.Drawing.Size( 167, 21 );
//            this.m_txtAmtLicense.TabIndex = 11;
//            this.m_txtAmtLicense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.m_txtAmtLicense.UseKeyEnter = true;
//            this.m_txtAmtLicense.UseKeyF3 = false;

//             m_txtAmtEx

//            this.m_txtAmtEx.BackColor = System.Drawing.SystemColors.Control;
//            this.m_txtAmtEx.CurrencyDecimalDigits = 4;
//            this.m_txtAmtEx.DecimalValue = new decimal( new int [] {
//            0,
//            0,
//            0,
//            0} );
//            this.m_txtAmtEx.ForeColor = System.Drawing.SystemColors.ControlText;
//            this.m_txtAmtEx.Location = new System.Drawing.Point( 611, 56 );
//            this.m_txtAmtEx.Mask = null;
//            this.m_txtAmtEx.MaxLength = 22;
//            this.m_txtAmtEx.Name = "m_txtAmtEx";
//            this.m_txtAmtEx.NullString = "0";
//            this.m_txtAmtEx.PositiveColor = System.Drawing.Color.Black;
//            this.m_txtAmtEx.ReadOnly = true;
//            this.m_txtAmtEx.RightToLeft = System.Windows.Forms.RightToLeft.No;
//            this.m_txtAmtEx.Size = new System.Drawing.Size( 167, 21 );
//            this.m_txtAmtEx.TabIndex = 8;
//            this.m_txtAmtEx.TabStop = false;
//            this.m_txtAmtEx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.m_txtAmtEx.UseKeyEnter = true;
//            this.m_txtAmtEx.UseKeyF3 = false;

//             panel13

//            this.panel13.BackColor = System.Drawing.Color.Transparent;
//            this.panel13.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel13.BackgroundImage" ) ) );
//            this.panel13.Location = new System.Drawing.Point( 5, 130 );
//            this.panel13.Name = "panel13";
//            this.panel13.Size = new System.Drawing.Size( 775, 1 );
//            this.panel13.TabIndex = 88;

//             panel14

//            this.panel14.BackColor = System.Drawing.Color.Transparent;
//            this.panel14.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel14.BackgroundImage" ) ) );
//            this.panel14.Location = new System.Drawing.Point( 5, 104 );
//            this.panel14.Name = "panel14";
//            this.panel14.Size = new System.Drawing.Size( 775, 1 );
//            this.panel14.TabIndex = 87;

//             panel19

//            this.panel19.BackColor = System.Drawing.Color.Transparent;
//            this.panel19.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel19.BackgroundImage" ) ) );
//            this.panel19.Location = new System.Drawing.Point( 5, 78 );
//            this.panel19.Name = "panel19";
//            this.panel19.Size = new System.Drawing.Size( 775, 1 );
//            this.panel19.TabIndex = 86;

//             panel12

//            this.panel12.BackColor = System.Drawing.Color.Transparent;
//            this.panel12.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel12.BackgroundImage" ) ) );
//            this.panel12.Location = new System.Drawing.Point( 5, 78 );
//            this.panel12.Name = "panel12";
//            this.panel12.Size = new System.Drawing.Size( 775, 1 );
//            this.panel12.TabIndex = 89;

//             panel9

//            this.panel9.BackColor = System.Drawing.Color.Transparent;
//            this.panel9.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel9.BackgroundImage" ) ) );
//            this.panel9.Location = new System.Drawing.Point( 5, 52 );
//            this.panel9.Name = "panel9";
//            this.panel9.Size = new System.Drawing.Size( 775, 1 );
//            this.panel9.TabIndex = 84;

//             panel10

//            this.panel10.BackColor = System.Drawing.Color.Transparent;
//            this.panel10.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel10.BackgroundImage" ) ) );
//            this.panel10.Location = new System.Drawing.Point( 5, 26 );
//            this.panel10.Name = "panel10";
//            this.panel10.Size = new System.Drawing.Size( 775, 1 );
//            this.panel10.TabIndex = 83;

//             panel15

//            this.panel15.BackColor = System.Drawing.Color.Transparent;
//            this.panel15.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel15.BackgroundImage" ) ) );
//            this.panel15.Location = new System.Drawing.Point( 5, 156 );
//            this.panel15.Name = "panel15";
//            this.panel15.Size = new System.Drawing.Size( 775, 1 );
//            this.panel15.TabIndex = 81;

//             panel16

//            this.panel16.BackColor = System.Drawing.Color.Transparent;
//            this.panel16.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel16.BackgroundImage" ) ) );
//            this.panel16.Location = new System.Drawing.Point( 5, 208 );
//            this.panel16.Name = "panel16";
//            this.panel16.Size = new System.Drawing.Size( 775, 1 );
//            this.panel16.TabIndex = 80;

//             panel17

//            this.panel17.BackColor = System.Drawing.Color.Transparent;
//            this.panel17.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel17.BackgroundImage" ) ) );
//            this.panel17.Location = new System.Drawing.Point( 5, 182 );
//            this.panel17.Name = "panel17";
//            this.panel17.Size = new System.Drawing.Size( 775, 1 );
//            this.panel17.TabIndex = 79;

//             panel18

//            this.panel18.BackColor = System.Drawing.Color.Transparent;
//            this.panel18.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel18.BackgroundImage" ) ) );
//            this.panel18.Location = new System.Drawing.Point( 5, 156 );
//            this.panel18.Name = "panel18";
//            this.panel18.Size = new System.Drawing.Size( 775, 1 );
//            this.panel18.TabIndex = 78;

//             panel6

//            this.panel6.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.panel6.Location = new System.Drawing.Point( 521, 1 );
//            this.panel6.Name = "panel6";
//            this.panel6.Size = new System.Drawing.Size( 85, 130 );
//            this.panel6.TabIndex = 57;

//             panel5

//            this.panel5.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.panel5.Location = new System.Drawing.Point( 261, 1 );
//            this.panel5.Name = "panel5";
//            this.panel5.Size = new System.Drawing.Size( 85, 182 );
//            this.panel5.TabIndex = 56;

//             panel4

//            this.panel4.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.panel4.Location = new System.Drawing.Point( 1, 1 );
//            this.panel4.Name = "panel4";
//            this.panel4.Size = new System.Drawing.Size( 85, 233 );
//            this.panel4.TabIndex = 55;

//             panel7

//            this.panel7.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
//            this.panel7.Location = new System.Drawing.Point( 261, 208 );
//            this.panel7.Name = "panel7";
//            this.panel7.Size = new System.Drawing.Size( 85, 26 );
//            this.panel7.TabIndex = 58;

//             m_txtToDate

//            this.m_txtToDate.CalendarBackColor = System.Drawing.Color.White;
//            this.m_txtToDate.DayColor = System.Drawing.Color.Black;
//            this.m_txtToDate.FriDayColor = System.Drawing.Color.Blue;
//            this.m_txtToDate.Location = new System.Drawing.Point( 351, 3 );
//            this.m_txtToDate.Mask = "####/##/##";
//            this.m_txtToDate.MaskBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtToDate.MaxDate = new System.DateTime( 9999, 12, 31, 23, 59, 59, 999 );
//            this.m_txtToDate.MinDate = new System.DateTime( 1800, 1, 1, 0, 0, 0, 0 );
//            this.m_txtToDate.Modified = false;
//            this.m_txtToDate.Name = "m_txtToDate";
//            this.m_txtToDate.PaddingCharacter = '_';
//            this.m_txtToDate.PassivePromptCharacter = '_';
//            this.m_txtToDate.PromptCharacter = '_';
//            this.m_txtToDate.SelectedDayColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 253 ) ) ) ), ( ( int )( ( ( byte )( 228 ) ) ) ), ( ( int )( ( ( byte )( 153 ) ) ) ) );
//            this.m_txtToDate.ShowToDay = true;
//            this.m_txtToDate.ShowTodayCircle = true;
//            this.m_txtToDate.ShowUpDown = false;
//            this.m_txtToDate.Size = new System.Drawing.Size( 92, 21 );
//            this.m_txtToDate.SunDayColor = System.Drawing.Color.Red;
//            this.m_txtToDate.TabIndex = 1;
//            this.m_txtToDate.TitleBackColor = System.Drawing.SystemColors.Control;
//            this.m_txtToDate.TitleForeColor = System.Drawing.Color.White;
//            this.m_txtToDate.ToDayColor = System.Drawing.Color.Red;
//            this.m_txtToDate.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
//            this.m_txtToDate.UseKeyF3 = false;
//            this.m_txtToDate.Value = new System.DateTime( 2004, 1, 1, 0, 0, 0, 0 );
//            this.m_txtToDate.Validated += new System.EventHandler( this.DataPickerValidated );
//            this.m_txtToDate.DateChanged += new System.EventHandler( this.DateDateChanged );

//             m_txtDtInspect

//            this.m_txtDtInspect.CalendarBackColor = System.Drawing.Color.White;
//            this.m_txtDtInspect.DayColor = System.Drawing.Color.Black;
//            this.m_txtDtInspect.FriDayColor = System.Drawing.Color.Blue;
//            this.m_txtDtInspect.Location = new System.Drawing.Point( 351, 135 );
//            this.m_txtDtInspect.Mask = "####/##/##";
//            this.m_txtDtInspect.MaskBackColor = System.Drawing.SystemColors.Window;
//            this.m_txtDtInspect.MaxDate = new System.DateTime( 9999, 12, 31, 23, 59, 59, 999 );
//            this.m_txtDtInspect.MinDate = new System.DateTime( 1800, 1, 1, 0, 0, 0, 0 );
//            this.m_txtDtInspect.Modified = false;
//            this.m_txtDtInspect.Name = "m_txtDtInspect";
//            this.m_txtDtInspect.PaddingCharacter = '_';
//            this.m_txtDtInspect.PassivePromptCharacter = '_';
//            this.m_txtDtInspect.PromptCharacter = '_';
//            this.m_txtDtInspect.SelectedDayColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 253 ) ) ) ), ( ( int )( ( ( byte )( 228 ) ) ) ), ( ( int )( ( ( byte )( 153 ) ) ) ) );
//            this.m_txtDtInspect.ShowToDay = true;
//            this.m_txtDtInspect.ShowTodayCircle = true;
//            this.m_txtDtInspect.ShowUpDown = false;
//            this.m_txtDtInspect.Size = new System.Drawing.Size( 92, 21 );
//            this.m_txtDtInspect.SunDayColor = System.Drawing.Color.Red;
//            this.m_txtDtInspect.TabIndex = 16;
//            this.m_txtDtInspect.TitleBackColor = System.Drawing.SystemColors.Control;
//            this.m_txtDtInspect.TitleForeColor = System.Drawing.Color.White;
//            this.m_txtDtInspect.ToDayColor = System.Drawing.Color.Red;
//            this.m_txtDtInspect.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
//            this.m_txtDtInspect.UseKeyF3 = false;
//            this.m_txtDtInspect.Value = new System.DateTime( 2004, 1, 1, 0, 0, 0, 0 );
//            this.m_txtDtInspect.Validated += new System.EventHandler( this.DataPickerValidated );
//            this.m_txtDtInspect.DateChanged += new System.EventHandler( this.DateDateChanged );

//             m_txtDtMedical

//            this.m_txtDtMedical.CalendarBackColor = System.Drawing.Color.White;
//            this.m_txtDtMedical.DayColor = System.Drawing.Color.Black;
//            this.m_txtDtMedical.FriDayColor = System.Drawing.Color.Blue;
//            this.m_txtDtMedical.Location = new System.Drawing.Point( 351, 160 );
//            this.m_txtDtMedical.Mask = "####/##/##";
//            this.m_txtDtMedical.MaskBackColor = System.Drawing.SystemColors.Window;
//            this.m_txtDtMedical.MaxDate = new System.DateTime( 9999, 12, 31, 23, 59, 59, 999 );
//            this.m_txtDtMedical.MinDate = new System.DateTime( 1800, 1, 1, 0, 0, 0, 0 );
//            this.m_txtDtMedical.Modified = false;
//            this.m_txtDtMedical.Name = "m_txtDtMedical";
//            this.m_txtDtMedical.PaddingCharacter = '_';
//            this.m_txtDtMedical.PassivePromptCharacter = '_';
//            this.m_txtDtMedical.PromptCharacter = '_';
//            this.m_txtDtMedical.SelectedDayColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 253 ) ) ) ), ( ( int )( ( ( byte )( 228 ) ) ) ), ( ( int )( ( ( byte )( 153 ) ) ) ) );
//            this.m_txtDtMedical.ShowToDay = true;
//            this.m_txtDtMedical.ShowTodayCircle = true;
//            this.m_txtDtMedical.ShowUpDown = false;
//            this.m_txtDtMedical.Size = new System.Drawing.Size( 92, 21 );
//            this.m_txtDtMedical.SunDayColor = System.Drawing.Color.Red;
//            this.m_txtDtMedical.TabIndex = 18;
//            this.m_txtDtMedical.TitleBackColor = System.Drawing.SystemColors.Control;
//            this.m_txtDtMedical.TitleForeColor = System.Drawing.Color.White;
//            this.m_txtDtMedical.ToDayColor = System.Drawing.Color.Red;
//            this.m_txtDtMedical.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
//            this.m_txtDtMedical.UseKeyF3 = false;
//            this.m_txtDtMedical.Value = new System.DateTime( 2004, 1, 1, 0, 0, 0, 0 );
//            this.m_txtDtMedical.Validated += new System.EventHandler( this.DataPickerValidated );
//            this.m_txtDtMedical.DateChanged += new System.EventHandler( this.DateDateChanged );

//             m_txtCdTrans

//            this.m_txtCdTrans.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtCdTrans.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
//            this.m_txtCdTrans.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "m_txtCdTrans.ButtonImage" ) ) );
//            this.m_txtCdTrans.ChildMode = "";
//            this.m_txtCdTrans.CodeName = "";
//            this.m_txtCdTrans.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
//            this.m_txtCdTrans.CodeValue = "";
//            this.m_txtCdTrans.ComboCheck = true;
//            this.m_txtCdTrans.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
//            this.m_txtCdTrans.ItemBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtCdTrans.Location = new System.Drawing.Point( 91, 30 );
//            this.m_txtCdTrans.Name = "m_txtCdTrans";
//            this.m_txtCdTrans.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
//            this.m_txtCdTrans.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
//            this.m_txtCdTrans.SearchCode = true;
//            this.m_txtCdTrans.SelectCount = 0;
//            this.m_txtCdTrans.SetDefaultValue = false;
//            this.m_txtCdTrans.SetNoneTypeMsg = "Please! Set Help Type!";
//            this.m_txtCdTrans.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtCdTrans.TabIndex = 3;
//            this.m_txtCdTrans.TabStop = false;
//            this.m_txtCdTrans.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.OnBpCodeTextBox_QueryAfter );

//             m_txtOpenBank

//            this.m_txtOpenBank.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtOpenBank.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
//            this.m_txtOpenBank.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "m_txtOpenBank.ButtonImage" ) ) );
//            this.m_txtOpenBank.ChildMode = "";
//            this.m_txtOpenBank.CodeName = "";
//            this.m_txtOpenBank.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
//            this.m_txtOpenBank.CodeValue = "";
//            this.m_txtOpenBank.ComboCheck = true;
//            this.m_txtOpenBank.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB;
//            this.m_txtOpenBank.ItemBackColor = System.Drawing.Color.Empty;
//            this.m_txtOpenBank.Location = new System.Drawing.Point( 611, 30 );
//            this.m_txtOpenBank.Name = "m_txtOpenBank";
//            this.m_txtOpenBank.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
//            this.m_txtOpenBank.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
//            this.m_txtOpenBank.SearchCode = true;
//            this.m_txtOpenBank.SelectCount = 0;
//            this.m_txtOpenBank.SetDefaultValue = false;
//            this.m_txtOpenBank.SetNoneTypeMsg = "Please! Set Help Type!";
//            this.m_txtOpenBank.Size = new System.Drawing.Size( 163, 21 );
//            this.m_txtOpenBank.TabIndex = 5;
//            this.m_txtOpenBank.TabStop = false;
//            this.m_txtOpenBank.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.OnBpCodeTextBox_QueryAfter );

//             m_txtGroupRcv

//            this.m_txtGroupRcv.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtGroupRcv.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
//            this.m_txtGroupRcv.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "m_txtGroupRcv.ButtonImage" ) ) );
//            this.m_txtGroupRcv.ChildMode = "";
//            this.m_txtGroupRcv.CodeName = "";
//            this.m_txtGroupRcv.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
//            this.m_txtGroupRcv.CodeValue = "";
//            this.m_txtGroupRcv.ComboCheck = true;
//            this.m_txtGroupRcv.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
//            this.m_txtGroupRcv.ItemBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtGroupRcv.Location = new System.Drawing.Point( 91, 56 );
//            this.m_txtGroupRcv.Name = "m_txtGroupRcv";
//            this.m_txtGroupRcv.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
//            this.m_txtGroupRcv.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
//            this.m_txtGroupRcv.SearchCode = true;
//            this.m_txtGroupRcv.SelectCount = 0;
//            this.m_txtGroupRcv.SetDefaultValue = false;
//            this.m_txtGroupRcv.SetNoneTypeMsg = "Please! Set Help Type!";
//            this.m_txtGroupRcv.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtGroupRcv.TabIndex = 6;
//            this.m_txtGroupRcv.TabStop = false;
//            this.m_txtGroupRcv.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.OnBpCodeTextBox_QueryAfter );

//             m_txtNmEmp

//            this.m_txtNmEmp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
//            this.m_txtNmEmp.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "m_txtNmEmp.ButtonImage" ) ) );
//            this.m_txtNmEmp.ChildMode = "";
//            this.m_txtNmEmp.CodeName = "";
//            this.m_txtNmEmp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
//            this.m_txtNmEmp.CodeValue = "";
//            this.m_txtNmEmp.ComboCheck = true;
//            this.m_txtNmEmp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
//            this.m_txtNmEmp.ItemBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtNmEmp.Location = new System.Drawing.Point( 351, 56 );
//            this.m_txtNmEmp.Name = "m_txtNmEmp";
//            this.m_txtNmEmp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
//            this.m_txtNmEmp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
//            this.m_txtNmEmp.SearchCode = true;
//            this.m_txtNmEmp.SelectCount = 0;
//            this.m_txtNmEmp.SetDefaultValue = false;
//            this.m_txtNmEmp.SetNoneTypeMsg = "Please! Set Help Type!";
//            this.m_txtNmEmp.Size = new System.Drawing.Size( 163, 21 );
//            this.m_txtNmEmp.TabIndex = 7;
//            this.m_txtNmEmp.TabStop = false;
//            this.m_txtNmEmp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.OnBpCodeTextBox_QueryAfter );

//             m_txtCondCustom

//            this.m_txtCondCustom.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
//            this.m_txtCondCustom.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
//            this.m_txtCondCustom.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "m_txtCondCustom.ButtonImage" ) ) );
//            this.m_txtCondCustom.ChildMode = "";
//            this.m_txtCondCustom.CodeName = "";
//            this.m_txtCondCustom.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
//            this.m_txtCondCustom.CodeValue = "";
//            this.m_txtCondCustom.ComboCheck = true;
//            this.m_txtCondCustom.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
//            this.m_txtCondCustom.ItemBackColor = System.Drawing.Color.Empty;
//            this.m_txtCondCustom.Location = new System.Drawing.Point( 351, 108 );
//            this.m_txtCondCustom.Name = "m_txtCondCustom";
//            this.m_txtCondCustom.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
//            this.m_txtCondCustom.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
//            this.m_txtCondCustom.SearchCode = true;
//            this.m_txtCondCustom.SelectCount = 0;
//            this.m_txtCondCustom.SetDefaultValue = false;
//            this.m_txtCondCustom.SetNoneTypeMsg = "Please! Set Help Type!";
//            this.m_txtCondCustom.Size = new System.Drawing.Size( 165, 21 );
//            this.m_txtCondCustom.TabIndex = 13;
//            this.m_txtCondCustom.TabStop = false;
//            this.m_txtCondCustom.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.OnBpCodeTextBox_QueryAfter );

//             m_btnImportCost

//            this.m_btnImportCost.BackColor = System.Drawing.Color.White;
//            this.m_btnImportCost.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
//            this.m_btnImportCost.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
//            this.m_btnImportCost.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.m_btnImportCost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.m_btnImportCost.Location = new System.Drawing.Point( 685, 60 );
//            this.m_btnImportCost.Name = "m_btnImportCost";
//            this.m_btnImportCost.Size = new System.Drawing.Size( 110, 24 );
//            this.m_btnImportCost.TabIndex = 69;
//            this.m_btnImportCost.TabStop = false;
//            this.m_btnImportCost.Tag = "IMPORT_COST";
//            this.m_btnImportCost.Text = "�δ��� ���";
//            this.m_btnImportCost.UseVisualStyleBackColor = false;
//            this.m_btnImportCost.Click += new System.EventHandler( this.m_btnImportCost_Click );

//             m_btnConfirmItem

//            this.m_btnConfirmItem.BackColor = System.Drawing.Color.White;
//            this.m_btnConfirmItem.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
//            this.m_btnConfirmItem.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
//            this.m_btnConfirmItem.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.m_btnConfirmItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.m_btnConfirmItem.Location = new System.Drawing.Point( 573, 60 );
//            this.m_btnConfirmItem.Name = "m_btnConfirmItem";
//            this.m_btnConfirmItem.Size = new System.Drawing.Size( 110, 24 );
//            this.m_btnConfirmItem.TabIndex = 70;
//            this.m_btnConfirmItem.TabStop = false;
//            this.m_btnConfirmItem.Tag = "MOD_TEXT";
//            this.m_btnConfirmItem.Text = "���� ���";
//            this.m_btnConfirmItem.UseVisualStyleBackColor = false;
//            this.m_btnConfirmItem.Click += new System.EventHandler( this.m_btnConfirmItem_Click );

//             P_TR_TO_IN

//            this.Controls.Add( this.m_btnConfirmItem );
//            this.Controls.Add( this.m_btnImportCost );
//            this.Controls.Add( this.panel_main );
//            this.ForeColor = System.Drawing.Color.Black;
//            this.Name = "P_TR_TO_IN";
//            this.Load += new System.EventHandler( this.P_TR_TO_IN_Load );
//            this.Paint += new System.Windows.Forms.PaintEventHandler( this.P_TR_TO_IN_Paint );
//            this.Controls.SetChildIndex( this.panel_main, 0 );
//            this.Controls.SetChildIndex( this.m_btnImportCost, 0 );
//            this.Controls.SetChildIndex( this.m_btnConfirmItem, 0 );
//            this.panel_main.ResumeLayout( false );
//            this.panel_main.PerformLayout();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtImCost ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtRateLicense ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtKorAmt ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtAmtLicense ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtAmtEx ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtToDate ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtDtInspect ) ).EndInit();
//            ( ( System.ComponentModel.ISupportInitialize )( this.m_txtDtMedical ) ).EndInit();
//            this.ResumeLayout( false );

//        }
//        #endregion

//        #region �� ������ Loading �� ��� ó�� �κ�
//         <summary>
//         ������ Loading �� ��� ó�� �κ�
//         </summary>
//         <param name="sender">Sender</param>
//         <param name="e">e</param>
//        private void P_TR_TO_IN_Load(object sender, System.EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;

//            �������� �ε��ϴ� ���Դϴ�.
//            ShowStatusBarMessage( 1 );

//            �ش��ϴ� �� �°� Label �ʱ�ȭ
//            InitializeControlText();

//            SetProgressBarValue( 100, 30 );
//        }

//         <summary>
//         �����ڵ� ��ȸ �� Default �߰� ��� Setting
//         </summary>
//         <param name="sender"></param>
//         <param name="e"></param>
//        private void P_TR_TO_IN_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
//        {
//            try
//            {
//                Paint -= new System.Windows.Forms.PaintEventHandler( P_TR_TO_IN_Paint );
//                Application.DoEvents();

//                ��� TABLE Column�� ��´�.
//                InitializeDataTable();

//                SetProgressBarValue( 100, 70 );
//                Application.DoEvents();

//                Cursor.Current = Cursors.WaitCursor;

//                �� �����ڵ� Setting
//                TOMainGetGubunCD();

//                SetProgressBarValue( 100, 70 );

//                Default �߰��� �Ѵ�.
//                OnToolBarAddButtonClicked( sender, e );

//                SetProgressBarValue( 100, 100 );

//                ToolBarSearchButtonEnabled = true;
//                ToolBarAddButtonEnabled = true;

//                ShowStatusBarMessage( 0 );
//                SetProgressBarValue( 100, 0 );

//                Cursor.Current = Cursors.Default;
//            }
//            catch ( Exception ex )
//            {
//                ShowStatusBarMessage( 0 );
//                SetProgressBarValue( 100, 0 );
//                ShowErrorMessage( ex, PageName );
//                Cursor.Current = Cursors.Default;
//            }
//        }

//         <summary>
//         ��Ʈ�ѵ��� ĸ���� ������ ������ �̿��Ͽ� �����Ѵ�.
//         </summary>
//        private void InitializeControlText()
//        {

//            m_lblTitle.Text = GetDataDictionaryItem("TR", this.m_lblTitle.Tag.ToString());
//            m_lblNoTo.Text = GetDataDictionaryItem( "TR", this.m_lblNoTo.Tag.ToString() );
//            m_lblCdTrans.Text = GetDataDictionaryItem( "TR", this.m_lblCdTrans.Tag.ToString() );
//            m_lblGroupRcv.Text = GetDataDictionaryItem( "TR", this.m_lblGroupRcv.Tag.ToString() );
//            m_lblCdCurrency.Text = GetDataDictionaryItem( "TR", this.m_lblCdCurrency.Tag.ToString() );
//            m_lblCondPrice.Text = GetDataDictionaryItem( "TR", this.m_lblCondPrice.Tag.ToString() );
//            m_lblNoInspect.Text = GetDataDictionaryItem( "TR", this.m_lblNoInspect.Tag.ToString() );
//            m_lblNoMedical.Text = GetDataDictionaryItem( "TR", this.m_lblNoMedical.Tag.ToString() );
//            m_lblRemark.Text = GetDataDictionaryItem( "TR", this.m_lblRemark.Tag.ToString() );
//            m_lblStDistribut.Text = GetDataDictionaryItem( "TR", this.m_lblStDistribut.Tag.ToString() );
//            m_lblToDate.Text = GetDataDictionaryItem( "TR", this.m_lblToDate.Tag.ToString() );
//            m_lblFgTrans.Text = GetDataDictionaryItem( "TR", this.m_lblFgTrans.Tag.ToString() );
//            m_lblNmEmp.Text = GetDataDictionaryItem( "TR", this.m_lblNmEmp.Tag.ToString() );
//            m_lblRateLicense.Text = GetDataDictionaryItem( "TR", this.m_lblRateLicense.Tag.ToString() );
//            m_lblCondCustom.Text = GetDataDictionaryItem( "TR", this.m_lblCondCustom.Tag.ToString() );
//            m_lblDtInspect.Text = GetDataDictionaryItem( "TR", this.m_lblDtInspect.Tag.ToString() );
//            m_lblDtMedical.Text = GetDataDictionaryItem( "TR", this.m_lblDtMedical.Tag.ToString() );
//            m_lblImCost.Text = GetDataDictionaryItem( "TR", this.m_lblImCost.Tag.ToString() );
//            m_lblNoBl.Text = GetDataDictionaryItem( "TR", this.m_lblNoBl.Tag.ToString() );
//            m_lblOpenBank.Text = GetDataDictionaryItem( "TR", this.m_lblOpenBank.Tag.ToString() );
//            m_lblAmtEx.Text = GetDataDictionaryItem( "TR", this.m_lblAmtEx.Tag.ToString() );
//            m_lblAmtLicense.Text = GetDataDictionaryItem( "TR", this.m_lblAmtLicense.Tag.ToString() );
//            m_lblKorAmt.Text = GetDataDictionaryItem( "TR", this.m_lblKorAmt.Tag.ToString() );

//            m_btnConfirmItem.Text = GetDataDictionaryItem( "TR", this.m_btnConfirmItem.Tag.ToString() );
//            m_btnImportCost.Text = GetDataDictionaryItem( "TR", this.m_btnImportCost.Tag.ToString() );

//            CurrencyEditBox �ʱ�ȭ
//            m_txtAmtEx.NumberFormatInfoObject.NumberDecimalDigits = 4;
//            m_txtAmtEx.DecimalValue = 0;
//            m_txtRateLicense.NumberFormatInfoObject.NumberDecimalDigits = 4;
//            m_txtRateLicense.DecimalValue = 0;
//            m_txtAmtLicense.NumberFormatInfoObject.NumberDecimalDigits = 4;
//            m_txtAmtLicense.DecimalValue = 0;
//            m_txtKorAmt.NumberFormatInfoObject.NumberDecimalDigits = 4;
//            m_txtKorAmt.DecimalValue = 0;
//            m_txtImCost.NumberFormatInfoObject.NumberDecimalDigits = 4;
//            m_txtImCost.DecimalValue = 0;


//            this.m_txtToDate.Mask = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT );
//            this.m_txtToDate.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

//            this.m_txtDtInspect.Mask = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT );
//            this.m_txtDtInspect.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

//            this.m_txtDtMedical.Mask = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT );
//            this.m_txtDtMedical.ToDayDate = this.MainFrameInterface.GetDateTimeToday();


//            this.m_txtToDate.Text = this.MainFrameInterface.GetStringToday;
//        }

//         <summary>
//         ��� TABLE�� �����.
//         </summary>
//        private void InitializeDataTable()
//        {
//            m_dtTOHead = new DataTable();
//            m_dtTOHead.Columns.Add( "NO_TO", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_COMPANY", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_PURGRP", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_PARTNER", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_SCBL", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_BANK", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DT_TO", typeof( string ) );
//            m_dtTOHead.Columns.Add( "FG_LC", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_EXCH", typeof( string ) );
//            m_dtTOHead.Columns.Add( "RT_EXCH", typeof( double ) );
//            m_dtTOHead.Columns.Add( "AM_EX", typeof( double ) );
//            m_dtTOHead.Columns.Add( "AM_LICENSE", typeof( double ) );
//            m_dtTOHead.Columns.Add( "AM", typeof( double ) );
//            m_dtTOHead.Columns.Add( "COND_PRICE", typeof( string ) );
//            m_dtTOHead.Columns.Add( "CD_CUSTOMS", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_LICENSE", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DT_LICENSE", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_INSP", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DT_INSP", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_QUAR", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DT_QUAR", typeof( string ) );
//            m_dtTOHead.Columns.Add( "REMARK", typeof( string ) );
//            m_dtTOHead.Columns.Add( "TOT_WEIGHT", typeof( double ) );
//            m_dtTOHead.Columns.Add( "CD_UNIT", typeof( string ) );
//            m_dtTOHead.Columns.Add( "WEIGHT", typeof( double ) );
//            m_dtTOHead.Columns.Add( "YN_DISTRIBU", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_EMP", typeof( string ) );
//            m_dtTOHead.Columns.Add( "ID_INSERT", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DTS_INSERT", typeof( string ) );
//            m_dtTOHead.Columns.Add( "ID_UPDATE", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DTS_UPDATE", typeof( string ) );
//            m_dtTOHead.Columns.Add( "LN_PARTNER", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NM_KOR", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NM_PURGRP", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NM_BANK", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NM_EXCH", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NM_CUSTOMS", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_SCLC", typeof( string ) );
//            m_dtTOHead.Columns.Add( "NO_COST", typeof( string ) );
//            m_dtTOHead.Columns.Add( "DT_DISTRIBU", typeof( string ) );
//            m_dtTOHead.Columns.Add( "RCVL", typeof( string ) );

//        }

//         <summary>
//         �� �����ڵ� Setting
//         </summary>
//        private void TOMainGetGubunCD()
//        {
//                        string[] lsa_args = {"N;PU_C000016", "N;MA_B000005", "N;TR_IM00002"};
//                        object[] args = {LoginInfo.CompanyCode, lsa_args};
//                        DataSet lds_Combo = (DataSet)InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);

//            DataSet lds_Combo = this.GetComboData( "N;TR_IM00005", "N;MA_B000005", "N;TR_IM00002" );

//            �ŷ������ڵ� ComboBox�� Add
//            m_comFgTrans.DataSource = lds_Combo.Tables [0];
//            m_comFgTrans.ValueMember = "CODE";
//            m_comFgTrans.DisplayMember = "NAME";

//            ��ȭ���� ComboBox�� Add
//            m_comCdCurrency.DataSource = lds_Combo.Tables [1];
//            m_comCdCurrency.ValueMember = "CODE";
//            m_comCdCurrency.DisplayMember = "NAME";

//            �������� ComboBox�� Add
//            m_comCondPrice.DataSource = lds_Combo.Tables [2];
//            m_comCondPrice.ValueMember = "CODE";
//            m_comCondPrice.DisplayMember = "NAME";

//            m_comFgTrans.SelectedIndex = -1;
//            m_comCdCurrency.SelectedIndex = -1;
//            m_comCondPrice.SelectedIndex = -1;
//        }
//        #endregion

//        #region �� �������� ��ư Event
//         <summary>
//         ��ȸ��ư Ŭ���� �̺�Ʈ
//         </summary>		
//        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
//        {
//            try
//            {
//                Cursor.Current = Cursors.WaitCursor;

//                trade.P_TR_TO_NO dlg_TR_TO_NO = new trade.P_TR_TO_NO( MainFrameInterface );

//                if ( dlg_TR_TO_NO.ShowDialog() == DialogResult.OK )
//                {
//                    m_dtTOHead = dlg_TR_TO_NO.GetTOHead;

//                    Application.DoEvents();
//                    Cursor.Current = Cursors.WaitCursor;


//                    if ( dlg_TR_TO_NO.GetTOHead.Rows [0] ["RCVL"].ToString() != "" )
//                    {
//                        ToolBarDeleteButtonEnabled = false;
//                        ToolBarSaveButtonEnabled = false;
//                        m_DeleteCheck = "Y";
//                    }
//                    else
//                    {
//                        ToolBarDeleteButtonEnabled = true;
//                        ToolBarSaveButtonEnabled = true;
//                        m_DeleteCheck = "N";
//                    }


//                     ��ȸ�� ����� �԰��Ƿ� �� ������ üũ
//                                        object[] args = new object[2]{LoginInfo.CompanyCode, m_dtTOHead.Rows[0]["NO_TO"].ToString()};
//                                        int li_cnt = (int)InvokeRemoteMethod("TradeImport_NTX", "trade.CC_TR_TO_IN_NTX", "CC_TR_TO_IN_NTX.rem", "DeleteCheck", args);

//                                        if(li_cnt > 0)
//                                        {
//                                            ToolBarDeleteButtonEnabled = false;
//                                            ToolBarSaveButtonEnabled = false;
//                                            m_DeleteCheck = "Y";
//                                        }
//                                        else
//                                        {
//                                            ToolBarDeleteButtonEnabled = true;
//                                            ToolBarSaveButtonEnabled = true;
//                                            m_DeleteCheck = "N";
//                                        }

//                    Binding Manager����
//                    SetBindingManager();                                                 

//                    ���� Setting
//                    SetVariable( m_dtTOHead );

//                    m_dtTOHead.AcceptChanges();

//                    SetControlEnable( false );
//                                        TO��ȣ�� ���´�.
//                                        m_txtNoTo.Enabled = false;
//                                        ��ȭ ���´�.
//                                        m_comCdCurrency.Enabled = false;
//                                        ����ȯ�� ���´�.
//                                        m_txtRateLicense.ReadOnly = true;
//                                        ���ű׷� ���´�.
//                                        m_txtGroupRcv.ReadOnly = true;
//                                        ���ű׷� ��ư ���´�.
//                                        m_btnGroupRcv.Enabled = false;

//                                        BL������ ���´�.
//                                        m_txtNoBl.ReadOnly = true;
//                                        m_btnNoBl.Enabled = false;

//                                        if(m_txtNoBl.Text != "")
//                                        {
//                                            ������ ��Ȱ�� �÷� ó��
//                                            m_txtCdTrans.ReadOnly = true;
//                                            m_btnCdTrans.Enabled = false;

//                                            m_comFgTrans.Enabled = false;

//                                            m_txtOpenBank.ReadOnly = true;
//                                            m_btnOpenBank.Enabled = false;
//                                        }

//                    ToolBarAddButtonEnabled = true;
//                }

//                Cursor.Current = Cursors.Default;
//            }
//            catch ( Exception ex )
//            {
//                ShowErrorMessage( ex, PageName );
//            }
//        }

//         <summary>
//         �߰���ư Ŭ���� �̺�Ʈ
//         </summary>
//        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
//        {
//            if ( ( m_dtTOHead.Select( "", "", DataViewRowState.Unchanged ).Length != m_dtTOHead.Rows.Count ) )
//            {
//                if ( m_DeleteCheck == "N" )
//                {
//                    ����� ������ �ֽ��ϴ�. �����Ŀ� �߰��Ͻðڽ��ϱ� ?
//                    if ( MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000027" ), PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
//                        OnToolBarSaveButtonClicked( sender, e );
//                }
//            }

//            try
//            {
//                DataTable ldt_Temp = m_dtTOHead.Clone();
//                m_dtTOHead = null;
//                m_dtTOHead = ldt_Temp.Clone();

//                �߰� �� Default Value ����
//                SetDefaultAdd();

//                Binding Manager����
//                SetBindingManager();

//                SetControlEnable( true );
//                                if(m_txtNoTo.Enabled == false)
//                                {
//                                    TO��ȣ�� Ǭ��.
//                                    m_txtNoTo.Enabled = true;
//                                    ��ȭ Ǭ��.
//                                    m_comCdCurrency.Enabled = true;
//                                    ����ȯ�� Ǭ��.
//                                    m_txtRateLicense.ReadOnly = false;
//                                    ���ű׷� Ǭ��.
//                                    m_txtGroupRcv.ReadOnly = false;
//                                    ���ű׷� ��ư Ǭ��.
//                                    m_btnGroupRcv.Enabled = true;

//                                    BL������ Ǭ��.
//                                    m_txtNoBl.ReadOnly = false;
//                                    m_btnNoBl.Enabled = true;
//                                }

//                m_DeleteCheck = "N";

//                                ���� ��Ȱ�� �÷� ���� ó��
//                                m_txtCdTrans.ReadOnly = false;
//                                m_btnCdTrans.Enabled = true;

//                                m_comFgTrans.Enabled = true;

//                                m_txtOpenBank.ReadOnly = false;
//                                m_btnOpenBank.Enabled = true;

//                m_txtNoTo.Focus();

//                ToolBarSaveButtonEnabled = true;
//            }
//            catch ( Exception ex )
//            {
//                ShowErrorMessage( ex, PageName );
//            }
//        }

//         <summary>
//         ������ư Ŭ���� �̺�Ʈ
//         </summary>
//        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
//        {
//            if ( m_dtTOHead.Rows.Count < 1 )
//                return;

//            ������ �����Ͻðڽ��ϱ� ?
//            if ( MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000014" ), PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
//            {
//                                string ls_NoTO = m_txtNoTo.Text;
//                                string ls_NoBL = m_dtTOHead.Rows[0]["NO_SCBL"].ToString();
//                                string ls_NoCost = m_dtTOHead.Rows[0]["NO_COST"].ToString();

//                Application.DoEvents();
//                Cursor.Current = Cursors.WaitCursor;

//                try
//                {//UP_TR_TO_IN_DELETE

//                    object [] m_obj = new object [2];
//                    m_obj [0] = m_txtNoTo.Text;
//                    m_obj [1] = this.MainFrameInterface.LoginInfo.CompanyCode;

//                    int li_result = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl", "pur.CC_PU_APP","CC_PU_APP.rem", "DeleteApp", m_obj));

//                    ResultData ret = ( ResultData )this.ExecSp( "UP_TR_TO_IN_DELETE", m_obj );
//                    if ( !ret.Result )
//                    {
//                        ������ ������ �߻��߽��ϴ�.
//                        this.ShowMessage( "TR_M000028" );

//                            MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000028"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        Cursor.Current = Cursors.Default;
//                        return;
//                    }

//                                        object[] ls_args = new object[4]{LoginInfo.CompanyCode, ls_NoTO, ls_NoBL, ls_NoCost};
//                                        if((int)InvokeRemoteMethod("TradeImport", "trade.CC_TR_TO_IN", "CC_TR_TO_IN.rem", "SaveDelete", ls_args) < 0)
//                                        {
//                                            ������ ������ �߻��߽��ϴ�.
//                                            MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000028"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            Cursor.Current = Cursors.Default;
//                                            return;
//                                        }
//                }
//                catch ( Exception ex )
//                {
//                    ShowErrorMessage( ex, PageName );
//                    Cursor.Current = Cursors.Default;
//                    return;
//                }

//                ȭ�鿡�� ����
//                m_dtTOHead.Rows [0].Delete();
//                m_txtToDate.Text = "";
//                m_txtDtInspect.Text = "";
//                m_txtDtMedical.Text = "";
//                m_dtTOHead.AcceptChanges();

//                �ڷᰡ ���������� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "CM_M000009" ), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information );

//                Default �߰��� �Ѵ�.
//                OnToolBarAddButtonClicked( sender, e );
//            }
//        }

//         <summary>
//         �����ư Ŭ���� �̺�Ʈ
//         </summary>
//        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
//        {
//            �ʼ��׸� üũ
//            if ( FieldCheck() == false )
//                return;

//            panel_main.Focus();

//            ���� Binding�۾�
//            m_CurrencyManager = ( CurrencyManager )BindingContext [m_dtTOHead];
//            m_CurrencyManager.Position = 0;

//            if ( ( m_dtTOHead.Select( "", "", DataViewRowState.Unchanged ).Length == 0 ) )
//            {
//                m_dtTOHead.Rows [0] ["DT_TO"] = m_txtToDate.MaskEditBox.ClipText;			//�Ű���
//                m_dtTOHead.Rows [0] ["DT_INSP"] = m_txtDtInspect.MaskEditBox.ClipText;	//�˻���
//                m_dtTOHead.Rows [0] ["DT_QUAR"] = m_txtDtMedical.MaskEditBox.ClipText;	//�˿���

//                ��������,�ð� ��������
//                string ls_date = MainFrameInterface.GetStringDetailToday;

//                �߰��� ��� TOHead Table�� Edit�Ѵ�.(�Է���,�Է�����)
//                if ( ( m_dtTOHead.Select( "", "", DataViewRowState.Added ) ).Length > 0 )
//                {
//                    m_dtTOHead.Rows [0] ["ID_INSERT"] = LoginInfo.EmployeeNo;					//�Է���
//                    m_dtTOHead.Rows [0] ["DTS_INSERT"] = ls_date;							//�Է½ð�
//                    m_dtTOHead.Rows [0] ["ID_UPDATE"] = m_dtTOHead.Rows [0] ["ID_INSERT"];	//�Է���
//                    m_dtTOHead.Rows [0] ["DTS_UPDATE"] = m_dtTOHead.Rows [0] ["DTS_INSERT"];//�Է½ð�
//                }

//                ������ ��� BLHead Table�� Edit�Ѵ�.(������,��������)
//                if ( ( m_dtTOHead.Select( "", "", DataViewRowState.ModifiedCurrent ) ).Length > 0 )
//                {
//                    m_dtTOHead.Rows [0] ["ID_UPDATE"] = LoginInfo.EmployeeNo;					//������
//                    m_dtTOHead.Rows [0] ["DTS_UPDATE"] = ls_date;							//�����ð�
//                }

//                Cursor.Current = Cursors.WaitCursor;

//                SaveTable()�� ���� PrimaryKey ����
//                m_dtTOHead.PrimaryKey = new DataColumn [] { m_dtTOHead.Columns ["NO_TO"], m_dtTOHead.Columns ["CD_COMPANY"] };

//                try
//                {
//                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
//                    si.DataValue = m_dtTOHead; 					//������ ������ ���̺�
//                    si.SpNameInsert = "UP_TR_TO_IN_INSERT";			//Insert ���ν�����
//                    si.SpNameUpdate = "UP_TR_TO_IN_UPDATE";			//Update ���ν�����
//                    si.SpNameDelete = "UP_TR_TO_IN_DELETE";			//Delete ���ν�����


//                    /*�� ���������̺��� �� ���ν������� ����� �Ķ���� Value�� ���� �÷��� �����Ѵ�.*/
//                    si.SpParamsDelete = new string [] { "NO_TO", "CD_COMPANY" };
//                    si.SpParamsInsert = new string [] { "NO_TO", "CD_COMPANY", "CD_PURGRP", "CD_PARTNER", "NO_SCBL", "CD_BANK", "DT_TO", "FG_LC", "CD_EXCH", 
//                                                         "RT_EXCH", "AM_EX", "AM_LICENSE", "AM", "COND_PRICE", "CD_CUSTOMS", "NO_LICENSE", "DT_LICENSE", "NO_INSP", 
//                                                         "DT_INSP", "NO_QUAR", "DT_QUAR", "REMARK", "TOT_WEIGHT", "CD_UNIT", "WEIGHT", "YN_DISTRIBU", "ID_INSERT1", "DTS_INSERT1", 
//                                                         "NO_EMP", "NO_COST", "DT_DISTRIBU"};
//                    si.SpParamsUpdate = new string [] { "NO_TO", "CD_COMPANY", "CD_PURGRP", "CD_PARTNER", "NO_SCBL", "CD_BANK", "DT_TO", "FG_LC", "CD_EXCH", 
//                                                         "RT_EXCH", "AM_EX", "AM_LICENSE", "AM", "COND_PRICE", "CD_CUSTOMS", "NO_LICENSE", "DT_LICENSE", "NO_INSP", 
//                                                         "DT_INSP", "NO_QUAR", "DT_QUAR", "REMARK", "TOT_WEIGHT", "CD_UNIT", "WEIGHT", "YN_DISTRIBU", "ID_INSERT1", "DTS_INSERT1", 
//                                                         "NO_EMP", "NO_COST", "DT_DISTRIBU"};

//                    /*���������̺��� �������� ���� �ʴ� �÷������� ��� �����ͷο쿡 ���������� ���� ���� �����Ѵ�.*/
//                    si.SpParamsValues.Add( ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday );
//                    si.SpParamsValues.Add( ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID );
//                    si.SpParamsValues.Add( ActionState.Update, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday );
//                    si.SpParamsValues.Add( ActionState.Update, "ID_INSERT1", this.LoginInfo.UserID );


//                    /*
//                        * �������� ����޼ҵ�(Save)ȣ��
//                        * ResultData Ÿ������ ���ϵȴ�.	
//                    */

//                    Duzon.Common.Util.ResultData result = ( Duzon.Common.Util.ResultData )this.Save( si );
//                    /*���忡 �����Ѱ��*/
//                    if ( result.Result )
//                    {
//                        this.ShowMessage( "TR_M000010" );

//                        m_dtTOHead.AcceptChanges();
//                        Cursor.Current = Cursors.Default;

//                        SetControlEnable( false );
//                                                TO��ȣ�� ���´�.
//                                                m_txtNoTo.Enabled = false;
//                                                ��ȭ ���´�.
//                                                m_comCdCurrency.Enabled = false;
//                                                ����ȯ�� ���´�.
//                                                m_txtRateLicense.ReadOnly = true;
//                                                ���ű׷� ���´�.
//                                                m_txtGroupRcv.ReadOnly = true;
//                                                ���ű׷� ��ư ���´�.
//                                                m_btnGroupRcv.Enabled = false;

//                                                BL������ ���´�.
//                                                m_txtNoBl.ReadOnly = true;
//                                                m_btnNoBl.Enabled = false;
//                        return;
//                    }

//                    this.ShowMessage( "TR_M000011" );
//                    Cursor.Current = Cursors.Default;
//                    return;


//                                        object[] ls_args = new object[3]{LoginInfo.CompanyCode, m_txtNoTo.Text, m_dtTOHead};
//                                        if((int)InvokeRemoteMethod("TradeImport", "trade.CC_TR_TO_IN", "CC_TR_TO_IN.rem", "SaveTO", ls_args) < 0)
//                                        {
//                                            �ڷ� ���� �� ������ �߻��߽��ϴ�.
//                                            MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000011"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                            Cursor.Current = Cursors.Default;
//                                            return;
//                                        }
//                }
//                catch ( Exception ex )
//                {
//                    ShowErrorMessage( ex, PageName, null, GetDataDictionaryItem( "TR", ( string )m_lblNoTo.Tag ) );
//                    Cursor.Current = Cursors.Default;
//                    return;
//                }

//                                m_dtTOHead.AcceptChanges();
//                                Cursor.Current = Cursors.Default;

//                                TO��ȣ�� ���´�.
//                                m_txtNoTo.Enabled = false;
//                                ��ȭ ���´�.
//                                m_comCdCurrency.Enabled = false;
//                                ����ȯ�� ���´�.
//                                m_txtRateLicense.ReadOnly = true;
//                                ���ű׷� ���´�.
//                                m_txtGroupRcv.ReadOnly = true;
//                                ���ű׷� ��ư ���´�.
//                                m_btnGroupRcv.Enabled = false;

//                                BL������ ���´�.
//                                m_txtNoBl.ReadOnly = true;
//                                m_btnNoBl.Enabled = false;

//                                �ڷḦ �����Ͽ����ϴ�.
//                                MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000010"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            else
//            {
//                ����� ������ �����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000013" ), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information );
//            }
//        }

//         <summary>
//         �μ��ư Ŭ���� �̺�Ʈ
//         </summary>
//        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
//        {
//        }

//         <summary>
//         �ݱ��ư Ŭ���� �̺�Ʈ
//         </summary>
//        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
//        {
//            if ( m_dtTOHead == null )
//                return true;

//            if ( this.m_txtNoTo.Text == "" )
//            {
//                return true;
//            }

//            bool lb_isSave = false;

//            DataTable ldt_temp = this.m_dtTOHead.GetChanges( DataRowState.Modified );
//            if ( ldt_temp != null )
//            {
//                lb_isSave = true;
//            }

//             �����Ͱ� ó�� �ε��� ������� ���� �߰��� ��� LC��ȣ�� ������ �Ǵ��Ͽ� ������ ������ �����.
//            DataTable ldt_table = this.m_dtTOHead.GetChanges( DataRowState.Added );

//            if ( this.m_txtNoTo.Text != "" && ldt_table != null )
//            {
//                lb_isSave = true;
//            }

//            if ( lb_isSave )
//            {
//                if ( m_DeleteCheck == "N" )
//                {
//                    ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ� ?
//                    DialogResult ldlg_ret = MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000015" ), PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );

//                    switch ( ldlg_ret )
//                    {
//                        case DialogResult.Yes:
//                            OnToolBarSaveButtonClicked( sender, e );
//                            return true;
//                        case DialogResult.No:
//                            return true;
//                        case DialogResult.Cancel:
//                            return false;
//                    }
//                }
//            }
//            return true;
//        }
//        #endregion

//        #region �� �������, �δ����� ��ư
//         <summary>
//         ������� ��ư Ŭ�� �̺�Ʈ
//         </summary>
//         <param name="sender">Sender</param>
//         <param name="e">e</param>
//        private void m_btnConfirmItem_Click(object sender, System.EventArgs e)
//        {
//            if ( m_dtTOHead.Rows.Count < 1 )
//                return;

//            m_CurrencyManager = ( CurrencyManager )BindingContext [m_dtTOHead];
//            m_CurrencyManager.Position = 0;

//            if ( ( m_dtTOHead.Select( "", "", DataViewRowState.Unchanged ).Length == 0 ) )
//            {
//                if ( m_DeleteCheck == "N" )
//                {
//                    ����� ������ �ֽ��ϴ�. �����Ŀ� ��������� �Ͻðڽ��ϱ� ?
//                    if ( MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000029" ), PageName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question ) == DialogResult.Cancel )
//                        return;

//                    Application.DoEvents();

//                    �ʼ��׸� üũ
//                    if ( FieldCheck() == false )
//                        return;

//                    �����ư Click
//                    OnToolBarSaveButtonClicked( sender, e );
//                }
//            }

//            �������Page �Ķ����
//            string [] ls_Line_args = new string [7] { m_txtNoTo.Text, m_CdTrans, m_NmTrans, m_CdEmp, m_NmEmp, m_txtNoBl.Text, m_DeleteCheck };

//            Cursor.Current = Cursors.WaitCursor;

//            try
//            {
//                �������Page�� �������� �����ϴ��� ����
//                if ( IsExistPage( "P_TR_TO_LINE", false ) == true )
//                {
//                    ���� ������ �ݱ�
//                    UnLoadPage( "P_TR_TO_LINE", false );
//                }

//                object [] obj_args = new object [2] { ls_Line_args, m_dtTOHead };
//                LoadPageFrom( "P_TR_TO_LINE", GetDataDictionaryItem( "TR", "MOD_TEXT_TO" ), Grant, obj_args );
//            }
//            catch ( Exception ex )
//            {
//                ShowErrorMessage( ex, PageName );
//                Cursor.Current = Cursors.Default;
//            }
//        }

//         <summary>
//         �δ����� Ŭ�� �̺�Ʈ
//         </summary>
//         <param name="sender">Sender</param>
//         <param name="e">e</param>
//        private void m_btnImportCost_Click(object sender, System.EventArgs e)
//        {
//            if ( m_dtTOHead.Rows.Count < 1 )
//                return;

//            m_CurrencyManager = ( CurrencyManager )BindingContext [m_dtTOHead];
//            m_CurrencyManager.Position = 0;

//            if ( ( m_dtTOHead.Select( "", "", DataViewRowState.Unchanged ).Length == 0 ) )
//            {
//                if ( m_DeleteCheck == "N" )
//                {
//                    ����� ������ �ֽ��ϴ�. �����Ŀ� �δ������� �Ͻðڽ��ϱ� ?
//                    if ( MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000064" ), PageName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question ) == DialogResult.Cancel )
//                        return;

//                    Application.DoEvents();

//                    �ʼ��׸� üũ
//                    if ( FieldCheck() == false )
//                        return;

//                    �����ư Click
//                    OnToolBarSaveButtonClicked( sender, e );
//                }
//            }

//            �δ���Page�� �������� �����ϴ��� ����
//            if ( IsExistPage( "P_TR_IMCOST", false ) == true )
//            {
//                �δ��� ������ �ݱ�
//                UnLoadPage( "P_TR_IMCOST", false );
//            }

//            �δ�����Page �Ķ����(ȭ�鱸��, �Ű��ȣ, LA����, L/C��ȣ, B/L��ȣ, �ŷ�ó�ڵ�,
//                                      �ŷ�ó��, ��ǥ����, �δ����ȣ, ȯ��, �ݾ�, CC�ڵ�, CC��)
//            string [] ls_Line_args = new string [13]{"TO", m_txtNoTo.Text, null, m_dtTOHead.Rows[0]["NO_SCLC"].ToString(), m_txtNoBl.Text, m_CdTrans,
//                                                    m_NmTrans, m_txtToDate.Text, m_dtTOHead.Rows[0]["NO_COST"].ToString(),
//                                                    m_comCdCurrency.SelectedValue.ToString(),m_txtAmtLicense.Text, m_CdCC, m_NmCC};
//            Cursor.Current = Cursors.WaitCursor;

//            try
//            {
//                object [] obj_args = new object [2] { ls_Line_args, m_dtTOHead };
//                LoadPageFrom( "P_TR_IMCOST", GetDataDictionaryItem( "TR", "IM_COST_TO" ), Grant, obj_args );
//            }
//            catch ( Exception ex )
//            {
//                ShowErrorMessage( ex, PageName );
//                Cursor.Current = Cursors.Default;
//            }
//        }
//        #endregion

//        #region �� ���� ��ư �̺�Ʈ
//         <summary>
//         B/L��ȣ ����â
//         </summary>
//         <param name="sender">Sender</param>
//         <param name="e">e</param>
//        private void m_btnNoBl_Click(object sender, System.EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;

//            trade.P_TR_BL_NO dlg_TR_BL_NO = new trade.P_TR_BL_NO( MainFrameInterface );

//            if ( dlg_TR_BL_NO.ShowDialog() == DialogResult.OK )
//            {
//                Application.DoEvents();
//                if ( m_dtTOHead == null )
//                {
//                    ��� TABLE Column�� ��´�.
//                    InitializeDataTable();

//                    Binding Manager����
//                    SetBindingManager();

//                    DataRow Idr_Head = m_dtTOHead.NewRow();
//                    m_dtTOHead.Rows.Add( Idr_Head );
//                }

//                m_dtTOHead.Rows [0] ["NO_SCBL"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NO_BL"];
//                m_dtTOHead.Rows [0] ["CD_COMPANY"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_COMPANY"];
//                m_dtTOHead.Rows [0] ["CD_PARTNER"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_PARTNER"];
//                m_dtTOHead.Rows [0] ["LN_PARTNER"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["LN_PARTNER"];
//                m_dtTOHead.Rows [0] ["CD_PURGRP"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_PURGRP"];
//                m_dtTOHead.Rows [0] ["NM_PURGRP"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_PURGRP"];
//                m_dtTOHead.Rows [0] ["FG_LC"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["FG_LC"];
//                m_dtTOHead.Rows [0] ["CD_BANK"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_BANK"];
//                m_dtTOHead.Rows [0] ["NM_BANK"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_BANK"];
//                m_dtTOHead.Rows [0] ["CD_EXCH"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_EXCH"];
//                m_dtTOHead.Rows [0] ["RT_EXCH"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["RT_EXCH"];
//                m_dtTOHead.Rows[0]["AM_EX"] = dlg_TR_BL_NO.GetBLHead.Rows[0]["AM_EX"];
//                m_dtTOHead.Rows [0] ["COND_PRICE"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["COND_PRICE"];
//                m_dtTOHead.Rows [0] ["NO_EMP"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NO_EMP"];
//                m_dtTOHead.Rows [0] ["NM_KOR"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_KOR"];
//                m_dtTOHead.Rows [0] ["REMARK"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["REMARK"];
//                m_dtTOHead.Rows [0] ["NO_SCLC"] = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NO_SCLC"];

//                �ŷ�ó�ڵ�/��Ī
//                m_CdTrans = ( string )dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_PARTNER"];
//                m_NmTrans = ( string )dlg_TR_BL_NO.GetBLHead.Rows [0] ["LN_PARTNER"];

//                ���ű׷��ڵ�/��Ī
//                m_CdPurGrp = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_PURGRP"].ToString();
//                m_NmPurGrp = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_PURGRP"].ToString();

//                ������ڵ�/��Ī
//                m_CdEmp = ( string )dlg_TR_BL_NO.GetBLHead.Rows [0] ["NO_EMP"];
//                m_NmEmp = ( string )dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_KOR"];

//                ���������ڵ�/��Ī
//                m_CdBank = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_BANK"].ToString();
//                m_NmBank = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_BANK"].ToString();

//                CC�ڵ�/��Ī
//                m_CdCC = dlg_TR_BL_NO.GetBLHead.Rows [0] ["CD_CC"].ToString();
//                m_NmCC = dlg_TR_BL_NO.GetBLHead.Rows [0] ["NM_CC"].ToString();

//                ��Ÿ����...
//                m_dtTOHead.Rows [0] ["YN_DISTRIBU"] = "N";								//���꿩��
//                m_dtTOHead.Rows [0] ["WEIGHT"] = 0;										//���߷�
//                m_dtTOHead.Rows [0] ["TOT_WEIGHT"] = 0;									//���߷�

//                Binding Manager����
//                SetBindingManager();

//                ������ ��Ȱ�� �÷� ó��
//                m_txtCdTrans.ReadOnly = ReadOnly.TotalReadOnly;

//                m_comFgTrans.Enabled = false;


//                m_txtOpenBank.ReadOnly = ReadOnly.TotalReadOnly;

//                ToolBarAddButtonEnabled = true;
//                ToolBarSaveButtonEnabled = true;
//                ToolBarDeleteButtonEnabled = true;

//                m_txtGroupRcv.Focus();
//            }

//            Cursor.Current = Cursors.Default;
//        }



//        #endregion

//        #region �� �ʼ��׸� üũ Function
//         <summary>
//         �ʼ��׸� üũ �Լ�
//         </summary>
//        private bool FieldCheck()
//        {
//            �Ű��ȣ
//            if ( m_txtNoTo.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblNoTo.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtNoTo.Focus();
//                return false;
//            }

//            �Ű���
//            if ( m_txtToDate.MaskEditBox.ClipText.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblToDate.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtToDate.Focus();
//                return false;
//            }

//            BL��ȣ
//            if ( m_txtNoBl.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblNoBl.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtNoBl.Focus();
//                return false;
//            }

//            �ŷ�ó
//            if ( m_txtCdTrans.CodeValue.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblCdTrans.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtCdTrans.Focus();
//                return false;
//            }

//            �ŷ�����
//            if ( m_comFgTrans.SelectedIndex == -1 )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblFgTrans.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_comFgTrans.Focus();
//                return false;
//            }

//            ���ű׷�
//            if ( m_txtGroupRcv.CodeValue.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblGroupRcv.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtGroupRcv.Focus();
//                return false;
//            }

//            �����
//            if ( m_txtNmEmp.CodeValue.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblNmEmp.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtNmEmp.Focus();
//                return false;
//            }

//            ��ȭ�ݾ�
//            if ( m_txtAmtEx.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblAmtEx.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtAmtEx.Focus();
//                return false;
//            }

//            ��ȭ
//            if ( m_comCdCurrency.SelectedIndex == -1 )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblCdCurrency.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_comCdCurrency.Focus();
//                return false;
//            }

//            ����ȯ��
//            if ( m_txtRateLicense.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblRateLicense.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtRateLicense.Focus();
//                return false;
//            }

//            ����ݾ�
//            if ( m_txtAmtLicense.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblAmtLicense.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtAmtLicense.Focus();
//                return false;
//            }

//            ��������
//            if ( m_comCondPrice.SelectedIndex == -1 )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblCondPrice.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_comCondPrice.Focus();
//                return false;
//            }

//            ��ȭ�ݾ�
//            if ( m_txtKorAmt.Text.Trim() == "" )
//            {
//                �ʼ��Է»����� �����Ǿ����ϴ�.
//                MessageBoxEx.Show( GetMessageDictionaryItem( "TR_M000002" ), m_lblKorAmt.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
//                m_txtKorAmt.Focus();
//                return false;
//            }

//            return true;
//        }
//        #endregion

//        #region �� Control Bingding
//         <summary>
//         Binding��ü�� �����Ͽ� Table�� TextBox�� Binding�Ѵ�.
//         </summary>
//        private void SetBindingManager()
//        {
//            BindingManagerBase ����
//            m_Manager = BindingContext [m_dtTOHead];
//              �뿭�ϰ� ���� 
//            Control�� Binding��ü ����
//            foreach ( Control lc_ctrl in panel_main.Controls )
//            {
//                if ( lc_ctrl is Duzon.Common.Controls.TextBoxExt )
//                    ( ( Duzon.Common.Controls.TextBoxExt )lc_ctrl ).DataBindings.Clear();

//                if ( lc_ctrl is Duzon.Common.Controls.DatePicker )
//                    ( ( Duzon.Common.Controls.DatePicker )lc_ctrl ).DataBindings.Clear();

//                if ( lc_ctrl is Duzon.Common.Controls.DropDownComboBox )
//                    ( ( Duzon.Common.Controls.DropDownComboBox )lc_ctrl ).DataBindings.Clear();

//                if ( lc_ctrl is Duzon.Common.Controls.CurrencyTextBox )
//                    ( ( Duzon.Common.Controls.CurrencyTextBox )lc_ctrl ).DataBindings.Clear();
//            }

//            Control�� Binding��ü �߰�
//            m_txtNoTo.DataBindings.Add( "Text", m_dtTOHead, "NO_TO" );					//�Ű��ȣ
//            m_txtToDate.DataBindings.Add( "Text", m_dtTOHead, "DT_TO" );					//�Ű���
//            m_txtNoBl.DataBindings.Add( "Text", m_dtTOHead, "NO_SCBL" );					//BL��ȣ
//            m_comFgTrans.DataBindings.Add( "SelectedValue", m_dtTOHead, "FG_LC" );		//LC����
//            m_txtAmtEx.DataBindings.Add( "Text", m_dtTOHead, "AM_EX" );					//��ȭ�ݾ�
//            m_comCdCurrency.DataBindings.Add( "SelectedValue", m_dtTOHead, "CD_EXCH" );	//��ȭ
//            m_txtRateLicense.DataBindings.Add( "Text", m_dtTOHead, "RT_EXCH" );			//�鼼ȯ��
//            m_txtAmtLicense.DataBindings.Add( "Text", m_dtTOHead, "AM_LICENSE" );			//����ݾ�
//            m_comCondPrice.DataBindings.Add( "SelectedValue", m_dtTOHead, "COND_PRICE" );	//��������
//            m_txtKorAmt.DataBindings.Add( "Text", m_dtTOHead, "AM" );						//��ȭ�ݾ�
//            m_txtNoInspect.DataBindings.Add( "Text", m_dtTOHead, "NO_INSP" );				//�˻�����ȣ
//            m_txtDtInspect.DataBindings.Add( "Text", m_dtTOHead, "DT_INSP" );				//�˻���
//            m_txtNoMedical.DataBindings.Add( "Text", m_dtTOHead, "NO_QUAR" );				//�˿�����ȣ
//            m_txtDtMedical.DataBindings.Add( "Text", m_dtTOHead, "DT_QUAR" );				//�˿���
//            m_txtRemark.DataBindings.Add( "Text", m_dtTOHead, "REMARK" );					//���
//            m_txtStDistribut.DataBindings.Add( "Text", m_dtTOHead, "YN_DISTRIBU" );		//�������

//            if ( m_dtTOHead.Rows.Count > 0 )
//            {
//                m_txtCdTrans.CodeName = m_dtTOHead.Rows [0] ["LN_PARTNER"].ToString();		//�ŷ�ó��
//                m_txtCdTrans.CodeValue = m_dtTOHead.Rows [0] ["CD_PARTNER"].ToString();		//�ŷ�ó��

//                m_txtOpenBank.CodeName = m_dtTOHead.Rows [0] ["NM_BANK"].ToString();			//��������
//                m_txtOpenBank.CodeValue = m_dtTOHead.Rows [0] ["CD_BANK"].ToString();			//��������
//                m_txtGroupRcv.CodeName = m_dtTOHead.Rows [0] ["NM_PURGRP"].ToString();		//���ű׷�
//                m_txtGroupRcv.CodeValue = m_dtTOHead.Rows [0] ["CD_PURGRP"].ToString();		//���ű׷�
//                m_txtNmEmp.CodeName = m_dtTOHead.Rows [0] ["NM_KOR"].ToString();				//�����
//                m_txtNmEmp.CodeValue = m_dtTOHead.Rows [0] ["NO_EMP"].ToString();				//�����
//                m_txtCondCustom.CodeName = m_dtTOHead.Rows [0] ["NM_CUSTOMS"].ToString();		//���Ҽ���
//                m_txtCondCustom.CodeValue = m_dtTOHead.Rows [0] ["CD_CUSTOMS"].ToString();		//���Ҽ���

//                ��ȭ�� "KRW"�ϰ��� ȯ���� 1�� Setting�Ѵ�.(Protect)
//                if ( m_comCdCurrency.SelectedValue.ToString() == "000" )
//                {
//                    m_txtRateLicense.BackColor = System.Drawing.SystemColors.Control;
//                    m_txtRateLicense.Enabled = false;
//                }
//                else
//                {
//                    m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( System.Byte )( 226 ) ), ( ( System.Byte )( 239 ) ), ( ( System.Byte )( 243 ) ) );
//                    m_txtRateLicense.Enabled = true;
//                }
//            }

//            m_Manager.Position = 0;
//        }

//         <summary>
//         �� ������ Setting�Ѵ�.
//         </summary>
//        private void SetVariable(DataTable ps_DataTable)
//        {
//            m_CdTrans = ps_DataTable.Rows [0] ["CD_PARTNER"].ToString();					//�ŷ�ó�ڵ�
//            m_NmTrans = ps_DataTable.Rows [0] ["LN_PARTNER"].ToString();					//�ŷ�ó��
//            m_CdBank = ps_DataTable.Rows [0] ["CD_BANK"].ToString();						//���������ڵ�
//            m_NmBank = ps_DataTable.Rows [0] ["NM_BANK"].ToString();						//���������
//            m_CdCond = ps_DataTable.Rows [0] ["CD_CUSTOMS"].ToString();					//���Ҽ���
//            m_NmCond = ps_DataTable.Rows [0] ["NM_CUSTOMS"].ToString();					//���Ҽ�����
//            m_CdPurGrp = ps_DataTable.Rows [0] ["CD_PURGRP"].ToString();					//���ű׷��ڵ�
//            m_NmPurGrp = ps_DataTable.Rows [0] ["NM_PURGRP"].ToString();					//���ű׷��
//            m_CdEmp = ps_DataTable.Rows [0] ["NO_EMP"].ToString();						//������ڵ�
//            m_NmEmp = ps_DataTable.Rows [0] ["NM_KOR"].ToString();						//����ڸ�
//            m_CdCC = ps_DataTable.Rows [0] ["CD_CC"].ToString();							//CC�ڵ�
//            m_NmCC = ps_DataTable.Rows [0] ["NM_CC"].ToString();							//CC��
//        }
//        #endregion

//        #region �� �ڷ� �߰�
//         <summary>
//         �߰��� ��� �� �׸� Default���� �ִ´�.
//         </summary>
//        private void SetDefaultAdd()
//        {
//            DataRow Idr_Head = m_dtTOHead.NewRow();

//            Idr_Head ["NO_TO"] = "";									//�Ű��ȣ
//            Idr_Head ["CD_COMPANY"] = LoginInfo.CompanyCode;			//ȸ���ڵ�

//            �α��� ������� �����ش�.
//            m_CdEmp = LoginInfo.EmployeeNo;
//            m_NmEmp = LoginInfo.EmployeeName;

//            m_txtNmEmp.CodeValue = LoginInfo.EmployeeNo;
//            m_txtNmEmp.CodeName = LoginInfo.EmployeeName;

//            Idr_Head ["NO_EMP"] = m_CdEmp;							//������ڵ�
//            Idr_Head ["NM_KOR"] = m_NmEmp;							//����ڸ�

//            Idr_Head ["AM_EX"] = 0;									//��ȭ�ݾ�
//            Idr_Head ["CD_EXCH"] = "000";							//��ȭ
//            Idr_Head ["RT_EXCH"] = 1;								//�鼼ȯ��
//            Idr_Head ["AM"] = 0;										//��ȭ�ݾ�
//            Idr_Head ["AM_LICENSE"] = 0;								//����ݾ�
//            Idr_Head ["YN_DISTRIBU"] = "N";							//���꿩��
//            Idr_Head ["WEIGHT"] = 0;									//���߷�
//            Idr_Head ["TOT_WEIGHT"] = 0;								//���߷�
//            Idr_Head ["DT_LICENSE"] = "00000000";					//��������
//            Idr_Head ["DT_DISTRIBU"] = "00000000";					//��������

//            m_dtTOHead.Rows.Add( Idr_Head );

//            m_CdTrans = "";
//            m_NmTrans = "";
//            m_CdCond = "";
//            m_NmCond = "";
//            m_CdBank = "";
//            m_NmBank = "";
//            m_CdPurGrp = "";
//            m_NmPurGrp = "";

//            m_txtCdTrans.ReadOnly = ReadOnly.None;
//            m_txtOpenBank.ReadOnly = ReadOnly.None;


//        }
//        #endregion

//        #region �� Control Event ����(�ŷ�ó, ����, ����ڵ�..)
//         <summary>
//         ������� Changed Event
//         </summary>
//         <param name="sender">Sender</param>
//         <param name="e">e</param>
//        private void m_txtStDistribut_TextChanged(object sender, System.EventArgs e)
//        {
//            switch ( m_txtStDistribut.Text )
//            {
//                case "Y":
//                    m_txtStDistribut.Text = "����";
//                    break;
//                case "N":
//                    m_txtStDistribut.Text = "������";
//                    break;
//            }
//        }



//         <summary>
//         ��ȭ���п��� �׸��� �������� ��� �߻� �̺�Ʈ
//         </summary>
//         <param name="sender"></param>
//         <param name="e"></param>
//        private void m_comCdCurrency_SelectionChangeCommitted(object sender, System.EventArgs e)
//        {
//            ��ȭ�� "KRW"�ϰ��� ȯ���� 1�� Setting�Ѵ�.(Protect)
//            if ( m_comCdCurrency.SelectedValue.ToString() == "000" )
//            {
//                m_txtRateLicense.DecimalValue = 1;
//                m_txtRateLicense.BackColor = System.Drawing.SystemColors.Control;
//                m_txtRateLicense.Enabled = false;
//                m_dtTOHead.Rows [0] ["RT_EXCH"] = 1;
//            }
//            else
//            {
//                m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( System.Byte )( 226 ) ), ( ( System.Byte )( 239 ) ), ( ( System.Byte )( 243 ) ) );
//                m_txtRateLicense.Enabled = true;
//            }
//        }
//        #endregion

//        #region  -> ��¥�� ���õ� �̺�Ʈ��


//        private void DataPickerValidated(object sender, System.EventArgs e)
//        {
//            try
//            {
//                if ( !( ( DatePicker )sender ).Modified )
//                    return;

//                if ( ( ( DatePicker )sender ).Text == string.Empty )
//                    return;

//                 ��ȿ�� �˻�
//                if ( !( ( DatePicker )sender ).IsValidated )
//                {
//                    MainFrameInterface.ShowMessage( "WK1_003" );
//                    ( ( DatePicker )sender ).Text = string.Empty;
//                    ( ( DatePicker )sender ).Focus();
//                    return;
//                }



//                                if(this.m_dtpFrom.Text != string.Empty && this.m_dtpTo.Text != string.Empty)
//                                {
//                                     From To üũ
//                                    CommonFunction objComm = new CommonFunction();
//                                    if(objComm.DiffDate(this.m_dtpFrom.Text, this.m_dtpTo.Text) > 0)
//                                    {
//                                        this.ShowMessage("WK1_007", GetDDItem("DT_DISCIP") );
//                                        this.m_dtpTo.Focus();
//                                        return;
//                                    }
//                                }

//            }
//            catch
//            {
//            }
//        }


//        #endregion

//        private void DateDateChanged(object sender, System.EventArgs e)
//        {
//            try
//            {
//                switch ( ( ( DatePicker )sender ).Name )
//                {

//                    case "m_txtToDate":
//                        m_dtTOHead.Rows [0] ["DT_TO"] = ( ( DatePicker )sender ).Text;
//                        break;
//                    case "m_txtDtInspect":
//                        m_dtTOHead.Rows [0] ["DT_INSP"] = ( ( DatePicker )sender ).Text;
//                        break;
//                    case "m_txtDtMedical":
//                        m_dtTOHead.Rows [0] ["DT_QUAR"] = ( ( DatePicker )sender ).Text;
//                        break;
//                }

//            }
//            catch
//            {
//            }
//        }

//        private void SetControlEnable(bool isEnable)
//        {
//            TO��ȣ�� ���´�.
//            m_txtNoTo.Enabled = isEnable;
//            ��ȭ ���´�.
//            m_comCdCurrency.Enabled = isEnable;
//            ����ȯ�� ���´�.
//            m_txtRateLicense.Enabled = isEnable;
//            ���ű׷� ���´�.
//            m_txtGroupRcv.Enabled = isEnable;

//            BL������ ���´�.
//            m_txtNoBl.Enabled = isEnable;
//            m_btnNoBl.Enabled = isEnable;

//            ������ ��Ȱ�� �÷� ó��
//            m_txtCdTrans.Enabled = isEnable;

//            m_comFgTrans.Enabled = isEnable;

//            m_txtOpenBank.Enabled = isEnable;

//            if ( isEnable )
//            {
//                try
//                {
//                    m_comCdCurrency.SelectedValue = "000";
//                    m_txtRateLicense.Enabled = false;
//                }
//                catch
//                {
//                }
//            }
//        }


//        #region -> BpControl Event

//        private void OnBpCodeTextBox_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
//        {
//            if ( e.DialogResult == DialogResult.OK )
//            {
//                System.Data.DataRow [] rows = e.HelpReturn.Rows;
//                Duzon.Common.BpControls.BpCodeTextBox contr = ( Duzon.Common.BpControls.BpCodeTextBox )sender;
//                switch ( e.HelpID )
//                {
//                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
//                        if ( contr.Name == "m_txtCondCustom" )
//                        {
//                            m_dtTOHead.Rows [0] ["CD_CUSTOMS"] = rows [0] ["CD_PARTNER"].ToString();
//                            m_dtTOHead.Rows [0] ["NM_CUSTOMS"] = rows [0] ["LN_PARTNER"].ToString();
//                        }
//                        else
//                        {
//                            m_dtTOHead.Rows [0] ["CD_PARTNER"] = rows [0] ["CD_PARTNER"].ToString();
//                            m_dtTOHead.Rows [0] ["LN_PARTNER"] = rows [0] ["LN_PARTNER"].ToString();
//                        }
//                        break;
//                    case Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB:
//                        m_dtTOHead.Rows [0] ["CD_BANK"] = e.CodeValue;//rows[0]["CD_PARTNER"].ToString();
//                        m_dtTOHead.Rows [0] ["NM_BANK"] = e.CodeName;//rows[0]["LN_PARTNER"].ToString();		
//                        break;
//                    case Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB:
//                        m_dtTOHead.Rows [0] ["CD_PURGRP"] = rows [0] ["CD_PURGRP"].ToString();
//                        m_dtTOHead.Rows [0] ["NM_PURGRP"] = rows [0] ["NM_PURGRP"].ToString();
//                            m_dtBLHead.Rows[0]["CD_CC"] = rows[0]["CD_CC"].ToString();
//                            m_dtBLHead.Rows[0]["NM_CC"] = rows[0]["NM_CC"].ToString();	

//                        break;
//                    case Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB:
//                        m_dtTOHead.Rows [0] ["NO_EMP"] = rows [0] ["NO_EMP"].ToString();
//                        m_dtTOHead.Rows [0] ["NM_KOR"] = rows [0] ["NM_KOR"].ToString();

//                        break;
//                    default:
//                        break;

//                }
//            }
//        }

//        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
//        {
//            switch ( e.HelpID )
//            {
//                case Duzon.Common.Forms.Help.HelpID.P_PU_TPPO_SUB:		// �������� ����â
//                    e.HelpParam.P61_CODE1 = "N";
//                    break;
//            }
//        }
//        #endregion


//    }
//}
