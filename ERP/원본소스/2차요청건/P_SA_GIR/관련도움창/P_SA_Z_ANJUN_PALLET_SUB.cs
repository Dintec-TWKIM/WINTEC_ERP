using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.Common.Forms;
using Duzon.Common.Util;

namespace sale.관련도움창
{
    public partial class P_SA_Z_ANJUN_PALLET_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 멤버필드

        string 공장코드 = Global.MainFrame.LoginInfo.CdPlant;
        DataRow[] 의뢰rows = null;

        #endregion

        #region -> 초기화

        #region -> 생성자
        public P_SA_Z_ANJUN_PALLET_SUB(DataRow[] _의뢰rows, string 공장)
        {
            try
            {
                InitializeComponent();

                공장코드 = 공장;
                의뢰rows = _의뢰rows;


            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                InitGrid의뢰();
                InitGrid팔레트();

                btn저장.Click += new EventHandler(btn저장_Click);
                m_btnApply.Click += new EventHandler(OnApply);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitGrid의뢰
        private void InitGrid의뢰()
        {
            _flex의뢰.BeginSetting(1, 1, false);

            _flex의뢰.SetCol("NO_GIR", "의뢰번호", 100);
            _flex의뢰.SetCol("SEQ_GIR", "의뢰항번", 100);
            _flex의뢰.SetCol("CD_ITEM", "품목코드", 100);
            _flex의뢰.SetCol("NM_ITEM", "품목명", 100);
            _flex의뢰.SetCol("STND_ITEM", "규격", 100);
            _flex의뢰.SetCol("UNIT", "단위", 100);
            _flex의뢰.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰.SettingVersion = "0.0.0.1";
            _flex의뢰.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flex의뢰.AfterRowChange += new RangeEventHandler(_flex의뢰_AfterRowChange);
            _flex의뢰.LoadUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex의뢰");

            _flex저장.BeginSetting(1, 1, false);
            _flex저장.SetCol("NO_PALLETLABEL", "팔레트번호", 100);
            _flex저장.SetCol("CD_ITEM", "품목코드", 100);
            _flex저장.SetCol("NM_ITEM", "품목명", 100);
            _flex저장.SetCol("NO_BOXLABEL", "BOX라벨번호", 100);
            _flex저장.SetCol("NO_LOT", "LOT번호", 100);
            _flex저장.SetCol("QT_PACK", "포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex저장.SettingVersion = "0.0.0.1";
            _flex저장.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flex저장.LoadUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex저장");
        }
        #endregion

        #region -> InitGrid팔레트
        private void InitGrid팔레트()
        {
            _flex팔레트H.BeginSetting(1, 1, false);
            _flex팔레트H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex팔레트H.SetCol("NO_PALLETLABEL", "팔레트번호", 100);
            _flex팔레트H.SetCol("CD_ITEM", "품목코드", 100);
            _flex팔레트H.SetCol("NM_ITEM", "품목명", 100);
            _flex팔레트H.SetCol("QT_PACK", "포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex팔레트H.SettingVersion = "0.0.0.1";
            _flex팔레트H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flex팔레트H.AfterRowChange += new RangeEventHandler(_flex팔레트H_AfterRowChange);
            _flex팔레트H.ValidateEdit += new ValidateEditEventHandler(_flex팔레트H_ValidateEdit);
            _flex팔레트H.LoadUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex팔레트H");

            _flex팔레트L.BeginSetting(1, 1, false);
            _flex팔레트L.SetCol("NO_BOXLABEL", "BOX라벨번호", 100);
            _flex팔레트L.SetCol("NO_LOT", "LOT번호", 100);
            _flex팔레트L.SetCol("QT_PACK", "포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex팔레트L.SettingVersion = "0.0.0.1";
            _flex팔레트L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flex팔레트L.LoadUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex팔레트L");
        }
        #endregion

        #region -> InitPaint
        protected override void InitPaint()
        {
            base.InitPaint();

            DataTable 의뢰dt = new DataTable();

            의뢰dt.Columns.Add("NO_GIR", typeof(string));
            의뢰dt.Columns.Add("SEQ_GIR", typeof(decimal));
            의뢰dt.Columns.Add("CD_ITEM", typeof(string));
            의뢰dt.Columns.Add("NM_ITEM", typeof(string));
            의뢰dt.Columns.Add("STND_ITEM", typeof(string));
            의뢰dt.Columns.Add("UNIT", typeof(string));
            의뢰dt.Columns.Add("QT_GIR", typeof(decimal));
            의뢰dt.Columns.Add("YN_GIR", typeof(string));

            foreach (DataRow row in 의뢰rows)
            {
                DataRow newRow = 의뢰dt.NewRow();
                newRow["NO_GIR"] = row["NO_GIR"];
                newRow["SEQ_GIR"] = row["SEQ_GIR"];
                newRow["CD_ITEM"] = row["CD_ITEM"];
                newRow["NM_ITEM"] = row["NM_ITEM"];
                newRow["STND_ITEM"] = row["STND_ITEM"];
                newRow["UNIT"] = row["UNIT"];
                newRow["QT_GIR"] = row["QT_GIR"];
                newRow["YN_GIR"] = "N";
                의뢰dt.Rows.Add(newRow);
            }


            DataSet ds = DBHelper.GetDataSet("UP_SA_Z_ANJUN_GIR_LABEL_S", new object[] { MA.Login.회사코드, 공장코드 });
            _flex저장.Binding = ds.Tables[2];
            _flex팔레트L.Binding = ds.Tables[1];
            _flex팔레트H.Binding = ds.Tables[0];
            _flex의뢰.Binding = 의뢰dt;

        }
        #endregion

        #endregion

        #region -> 화면내버튼 클릭

        #region -> 적용버튼클릭

        #region -> 적용

        private void OnApply(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flex팔레트H.HasNormalRow) return;

                if (D.GetString(_flex의뢰["YN_GIR"]) == "Y")
                {
                    Global.MainFrame.ShowMessage("이미 적용된 데이터입니다.");
                    return;
                }

                string CD_ITEM = D.GetString(_flex의뢰["CD_ITEM"]);

                DataRow[] dr = _flex팔레트H.DataTable.Select("S = 'Y' AND CD_ITEM = '" + CD_ITEM + "'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                decimal 의뢰수량 = D.GetDecimal(_flex의뢰["QT_GIR"]);
                decimal 포장수량 = 0;

                foreach (DataRow row in dr)
                {
                    포장수량 += D.GetDecimal(row["QT_PACK"]);
                }

                if (의뢰수량 != 포장수량)
                {
                    Global.MainFrame.ShowMessage("의뢰수량과 포장수량의 합이 일치하지 않습니다.");
                    return;
                }

                foreach (DataRow row in dr)
                {
                    string Filter = string.Format("NO_PALLETLABEL = '{0}' AND CD_ITEM = '{1}'", D.GetString(row["NO_PALLETLABEL"]), D.GetString(row["CD_ITEM"]));

                    DataRow[] 저장rows = _flex저장.DataTable.Select(Filter);

                    foreach (DataRow row2 in 저장rows)
                    {
                        row2["NO_GIR"] = D.GetString(_flex의뢰["NO_GIR"]);
                        row2["SEQ_GIR"] = D.GetDecimal(_flex의뢰["SEQ_GIR"]);
                    }

                    row["YN_GIR"] = "Y";
                }

                _flex의뢰["YN_GIR"] = "Y";

                if (!_flex팔레트H.HasNormalRow) _flex팔레트L.RowFilter = "CD_ITEM = '!@#$%^'";

                Global.MainFrame.ShowMessage("적용되었습니다.");

                _flex저장.SumRefresh();
                _flex의뢰.SumRefresh();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장

        private void btn저장_Click(object sender, System.EventArgs e)
        {
            try
            {
                DataTable dt = _flex저장.GetChanges();

                if (dt == null || dt.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage("적용된내용이없습니다.");
                    return;
                }

                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameUpdate = "UP_SA_Z_ANJUN_GIR_LABEL_U";
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_LINE", "NO_PALLETLABEL", "NO_BOXLABEL", "NO_BOX", "NO_GIR", "SEQ_GIR", "ID_UPDATE" };

                DBHelper.Save(si);

                Global.MainFrame.ShowMessage("저장되었습니다.");

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region -> OnClosed(화면이 닫힐때)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flex의뢰.SaveUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex의뢰");
            _flex팔레트H.SaveUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex팔레트H");
            _flex팔레트L.SaveUserCache("P_SA_Z_ANJUN_PALLET_SUB_flex팔레트L");
        }
        #endregion

        #endregion

        #region -> 그리드 이벤트

        #region -> _flex팔레트H_AfterRowChange

        private void _flex팔레트H_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                string Filter = string.Format("NO_PALLETLABEL = '{0}' AND CD_ITEM = '{1}'", D.GetString(_flex팔레트H["NO_PALLETLABEL"]), D.GetString(_flex팔레트H["CD_ITEM"]));
                _flex팔레트L.RowFilter = Filter;
                Filter = string.Format("NO_GIR = '{0}' AND SEQ_GIR = {1}", D.GetString(_flex의뢰["NO_GIR"]), D.GetDecimal(_flex의뢰["SEQ_GIR"]));
                _flex저장.RowFilter = Filter;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        private void _flex의뢰_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {

                
                string Filter2 = string.Format("CD_ITEM = '{0}' AND YN_GIR = 'N' ", D.GetString(_flex의뢰["CD_ITEM"]));
                _flex팔레트H.RowFilter = Filter2;
                if (!_flex팔레트H.HasNormalRow) _flex팔레트L.RowFilter = "CD_ITEM = '!@#$%^'";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }



        #region -> _flex팔레트H_ValidateEdit

        private void _flex팔레트H_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                //FlexGrid _flex = sender as FlexGrid;
                //if (_flex == null) return;

                //string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                //if (ColName == "S")
                //{
                //    string Filter = string.Format("NO_PALLETLABEL = '{0}' AND CD_ITEM = '{1}'", D.GetString(_flex팔레트H["NO_PALLETLABEL"]), D.GetString(_flex팔레트H["CD_ITEM"]));

                //    DataRow[] rows = _flex팔레트L.DataTable.Select();

                //    string NO_GIR = D.GetString(_flex의뢰["NO_GIR"]);
                //    decimal SEQ_GIR = D.GetDecimal(_flex의뢰["SEQ_GIR"]);

                //    foreach (DataRow row in rows)
                //    {
                //        row["NO_GIR"] = NO_GIR;
                //        row["SEQ_GIR"] = SEQ_GIR;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

    }
}
