using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_LEAD_TIME_RPT : PageBase
    {
        P_CZ_SA_LEAD_TIME_RPT_BIZ _biz;

        public P_CZ_SA_LEAD_TIME_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this._biz = new P_CZ_SA_LEAD_TIME_RPT_BIZ();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
        }

        private void InitGrid()
        {
            try
            {
                #region Header
                this._flex.BeginSetting(1, 1, false);

                this._flex.SetCol("NO_KEY", "파일번호", 80);
                this._flex.SetCol("LN_PARTNER", "매출처", 80);
                this._flex.SetCol("NM_SUPPLIER", "매입처", 80);
                this._flex.SetCol("NM_SALEGRP", "영업그룹", 80);
                this._flex.SetCol("NM_ITEMGRP", "유형", 80);
                this._flex.SetCol("QT_ITEM", "종수", 40, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex.SetCol("ID_SALES", "영업담당", 80);
                this._flex.SetCol("NM_SALES", "영업담당", 80);
                this._flex.SetCol("ID_TYPIST", "입력지원", 80);
                this._flex.SetCol("NM_TYPIST", "입력지원", 80);
                this._flex.SetCol("01", "고객문의등록", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("02", "매입문의등록", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("03", "문의서검토", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("04", "매입가등록", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("05", "견적작성", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("06", "견적제출", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("07", "고객문의클로징", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("NM_EXCH", "통화명", 60);
                this._flex.SetCol("AM_EX_Q", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_KR_Q", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("TP_CLOSE", "클로징사유", false);

                this._flex.SetDataMap("TP_CLOSE", Global.MainFrame.GetComboDataCombine("N;CZ_SA00022"), "CODE", "NAME");

                this._flex.Cols["01"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["01"].Format = "####/##/##/##:##:##";

                this._flex.Cols["02"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["02"].Format = "####/##/##/##:##:##";

                this._flex.Cols["03"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["03"].Format = "####/##/##/##:##:##";
                
                this._flex.Cols["04"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["04"].Format = "####/##/##/##:##:##";

                this._flex.Cols["05"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["05"].Format = "####/##/##/##:##:##";

                this._flex.Cols["06"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["06"].Format = "####/##/##/##:##:##";

                this._flex.Cols["07"].TextAlign = TextAlignEnum.CenterCenter;
                this._flex.Cols["07"].Format = "####/##/##/##:##:##";

                this._flex.ExtendLastCol = true;

                this._flex.SettingVersion = "0.0.0.1";
                this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                this._flex.SetExceptSumCol("QT_ITEM", "AM_EX_Q");
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp조회기간.StartDate = this.MainFrameInterface.GetDateTimeToday().AddMonths(-1);
                this.dtp조회기간.EndDate = this.MainFrameInterface.GetDateTimeToday();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.dtp조회기간.StartDateToString,
                                                                     this.dtp조회기간.EndDateToString,
                                                                     this.txt파일번호.Text,
                                                                     this.ctx영업담당자.CodeValue,
                                                                     this.ctx매출처.CodeValue,
																	 this.ctx영업그룹.CodeValue,
                                                                     this.ctx매입처.CodeValue });

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


    }
}
