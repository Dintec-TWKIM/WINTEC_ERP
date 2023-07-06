using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.Erpiu.ComponentModel;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_DEPOSIT_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_MA_PARTNER_DEPOSIT_SUB_BIZ _biz = new P_CZ_MA_PARTNER_DEPOSIT_SUB_BIZ();

        public P_CZ_MA_PARTNER_DEPOSIT_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_DEPOSIT_SUB(string 거래처코드, string 거래처명)
            : this()
        {
            this.ctx거래처.CodeValue = 거래처코드;
            this.ctx거래처.CodeName = 거래처명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_USE", "주요사용", 70, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_DEPOSIT", "계좌구분", 150);
            this._flex.SetCol("NO_DEPOSIT", "계좌번호", 150);
            this._flex.SetCol("CD_BANK", "은행", 120);
            this._flex.SetCol("NM_BANK", "은행명", 120);
            this._flex.SetCol("CD_BANK_NATION", "은행소재국", 120);
            this._flex.SetCol("NO_SORT", "은행코드", 90);
            this._flex.SetCol("CD_DEPOSIT_NATION", "예금주국적", 120);
            this._flex.SetCol("NO_SWIFT", "BIC(SWIFT)코드", 90);
            this._flex.SetCol("NM_DEPOSIT", "예금주", 100);
            this._flex.SetCol("DC_DEPOSIT_TEL", "예금주전화번호", 120);
            this._flex.SetCol("DC_DEPOSIT_ADDRESS", "예금주주소", 120);
            this._flex.SetCol("NO_BANK_BIC", "중계은행(BIC)", 100);
            this._flex.SetCol("DC_RMK", "비고", 100);
            this._flex.SetCol("NM_INSERT", "등록자", 80, false, typeof(string));
            this._flex.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE", "수정자", 80, false, typeof(string));
            this._flex.SetCol("DTS_UPDATE", "수정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.SetOneGridBinding(new object[] { this.txt계좌번호 }, new IUParentControl[] { this.oneGrid1 });

            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flex.SetDummyColumn("S");
            this._flex.VerifyPrimaryKey = new string[] { "CD_PARTNER", "NO_DEPOSIT" };
            this._flex.VerifyNotNull = new string[] { "CD_DEPOSIT", "NO_DEPOSIT", "CD_BANK" };

            this._flex.SetDataMap("CD_BANK", MA.GetCode("MA_B000043", false), "CODE", "NAME");
            this._flex.SetDataMap("CD_BANK_NATION", MA.GetCode("CZ_MA00016", false), "CODE", "NAME");
            this._flex.SetDataMap("CD_DEPOSIT", MA.GetCode("MA_B000079", false), "CODE", "NAME");
            this._flex.SetDataMap("CD_DEPOSIT_NATION", MA.GetCode("CZ_MA00016", false), "CODE", "NAME");
            
            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);

            this.cbo계좌구분.SelectionChangeCommitted += new EventHandler(this.cbo계좌구분_SelectionChangeCommitted);
            this.cbo은행.SelectionChangeCommitted += new EventHandler(this.cbo은행_SelectionChangeCommitted);

            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "DE_ADD", this.btn추가, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "DE_DEL", this.btn삭제, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "DE_SAVE", this.btn저장, true);

            this.cbo은행.DataSource = MA.GetCode("MA_B000043", false);
            this.cbo은행.ValueMember = "CODE";
            this.cbo은행.DisplayMember = "NAME";

            this.cbo계좌구분.DataSource = MA.GetCode("MA_B000079", false);
            this.cbo계좌구분.ValueMember = "CODE";
            this.cbo계좌구분.DisplayMember = "NAME";

            SetControl setControl = new SetControl();

            setControl.SetCombobox(this.cbo은행소재국, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo예금주국적, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            this.btn조회_Click(null, null);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx거래처.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처.Text);
                    return;
                }

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.ctx거래처.CodeValue });

                if (!this._flex.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                
                this._flex["CD_PARTNER"] = this.ctx거래처.CodeValue;
                this._flex["CD_DEPOSIT"] = "001";
                this._flex["YN_USE"] = "N";
                
                this._flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    this._flex.Redraw = false;

                    foreach (DataRow row in dataRowArray)
                        row.Delete();
                    
                    this._flex.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.SaveData()) return;

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool SaveData()
        {
            if (!this._flex.IsDataChanged) return false;
            if (!Global.MainFrame.VerifyGrid(this._flex)) return false;

            DataRow[] dataRowArray = this._flex.DataTable.Select("YN_USE = 'Y'");

            if (dataRowArray.Length > 1)
            {
                Global.MainFrame.ShowMessage("주요 사용은 하나만 선택가능합니다.");
                return false;
            }

            if (!this._biz.Save(this._flex.GetChanges()))
                return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void btn종료_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cbo계좌구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ControlEnable();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cbo은행_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.cbo계좌구분.SelectedValue) == "001")
                {
                    if (D.GetString(this.cbo은행.SelectedValue) == "300")
                    {
                        Global.MainFrame.ShowMessage("국내계좌의 경우 해외은행을 선택할 수 없습니다.");
                        this.cbo은행.SelectedValue = string.Empty;
                        this.cbo은행.Focus();
                    }
                    else
                    {
                        this.txt은행명.Text = this.cbo은행.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ControlEnable()
        {
            if (D.GetString(this.cbo계좌구분.SelectedValue) == "001")
            {
                this.txt은행명.Enabled = false;
                this.cbo은행소재국.Enabled = false;
                this.txt은행코드.Enabled = false;
                this.txtSwift코드.Enabled = false;
                this.txt예금주주소.Enabled = false;
                this.txt중계은행.Enabled = false;
                this.txt예금주전화번호.Enabled = false;
                this.cbo예금주국적.Enabled = false;
                this.cbo은행.Enabled = true;
            }
            else if (D.GetString(this.cbo계좌구분.SelectedValue) == "002")
            {
                this.txt은행명.Enabled = true;
                this.cbo은행소재국.Enabled = true;
                this.txt은행코드.Enabled = true;
                this.txtSwift코드.Enabled = true;
                this.txt예금주주소.Enabled = true;
                this.txt중계은행.Enabled = true;
                this.txt예금주전화번호.Enabled = true;
                this.cbo예금주국적.Enabled = true;
                this.cbo은행.SelectedValue = "300";
                this.cbo은행.Enabled = false;
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ControlEnable();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (this._flex.Cols[this._flex.Col].Name != "YN_USE" || this._flex["YN_USE"].ToString() != "N")
                    return;

                for (int index = 1; index < this._flex.Rows.Count; ++index)
                {
                    if (index != this._flex.Row)
                        this._flex[index, "YN_USE"] = "N";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
