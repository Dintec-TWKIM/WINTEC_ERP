using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using System.Windows.Forms;
using Dintec;
using Duzon.Erpiu.ComponentModel;
using Duzon.Common.Util.Uploader;
using System.Linq;

namespace cz
{
    public partial class P_CZ_FI_PARTNERPTR_SUB : Duzon.Common.Forms.CommonDialog
    {
        private FileUploader _fileUploader;
        private ContextMenu _ctm담당자;
        private string 거래처코드 = string.Empty;
        private object[] _ResultObject;

        public object[] ReturnValues
        {
            get
            {
                return this._ResultObject;
            }
        }

        public P_CZ_FI_PARTNERPTR_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_FI_PARTNERPTR_SUB(string _거래처코드)
        {
            this.InitializeComponent();
            this.거래처코드 = _거래처코드;
        }
        protected override void InitLoad()
        {
            base.InitLoad();

            this._fileUploader = new FileUploader(Global.MainFrame);

            this.InitGrid();
            this.InitEvent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                e.Cancel = true;

            base.OnClosing(e);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("USE_YN", "주요사용", 70, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("SEQ", "순번", 60);
            this._flex.SetCol("NM_PTR", "담당자명", 150, true);
            this._flex.SetCol("EN_PTR", "담당자명(영)", false);
            this._flex.SetCol("TP_PTR", "담당유형", 80, true);
            this._flex.SetCol("YN_TYPE", "대표여부(담당유형)", 70, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_OUTSTANDING_INV", "미수금담당", 70, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_LIMIT", "납기담당", 70, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NM_DEPT", "부서", 250, true);
            this._flex.SetCol("NM_DUTY_RESP", "직책", 250, true);
            this._flex.SetCol("EN_DUTY_RESP", "직책(영)", false);
            this._flex.SetCol("NO_HP", "휴대폰번호", 120, true);
            this._flex.SetCol("NM_EMAIL", "메일주소", 200, true);
            this._flex.SetCol("NO_FAX", "팩스번호", 120, true);
            this._flex.SetCol("NO_TEL", "전화번호", 120, true);
            this._flex.SetCol("NO_MOBILE", "카카오톡전송", 120, true);
            this._flex.SetCol("NM_INSERT", "등록자", 100);
            this._flex.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE", "수정자", 100);
            this._flex.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CLIENT_NOTE", "비고", 120, true);

            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flex.ExtendLastCol = true;

            this._flex.SetOneGridBinding(null, new IUParentControl[] { this.pnl담당자 });
            this._flex.SetDataMap("TP_PTR", Global.MainFrame.GetComboDataCombine("S;CZ_MA00011"), "CODE", "NAME");

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);

            this.pic담당자이미지.DoubleClick += new EventHandler(this.btn담당자이미지추가_Click);

            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this._ctm담당자 = new ContextMenu();
            this._ctm담당자.MenuItems.Add(Global.MainFrame.DD("추가"), new EventHandler(this.btn담당자이미지추가_Click));
            this._ctm담당자.MenuItems.Add(Global.MainFrame.DD("삭제"), new EventHandler(this.btn담당자이미지삭제_Click));
            this.pic담당자이미지.ContextMenu = this._ctm담당자;

            this.cbo담당유형.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_MA00011");
            this.cbo담당유형.ValueMember = "CODE";
            this.cbo담당유형.DisplayMember = "NAME";

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "PTR_ADD", this.btn추가, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "PTR_DEL", this.btn삭제, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "PTR_SAVE", this.btn저장, true);

            this.btn조회_Click(null, null);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this.Search();
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
                if (!this.추가전확인())
                    return;
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Rows.Add();
                this._flex[this._flex.Row, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex[this._flex.Row, "CD_PARTNER"] = this.거래처코드;
                this._flex[this._flex.Row, "SEQ"] = this.SeqMax();
                this._flex[this._flex.Row, "USE_YN"] = "N";
                this._flex.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.Row < 0) return;

                this._flex.RemoveItem(this._flex.Row);
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
                if (!this.BeforeSave()) return;

                if (this._fileUploader.Count > 0)
                {
                    if (!this._fileUploader.Start())
                    {
                        Global.MainFrame.ShowMessage("파일 전송 중 오류가 발생했습니다.");
                        return;
                    }
                }

                if (this.Save())
                    this._flex.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn종료_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select("USE_YN = 'Y'");

                if (dataRowArray.Length > 0)
                    this._ResultObject = new object[] { dataRowArray[0]["NM_PTR"],
                                                        dataRowArray[0]["NM_EMAIL"],
                                                        dataRowArray[0]["NO_HP"],
                                                        dataRowArray[0]["NO_TEL"],
                                                        dataRowArray[0]["NO_FAX"] };
                else
                    this._ResultObject = null;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn담당자이미지추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;
                if (this._flex.GetDataRow(this._flex.Row).RowState == DataRowState.Added)
                {
                    Global.MainFrame.ShowMessage("저장후 사진을 등록해 주시기 바랍니다.");
                    return;
                }

                FileInfoCs fileInfoCs = this._fileUploader.Add(D.GetString(this._flex["SEQ"]), "Upload/P_CZ_FI_PARTNERPTR_SUB/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.거래처코드 + "/");

                if (fileInfoCs != null)
                {
                    this._flex["DC_PHOTO"] = fileInfoCs.FileName;
                    this.pic담당자이미지.Image = fileInfoCs.Image;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn담당자이미지삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (!string.IsNullOrEmpty(D.GetString(this._flex["DC_PHOTO"])))
                {
                    this._fileUploader.Remove(D.GetString(this._flex["DC_PHOTO"]));
                    this.pic담당자이미지.Image = null;
                    this._flex["DC_PHOTO"] = "";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "USE_YN":
                        for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
                        {
                            if (this._flex.GetCellCheck(@fixed, this._flex.Cols["USE_YN"].Index) == CheckEnum.Checked)
                                this._flex.SetCellCheck(@fixed, this._flex.Cols["USE_YN"].Index, CheckEnum.Unchecked);
                        }
                        break;
                }
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
                string colname = this._flex.Cols[e.Col].Name;
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                switch (colname)
                {
                    case "YN_TYPE":
                        if (newValue == "Y" && this._flex.DataTable.Select("TP_PTR = '" + D.GetString(this._flex["TP_PTR"]) + "' AND YN_TYPE = 'Y'").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("담당유형별 대표자는 한명만 지정 할 수 있습니다.");
                            this._flex["YN_TYPE"] = oldValue;
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                this.pnl담당자이미지.TitleText = (D.GetString(this._flex["NM_PTR"]) == string.Empty ? Global.MainFrame.DD("담당자이미지") : D.GetString(this._flex["NM_PTR"]));

                FileInfoCs fileInfoCs = this._fileUploader[D.GetString(this._flex["DC_PHOTO"]), "Upload/P_CZ_FI_PARTNERPTR_SUB/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.거래처코드 + "/"];

                if (fileInfoCs != null)
                    this.pic담당자이미지.Image = fileInfoCs.Image;
                else
                    this.pic담당자이미지.Image = null;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private DataTable Search()
        {
            return DBHelper.GetDataTable("SP_CZ_FI_PARTNERPTR_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this.거래처코드 });
        }

        private bool Save()
        {
            try
            {
                DataTable dt = this._flex.GetChanges();

                if (dt != null && dt.Rows.Count != 0)
                {
                    SpInfo si = new SpInfo();

                    si.DataValue = Util.GetXmlTable(dt);
                    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;

                    si.SpNameInsert = "SP_CZ_FI_PARTNERPTR_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };


                    DBHelper.Save(si);
                    Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private Decimal SeqMax()
        {
            Decimal num = 1;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM FI_PARTNERPTR WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + 
                                                        @"AND CD_PARTNER = '" + this.거래처코드 + "'");
            
            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

            if (num <= this._flex.GetMaxValue("SEQ"))
                num = (this._flex.GetMaxValue("SEQ") + 1);

            return num;
        }

        private bool BeforeSave()
        {
            DataTable changes = this._flex.GetChanges();

            if (changes == null)
            {
                Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다, new string[0]);
                return false;
            }
            
            if (this._flex.DataTable.Select("USE_YN = 'Y'").Length > 1)
            {
                Global.MainFrame.ShowMessage("주요 사용은 하나만 선택가능합니다.");
                return false;
            }
            
            foreach (DataRow dataRow in changes.Rows)
            {
                if (dataRow.RowState != DataRowState.Deleted && string.IsNullOrEmpty(D.GetString(dataRow["NM_EMAIL"])))
                {
                    Global.MainFrame.ShowMessage("E-Mail 은(는) 필수입력항목입니다.");
                    return false;
                }
            }

            if (changes.Select().AsEnumerable().Where(x => Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", x["NM_EMAIL"].ToString()))) == 0).Count() > 0)
            {
                Global.MainFrame.ShowMessage("메일주소에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
                return false;
            }


            if (this.중복확인(this._flex.DataTable, "NM_PTR", "담당자명") == true)
                return false;

            // 서로 다른 담당자가 하나의 메일을 공용으로 사용하는 경우도 있기 때문에 주석처리 함
            //if (this.중복확인(this._flex.DataTable, "NM_EMAIL", "이메일") == true)
            //    return false;

            return true;
        }

        private bool 추가전확인()
        {
            if (this._flex.Rows.Count > 1)
            {
                for (int index = 0; index < this._flex.Rows.Count; ++index)
                {
                    if (string.IsNullOrEmpty(D.GetString(this._flex.Rows[index]["NM_EMAIL"])))
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "E-Mail" });
                        return false;
                    }
                }
            }
            return true;
        }

        private bool 중복확인(DataTable sourceDt, string column, string columnName)
        {
            int count;
            string 중복값 = string.Empty;

            try
            {
                if (column == "NM_EMAIL")
                {
                    var temp = sourceDt.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows)
                                       .AsEnumerable()
                                       .Select(x => x[column].ToString())
                                       .Where(x => x.ToString() != "@")
                                       .GroupBy(x => x, y => y, (x, y) => new { key = x, count = y.Count() })
                                       .Where(x => x.count > 1);

                    count = temp.Count();

                    if (count > 0)
                        중복값 = temp.SingleOrDefault().key;
                }
                else
                {
                    var temp = sourceDt.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows)
                                       .AsEnumerable()
                                       .Select(x => x[column].ToString())
                                       .GroupBy(x => x, y => y, (x, y) => new { key = x, count = y.Count() })
                                       .Where(x => x.count > 1);

                    count = temp.Count();
                    
                    if (count > 0)
                        중복값 = temp.SingleOrDefault().key;
                }

                if (count > 0)
                {
                    Global.MainFrame.ShowMessage(string.Format("{0} 의 값이 중복 되었습니다. (중복값 : {1})", columnName, 중복값));
                    return true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }
    }
}
