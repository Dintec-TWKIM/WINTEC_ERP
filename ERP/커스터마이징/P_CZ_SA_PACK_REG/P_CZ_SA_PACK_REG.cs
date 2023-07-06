using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Erpiu.ComponentModel;

namespace cz
{
    public partial class P_CZ_SA_PACK_REG : PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_SA_PACK_REG_BIZ _biz;
        private string 의뢰일자;

        public P_CZ_SA_PACK_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_PACK_REG(string 회사, string 회사명, string 의뢰번호, string 의뢰일자, string 매출처코드, string 매출처, string IMO번호, string 호선번호, string 호선명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.ctx회사.CodeValue = 회사;
            this.ctx회사.CodeName = 회사명;
            this.txt의뢰번호.Text = 의뢰번호;
            this.의뢰일자 = 의뢰일자;
            this.ctx매출처.CodeValue = 매출처코드;
            this.ctx매출처.CodeName = 매출처;
            this.ctx호선번호.CodeValue = IMO번호;
            this.ctx호선번호.CodeName = 호선번호;
            this.txt호선명.Text = 호선명;
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_PACK_REG_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            string 포장사이즈;

            try
            {
                base.InitPaint();

                this.splitContainer2.SplitterDistance = 1056;

                this.dtp의뢰일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);

                this.cbo포장유형.DataSource = this.GetComboDataCombine("S;MA_CODEDTL2;CZ_SA00026");
                this.cbo포장유형.ValueMember = "CODE";
                this.cbo포장유형.DisplayMember = "NAME";

                if (!string.IsNullOrEmpty(this.txt의뢰번호.Text))
                {
                    this.dtp의뢰일자.Text = this.의뢰일자;

                    this._flexH.Binding = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                          Global.MainFrame.LoginInfo.Language,
                                                                          this.txt의뢰번호.Text });

                    포장사이즈 = D.GetString(this._flexH["CD_SIZE"]);

                    this.cbo포장사이즈.DataSource = this.GetComboDataCombine("S;MA_CODEDTL2;" + D.GetString(((DataRowView)this.cbo포장유형.SelectedItem).Row["CD_FLAG1"]));
                    this.cbo포장사이즈.ValueMember = "CODE";
                    this.cbo포장사이즈.DisplayMember = "NAME";

                    this.cbo포장사이즈.SelectedValue = 포장사이즈;

                    this._flexH.AcceptChanges();

