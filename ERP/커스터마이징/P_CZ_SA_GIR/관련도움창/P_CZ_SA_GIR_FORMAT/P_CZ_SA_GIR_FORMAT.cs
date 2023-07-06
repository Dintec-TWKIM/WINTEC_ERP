using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;

namespace cz
{
    public partial class P_CZ_SA_GIR_FORMAT : Duzon.Common.Forms.CommonDialog
    {
        private bool _포장여부;
        P_CZ_SA_GIR_FORMAT_BIZ _biz = new P_CZ_SA_GIR_FORMAT_BIZ();
        public bool 송장정보포함
        {
            get
            {
                return this.chk송장정보포함.Checked;
            }
        }

        public DataRow 적용데이터
        {
            get
            {
                return this._flex.GetDataRow(this._flex.Row);
            }
        }

        public P_CZ_SA_GIR_FORMAT(string 매출처코드, string 매출처명, string IMO번호, string 호선번호, string 호선명, bool 포장여부)
        {
            InitializeComponent();

            this.ctx매출처.CodeValue = 매출처코드;
            this.ctx매출처.CodeName = 매출처명;

            this.ctx호선번호.CodeValue = IMO번호;
            this.ctx호선번호.CodeName = 호선번호;

            this.txt호선명.Text = 호선명;

            this._포장여부 = 포장여부;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();

            this.btn조회_Click(null, null);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("LN_PARTNER", "매출처", 100);
            this._flex.SetCol("NM_VESSEL", "호선명", 100);
            this._flex.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_DELIVERY_TO", "납품처", 100);
            this._flex.SetCol("NM_DELIVERY_TO_OLD", "납품처(OLD)", 100);
            this._flex.SetCol("NM_MAIN_CATEGORY", "Main Category", 100);
            this._flex.SetCol("NM_SUB_CATEGORY", "Sub Category", 100);
            this._flex.SetCol("NM_RMK", "협조내용", 200);
            this._flex.SetCol("CD_CONSIGNEE", "수하인코드", 200);
            this._flex.SetCol("NM_CONSIGNEE", "수하인명", 200);
            this._flex.SetCol("ADDR1_CONSIGNEE", "수하인주소1", 200);
            this._flex.SetCol("ADDR2_CONSIGNEE", "수하인주소2", 200);
            this._flex.SetCol("CD_PRODUCT", "HS코드", 200);
            this._flex.SetCol("NM_ARRIVER_COUNTRY", "도착국가", 200);
            this._flex.SetCol("PORT_ARRIVER", "도착도시", 200);
            this._flex.SetCol("REMARK1", "TEL(FAX)", 200);
            this._flex.SetCol("REMARK2", "이메일", 200);
            this._flex.SetCol("REMARK3", "PIC", 200);

            this._flex.SetDataMap("CD_PRODUCT", Global.MainFrame.GetComboDataCombine("S;CZ_SA00018"), "CODE", "NAME");

            this._flex.SetDummyColumn("S");
            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.ctx호선번호.QueryAfter += new BpQueryHandler(this.ctx호선번호_QueryAfter);
            this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

            this._flex.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void ctx호선번호_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                txt호선명.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx호선번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ctx호선번호.Text))
                {
                    this.txt호선명.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this._flex.MouseRow < this._flex.Rows.Fixed) return;

                if (this.적용데이터["YN_ERROR"].ToString() == "Y")
				{
                    Global.MainFrame.ShowMessage("이전 납품처가 들어가 있는 항목은 적용 할 수 없습니다.");
                    return;
				}

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.ctx매출처.CodeValue,
                                                     this.ctx호선번호.CodeValue,
                                                     this._포장여부 == true ? "Y" : "N" });

                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow == false) return;

                if (this.적용데이터["YN_ERROR"].ToString() == "Y")
                {
                    Global.MainFrame.ShowMessage("이전 납품처가 들어가 있는 항목은 적용 할 수 없습니다.");
                    return;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
