using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;



using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.BizOn.Erpu.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    public partial class P_CZ_INTER_PRICE : PageBase
    {

        #region 생성자

        public P_CZ_INTER_PRICE()
        {
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { _flex_H, _flex_L };
            _flex_H.DetailGrids = new FlexGrid[] { _flex_L };

        }

        #endregion

        //초기화
        P_CZ_INTER_PRICE_BIZ _biz = new P_CZ_INTER_PRICE_BIZ();


        protected override void InitLoad()
        {
            base.InitLoad();
            InitGridH();
            InitGridL();
            InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            dtpFrom.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtpFrom.Text = Global.MainFrame.GetStringFirstDayInMonth;

            dtpTo.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtpTo.Text = Global.MainFrame.GetStringToday;
        }

        //메인 그리드
        private void InitGridH()
        {

            _flex_H.BeginSetting(1, 1, true);

            //_flex_H.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flex_H.SetCol("ITEM_CODE", "분류명", 180, false);
            //_flex_H.SetCol("STND_ITEM", "규격", 100, true);
            //_flex_H.SetCol("STND_DETAIL_ITEM", "세부규격", 100, true);
            _flex_H.SetCol("UNIT_IM", "단위", 70);
            _flex_H.SetCol("INSERT_DATE", "입력일자", 100);
            _flex_H.SetCol("PRICEH_SEQ", "순번", 100);
            //_flex_H.SetCol("CD_ITEM", "공장품목", 50, true);
            //_flex_H.SetCol("NM_ITEM", "공장품목명", 130, true);


            _flex_H.Cols["ITEM_CODE"].TextAlign = TextAlignEnum.LeftCenter;
            //_flex_H.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
            //_flex_H.Cols["STND_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
            //_flex_H.Cols["STND_DETAIL_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
            //_flex_H.Cols["UNIT_IM"].TextAlign = TextAlignEnum.CenterCenter;
            _flex_H.Cols["INSERT_DATE"].TextAlign = TextAlignEnum.RightCenter;
            //_flex_H.Cols["NM_ITEM"].TextAlign = TextAlignEnum.LeftCenter;
            //_flex_H.SetDummyColumn("S");

            _flex_H.AddRow += new EventHandler(OnToolBarAddButtonClicked);

            //팝업함수정보가 필요 
            this._flex_H.SetCodeHelpCol("NM_ITEM", HelpID.P_MA_ITEM_SUB, ShowHelpEnum.Always, new string[] { "NM_ITEM", "CD_ITEM" }, new string[] { "NM_ITEM", "CD_ITEM" });


            _flex_H.VerifyPrimaryKey = new string[] { "ITEM_CODE" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flex_H.VerifyAutoDelete = new string[] { "ITEM_CODE" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex_H.VerifyNotNull = new string[] { "ITEM_CODE" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.

            _flex_H.SettingVersion = "0.0.0.1";
            _flex_H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

        }

        //상세 그리드
        private void InitGridL()
        {

            _flex_L.BeginSetting(1, 1, true);

            
            _flex_L.SetCol("PRICEH_SEQ", " 헤더순번", 100, true);
            _flex_L.SetCol("INSERT_DATE", "일시", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex_L.SetCol("CBOT", "선물가격", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("COST", "스프레드", 100);
            _flex_L.SetCol("FOB", "FOB", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("USD", "USD", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("JPY", "JPY", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("EUR", "EUR", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("NOTE", "비고", 250);
            _flex_L.SetCol("WTI", "WTI(US$/BBL)", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("DUBAI", "두바이(US$/BBL)", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex_L.SetCol("PRICEL_SEQ", " 디테일순번", 100);


            _flex_L.Cols["INSERT_DATE"].TextAlign = TextAlignEnum.LeftCenter;
            _flex_L.Cols["CBOT"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["COST"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["FOB"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["USD"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["JPY"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["EUR"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["WTI"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["DUBAI"].TextAlign = TextAlignEnum.RightCenter;
            _flex_L.Cols["NOTE"].TextAlign = TextAlignEnum.LeftCenter;

            _flex_L.Cols["CBOT"].Format = "0.00";
            _flex_L.Cols["COST"].Format = "0.00";
            _flex_L.Cols["FOB"].Format = "0.00";
            _flex_L.Cols["USD"].Format = "0.00";
            _flex_L.Cols["JPY"].Format = "0.00";
            _flex_L.Cols["EUR"].Format = "0.00";
            _flex_L.Cols["WTI"].Format = "0.00";
            _flex_L.Cols["DUBAI"].Format = "0.00";

            //팝업함수정보가 필요 
            //this._flex_L.SetCodeHelpCol("EXCHANGE_KIND", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "EXCHANGE_KIND", "CD_FIELD" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            //this._flex_L.SetCodeHelpCol("EXCHANGE_KIND", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "EXCHANGE_KIND", "CD_FIELD" }, new string[] { "CODE", "NAME" });
            //this._flex.SetCodeHelpCol("FG_SL", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "FG_SL", "NM_FG_SL" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            //_flex_L.VerifyPrimaryKey = new string[] { "PRICEH_SEQ" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.

            _flex_L.VerifyPrimaryKey = new string[] { "INSERT_DATE" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            //_flex_L.VerifyAutoDelete = new string[] { "INSERT_DATE" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex_L.VerifyNotNull = new string[] { "INSERT_DATE" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.


            _flex_L.SettingVersion = "0.0.0.1";
            _flex_L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            //그리드이벤트

            _flex_H.AfterRowChange += new RangeEventHandler(_flex_H_AfterRowChange);
            
            //_flex_H.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_H_BeforeCodeHelp);
            //_flex_H.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_H_AfterCodeHelp);

            //화면버튼이벤트
            btn_추가.Click += new EventHandler(btn_추가_Click);
            btn_삭제.Click += new EventHandler(btn_삭제_Click);
           

        }


        #region 점검 메소드

        protected override bool BeforeSearch()
        {
            //if (!base.BeforeSearch() ) return false;   데이터가 많으면 품목정보를 필수 값으로 걸어줘야 함
            return true;
        }


        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            return true;
        }

        #endregion


        #region 상단 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                _flex_H.Binding = _biz.SearchH(ctx품목정보.Text);


                if (!_flex_H.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 추가버튼

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd()) return false;
            return true;
        }


        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _flex_H.Rows.Add();
                _flex_H.Row = _flex_H.Rows.Count - _flex_H.Rows.Fixed;
                _flex_H["CD_ITEM"] = ctx품목정보.Text;
                _flex_H.AddFinished();
                _flex_H.Focus();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 상단 삭제버튼

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeDelete() || !_flex_H.HasNormalRow) return;

                _flex_H.Rows.Remove(_flex_H.Row);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 상단 저장버튼

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");

                _biz.SearchH(ctx품목정보.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool Verify()
        {
            if (!base.Verify()) return false;
            return true;
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify()) return false;

            _biz.Save(_flex_H.GetChanges(), _flex_L.GetChanges());
            _flex_H.AcceptChanges();
            _flex_L.AcceptChanges();
            return true;
        }

        #endregion

        #region 화면버튼


        void btn_추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex_H.HasNormalRow) return;

                _flex_L.Rows.Add();
                _flex_L.Row = _flex_L.Rows.Count - _flex_L.Rows.Fixed;
                
                _flex_L["PRICEH_SEQ"] = _flex_H["PRICEH_SEQ"];

                _flex_L.AddFinished();
                _flex_L.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void btn_삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex_L.HasNormalRow) return;
                _flex_L.Rows.Remove(_flex_L.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 그리드이벤트

        void _flex_H_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                string 헤더순번 = D.GetString(_flex_H["PRICEH_SEQ"]);

                string Filter = "PRICEH_SEQ = '" + 헤더순번 + "'";
                if (_flex_H.DetailQueryNeed)
                {
                    _flex_L.Redraw = false; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다
                    DataTable dt = _biz.SearchLDetail(헤더순번, dtpFrom.Text, dtpTo.Text);
                    _flex_L.BindingAdd(dt, Filter);
                    _flex_L.Redraw = true; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다
                }

                _flex_L.RowFilter = Filter;
            }

            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        /*void _flex_H_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            //MessageBox.Show(_flex_H.Cols[e.Col].Name);

            {
                try
                {
                    switch (_flex_H.Cols[e.Col].Name)
                    {
                            
                        case "CD_ITEM":
                            
                            string SEQ_헤더 = D.GetString(_flex_H["PRICEH_SEQ"]);

                            MessageBox.Show("1");


                            string Filter = "PRICEH_SEQ = '" + SEQ_헤더 + "'";
                            DataTable dt = _biz.SearchL(SEQ_헤더);
                            _flex_L.BindingAdd(dt, Filter);

                            _flex_L.RowFilter = Filter;

                            break;

                        case "품목명":
                            MessageBox.Show("2");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.MsgEnd(ex);
                }
            }

        }
         */

        private void _flex_H_AfterCodeHelp(object sender, EventArgs e)
        {
            try
            {
                if (!_flex_H.HasNormalRow) return;

                string colName = _flex_H.Cols[_flex_H.Col].Name; //컬럼명을 가져온다
                string SEQ_헤더 = D.GetString(_flex_H["PRICEH_SEQ"]);
                string Filter = "PRICEH_SEQ = '" + SEQ_헤더 + "'";

                switch (colName)
                {
                    case "S":
    

                        if (_flex_H.DetailQueryNeed)
                        {

                            DataTable dt = _biz.SearchL(SEQ_헤더);
                            _flex_L.BindingAdd(dt, Filter);

                        }

                        _flex_L.RowFilter = Filter;
                        break;


                }





            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }


        #endregion


        #region 그리드 제어 이벤트

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {


            try
            {
                switch (_flex_H.Cols[e.Col].Name)
                {
                    case "NM_ITEM":
                        string CD_FILED = D.GetString(_flex_H["CD_FILED"]);
                        string NM_ITEM = "NM_ITEM";
                        e.Parameter.UserParams = "품목도움창;P_MA_ITEM_SUB;" + CD_FILED + ";" + NM_ITEM;
                        break;

                }
                
                switch (_flex_L.Cols[e.Col].Name)
                {
                    case "EXCHANGE_KIND":
                        //string EXCHANGE_KIND = D.GetString(_flex_H["EXCHANGE_KIND"]);
                        //string CD_FIELD = "PU_C000004";
                        //e.Parameter.UserParams = "품목도움창;P_MA_CODE_SUB;" + EXCHANGE_KIND + ";" + CD_FIELD;

                        e.Parameter.P41_CD_FIELD1 = "PU_C000004";

                        //e.Parameter.P41_CD_FIELD1 = "PU_C000004";
                        //e.Parameter.UserParams = "환종";
                        break;

                        

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion



    }
}
