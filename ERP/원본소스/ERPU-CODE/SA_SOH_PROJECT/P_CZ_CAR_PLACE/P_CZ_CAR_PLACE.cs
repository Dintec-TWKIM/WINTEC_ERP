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
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    public partial class P_CZ_CAR_PLACE : PageBase
    {

        P_CZ_CAR_PLACE_BIZ _biz = new P_CZ_CAR_PLACE_BIZ();

        public P_CZ_CAR_PLACE()
        {
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flex };
        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        private void InitGrid()
        {

            _flex.BeginSetting(1, 1, true);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("PLACE_NAME", "상/하차지명", 150, false);
            _flex.SetCol("NM_PLANT", "공장코드명", 150, true);
            _flex.SetCol("INSERT_DATE", "생성일자", 100);
            _flex.SetCol("PLACE_CODE", "상하차지", 130, false);
            _flex.SetCol("CD_PLANT", "공장코드", 100, false);
            _flex.SetCol("CD_COMPANY", "회사", 150, false);
            _flex.SetCol("NM_COMPANY", "회사명", 150, false);

            _flex.Cols["PLACE_NAME"].TextAlign = TextAlignEnum.LeftCenter;
            _flex.Cols["NM_PLANT"].TextAlign = TextAlignEnum.LeftCenter;
            _flex.Cols["NM_COMPANY"].TextAlign = TextAlignEnum.LeftCenter;

            _flex.Cols["INSERT_DATE"].Visible = false;
            _flex.Cols["PLACE_CODE"].Visible = false;
            _flex.Cols["CD_COMPANY"].Visible = false;



            _flex.VerifyPrimaryKey = new string[] { "PLACE_NAME" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyAutoDelete = new string[] { "PLACE_NAME" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyNotNull = new string[] { "PLACE_NAME" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.



            //팝업함수정보가 필요
            this._flex.SetCodeHelpCol("NM_PLANT", HelpID.P_MA_PLANT_SUB, ShowHelpEnum.Always, new string[] { "NM_PLANT", "CD_PLANT" }, new string[] { "NM_PLANT", "CD_PLANT" });

            _flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.SettingVersion = "0.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            
        }

        protected override void InitPaint()
        {
            base.InitPaint();

        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (!BeforeSearch()) return;


                _flex.Binding = _biz.Search(ctx상하차지명.Text, bpc공장명.CodeValue);

                if (!_flex.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);

                //MsgControl.CloseMsg;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 상단 추가버튼

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex["PLACE_NAME"] = ctx상하차지명.Text;
                _flex["CD_PLANT"] = bpc공장명.CodeValue;
                _flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                _flex["NM_COMPANY"] = Global.MainFrame.LoginInfo.CompanyName;
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();


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
                if (!BeforeDelete() || !_flex.HasNormalRow) return;

                DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
                if (result == DialogResult.Yes)
                {
                    foreach (DataRow row in dr)
                    {
                        _biz.Delete(D.GetString(row["PLACE_CODE"]), D.GetString(row["PLACE_NAME"]), D.GetString(row["CD_COMPANY"]));
                    }

                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);

                    _flex.Binding = _biz.Search(ctx상하차지명.Text, bpc공장명.CodeValue);

                    if (!_flex.HasNormalRow)
                        ShowMessage(PageResultMode.SearchNoData);

                }
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

                //MessageBox.Show(cbo차량종류.SelectedValue.ToString());

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion


        #region ♪ 저장 관련     ♬

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify()) return false;

            _biz.Save(_flex.GetChanges());
            _flex.AcceptChanges();
            return true;
        }

        #endregion


        #region ♪ 화면 내 버튼  ♬

        #endregion

        #region ♪ 저장 관련     ♬

        #endregion

        #region ♪ 그리드 이벤트 ♬

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {


            try
            {
                switch (_flex.Cols[e.Col].Name)
                {

                    case "NM_PLANT":
                        string CD_PLANT = D.GetString(_flex["CD_PLANT"]);
                        string NM_PLANT = "NM_PLANT";
                        e.Parameter.UserParams = "공장도움창;P_MA_PLANT_SUB;" + CD_PLANT + ";" + NM_PLANT;
                        break;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion


        #region ♪ 기타 메서드   ♬

        #endregion

    }
}