                    this.btn의뢰적용.Enabled = false;
                    this.InitControl(false);
                }
                else
                {
                    this.InitControl(true);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("YN_OUTSOURCING", "외주여부", 40, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_PACK", "포장명", 100, false, typeof(decimal));
            this._flexH.SetCol("DT_PACK", "포장일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_TYPE", "포장유형", 100);
            this._flexH.SetCol("NM_SIZE", "포장사이즈", 150);
            this._flexH.SetCol("QT_NET_WEIGHT", "순중량", 60);
            this._flexH.SetCol("QT_GROSS_WEIGHT", "총중량", 60);
            this._flexH.SetCol("QT_WIDTH", "가로", 60);
            this._flexH.SetCol("QT_LENGTH", "세로", 60);
            this._flexH.SetCol("QT_HEIGHT", "높이", 60);
            this._flexH.SetCol("DC_RMK", "비고", 100);

            this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid2, this.pnl비고 });
            this._flexH.ExtendLastCol = true;

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Left
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("SEQ_GIR", "의뢰항번", 80);
            this._flexL.SetCol("NO_FILE", "파일번호", 80);
            this._flexL.SetCol("NO_DSP", "파일항번", 80);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품목코드", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품목명", 100);
            this._flexL.SetCol("QT_PACK", "포장수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SetDummyColumn(new string[] { "S" });

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Right
            this._flexR.BeginSetting(1, 1, false);

            this._flexR.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexR.SetCol("SEQ_GIR", "의뢰항번", 80);
            this._flexR.SetCol("NO_FILE", "파일번호", 80);
            this._flexR.SetCol("NO_DSP", "파일항번", 80);
            this._flexR.SetCol("CD_ITEM", "품목코드", 100);
            this._flexR.SetCol("NM_ITEM", "품목명", 100);
            this._flexR.SetCol("CD_ITEM_PARTNER", "매출처품목코드", 100);
            this._flexR.SetCol("NM_ITEM_PARTNER", "매출처품목명", 100);
            this._flexR.SetCol("QT_GIR", "의뢰잔량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexR.SetCol("QT_PACK", "포장수량", 60, true, typeof(decimal), FormatTpType.QUANTITY);

            this._flexR.SetDummyColumn(new string[] { "S" });

            this._flexR.SettingVersion = "0.0.0.1";
            this._flexR.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this.btn추가H.Click += new EventHandler(this.btn추가H_Click);
            this.btn제거H.Click += new EventHandler(this.btn제거H_Click);
            this.btn추가L.Click += new EventHandler(this.btn적용_Click);
            this.btn제거L.Click += new EventHandler(this.btn적용_Click);
            this.btn의뢰적용.Click += new EventHandler(this.btn의뢰적용_Click);

            this.cbo포장유형.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo포장사이즈.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cur총중량.DecimalValueChanged += new EventHandler(this.cur총중량_DecimalValueChanged);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexR.ValidateEdit += new ValidateEditEventHandler(this._flexR_ValidateEdit);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string pageId, pageName;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                pageId = "P_CZ_SA_PACK_MNG";
                pageName = "포장관리";

                if (this.IsExistPage(pageId, false))
                    this.UnLoadPage(pageId, false);

                this.LoadPageFrom(pageId, pageName, this.Grant, new object[] { this.txt의뢰번호.Text });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                if (this.btn의뢰적용.Enabled == false)
                {
                    this.MsgAndSave(PageActionMode.Search);
                    this.InitControl(true);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (this._flexH.IsDataChanged && this._flexH.GetChanges().Select("YN_OUTSOURCING = 'Y'").Length > 0)
            {
                this.ShowMessage("외주포장 건은 수정 할 수 없습니다.");
                return false;
            }

            if (Global.MainFrame.LoginInfo.UserID.Left(2) == "A-")
            {
                if (this._flexH.DataTable.Select("CD_TYPE IS NULL OR CD_TYPE <> '003'").Length != 0)
                {
                    this.ShowMessage("Wooden Packing 유형만 선택 가능 합니다.");
                    return false;
                }
                if (this._flexH.DataTable.Select("QT_WIDTH = 0").Length != 0)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "가로" });
                    return false;
                }
                if (this._flexH.DataTable.Select("QT_LENGTH = 0").Length != 0)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "세로" });
                    return false;
                }
                if (this._flexH.DataTable.Select("QT_HEIGHT = 0").Length != 0)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "높이" });
                    return false;
                }
            }

            foreach (DataRow dr in this._flexH.DataTable.Rows)
			{
                if (this._flexL.DataTable.Select(string.Format("NO_PACK = '{0}'", dr["NO_PACK"].ToString())).Length == 0)
				{
                    this.ShowMessage(string.Format("품목이 추가되지 않은 포장데이터가 있습니다.\n포장명 : {0}", dr["NO_PACK"].ToString()));
                    return false;
				}
			}

            return base.BeforeSave();
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            //DataRow[] dataRowArray, dataRowArray1;

            try
            {
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
                if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false) return false;
                
                //dataRowArray = this._flexH.DataTable.Select();
                //foreach (DataRow dr in dataRowArray)
                //{
                //    dataRowArray1 = this._flexL.DataTable.Select("NO_PACK = '" + D.GetString(dr["NO_PACK"]) + "'");
                //    if (dataRowArray1.Length == 0)
                //    {
                //        dr.Delete();
                //    }
                //}

                if (this._flexH.Rows.Count == this._flexH.Rows.Fixed)
                {
                    this.oneGrid2.Enabled = false;
                    this.btn추가L.Enabled = false;
                    this.btn제거L.Enabled = false;
                }

                DataTable dtH = this._flexH.GetChanges();
                DataTable dtL = this._flexL.GetChanges();

                if (!this._biz.Save(this.ctx회사.CodeValue, this.txt의뢰번호.Text, dtH, dtL)) return false;

                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        protected override bool BeforeDelete()
        {
            if (this._flexH.DataTable.Select("YN_OUTSOURCING = 'Y'").Length > 0)
            {
                this.ShowMessage("외주포장 건은 삭제 할 수 없습니다.");
                return false;
            }

            return base.BeforeDelete();
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            bool result;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                result = this._biz.Delete(new object[] { this.ctx회사.CodeValue,
                                                         this.txt의뢰번호.Text,
                                                         Global.MainFrame.LoginInfo.UserID });

                if (result == true)
                {
                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    this.OnToolBarAddButtonClicked(sender, e);        //삭제 후 바로 초기화 시켜준다.
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn추가H_Click(object sender, EventArgs e)
        {
            decimal maxSeq;

            try
            {
                this.btn추가H.Enabled = false;

                maxSeq = this._flexH.GetMaxValue("NO_PACK");

                this._flexH.Rows.Add();
                this._flexH.Row = this._flexH.Rows.Count - 1;

                this._flexH["DT_PACK"] = Global.MainFrame.GetStringToday;
                this._flexH["NO_PACK"] = (maxSeq + 1);
                this._flexH["NM_PACK"] = (maxSeq + 1);
                this._flexH["QT_NET_WEIGHT"] = 0;
                this._flexH["QT_GROSS_WEIGHT"] = 0;
                this._flexH["QT_WIDTH"] = 0;
                this._flexH["QT_LENGTH"] = 0;
                this._flexH["QT_HEIGHT"] = 0;

                this._flexH.AddFinished();
                this._flexH.Col = this._flexH.Cols.Fixed;
                this._flexH.Focus();

                if (this._flexH.Rows.Count > this._flexH.Rows.Fixed)
                {
                    this.oneGrid2.Enabled = true;
                    this.btn추가L.Enabled = true;
                    this.btn제거L.Enabled = true;
                }

                this.btn추가H.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.btn추가H.Enabled = true;
            }
        }

        private void btn제거H_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                this.btn제거H.Enabled = false;

                dataRowArray = this._flexL.DataTable.Select("NO_PACK = '" + D.GetString(this._flexH["NO_PACK"]) + "'");

                this.아이템이동(this._flexR, dataRowArray, false);

                this._flexH.Rows.Remove(this._flexH.Row);

                if (this._flexH.Rows.Count == this._flexH.Rows.Fixed)
                {
                    this.oneGrid2.Enabled = false;
                    this.btn추가L.Enabled = false;
                    this.btn제거L.Enabled = false;
                }

                this.btn제거H.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.btn제거H.Enabled = true;
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            FlexGrid source, destination;
            bool 추가여부;

            try
            {
                if (((Control)sender).Name == this.btn추가L.Name)
                {
                    추가여부 = true;
                    source = this._flexR;
                    destination = this._flexL;
                }
                else
                {
                    추가여부 = false;
                    source = this._flexL;
                    destination = this._flexR;
                }

                if (source.DataTable == null || destination.DataTable == null) return;

                dataRowArray = source.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0) return;

                this.아이템이동(destination, dataRowArray, 추가여부);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn의뢰적용_Click(object sender, EventArgs e)
        {
            P_CZ_SA_PACK_REG_SUB dialog;

            try
            {
                dialog = new P_CZ_SA_PACK_REG_SUB();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.ReturnHeaderData != null)
                    {
                        this.ctx회사.CodeValue = D.GetString(dialog.ReturnHeaderData["CD_COMPANY"]);
                        this.ctx회사.CodeName = D.GetString(dialog.ReturnHeaderData["NM_COMPANY"]);

                        this.txt의뢰번호.Text = D.GetString(dialog.ReturnHeaderData["NO_GIR"]);
                        this.dtp의뢰일자.Text = D.GetString(dialog.ReturnHeaderData["DT_GIR"]);
                        this.ctx매출처.CodeValue = D.GetString(dialog.ReturnHeaderData["CD_PARTNER"]);
                        this.ctx매출처.CodeName = D.GetString(dialog.ReturnHeaderData["NM_PARTNER"]);
                        this.ctx호선번호.CodeValue = D.GetString(dialog.ReturnHeaderData["NO_IMO"]);
                        this.ctx호선번호.CodeName = D.GetString(dialog.ReturnHeaderData["NO_HULL"]);
                        this.txt호선명.Text = D.GetString(dialog.ReturnHeaderData["NM_VESSEL"]);

                        this._flexH.Binding = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                              Global.MainFrame.LoginInfo.Language,
                                                                              this.txt의뢰번호.Text });

                        foreach (DataRow dr in dialog.ReturnLineData)
                        {
                            this._flexR.DataTable.ImportRow(dr);
                        }

                        this.btn의뢰적용.Enabled = false;
                        this.btn추가H.Enabled = true;
                        this.btn제거H.Enabled = true;

                        if (this._flexH.Rows.Count > this._flexH.Rows.Fixed)
                        {
                            this.btn추가L.Enabled = true;
                            this.btn제거L.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.cbo포장유형.Name)
                {
                    this._flexH["NM_TYPE"] = this.cbo포장유형.Text;

                    this.cbo포장사이즈.DataSource = this.GetComboDataCombine("S;MA_CODEDTL2;" + D.GetString(((DataRowView)this.cbo포장유형.SelectedItem).Row["CD_FLAG1"]));
                    this.cbo포장사이즈.ValueMember = "CODE";
                    this.cbo포장사이즈.DisplayMember = "NAME";

                    this.cur가로.DecimalValue = 0;
                    this.cur세로.DecimalValue = 0;
                    this.cur높이.DecimalValue = 0;

                    this.순중량계산();
                }
                else if (name == this.cbo포장사이즈.Name)
                {
                    this._flexH["NM_SIZE"] = this.cbo포장사이즈.Text;

                    this.cur가로.DecimalValue = D.GetDecimal(((DataRowView)this.cbo포장사이즈.SelectedItem).Row["CD_FLAG1"]);
                    this.cur세로.DecimalValue = D.GetDecimal(((DataRowView)this.cbo포장사이즈.SelectedItem).Row["CD_FLAG2"]);
                    this.cur높이.DecimalValue = D.GetDecimal(((DataRowView)this.cbo포장사이즈.SelectedItem).Row["CD_FLAG3"]);

                    this.순중량계산();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void cur총중량_DecimalValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.순중량계산();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dtL;
            string key, filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                key = D.GetString(this._flexH["NO_PACK"]);
                filter = "NO_PACK = '" + key + "'";
                dtL = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dtL = this._biz.SearchDetail(new object[] { this.ctx회사.CodeValue,
                                                                this.txt의뢰번호.Text,
                                                                key });
                }

                this._flexL.BindingAdd(dtL, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexR_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string colname = this._flexR.Cols[e.Col].Name;

                if (colname == "QT_PACK")
                {
                    string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                    string newValue = ((FlexGrid)sender).EditData;

                    if (D.GetDecimal(newValue) < 0)
                    {
                        this._flexR[e.Row, "QT_PACK"] = D.GetDecimal(oldValue);
                        ShowMessage(공통메세지._은_보다커야합니다, new string[] { this.DD("포장수량"), "-1" });
                        e.Cancel = true;
                        return;
                    }

                    decimal 의뢰잔량 = D.GetDecimal(this._flexR["QT_GIR"]);
                    if (D.GetDecimal(newValue) > 의뢰잔량)
                    {
                        this._flexR[e.Row, "QT_PACK"] = D.GetDecimal(oldValue);
                        ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.DD("포장수량"), this.DD("의뢰잔량") });
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void 아이템이동(FlexGrid dest, DataRow[] dataRowArray, bool 추가여부)
        {
            int index;
            DataRow[] dataRowArray1;

            try
            {
                this._flexL.Redraw = false;
                this._flexR.Redraw = false;

                index = 0;

                foreach (DataRow dr in dataRowArray)
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                    if (추가여부 == true)
                        dataRowArray1 = dest.DataTable.Select("NO_PACK = '" + D.GetString(this._flexH["NO_PACK"]) + "' AND SEQ_GIR = '" + D.GetString(dr["SEQ_GIR"]) + "'");
                    else
                        dataRowArray1 = dest.DataTable.Select("SEQ_GIR = '" + D.GetString(dr["SEQ_GIR"]) + "'");

                    if (dataRowArray1.Length > 0)
                    {
                        if (추가여부 == true)
                            dataRowArray1[0]["QT_PACK"] = (D.GetDecimal(dataRowArray1[0]["QT_PACK"]) + D.GetDecimal(dr["QT_PACK"]));
                        else
                        {
                            dataRowArray1[0]["QT_GIR"] = (D.GetDecimal(dataRowArray1[0]["QT_GIR"]) + D.GetDecimal(dr["QT_PACK"]));
                            dataRowArray1[0]["QT_PACK"] = dataRowArray1[0]["QT_GIR"];
                        }
                    }
                    else
                    {
                        dest.Rows.Add();
                        dest.Row = dest.Rows.Count - 1;

                        if (추가여부 == true)
                        {
                            dest["NO_PACK"] = D.GetString(this._flexH["NO_PACK"]);
                            dest["QT_PACK"] = D.GetDecimal(dr["QT_PACK"]);
                        }
                        else
                        {
                            dest["QT_GIR"] = D.GetDecimal(dr["QT_PACK"]);
                            dest["QT_PACK"] = D.GetDecimal(dr["QT_PACK"]);
                        }

                        dest["SEQ_GIR"] = D.GetString(dr["SEQ_GIR"]);
                        dest["NO_FILE"] = D.GetString(dr["NO_FILE"]);
                        dest["NO_QTLINE"] = D.GetString(dr["NO_QTLINE"]);
                        dest["NO_DSP"] = D.GetString(dr["NO_DSP"]);
                        dest["CD_ITEM"] = D.GetString(dr["CD_ITEM"]);
                        dest["NM_ITEM"] = D.GetString(dr["NM_ITEM"]);
                        dest["CD_ITEM_PARTNER"] = D.GetString(dr["CD_ITEM_PARTNER"]);
                        dest["NM_ITEM_PARTNER"] = D.GetString(dr["NM_ITEM_PARTNER"]);

                        dest.AddFinished();
                        dest.Col = dest.Cols.Fixed;
                        dest.Focus();
                    }

                    if (추가여부 == true)
                    {
                        dr["QT_GIR"] = (D.GetDecimal(dr["QT_GIR"]) - D.GetDecimal(dr["QT_PACK"]));
                        dr["QT_PACK"] = dr["QT_GIR"];

                        if (D.GetDecimal(dr["QT_GIR"]) == 0)
                            dr.Delete();
                    }
                    else
                        dr.Delete();
                }

                this._flexL.Redraw = true;
                this._flexR.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexL.Redraw = true;
                this._flexR.Redraw = true;
            }
        }

        private void InitControl(bool 전체초기화)
        {
            try
            {
                if (전체초기화 == true)
                {
                    this.btn의뢰적용.Enabled = true;

                    this.oneGrid2.Enabled = false;
                    this.btn추가H.Enabled = false;
                    this.btn제거H.Enabled = false;
                    this.btn추가L.Enabled = false;
                    this.btn제거L.Enabled = false;

                    this.cur포장명.DecimalValue = 0;
                    this.cbo포장유형.SelectedValue = string.Empty;
                    this.cbo포장사이즈.SelectedValue = string.Empty;
                    this.cur가로.DecimalValue = 0;
                    this.cur세로.DecimalValue = 0;
                    this.cur높이.DecimalValue = 0;
                    this.cur순중량.DecimalValue = 0;
                    this.cur총중량.DecimalValue = 0;
                    this.txt비고.Text = string.Empty;

                    this.txt의뢰번호.Text = string.Empty;
                    this.dtp의뢰일자.Text = string.Empty;
                    this.ctx매출처.CodeValue = string.Empty;
                    this.ctx매출처.CodeName = string.Empty;
                    this.ctx호선번호.CodeValue = string.Empty;
                    this.ctx호선번호.CodeName = string.Empty;
                    this.txt호선명.Text = string.Empty;

                    this._flexH.Binding = null;
                    this._flexL.Binding = null;
                }
                
                #region Right
                DataTable dtR = new DataTable();

                dtR.Columns.Add("S", typeof(string));
                dtR.Columns.Add("SEQ_GIR", typeof(string));
                dtR.Columns.Add("NO_FILE", typeof(string));
                dtR.Columns.Add("NO_QTLINE", typeof(string));
                dtR.Columns.Add("NO_DSP", typeof(string));
                dtR.Columns.Add("CD_ITEM", typeof(string));
                dtR.Columns.Add("NM_ITEM", typeof(string));
                dtR.Columns.Add("CD_ITEM_PARTNER", typeof(string));
                dtR.Columns.Add("NM_ITEM_PARTNER", typeof(string));
                dtR.Columns.Add("QT_GIR", typeof(decimal));
                dtR.Columns.Add("QT_PACK", typeof(decimal));

                this._flexR.Binding = dtR;
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 순중량계산()
        {
            try
            {
                switch (D.GetString(this.cbo포장유형.SelectedValue))
                {
                    case "001": // Carton Box
                        if (this.cur총중량.DecimalValue <= 1)
                            this.cur순중량.DecimalValue = (this.cur총중량.DecimalValue - D.GetDecimal(0.5));
                        else
                            this.cur순중량.DecimalValue = (this.cur총중량.DecimalValue - 1);
                        break;
                    case "002": // Palette
                        if (D.GetDecimal(this.cbo포장사이즈.SelectedValue) <= 60)
                            this.cur순중량.DecimalValue = (this.cur총중량.DecimalValue - 8);
                        else
                            this.cur순중량.DecimalValue = (this.cur총중량.DecimalValue - 10);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion
    }
}