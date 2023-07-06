using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using master;

namespace cz
{
    public partial class P_CZ_MA_EXCHANGE : PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_MA_EXCHANGE_BIZ _biz;
        private string _getexchange;

        public P_CZ_MA_EXCHANGE()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
            this._biz = new P_CZ_MA_EXCHANGE_BIZ();
        }
        #endregion

        #region 초기화
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            DataTable comboDt;

            try
            {
                base.InitPaint();

                this.oneGrid1.UseCustomLayout = true;
                this.bpPanelControl1.IsNecessaryCondition = true;
                this.oneGrid1.InitCustomLayout();

                this.dtp년월.Mask = this.GetFormatDescription(DataDictionaryTypes.MA, FormatTpType.YEAR_MONTH, FormatFgType.SELECT);
                this.dtp년월.ToDayDate = Global.MainFrame.GetDateTimeFirstMonth;
                this.dtp년월.Text = Global.MainFrame.GetStringFirstDayInMonth;

                comboDt = this.GetComboDataCombine("N;MA_B000005");

                DataTable dt = this._biz.MaxNoSeq(this.dtp년월.Text);

                SetControl setControl = new SetControl();
                setControl.SetCombobox(this.cbo외화화폐, comboDt.Copy());
                setControl.SetCombobox(this.cbo원화화폐, comboDt.Copy());
                setControl.SetCombobox(this.cbo고시회차, dt);

                this.cbo외화화폐.SelectedValue = "001";
                this.cbo원화화폐.SelectedValue = "000";
                this._flex.SetDataMap("CURR_SOUR", comboDt.Copy(), "CODE", "NAME");
                this._flex.SetDataMap("CURR_DEST", comboDt.Copy(), "CODE", "NAME");

                UGrant ugrant = new UGrant();
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "EXRATEINFO", this.btn환율정보가져오기, true);
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "COPY", this.btn복사, true);
                this._getexchange = BASIC.GetMAEXC("환율정보등록-환율가져오기");

                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("YYMMDD", "년월일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_SEQ", "고시회차", 80, true);
            this._flex.SetCol("QUOTATION_TIME", "고시시간", 80);
            this._flex.SetCol("CURR_SOUR", "외화화폐", 80);
            this._flex.SetCol("CURR_DEST", "원화화폐", 80);
            this._flex.SetCol("RATE_BASE", "기준환율", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("RATE_BUY", "송금보낼때", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("RATE_SALE", "송금받을때", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("RATE_PURCHASE", "매입환율(영업)", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("RATE_SALES", "매출환율(영업)", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            this._flex.Cols["RATE_BASE"].Format = "##0.###0";
            this._flex.Cols["RATE_BUY"].Format = "##0.###0";
            this._flex.Cols["RATE_SALE"].Format = "##0.###0";
            this._flex.Cols["RATE_PURCHASE"].Format = "##0.###0";
            this._flex.Cols["RATE_SALES"].Format = "##0.###0";

            this._flex.Cols["QUOTATION_TIME"].Format = "0#:##:##";
            this._flex.Cols["QUOTATION_TIME"].EditMask = "0#:##:##";
            this._flex.Cols["QUOTATION_TIME"].TextAlign = TextAlignEnum.CenterCenter;

            this._flex.SetStringFormatCol(new string[] { "QUOTATION_TIME" });
            this._flex.SetNoMaskSaveCol("QUOTATION_TIME");
            this._flex.SetColMaxLength("RATE_BASE", 14);

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.VerifyAutoDelete = new string[] { "CURR_SOUR", "CURR_DEST" };
            this._flex.VerifyPrimaryKey = new string[] { "YYMMDD", "CURR_SOUR", "CURR_DEST", "NO_SEQ" };
            //this._flex.VerifyNotNull = new string[] { "RATE_BASE" };

            this._flex.VerifyCompare(this._flex.Cols["CURR_SOUR"], this._flex.Cols["CURR_DEST"], OperatorEnum.NotEqual);
            this._flex.AllowFreezing = AllowFreezingEnum.Columns;
            this._flex.ShowButtons = ShowButtonsEnum.Always;
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn환율정보동기화.Click += new EventHandler(this.btn환율정보동기화_Click);
            this.btn환율정보가져오기.Click += new EventHandler(this.btn환율정보가져오기_Click);
            this.btn복사.Click += new EventHandler(this.btn복사_Click);

            this._flex.AddRow += new EventHandler(this.OnToolBarAddButtonClicked);
            this._flex.ValidateEdit += new ValidateEditEventHandler(flex_ValidateEdit);
        }
        #endregion
        
        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(this.dtp년월.Text,
                                                      D.GetDecimal(this.cbo고시회차.SelectedValue),
                                                      D.GetString(this.cbo외화화폐.SelectedValue),
                                                      D.GetString(this.cbo원화화폐.SelectedValue));

                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex["NO_SEQ"] = D.GetString(this.cbo고시회차.SelectedValue) == null || D.GetString(this.cbo고시회차.SelectedValue) == string.Empty ? "1" : D.GetString(this.cbo고시회차.SelectedValue);
                this._flex["CURR_SOUR"] = D.GetString(this.cbo외화화폐.SelectedValue);
                this._flex["CURR_DEST"] = D.GetString(this.cbo원화화폐.SelectedValue);

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;

                this.ShowMessage(PageResultMode.SaveGood);
                new SetControl().SetCombobox(this.cbo고시회차, this._biz.MaxNoSeq(this.dtp년월.Text));

                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;

            this._biz.Save(this._flex.GetChanges());
            this._flex.AcceptChanges();

            return true;
        }
        #endregion

        #region 버튼 이벤트
        private void btn환율정보가져오기_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._getexchange != "100")
                {
                    MsgControl.ShowMsg("외환은행에서 제공하는\n실시간 환율정보를 가져오는 중입니다..");
                    Uri address = new Uri("http://203.234.132.2/ip/exrate.txt");
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Stream stream = webClient.OpenRead(address);
                    DataTable dtExrate = (DataTable)null;
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                    {
                        string str1 = string.Empty;
                        int num1 = 0;
                        this.CreateDataTable(ref dtExrate);
                        string str2 = string.Empty;
                        int num2 = 1;
                        string str3 = string.Empty;
                        string str4;
                        while ((str4 = streamReader.ReadLine()) != null && !(str4.Substring(0, 1).ToUpper() == "E"))
                        {
                            DataRow row = dtExrate.NewRow();
                            if (num1 == 0)
                            {
                                str2 = str4.Substring(10, 8);
                                num2 = D.GetInt((object)str4.Substring(32, 2));
                                str3 = str4.Substring(34, 6);
                                str4 = streamReader.ReadLine();
                                ++num1;
                            }
                            if (num1 % 2 > 0)
                            {
                                row["YYMMDD"] = (object)str2;
                                row["NO_SEQ"] = (object)num2;
                                row["QUOTATION_TIME"] = (object)str3;
                                row["CURR_SOUR"] = (object)str4.Substring(3, 3);
                                row["CURR_DEST"] = (object)"KRW";
                                str4 = streamReader.ReadLine();
                                ++num1;
                            }
                            if (num1 % 2 == 0)
                            {
                                if (!str4.Contains("#DIV/0!") && !str4.Contains("#REF!"))
                                {
                                    row["RATE_SALE"] = (object)D.GetDecimal((object)string.Format("{0:###.#0}", (object)((double)D.GetInt64((object)str4.Substring(10, 7)) * 0.01)));
                                    row["RATE_BUY"] = (object)D.GetDecimal((object)string.Format("{0:###.#0}", (object)((double)D.GetInt64((object)str4.Substring(31, 7)) * 0.01)));
                                    row["RATE_BASE"] = (object)D.GetDecimal((object)string.Format("{0:###.#0}", (object)((double)D.GetInt64((object)str4.Substring(38, 7)) * 0.01)));
                                }
                                else
                                {
                                    row["RATE_SALE"] = (object)new Decimal(0);
                                    row["RATE_BUY"] = (object)new Decimal(0);
                                    row["RATE_BASE"] = (object)new Decimal(0);
                                }
                            }
                            ++num1;
                            dtExrate.Rows.Add(row);
                        }
                        streamReader.Close();
                    }
                    stream.Close();
                    this._biz.ExcRateInfo(dtExrate);
                    MsgControl.CloseMsg();
                    this.ShowMessage("환율정보 가져오기를 완료했습니다.");
                    this.OnToolBarSearchButtonClicked((object)null, (EventArgs)null);
                }
                else if (((Form)new P_MA_EXCHANGE_GET_SUB()).ShowDialog() == DialogResult.OK)
                    this.OnToolBarSearchButtonClicked((object)null, (EventArgs)null);
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void CreateDataTable(ref DataTable dtExrate)
        {
            dtExrate = new DataTable();

            dtExrate.Columns.Add("YYMMDD", typeof(string));
            dtExrate.Columns.Add("NO_SEQ", typeof(int));
            dtExrate.Columns.Add("QUOTATION_TIME", typeof(string));
            dtExrate.Columns.Add("CURR_SOUR", typeof(string));
            dtExrate.Columns.Add("CURR_DEST", typeof(string));
            dtExrate.Columns.Add("CD_COMPANY", typeof(string));
            dtExrate.Columns.Add("RATE_BASE", typeof(decimal));
            dtExrate.Columns.Add("RATE_SALE", typeof(decimal));
            dtExrate.Columns.Add("RATE_BUY", typeof(decimal));
        }

        private void btn환율정보동기화_Click(object sender, EventArgs e)
        {
            try
            {
                this._biz.환율정보동기화(new object[] { this.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (new P_CZ_MA_EXCHANGE_SUB(D.GetString(this._flex["YYMMDD"]), D.GetString(this._flex["NO_SEQ"])).ShowDialog() != DialogResult.Yes)
                    return;

                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string @string = D.GetString(this._flex.GetData(e.Row, e.Col));
                string editData = this._flex.EditData;

                if (@string == editData) return;

                switch (this._flex.Cols[e.Col].Name)
                {
                    case "YYMMDD":
                        CommonFunction commonFunction = new CommonFunction();
                        if (D.GetString(this._flex["YYMMDD"]).Length != 8)
                        {
                            e.Cancel = true;
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            break;
                        }

                        if (!commonFunction.CheckDate(D.GetString(this._flex["YYMMDD"]).Substring(0, 4) + "/" + D.GetString(this._flex["YYMMDD"]).Substring(4, 2) + "/" + D.GetString(this._flex["YYMMDD"]).Substring(6, 2)))
                        {
                            e.Cancel = true;
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            break;
                        }
                        break;
                    case "RATE_BASE":
                        if (D.GetString(Decimal.Truncate(D.GetDecimal(editData))).Length > 7)
                        {
                            if (Global.CurLanguage == Language.KR)
                            {
                                this.ShowMessage("입력형식이 올바르지 않습니다.(#,###,###.####)");
                                e.Cancel = true;
                                break;
                            }

                            this.ShowMessage("MaxLength 7");
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (!new UGrant().GrantButton(Global.MainFrame.CurrentPageID, "COPY"))
                    return;
                this.btn복사.Enabled = this._flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion
    }
}
