using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_MA_IPLIST : PageBase
    {

        #region 생성자 및 초기화
        P_MA_IPLIST_BIZ _biz = new P_MA_IPLIST_BIZ();

        public P_CZ_MA_IPLIST()
        {
            StartUp.Certify(this);
            InitializeComponent();

        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {

        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(2, 1, true);

            this._flex.SetCol("NM_COMPANY", "회사명", 80);
            this._flex.SetCol("CD_COMPANY", "회사코드", false);
            this._flex.SetCol("NM_CC", "코스트센터", 80);
            this._flex.SetCol("SEQ", "SEQ", false);
            this._flex.SetCol("NM_KOR", "사용자", 80);
            this._flex.SetCol("NO_EMP", "사번", false);
            this._flex.SetCol("IP_ADDR", "아이피", 110, true, typeof(string));
            this._flex.SetCol("MAC_ADDR", "MAC", 130);

            this._flex.SetCol("NM_PC", "이름", 130);
            this._flex.SetCol("DT_PC", "지급일", 100);
            this._flex.SetCol("SPEC", "사양", 200);
            this._flex.SetCol("KEY_PC", "Key&Mouse", 130);
            this._flex.SetCol("SERIAL_PC", "Serial", 100);
            this._flex.SetCol("OFFICE_KEY", "Office", 200);
            this._flex.SetCol("OS_KEY", "Windows", 200);
            this._flex.SetCol("DC_RMK", "비고", 200);



            //this._flex.SetCol("NM_COMPANY_1", "회사명1", false);
            //this._flex.SetCol("CD_COMPANY_1", "회사코드1", false);
            //this._flex.SetCol("NM_KOR_1", "사용자1", false);
            //this._flex.SetCol("NM_COMPANY_2", "회사명2", false);
            //this._flex.SetCol("CD_COMPANY_2", "회사코드2", false);
            //this._flex.SetCol("NM_KOR_2", "사용자2", false);
            //this._flex.SetCol("NM_COMPANY_3", "회사명3", false);
            //this._flex.SetCol("CD_COMPANY_3", "회사코드3", false);
            //this._flex.SetCol("NM_KOR_3", "사용자3", false);
            //this._flex.SetCol("NM_COMPANY_4", "회사명4", false);
            //this._flex.SetCol("CD_COMPANY_4", "회사코드4", false);
            //this._flex.SetCol("NM_KOR_4", "사용자4", false);
            //this._flex.SetCol("NM_COMPANY_5", "회사명5", false);
            //this._flex.SetCol("CD_COMPANY_5", "회사코드5", false);
            //this._flex.SetCol("NM_KOR_5", "사용자5", false);
            //this._flex.SetCol("NM_COMPANY_6", "회사명6", false);
            //this._flex.SetCol("CD_COMPANY_6", "회사코드6", false);
            //this._flex.SetCol("NM_KOR_6", "사용자6", false);
            //this._flex.SetCol("NM_COMPANY_7", "회사명7", false);
            //this._flex.SetCol("CD_COMPANY_7", "회사코드7", false);
            //this._flex.SetCol("NM_KOR_7", "사용자7", false);
            //this._flex.SetCol("NM_COMPANY_8", "회사명8", false);
            //this._flex.SetCol("CD_COMPANY_8", "회사코드8", false);
            //this._flex.SetCol("NM_KOR_8", "사용자8", false);
            //this._flex.SetCol("NM_COMPANY_9", "회사명9", false);
            //this._flex.SetCol("CD_COMPANY_9", "회사코드9", false);
            //this._flex.SetCol("NM_KOR_9", "사용자9", false);
            //this._flex.SetCol("NM_COMPANY_10", "회사명10", false);
            //this._flex.SetCol("CD_COMPANY_10", "회사코드10", false);
            //this._flex.SetCol("NM_KOR_10", "사용자10", false);
            this._flex.SetCol("ID_INSERT", "등록자", false);
            this._flex.SetCol("DTS_INSERT", "등록일자", false);
            this._flex.SetCol("ID_UPDATE", "수정자", false);
            this._flex.SetCol("DTS_UPDATE", "수정일자",false);            
            

            //this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            //this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            //this._flex.Cols["IP_ADDR"].EditMask = "999.999.99#.99#";
            //this._flex.Cols["MAC_ADDR"].EditMask = "CC:CC:CC:CC:CC:CC";

            //this._flex.VerifyPrimaryKey = new string[] { "CD_COMPANY", "NM_KOR", "IP_ADDR" };
            //this._flex.VerifyPrimaryKey = new string[] { "CD_COMPANY"};




            this._flex.SetCodeHelpCol("NM_COMPANY", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY", "NM_COMPANY" }, new string[] { "CD_COMPANY", "NM_COMPANY" });

            //P_MA_EMP_SUB : NM_KOR 한글이름, NO_EMP 사원번호
            this._flex.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });

            //// 회사
            //this._flex.SetCodeHelpCol("NM_COMPANY_1", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_1", "NM_COMPANY_1" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_2", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_2", "NM_COMPANY_2" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_3", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_3", "NM_COMPANY_3" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_4", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_4", "NM_COMPANY_4" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_5", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_5", "NM_COMPANY_5" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_6", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_6", "NM_COMPANY_6" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_7", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_7", "NM_COMPANY_7" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_8", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_8", "NM_COMPANY_8" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_9", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_9", "NM_COMPANY_9" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //this._flex.SetCodeHelpCol("NM_COMPANY_10", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY_10", "NM_COMPANY_10" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            //
            //// 사용자
            //this._flex.SetCodeHelpCol("NM_KOR_1", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_1", "NO_EMP_1" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_2", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_2", "NO_EMP_2" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_3", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_3", "NO_EMP_3" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_4", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_4", "NO_EMP_4" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_5", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_5", "NO_EMP_5" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_6", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_6", "NO_EMP_6" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_7", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_7", "NO_EMP_7" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_8", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_8", "NO_EMP_8" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_9", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_9", "NO_EMP_9" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_KOR_10", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR_10", "NO_EMP_10" }, new string[] { "NM_KOR", "NO_EMP" });


            this._flex[0, _flex.Cols["NM_PC"].Index] = "PC";
            this._flex[0, _flex.Cols["DT_PC"].Index] = "PC";
            this._flex[0, _flex.Cols["SPEC"].Index] = "PC";
            this._flex[0, _flex.Cols["SERIAL_PC"].Index] = "PC";
            this._flex[0, _flex.Cols["KEY_PC"].Index] = "PC";
            this._flex[0, _flex.Cols["OFFICE_KEY"].Index] = "PC";
            this._flex[0, _flex.Cols["OS_KEY"].Index] = "PC";
            this._flex[0, _flex.Cols["MAC_ADDR"].Index] = "PC";

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "22.06.03.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {

        }

        #endregion 생성자 및 초기화


        #region 조회
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

                string txt회사 = string.Empty;
                string txt아이피 = this.meb아이피.Text.Replace("_","");
                string txtMAC = this.mebMAC.Text.Replace("_","");
                txtMAC = txtMAC.Replace(":","");

                if (txt아이피.Length == 3)
                {
                    txt아이피 = "";
                }

                if (txtMAC.Length == 5)
                {
                    txtMAC = "";
                }


                

                this._flex.Binding = this._biz.Search(new object[] { 
                                                                      txt아이피,
                                                                      this.ctx사용자.CodeValue,
                                                                      txtMAC,
                                                                      this.ctx회사.CodeValue,
                                                                       });


                for(int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if (_flex.Rows[i]["IP_ADDR"].ToString().StartsWith("192.168.0."))
                        SetGrid.CellRed(_flex, i, _flex.Cols["IP_ADDR"].Index);
                    else if (_flex.Rows[i]["IP_ADDR"].ToString().StartsWith("192.168.2."))
                        SetGrid.CellBlue(_flex, i, _flex.Cols["IP_ADDR"].Index);
                    else if (_flex.Rows[i]["IP_ADDR"].ToString().StartsWith("192.168.3."))
                        SetGrid.CellGreen(_flex, i, _flex.Cols["IP_ADDR"].Index);
                }


                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 조회


        #region 추가
        protected override bool BeforeAdd()
        {
            return base.BeforeAdd();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTB = _flex.DataTable;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;

                _flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 추가


        #region 저장
        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            if (!BeforeSave()) return;
            if (!MsgAndSave(PageActionMode.Save)) return;

            ShowMessage(PageResultMode.SaveGood);
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;

            try
            {
                _flex["ID_INSERT"] = Global.MainFrame.LoginInfo.UserName;
                _flex["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserName;

                DataTable dt = this._flex.GetChanges();

                if (!_biz.Save(dt))
                {
                    return false;
                }

                this._flex.AcceptChanges();

                this._flex.Focus();
            }
            catch (Exception ex)
            {
                return Util.ShowMessage(Util.GetErrorMessage(ex.Message));
            }

            return true;
        }
        #endregion 저장


        #region 삭제
        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;

                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 삭제

    }
}
