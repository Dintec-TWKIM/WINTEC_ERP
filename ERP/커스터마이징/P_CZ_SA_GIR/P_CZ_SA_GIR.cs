using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.Windows.Print;
using DX;
using DzHelpFormLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace cz
{
    public partial class P_CZ_SA_GIR : PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_CZ_SA_GIR_BIZ _biz;
        private FreeBinding _기본정보 = new FreeBinding(); //FreeBinding 생성
        private FreeBinding _송장정보 = new FreeBinding();

        //영업환경설정  : 수주수량 초과허용
        private bool is수주수량초과허용 = false;   //영업환경설정 : 수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
        private string 파일번호;
        private string 협조전번호;
        private string 회사코드;
        private decimal 차수;

        public P_CZ_SA_GIR()
        {
            StartUp.Certify(this);
            InitializeComponent();
            
            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.회사코드 = this.LoginInfo.CompanyCode;
            this._biz = new P_CZ_SA_GIR_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR(string 파일번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;
            this.파일번호 = 파일번호;
            this.회사코드 = this.LoginInfo.CompanyCode;
            this._biz = new P_CZ_SA_GIR_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR(string 페이지명, string 협조전번호, string 회사코드)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.PageName = 페이지명;
            this.협조전번호 = 협조전번호;
            this.회사코드 = 회사코드;
            this._biz = new P_CZ_SA_GIR_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR(string 페이지명, string 회사코드, string 협조전번호, decimal 차수)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.PageName = 페이지명;
            this.회사코드 = 회사코드;
            this.협조전번호 = 협조전번호;
            this.차수 = 차수;
            this._biz = new P_CZ_SA_GIR_BIZ(this.회사코드);
        }
        #endregion

        #region ♣ 초기화 이벤트 / 메소드
        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._품목정보 };

            this._품목정보.BeginSetting(1, 1, false);

            this._품목정보.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._품목정보.SetCol("NO_SO", "수주번호", 100);
            this._품목정보.SetCol("SEQ_SO", "수주항번", 40);
            this._품목정보.SetCol("NO_DSP", "순번", 40);
            this._품목정보.SetCol("NM_SUBJECT", "주제", false);
            this._품목정보.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); // ITEM CODE 필드 추가 !!
            this._품목정보.SetCol("NM_ITEM_PARTNER", "매출처품명", 120); // DESCRIPTION 필드 추가 !!
            this._품목정보.SetCol("CD_ITEM", "품목코드", 100);
            this._품목정보.SetCol("NM_ITEM", "품목명", 120);
            this._품목정보.SetCol("STND_ITEM", "규격", false);
            this._품목정보.SetCol("DT_DUEDATE", "납기요청일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._품목정보.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY); // 입고일자 필드 추가 !!
            this._품목정보.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._품목정보.SetCol("YN_ADD_STOCK", "추가반출", 60, true, CheckTypeEnum.Y_N);

            this._품목정보.SetCol("CD_SL", "출고창고코드", false);
            this._품목정보.SetCol("NM_SL", "출고창고", 80, true);

            this._품목정보.SetCol("NO_LOCATION", "로케이션", 80);
            this._품목정보.SetCol("NO_LOCATION_STOCK", "재고로케이션", 80);
            this._품목정보.SetCol("NM_GI", "출고형태", false);
            this._품목정보.SetCol("CD_EXCH", "통화명", false);
            this._품목정보.SetCol("QT_GIR_IM", "재고수량", false);
            this._품목정보.SetCol("QT_INV", "현재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_BL", "납부대상수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_TAX", "납부수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("CUSTOMS", "납부금액", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._품목정보.SetCol("QT_SO", "수주수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GIR", "의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GIR_STOCK", "재고의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GI", "출고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._품목정보.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._품목정보.SetCol("AM_GIR", "금액", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._품목정보.SetCol("RT_EXCH", "환율", false);
            this._품목정보.SetCol("AM_GIRAMT", "원화금액", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._품목정보.SetCol("AM_VAT", "부가세", false);
            this._품목정보.SetCol("AMT", "합계", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._품목정보.SetCol("UNIT", "재고단위", false);
            this._품목정보.SetCol("CD_SUPPLIER", "매입처코드", false);
            this._품목정보.SetCol("NM_SUPPLIER", "매입처", 120);
            this._품목정보.SetCol("GI_PARTNER", "납품처코드", false);
            this._품목정보.SetCol("LN_PARTNER", "납품처명", 120);
            this._품목정보.SetCol("NM_PROJECT", "프로젝트", false);
            this._품목정보.SetCol("CD_SALEGRP", "영업그룹", false);
            this._품목정보.SetCol("NM_SALEGRP", "영업그룹명", false);
            this._품목정보.SetCol("DC_RMK", "비고", 150);

            this._품목정보.SetCol("NO_PO_PARTNER", "매출처발주번호", 140);
            this._품목정보.SetCol("NO_POLINE_PARTNER", "매출처발주항번", 140, false, typeof(decimal), FormatTpType.QUANTITY);

            this._품목정보.SetDummyColumn("S");
            this._품목정보.SettingVersion = "1.0.0.1";
            this._품목정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._품목정보.SetExceptSumCol("UM", "RT_EXCH", "SEQ_PROJECT");

            this._품목정보.SetCodeHelpCol("NM_SL", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CODE", "NAME" }, ResultMode.FastMode);
            this._품목정보.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });

            this._품목정보.AddMyMenu = true;
            this._품목정보.AddMenuSeperator();
            ToolStripMenuItem parent = this._품목정보.AddPopup("관련 현황");
            this._품목정보.AddMenuItem(parent, "현재고조회", SubMenuClick);

            this._품목정보.Styles.Add("미납부").ForeColor = Color.Black;
            this._품목정보.Styles.Add("미납부").BackColor = Color.White;
            this._품목정보.Styles.Add("납부").ForeColor = Color.Red;
            this._품목정보.Styles.Add("납부").BackColor = Color.Yellow;

            this._품목정보.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.PageDataChanged);

            this._품목정보.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._품목정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);

            this._기본정보.ControlValueChanged += new FreeBindingEventHandler(this.ControlValueChanged);
            this._송장정보.ControlValueChanged += new FreeBindingEventHandler(this.ControlValueChanged);

            this.chk자동제출.CheckedChanged += new EventHandler(this.chk자동제출_CheckedChanged);
			this.chk검토필요.CheckedChanged += Chk검토필요_CheckedChanged;
			this.chk포장전.CheckedChanged += Check_CheckedChanged;
            this.chk포장후.CheckedChanged += Check_CheckedChanged;
            this.chk사양확인.CheckedChanged += Check_CheckedChanged;
            this.chk위험물별도포장.CheckedChanged += Check_CheckedChanged;
			this.chk미포장건.CheckedChanged += Check_CheckedChanged;

            this.btn제출.Click += new EventHandler(this.btn제출_Click);
			this.btn제출취소.Click += btn제출취소_Click;
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn삭제요청.Click += new EventHandler(this.btn삭제요청_Click);
            this.btn삭제요청취소.Click += new EventHandler(this.btn삭제요청취소_Click);
            this.btn협조전적용.Click += new EventHandler(this.btn협조전적용_Click);
            this.btn수주적용.Click += new EventHandler(this.btn수주적용_Click);
            this.btn부대비용.Click += Btn부대비용_Click;
			this.btn입출항정보.Click += Btn입출항정보_Click;
			this.btn송품서류등록.Click += Btn송품서류등록_Click;
			this.btnCPR설정.Click += BtnCPR설정_Click;
			this.btn컷오프시간설정.Click += Btn컷오프시간설정_Click;
			this.btn사진촬영적용.Click += Btn적용_Click;
			this.btn사양확인적용.Click += Btn적용_Click;
			this.btn위험물적용.Click += Btn적용_Click;
			this.btn비고생성.Click += Btn비고생성_Click;

            this.cboMainCategory.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
            this.cboSubCategory.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
            this.cbo비용청구방법.SelectionChangeCommitted += Cbo비용청구방법_SelectionChangeCommitted;
			this.cboHSCode.SelectionChangeCommitted += CboHSCode_SelectionChangeCommitted;

            this.ctx원산지.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx도착국가.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.ctx수주번호.QueryBefore += new BpQueryHandler(this.ctx수주번호검색_QueryBefore);
            this.ctx수주번호.QueryAfter += new BpQueryHandler(this.ctx수주번호검색_QueryAfter);

			this.ctx납품처.QueryBefore += Ctx납품처_QueryBefore;
            this.ctx납품처.QueryAfter += new BpQueryHandler(this.ctxDeliveryTo_QueryAfter);

            this.ctx매출처.QueryAfter += new BpQueryHandler(this.ctx매출처_QueryAfter);
            this.ctx수출자.QueryAfter += new BpQueryHandler(this.ctx수출자_QueryAfter);
            this.ctx수하인.QueryAfter += new BpQueryHandler(this.ctx수하인_QueryAfter);
            this.ctx착하통지처.QueryAfter += new BpQueryHandler(this.ctx착하통지처_QueryAfter);
            this.ctx호선번호.QueryAfter += new BpQueryHandler(this.ctx호선번호_QueryAfter);
            this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

            this.ctx청구호선.CodeChanged += new EventHandler(this.ctx청구호선_CodeChanged);

			this.txt매출처.TextChanged += txt매출처_TextChanged;
            this.txt수하인.TextChanged += new EventHandler(this.txt수하인_TextChanged);
            this.txt착하통지처.TextChanged += new EventHandler(this.txt착하통지처_TextChanged);

            this.txt매출처주소1.KeyDown += Control_KeyDown;
            this.txt매출처주소2.KeyDown += Control_KeyDown;
            this.txt수출자주소1.KeyDown += Control_KeyDown;
            this.txt수출자주소2.KeyDown += Control_KeyDown;
            this.txt수하인주소1.KeyDown += Control_KeyDown;
            this.txt수하인주소2.KeyDown += Control_KeyDown;
            this.txt착하통지처주소1.KeyDown += Control_KeyDown;
            this.txt착하통지처주소2.KeyDown += Control_KeyDown;

			this.dtp접안일시.ValueChanged += Date_ValueChanged;
            this.dtp출항일시.ValueChanged += Date_ValueChanged;
            this.dtp서류마감.ValueChanged += Date_ValueChanged;

			this.txt수취업체명.TextChanged += Text_TextChanged;
            this.txt수취인연락처.TextChanged += Text_TextChanged;
            this.txt수취인주소.TextChanged += Text_TextChanged;
            this.txt화물도착지점.TextChanged += Text_TextChanged;

			this.btn선박위치확인.Click += btn선박위치확인_Click;
			this.txt경고메시지.LinkClicked += txt경고메시지_LinkClicked;

            foreach (var c in GetAll(this, typeof(DropDownComboBox)))
            {
                DropDownComboBox dropDownComboBox = (DropDownComboBox)c;
                dropDownComboBox.MouseWheel += DropDownComboBox_MouseWheel;
            }
        }

		private void txt매출처_TextChanged(object sender, EventArgs e)
		{
            try
            {
                this._송장정보.CurrentRow["NM_PARTNER"] = this.txt매출처.Text;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void txt경고메시지_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start("msedge.exe", e.LinkText);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선박위치확인_Click(object sender, EventArgs e)
		{
            try
            {
                if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
                    return;

                string url = "http://192.168.1.140:1123/Datalastic.asmx";
                string soapAction = "http://192.168.1.140:1123/GetVesselInfo";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", soapAction);
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                XmlDocument soapEnvelope = new XmlDocument();
                soapEnvelope.LoadXml(string.Format(@"<?xml version='1.0' encoding='utf-8'?>
<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
  <soap:Body>
    <GetVesselInfo xmlns='http://192.168.1.140:1123/'>
      <imo>{0}</imo>
    </GetVesselInfo>
  </soap:Body>
</soap:Envelope>", this.ctx호선번호.CodeValue));

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelope.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(soapResult);

                        string source = doc.DocumentElement.GetElementsByTagName("GetVesselInfoResult")[0].InnerText;

                        if (!source.Contains('{'))
						{
                            this.txt경고메시지.Text += source;
                            return;
						}

                        JObject obj = (JObject)JsonConvert.DeserializeObject(source);

                        object obj1 = ((JValue)obj["data"]["dep_port"]).Value;
                        string dep_port = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dep_port_unlocode"]).Value;
                        string dep_port_unlocode = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["destination"]).Value;
                        string destination = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dest_port"]).Value;
                        string dest_port = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dest_port_unlocode"]).Value;
                        string dest_port_unlocode = (obj1 == null ? string.Empty : obj1.ToString());

                        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"); // 한국 시간대

                        obj1 = ((JValue)obj["data"]["last_position_UTC"]).Value;
                        string 업데이트시간_STR = string.Empty;
                        if (obj1 != null)
                        {
                            DateTime 업데이트시간_UTC = (DateTime)obj1;
                            DateTime 업데이트시간 = TimeZoneInfo.ConvertTimeFromUtc(업데이트시간_UTC, timeZone);
                            업데이트시간_STR = 업데이트시간.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        obj1 = ((JValue)obj["data"]["atd_UTC"]).Value;
                        string ATD_STR = string.Empty;
                        if (obj1 != null)
                        {
                            DateTime ATD_UTC = (DateTime)obj1;
                            DateTime ATD = TimeZoneInfo.ConvertTimeFromUtc(ATD_UTC, timeZone);
                            ATD_STR = ATD.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        obj1 = ((JValue)obj["data"]["eta_UTC"]).Value;
                        string ETA_STR = string.Empty;
                        if (obj1 != null)
                        {
                            DateTime ETA_UTC = (DateTime)obj1;
                            DateTime ETA = TimeZoneInfo.ConvertTimeFromUtc(ETA_UTC, timeZone);
                            ETA_STR = ETA.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        this.txt경고메시지.Text += "** 선박위치정보" + Environment.NewLine +
                                                   dep_port + " -> " + dest_port + "(" + destination + ")" + Environment.NewLine +
                                                   "- 업데이트 : " + 업데이트시간_STR + Environment.NewLine +
                                                   "- 출발 : " + ATD_STR + " (" + dep_port_unlocode + ")" + Environment.NewLine +
                                                   "- 도착 : " + ETA_STR + " (" + dest_port_unlocode + ")" + Environment.NewLine +
                                                   "- 지도보기 : " + "http://maps.google.co.kr/maps?q=" + HttpUtility.UrlEncode(obj["data"]["lat"].ToString(), Encoding.UTF8) + "," + HttpUtility.UrlEncode(obj["data"]["lon"].ToString(), Encoding.UTF8);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chk주소표시.Checked = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Date_ValueChanged(object sender, EventArgs e)
		{
            try
            {
                if (this.dtp접안일시.Checked)
                    this._기본정보.CurrentRow["DTS_ETB"] = this.dtp접안일시.Text;
                else
                    this._기본정보.CurrentRow["DTS_ETB"] = string.Empty;

                if (this.dtp출항일시.Checked)
                    this._기본정보.CurrentRow["DTS_ETD"] = this.dtp출항일시.Text;
                else
                    this._기본정보.CurrentRow["DTS_ETD"] = string.Empty;

                if (this.dtp서류마감.Checked)
                    this._기본정보.CurrentRow["DTS_DEADLINE"] = this.dtp서류마감.Text;
                else
                    this._기본정보.CurrentRow["DTS_DEADLINE"] = string.Empty;

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void CboHSCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this._송장정보.CurrentRow["CD_PRODUCT"] = D.GetString(this.cboHSCode.SelectedValue);

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk포장전.Checked == true || this.chk포장후.Checked == true)
				{
                    this.btn사진촬영적용.Enabled = true;
                    this.txt사진촬영.ReadOnly = false;
                }
                else
				{
                    this.btn사진촬영적용.Enabled = false;
                    this.txt사진촬영.ReadOnly = true;
                }
                
                if (this.chk사양확인.Checked == true)
				{
                    this.btn사양확인적용.Enabled = true;
                    this.txt사양확인.ReadOnly = false;
                }
                else
				{
                    this.btn사양확인적용.Enabled = false;
                    this.txt사양확인.ReadOnly = true;
                }
                
                if (this.chk위험물별도포장.Checked == true)
				{
                    this.btn위험물적용.Enabled = true;
                    this.txt위험물별도포장.ReadOnly = false;
                }
                else
				{
                    this.btn위험물적용.Enabled = false;
                    this.txt위험물별도포장.ReadOnly = true;
                }

                if (this.chk미포장건.Checked == true)
                {
                    this.btn미포장적용.Enabled = true;
                    this.txt미포장건.ReadOnly = false;
                }
                else
                {
                    this.btn미포장적용.Enabled = false;
                    this.txt미포장건.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn비고생성_Click(object sender, EventArgs e)
        {
            try
            {
                #region 서류처리 비고 생성
                string 서류처리 = string.Empty;

                #region 선적
                if (this.cboMainCategory.SelectedValue.ToString() == "001") // 본선선적
                {
                    string temp = string.Empty;

                    if (!string.IsNullOrEmpty(this.txt추가요청_선적.Text))
                        서류처리 += this.txt추가요청_선적.Text + Environment.NewLine;

                    if (this.회사코드 == "K100" && this._품목정보.DataTable.Select("GRP_ITEM = 'PV'").Length > 0)
                        서류처리 += "★선식협조전 선식반품대장 출력 후 진행요망" + " | ";

                    if (this.회사코드 == "K100" && this._품목정보.DataTable.Select("GRP_ITEM = 'BD'").Length > 0)
                        서류처리 += "★보세협조전 보세반품대장 출력 후 진행요망" + " | ";

                    if (this.cbo지불조건.SelectedValue.ToString() == "102")
                        서류처리 += "캐쉬수령건 " + this.cbo통화.Text + " " + this.cur외화금액.DecimalValue.ToString() + " | ";

                    if (this.dtp접안일시.Checked == true)
                        temp += "접안 : " + this.dtp접안일시.Text + " / ";
                    if (this.dtp출항일시.Checked == true)
                        temp += "출항 : " + this.dtp출항일시.Text;
                    if (!string.IsNullOrEmpty(this.txt타배선적선명.Text))
                        temp += this.txt타배선적선명.Text + " 로 타배선적 요청 ";

                    서류처리 += temp + Environment.NewLine;

                    temp = string.Empty;

                    if (!string.IsNullOrEmpty(this.cbo선적부두.Text))
                        temp += "부두 : " + this.cbo선적부두.Text + " / ";
                    if (!string.IsNullOrEmpty(this.txt대리점.Text))
                        temp += "대리점 : " + this.txt대리점.Text + " / ";
                    if (!string.IsNullOrEmpty(this.txt작업선크레인.Text))
                        temp += "작업선/크레인 : " + this.txt작업선크레인.Text;

                    서류처리 += temp + Environment.NewLine;
                }
                #endregion

                if (this.dtp서류마감.Checked == true)
                    서류처리 += "서류마감 : " + this.dtp서류마감.Text + " | ";

                if (this.chk선사서류_쉬핑마크.Checked ||
                    this.chk선사서류_인수증.Checked ||
                    this.chk선사서류_CI.Checked)
                {
                    서류처리 += "선사서류 사용 필요 (";

                    if (this.chk선사서류_쉬핑마크.Checked)
                        서류처리 += "쉬핑마크,";

                    if (this.chk선사서류_인수증.Checked)
                        서류처리 += "인수증,";

                    if (this.chk선사서류_CI.Checked)
                        서류처리 += "CI,";

                    서류처리 += ") | ";
                }

                if (this.chk기타서류부착.Checked)
                    서류처리 += "기타서류부착 요청 | ";

                if (this.chk관세환급여부.Checked)
                    서류처리 += "★관세환급 받아야 하는 건 입니다. | ";

                if (!string.IsNullOrEmpty(this.txt참조번호.Text))
                    서류처리 += "참조번호 : " + this.txt참조번호.Text + " | ";

                if (this.chk당사발행.Checked)
                    서류처리 += "적재허가서 당사발행 필요 | ";

                if (!string.IsNullOrEmpty(this.txt추가요청_서류.Text))
                    서류처리 += this.txt추가요청_서류.Text;

                this.txt서류처리.Text = 서류처리;
                this._기본정보.CurrentRow["DC_RMK"] = 서류처리;
                #endregion

                #region 포장비고 생성
                string 포장비고 = string.Empty;

                if (this.dtp서류마감.Checked == true)
                    포장비고 += "서류마감 : " + this.dtp서류마감.Text + " ";

                #region 납품처
                if (!string.IsNullOrEmpty(this.ctx납품처.CodeValue) && this.chk주소표시.Checked == true)
                {
                    if ((this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "004") || // 직접송품-택배
                        (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "010") || // 직접송품-퀵
                        (this.cboMainCategory.SelectedValue.ToString() == "002" && this.cboSubCategory.SelectedValue.ToString() == "DIR") || // 대리점전달-물류부 직접전달
                        (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "005")) // 직접송품-포워딩 직접픽업
                    {
                        포장비고 += this.txt수취인주소.Text + " ";
                        포장비고 += this.txt수취인연락처.Text + " | ";
                    }
                    else if (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "011") // 직접송품-화물
                    {
                        포장비고 += this.txt수취업체명.Text + " ";
                        포장비고 += this.txt화물도착지점.Text + " ";
                        포장비고 += this.txt수취인연락처.Text + " | ";
                    }
                }
                #endregion

                if (!string.IsNullOrEmpty(this.cbo전달시간.Text))
				{
                    if ((this.cboMainCategory.SelectedValue.ToString() == "002" && this.cboSubCategory.SelectedValue.ToString() == "DIR") || // 대리점전달-물류부 직접전달
                        (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "010")) // 직접송품-퀵
                        포장비고 += "전달일시 : " + Util.GetTo_DateStringS(this.dtp출고예정일.Text) + " " + this.cbo전달시간.Text + " | ";
                    else if ((this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "005") || // 직접송품-포워딩 직접픽업
                             (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "002")) // 직접송품-해송
                        포장비고 += "픽업일시 : " + Util.GetTo_DateStringS(this.dtp출고예정일.Text) + " " + this.cbo전달시간.Text + " | ";
                }
                
                if (this.rdo국내선적.Checked)
                    포장비고 += "국내선적 진행건 | ";
                else
                    포장비고 += "해외탁송 진행건 | ";

                if (!string.IsNullOrEmpty(this.cbo선적위치.Text))
                    포장비고 += "선적위치 : " + this.cbo선적위치.Text + " | ";

                if (this.chk포장전.Checked || this.chk포장후.Checked)
                {
                    if (this.chk포장전.Checked && this.chk포장후.Checked)
                        포장비고 += "포장 전/후 사진촬영 요청";
                    else if (this.chk포장전.Checked)
                        포장비고 += "포장 전 사진촬영 요청";
                    else if (this.chk포장후.Checked)
                        포장비고 += "포장 후 사진촬영 요청";

                    포장비고 += string.Format("({0}) | ", this.txt사진촬영.Text);
                }

                if (this.chk사양확인.Checked)
                {
                    포장비고 += "사양확인 요청";
                    포장비고 += string.Format("({0}) | ", this.txt사양확인.Text);
                }

                if (this.chk위험물별도포장.Checked)
                {
                    포장비고 += "위험물 별도 포장 요청";
                    포장비고 += string.Format("({0}) | ", this.txt위험물별도포장.Text);
                }

                if (this.chk미포장건.Checked)
                {
                    포장비고 += "미포장 건";
                    포장비고 += string.Format("({0}) | ", this.txt미포장건.Text);
                }

                if (this.chk서트부착유무확인.Checked)
                    포장비고 += "서트 부착 유무 확인 요청 | ";

                if (this.chk무지포장진행.Checked)
                    포장비고 += "무지 포장 진행 요청 | ";

                if (!string.IsNullOrEmpty(this.txt원본파일번호.Text))
                    포장비고 += this.txt원본파일번호.Text + " | ";

                if (!string.IsNullOrEmpty(this.txt추가요청_포장.Text))
                    포장비고 += this.txt추가요청_포장.Text;

                this.txt포장비고.Text = 포장비고;
                this._기본정보.CurrentRow["DC_RMK4"] = 포장비고;
                #endregion

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Chk검토필요_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk검토필요.Checked == true)
				{
                    this.chk선사서류_쉬핑마크.Enabled = true;
                    this.chk선사서류_CI.Enabled = true;
                    this.chk선사서류_인수증.Enabled = true;
                    this.chk기타서류부착.Enabled = true;

                    this.chk관세환급여부.Enabled = true;

                    this.chk당사발행.Enabled = true;

                    this.txt참조번호.ReadOnly = false;
                    this.txt추가요청_서류.ReadOnly = false;

                    this.cbo선적부두.Enabled = true;
                    this.txt대리점.ReadOnly = false;
                    this.dtp접안일시.Enabled = true;
                    this.dtp출항일시.Enabled = true;
                    this.txt추가요청_선적.ReadOnly = false;
                    this.txt타배선적선명.ReadOnly = false;
                    this.txt작업선크레인.ReadOnly = false;
                }
                else
				{
                    this.chk선사서류_쉬핑마크.Enabled = false;
                    this.chk선사서류_CI.Enabled = false;
                    this.chk선사서류_인수증.Enabled = false;
                    this.chk기타서류부착.Enabled = false;

                    this.chk관세환급여부.Enabled = false;

                    this.chk당사발행.Enabled = false;

                    this.txt참조번호.ReadOnly = true;
                    this.txt추가요청_서류.ReadOnly = true;

                    this.cbo선적부두.Enabled = false;
                    this.txt대리점.ReadOnly = true;
                    this.dtp접안일시.Enabled = false;
                    this.dtp출항일시.Enabled = false;
                    this.txt추가요청_선적.ReadOnly = true;
                    this.txt타배선적선명.ReadOnly = true;
                    this.txt작업선크레인.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            TextBoxExt textBox;
            string name, column;

            try
            {
                if (!this._품목정보.HasNormalRow) return;

                name = ((Control)sender).Name;

                if (name == this.btn사진촬영적용.Name)
				{
                    textBox = this.txt사진촬영;
                    column = "DC_PHOTO";
                }
                else if (name == this.btn사양확인적용.Name)
				{
                    textBox = this.txt사양확인;
                    column = "DC_SPEC";
                }
                else
				{
                    textBox = this.txt위험물별도포장;
                    column = "DC_SEPARATE";
                }
                
                dataRowArray = this._품목정보.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    else
                        this.txt경고메시지.Text += "선택된 자료가 없습니다." + Environment.NewLine;

                    return;
                }
                else if (this._품목정보.DataTable.Select("S <> 'Y'").Length == 0)
				{
                    textBox.Text = "전체";
                    this._기본정보.CurrentRow[column] = "전체";
                    this.ToolBarSaveButtonEnabled = true;
                }
                else
				{
                    textBox.Text = string.Empty;

                    DataTable dt = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_SO" }, true);

                    foreach (DataRow dr in dt.Rows)
					{
                        textBox.Text += dr["NO_SO"].ToString() + "(";

                        foreach (DataRow dr1 in this._품목정보.DataTable.Select(string.Format("S = 'Y' AND NO_SO = '{0}'", dr["NO_SO"].ToString()), "NO_DSP ASC"))
						{
                            textBox.Text += D.GetInt(dr1["NO_DSP"]).ToString() + ",";
                        }

                        textBox.Text += ")";
                    }

                    this._기본정보.CurrentRow[column] = textBox.Text;
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxExt textBox;

            try
            {
                textBox = (TextBoxExt)sender;

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V && 
                    Clipboard.GetData(DataFormats.Text) != null)
				{
                    textBox.Text += Clipboard.GetData(DataFormats.Text).ToString().Replace(Environment.NewLine, " ");
                    Clipboard.Clear();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn컷오프시간설정_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호가 존재하지 않습니다." + Environment.NewLine;

                    return;
                }

                P_CZ_SA_GIR_CUTOFF dialog = new P_CZ_SA_GIR_CUTOFF(this.회사코드, this.txt의뢰번호.Text);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void BtnCPR설정_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호는 필수 입력 항목 입니다." + Environment.NewLine;

                    return;
				}

                P_CZ_SA_GIR_AUTO_CPR dialog = new P_CZ_SA_GIR_AUTO_CPR(this.회사코드, this.txt의뢰번호.Text);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn송품서류등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호는 필수 입력 항목 입니다." + Environment.NewLine;

                    return;
				}

                if (string.IsNullOrEmpty(this.dtp의뢰일자.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰일자.Text);
                    else
                        this.txt경고메시지.Text += "의뢰일자는 필수 입력 항목 입니다." + Environment.NewLine;

                    return;
                }

                P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "SA", "P_CZ_SA_GIR", this.txt의뢰번호.Text, "P_CZ_SA_GIR" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp의뢰일자.Text.Substring(0, 4));
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Ctx납품처_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "납품처";
                e.HelpParam.P61_CODE1 = @"CD_PARTNER AS CODE,
                                          LN_PARTNER AS NAME,
                                          (ISNULL(DC_ADDRESS, '') + ' ' + ISNULL(DC_ADDRESS1, '')) AS DC_ADDRESS,
                                          (CASE WHEN ISNULL(NM_PIC, '') = '' THEN TRIM(ISNULL(NO_TEL, '')) ELSE TRIM(ISNULL(NM_PIC, '') + ' ' + ISNULL(NO_TEL, '')) END) AS NO_TEL";
                e.HelpParam.P62_CODE2 = "CZ_MA_DELIVERY WITH(NOLOCK)";
                e.HelpParam.P63_CODE3 = string.Format("WHERE CD_COMPANY = '{0}' AND ISNULL(YN_USE, 'N') = 'Y' AND CD_PARTNER LIKE 'DLV%'", 회사코드);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn입출항정보_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl호선.Text);
                    else
                        this.txt경고메시지.Text += "호선은 필수 입력 항목 입니다." + Environment.NewLine;

                    return;
				}

                dataRowArray = VesselSchedule.입출항정보조회(Global.MainFrame.LoginInfo.CompanyCode, this.ctx호선번호.CodeValue);

                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    if (dataRowArray.Length >= 1)
                        this.txt경고메시지.Text += dataRowArray[0]["DC_SCHEDULE"].ToString() + Environment.NewLine;

                    if (dataRowArray.Length >= 2)
                        this.txt경고메시지.Text += dataRowArray[1]["DC_SCHEDULE"].ToString() + Environment.NewLine;

                    if (dataRowArray.Length >= 3)
                        this.txt경고메시지.Text += dataRowArray[2]["DC_SCHEDULE"].ToString() + Environment.NewLine;

                    if (dataRowArray.Length >= 4)
                        this.txt경고메시지.Text += dataRowArray[3]["DC_SCHEDULE"].ToString() + Environment.NewLine;

                    if (dataRowArray.Length >= 5)
                        this.txt경고메시지.Text += dataRowArray[4]["DC_SCHEDULE"].ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Cbo비용청구방법_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo비용청구방법.SelectedValue.ToString() == "002")
                    this.btn부대비용.Enabled = true;
                else
                    this.btn부대비용.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn부대비용_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호가 존재하지 않습니다." + Environment.NewLine;

                    return;
                }

                P_CZ_SA_GIR_CHARGE dialog = new P_CZ_SA_GIR_CHARGE(this.회사코드, this.txt의뢰번호.Text, this.cbo통화.Text);
                dialog.ShowDialog();

                this.부대비용();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                this.SetToolBarButtonState(true, true, true, true, true);

                ds = this.GetComboData("S;CZ_SA00006",
                                       "S;CZ_SA00012",
                                       "S;TR_IM00003",
                                       "S;CZ_SA00005",
                                       "S;CZ_SA00013",
                                       "S;PU_C000016",
                                       "N;MA_B000005",
                                       "S;CZ_SA00018");

                this.cboMainCategory.DataSource = ds.Tables[0];
                this.cboMainCategory.DisplayMember = "NAME";
                this.cboMainCategory.ValueMember = "CODE";

                this.cboFreight.DataSource = ds.Tables[1];
                this.cboFreight.DisplayMember = "NAME";
                this.cboFreight.ValueMember = "CODE";

                this.cbo선적조건.DataSource = ds.Tables[2];
                this.cbo선적조건.DisplayMember = "NAME";
                this.cbo선적조건.ValueMember = "CODE";

                this.cboINCOTERMS지역.DataSource = ds.Tables[3];
                this.cboINCOTERMS지역.DisplayMember = "NAME";
                this.cboINCOTERMS지역.ValueMember = "CODE";

                this.cbo지불조건.DataSource = ds.Tables[4];
                this.cbo지불조건.DisplayMember = "NAME";
                this.cbo지불조건.ValueMember = "CODE";

                this.cbo거래구분.DataSource = ds.Tables[5];
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.ValueMember = "CODE";

                this.cbo통화.DataSource = ds.Tables[6];
                this.cbo통화.DisplayMember = "NAME";
                this.cbo통화.ValueMember = "CODE";

                this.cboHSCode.DataSource = new DataView(ds.Tables[7], string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable();
                this.cboHSCode.DisplayMember = "NAME";
                this.cboHSCode.ValueMember = "CODE";

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("CODE", typeof(string));
                dt1.Columns.Add("NAME", typeof(string));
                DataRow row1 = null;
                row1 = dt1.NewRow(); row1["CODE"] = "N"; row1["NAME"] = DD("출고"); dt1.Rows.Add(row1);
                //row1 = dt1.NewRow(); row1["CODE"] = "Y"; row1["NAME"] = DD("반품"); dt1.Rows.Add(row1);

                this.cbo출고구분.DataSource = dt1;
                this.cbo출고구분.DisplayMember = "NAME";
                this.cbo출고구분.ValueMember = "CODE";

                this.cbo비용청구방법.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "자동청구", "수동청구" }, true);
                this.cbo비용청구방법.DisplayMember = "NAME";
                this.cbo비용청구방법.ValueMember = "CODE";

                this.cbo선적위치.DataSource = MA.GetCode("CZ_SA00043", true);
                this.cbo선적위치.DisplayMember = "NAME";
                this.cbo선적위치.ValueMember = "CODE";

                this.cbo전달시간.DataSource = MA.GetCode("CZ_MA00028", true);
                this.cbo전달시간.DisplayMember = "NAME";
                this.cbo전달시간.ValueMember = "CODE";

                this.cbo선적부두.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT '' AS CODE, '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE, NM_SYSDEF AS NAME 
FROM CZ_MA_CODEDTL WITH(NOLOCK) 
WHERE CD_COMPANY = '{0}' 
AND CD_FIELD = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, "CZ_SA00070"));
                this.cbo선적부두.DisplayMember = "NAME";
                this.cbo선적부두.ValueMember = "CODE";

                this.cur외화금액.Mask = this.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT);

                this.chk자동제출.Enabled = false;
                this.lbl자동제출.Visible = false;
                this.nud자동제출.Visible = false;
                this.lbl자동제출1.Visible = false;

                if (회사코드 == "S100")
                {
                    this.oneGrid1.ItemCollection.Remove(this.oneGridItem8);
                    this.oneGrid1.Controls.Remove(this.oneGridItem8);

                    this.oneGridItem1.ControlEdit.Remove(this.bpPanelControl52);
                    this.oneGridItem1.ControlEdit.Remove(this.bpPanelControl53);
                    this.oneGridItem1.ControlEdit.Remove(this.btn부대비용);

                    this.oneGridItem1.ControlEdit.Remove(this.bpPanelControl51);

                    this.btn부대비용.Visible = false;
                    this.btnCPR설정.Visible = false;
                    this.btn컷오프시간설정.Visible = false;

                    this.txt서류처리.ReadOnly = false;
                    this.txt포장비고.ReadOnly = false;
                }
                else
                {
                    this.btn협조전적용.Visible = true;
                    this.btnCPR설정.Visible = true;
                    this.btn컷오프시간설정.Visible = true;

                    this.txt서류처리.ReadOnly = true;
                    this.txt포장비고.ReadOnly = true;
                }

                this.chk관세환급여부.Visible = false;
                this.bpPanelControl55.Visible = false; // 청구호선

                this.lbl출고의뢰자.Text = this.DD("출고의뢰자");

                this.cboMainCategory.Tag = "CD_MAIN_CATEGORY";
                this.ctx납품처.Tag = "CD_DELIVERY_TO;LN_DELIVERY_TO";

                this.btn부대비용.Visible = true;
                this.btn부대비용.Enabled = false;
                this.btn입출항정보.Visible = true;

                if (!string.IsNullOrEmpty(this.협조전번호))
                    this.협조전적용(this.회사코드, this.협조전번호, this.차수);
                else
                    this.InitData();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private static IEnumerable<Control> GetAll(Control control, Type type = null)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        private static void DropDownComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void InitData()
        {
            try
            {
                #region 기본정보
                DataSet ds = this._biz.기본정보검색(new object[] { this.회사코드,
                                                                   Global.MainFrame.LoginInfo.Language,
                                                                   string.Empty,
                                                                   0 });

                this._기본정보.SetBinding(ds.Tables[0], this.tpg기본정보);
                this._기본정보.ClearAndNewRow();        // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해
                this._기본정보.AcceptChanges();

                this.부대비용();
                this.입항정보(false);

                this._품목정보.Binding = ds.Tables[1];
                this._품목정보.AcceptChanges();

                this.chk관세환급여부.Visible = false;
                this.bpPanelControl55.Visible = false;

                DataTable dt포워더 = this.GetComboDataCombine("N;CZ_SA00015");

                this.rboA포워더.Text = dt포워더.Select("CODE = '001'")[0]["NAME"].ToString();
                this.rboB포워더.Text = dt포워더.Select("CODE = '002'")[0]["NAME"].ToString();
                this.rboC포워더.Text = dt포워더.Select("CODE = '004'")[0]["NAME"].ToString();

                this.cboSubCategory.DataSource = null;

                if (!string.IsNullOrEmpty(this.파일번호))
                {
                    this.ctx수주번호.CodeValue = this.파일번호;
                    this.ctx수주번호.CodeName = this.파일번호;

                    this.수주번호검색(this.파일번호);
                    this.파일번호 = string.Empty;
                }
                else
                {
                    this.ctx수주번호.CodeValue = string.Empty;
                    this.ctx수주번호.CodeName = string.Empty;
                }

                this.dtp접안일시.Checked = false;
                this.dtp출항일시.Checked = false;
                this.dtp서류마감.Checked = false;
                #endregion

                #region 송장정보
                DataTable dt = this._biz.송장정보검색(new object[] { this.회사코드,
                                                                     string.Empty, 
                                                                     0 });

                this._송장정보.SetBinding(dt, this.tpg송장정보);
                this._송장정보.ClearAndNewRow();
                this._송장정보.AcceptChanges();

                this.cboHSCode.SelectedValue = string.Empty;
                #endregion

                this.txt경고메시지.Text = string.Empty;

                this.수정여부설정(true);

                this.포워더정보설정();
                this.포워딩비용청구방식설정();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool IsChanged()
        {
            return (base.IsChanged() || _기본정보.GetChanges() != null ? true : false || _송장정보.GetChanges() != null ? true : false);
        } 
        #endregion

        #region ♣ 메인버튼 클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string 반품여부;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                //TODO KKH : 추후 커스터마이징 예정
                반품여부 = "N";

                P_CZ_SA_GIR_SCH_SUB dlg = new P_CZ_SA_GIR_SCH_SUB(반품여부, false);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.임시변수초기화();
                    this.협조전적용(Global.MainFrame.LoginInfo.CompanyCode, D.GetString(dlg.returnParams[0]), 0);
                }   
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 협조전적용(string 회사코드, string 의뢰번호, decimal 차수)
        {
            string 포워더;
            DataTable dt;
            DataRow dr;

            try
            {
                #region 기본정보
                DataSet ds = this._biz.기본정보검색(new object[] { 회사코드,
                                                                   Global.MainFrame.LoginInfo.Language,
                                                                   의뢰번호,
                                                                   차수 });

                dt = ds.Tables[0];

                this._기본정보.SetBinding(ds.Tables[0].Copy(), this.tpg기본정보);
                this._기본정보.ClearAndNewRow();
                this._기본정보.AcceptChanges();

                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];

                    if (ds.Tables[1].Select("ISNULL(QT_BL, 0) > 0").Length > 0)
                    {
                        this.chk관세환급여부.Visible = true;
                    }
                    else
                    {
                        this.chk관세환급여부.Visible = false;
                    }

                    if (ds.Tables[1].Select("ISNULL(YN_CONFIRM, 'N') = 'N'").Length > 0)
                        this.bpPanelControl55.Visible = false;
                    else
                        this.bpPanelControl55.Visible = true;

                    this.ctx호선번호.CodeValue = dr["NO_IMO"].ToString();
                    this.ctx호선번호.CodeName = dr["NO_HULL"].ToString();

                    this.입항정보(false);

                    this.ctx청구호선.CodeValue = dr["NO_IMO_BILL"].ToString();
                    this.ctx청구호선.CodeName = dr["NO_HULL_BILL"].ToString();

                    DateTime dateTime;
                    
                    if (!string.IsNullOrEmpty(dr["DTS_ETB"].ToString()))
					{
                        DateTime.TryParseExact(dr["DTS_ETB"].ToString(), "MM.dd HH:00", null, DateTimeStyles.None, out dateTime);
                        this.dtp접안일시.Value = dateTime;
                    }
                    
                    if (!string.IsNullOrEmpty(dr["DTS_ETD"].ToString()))
					{
                        DateTime.TryParseExact(dr["DTS_ETD"].ToString(), "MM.dd HH:00", null, DateTimeStyles.None, out dateTime);
                        this.dtp출항일시.Value = dateTime;
                    }

                    if (!string.IsNullOrEmpty(dr["DTS_DEADLINE"].ToString()))
                    {
                        DateTime.TryParseExact(dr["DTS_DEADLINE"].ToString(), "MM.dd HH:00", null, DateTimeStyles.None, out dateTime);
                        this.dtp서류마감.Value = dateTime;
                    }

                    this.SetSubCategory(D.GetString(dr["CD_MAIN_CATEGORY"]));
                    this.Set협조내용(D.GetString(dr["CD_MAIN_CATEGORY"]), D.GetString(dr["CD_SUB_CATEGORY"]));
                    this.Set비고설정(D.GetString(dr["CD_MAIN_CATEGORY"]), D.GetString(dr["CD_SUB_CATEGORY"]));

                    #region 포워더 설정
                    DataTable dt포워더 = this.GetComboDataCombine("N;CZ_SA00015");

                    포워더 = dr["NM_FORWARDER_A"].ToString();
                    if (string.IsNullOrEmpty(포워더))
                        this.rboA포워더.Text = dt포워더.Select("CODE = '001'")[0]["NAME"].ToString();
                    else
                        this.rboA포워더.Text = 포워더;

                    포워더 = dr["NM_FORWARDER_B"].ToString();
                    if (string.IsNullOrEmpty(포워더))
                        this.rboB포워더.Text = dt포워더.Select("CODE = '002'")[0]["NAME"].ToString();
                    else
                        this.rboB포워더.Text = 포워더;

                    포워더 = dr["NM_FORWARDER_C"].ToString();
                    if (string.IsNullOrEmpty(포워더))
                        this.rboC포워더.Text = dt포워더.Select("CODE = '004'")[0]["NAME"].ToString();
                    else
                        this.rboC포워더.Text = 포워더;
                    #endregion

                    this._기본정보.SetDataTable(dt);
                    this._기본정보.AcceptChanges();

                    this.부대비용();
                }

                this.포워더정보설정();
                this.포워딩비용청구방식설정();
                #endregion

                #region 품목정보
                this._품목정보.Binding = ds.Tables[1];
                this._품목정보.AcceptChanges();
                #endregion

                #region 송장정보
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dt = this._biz.송장정보검색(new object[] { 회사코드, 
                                                               D.GetString(ds.Tables[0].Rows[0]["NO_INV"]),
                                                               차수 });

                    this._송장정보.SetBinding(dt, this.tpg송장정보);

                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        this.cboHSCode.SelectedValue = dr["CD_PRODUCT"].ToString();

                        this._송장정보.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨
                        this._송장정보.AcceptChanges();
                    }
                    else
                    {
                        this._송장정보.ClearAndNewRow();
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                this.수정여부설정(false);
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

                //초기화 : 조회조건, 헤더그리드, 라인그리드 모두 초기화 된다.
                this.임시변수초기화();
                this.InitData();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;
            
            string 협조전상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

            if (협조전상태 == "C")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("종결"));
                else
                    this.txt경고메시지.Text += "종결 상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "R")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_확정상태는 삭제요청으로 삭제해야 합니다.");
                else
                    this.txt경고메시지.Text += "확정상태는 삭제요청으로 삭제해야 합니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "D")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("삭제요청"));
                else
                    this.txt경고메시지.Text += "삭제요청 상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "O")
			{
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("제출"));
                else
                    this.txt경고메시지.Text += "제출 상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }

            if (!this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt의뢰번호.Text);
                else
                    this.txt경고메시지.Text += "의뢰번호로 등록된 자료가 없습니다." + Environment.NewLine;

                return false;
            }

            if (!this._biz.송장번호중복체크(this.txt송장번호.Text))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt송장번호.Text);
                else
                    this.txt경고메시지.Text += "송장번호로 등록된 자료가 없습니다." + Environment.NewLine;

                return false;
            }
            
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                return true;
            else
                return false;            
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            bool result;
            
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!BeforeDelete()) return;

                result = false;

                result = this._biz.기본정보제거(this.txt의뢰번호.Text);
                result = this._biz.송장정보제거(this.txt송장번호.Text);

                if (result == true)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    else
                        this.txt경고메시지.Text += "자료가 정상적으로 삭제 되었습니다." + Environment.NewLine;

                    this.OnToolBarAddButtonClicked(sender, e);        //삭제 후 바로 초기화 시켜준다.
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            DataTable dt;
            string query, 포장예정일;
            
            #region 의뢰상태확인
            if (Global.MainFrame.LoginInfo.UserID != "S-180" &&
                Global.MainFrame.LoginInfo.UserID != "S-304" &&
                Global.MainFrame.LoginInfo.UserID != "S-587")
			{
                string 의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "C")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("종결"));
                    else
                        this.txt경고메시지.Text += "종결 상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }

                if (의뢰상태 == "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "삭제요청 상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }

                if (의뢰상태 == "R")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("확정"));
                    else
                        this.txt경고메시지.Text += "확정 상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }
            }
            #endregion
            
            #region 기본정보
            if (this.chk자동제출.Checked == true)
            {
                #region 자동제출
                if (string.IsNullOrEmpty(D.GetString(this.cbo협조내용.SelectedValue)))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl협조내용.Text);
                    else
                        this.txt경고메시지.Text += "협조내용은 필수 입력 항목 입니다." + Environment.NewLine;

                    this.cbo협조내용.Focus();
                    return false;
                }
                #endregion
            }
            else
            {
                if (this.dtp출고예정일.Text == "" || this.dtp출고예정일.Text == string.Empty)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고예정일.Text);
                    else
                        this.txt경고메시지.Text += "출고예정일은 필수 입력 항목 입니다." + Environment.NewLine;

                    this.dtp출고예정일.Focus();
                    return false;
                }

                if (D.GetDecimal(this.dtp출고예정일.Text) < D.GetDecimal(this.dtp의뢰일자.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은_보다크거나같아야합니다, new string[] { this.lbl출고예정일.Text, this.lbl의뢰일자.Text });
                    else
                        this.txt경고메시지.Text += "출고예정일은 의뢰일자보다 크거나 같아야 합니다." + Environment.NewLine;

                    this.dtp출고예정일.Text = this.dtp의뢰일자.Text;
                    this._기본정보.CurrentRow["DT_COMPLETE"] = this.dtp의뢰일자.Text;
                    this.dtp출고예정일.Focus();
                    return false;
                }
            }

            if (this.ctx호선번호.IsEmpty() || this.ctx호선번호.CodeValue == "" || this.ctx호선번호.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl호선.Text);
                else
                    this.txt경고메시지.Text += "호선은 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx호선번호.Focus();
                return false;
            }

            if (this.ctx매출처S.IsEmpty() || this.ctx매출처S.CodeValue == "" || this.ctx매출처S.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출처S.Text);
                else
                    this.txt경고메시지.Text += "매출처는 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx매출처S.Focus();
                return false;
            }
            
            if (this.ctx출고의뢰자.IsEmpty() || this.ctx출고의뢰자.CodeValue == "" || this.ctx출고의뢰자.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고의뢰자.Text);
                else
                    this.txt경고메시지.Text += "출고의뢰자는 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx출고의뢰자.Focus();
                return false;
            }
            
            if (this.dtp의뢰일자.Text == "" || this.dtp의뢰일자.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰일자.Text);
                else
                    this.txt경고메시지.Text += "의뢰일자는 필수 입력 항목 입니다." + Environment.NewLine;

                this.dtp의뢰일자.Focus();
                return false;
            }
            
            if (string.IsNullOrEmpty(D.GetString(this.cboMainCategory.SelectedValue)))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblMainCategory.Text);
                else
                    this.txt경고메시지.Text += "MainCategory는 필수 입력 항목 입니다." + Environment.NewLine;

                this.cboMainCategory.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(D.GetString(this.cboSubCategory.SelectedValue)))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblSubCategory.Text);
                else
                    this.txt경고메시지.Text += "SubCategory는 필수 입력 항목 입니다." + Environment.NewLine;

                this.cboSubCategory.Focus();
                return false;
            }

            if (회사코드 != "S100")
            {
                if (string.IsNullOrEmpty(this.ctx납품처.CodeValue))
                {
                    if (this.cboMainCategory.SelectedValue.ToString() == "002" &&
                        this.cboSubCategory.SelectedValue.ToString() == "DIR")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl납품처.Text);
                        else
                            this.txt경고메시지.Text += "납품처는 필수 입력 항목 입니다." + Environment.NewLine;

                        this.ctx납품처.Focus();
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(this.cbo비용청구방법.SelectedValue.ToString()))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl비용청구방법.Text);
                    else
                        this.txt경고메시지.Text += "비용청구방법은 필수 입력 항목 입니다." + Environment.NewLine;

                    this.cbo비용청구방법.Focus();
                    return false;
                }

                //직접송품 - COURIER (해외), 페더럴익스프레스 코리아 유한회사(FEDEX)
                if (this.cboMainCategory.SelectedValue.ToString() == "003" &&
                    this.cboSubCategory.SelectedValue.ToString() == "003" &&
                    this.ctx납품처.CodeValue == "12448" &&
                    this.cbo비용청구방법.SelectedValue.ToString() == "001")
                {
                    if (this.ShowMessage("FEDEX 송품시 비용청구방법을 수동청구로 지정해야 합니다.\n비용청구방법 : 자동청구 선택 됨\n저장하시겠습니까 ?", "QY2") != DialogResult.Yes)
                    {
                        this.cbo비용청구방법.Focus();
                        return false;
                    }
                }

                query = @"SELECT * 
FROM CZ_SA_GIRH_CHARGE GC WITH(NOLOCK)
WHERE GC.CD_COMPANY = '{0}'
AND GC.NO_GIR = '{1}'";

                dt = DBHelper.GetDataTable(string.Format(query, this.회사코드, this.txt의뢰번호.Text));

                if (!string.IsNullOrEmpty(this.txt의뢰번호.Text) &&
                    this.cbo비용청구방법.SelectedValue.ToString() == "002" &&
                    (dt == null || dt.Rows.Count == 0))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        Global.MainFrame.ShowMessage("수동청구 - 부대비용이 등록 되어 있지 않습니다.");
                    else
                        this.txt경고메시지.Text += "수동청구 - 부대비용이 등록 되어 있지 않습니다." + Environment.NewLine;

                    return false;
                }

                if (!string.IsNullOrEmpty(this.txt의뢰번호.Text) &&
                    this.cbo비용청구방법.SelectedValue.ToString() != "002" &&
                    (dt != null && dt.Rows.Count > 0))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        Global.MainFrame.ShowMessage("자동청구 - 부대비용이 등록 되어 있습니다.");
                    else
                        this.txt경고메시지.Text += "자동청구 - 부대비용이 등록 되어 있습니다." + Environment.NewLine;

                    return false;
                }
            }

            if (!this.chk검토필요.Checked)
			{
                if (this.chk선사서류_쉬핑마크.Checked ||
                    this.chk선사서류_CI.Checked ||
                    this.chk선사서류_인수증.Checked ||
                    this.chk기타서류부착.Checked ||
                    this.chk관세환급여부.Checked ||
                    this.chk당사발행.Checked ||
                    this.dtp접안일시.Checked ||
                    this.dtp출항일시.Checked ||
                    !string.IsNullOrEmpty(this.txt참조번호.Text) ||
                    !string.IsNullOrEmpty(this.txt추가요청_서류.Text) ||
                    !string.IsNullOrEmpty(this.cbo선적부두.SelectedValue.ToString()) ||
                    !string.IsNullOrEmpty(this.txt대리점.Text) ||
                    !string.IsNullOrEmpty(this.txt추가요청_선적.Text) ||
                    !string.IsNullOrEmpty(this.txt타배선적선명.Text) ||
                    !string.IsNullOrEmpty(this.txt작업선크레인.Text))
				{
                    this.txt경고메시지.Text += "검토필요 항목에 내용이 입력되어 있습니다. 검토필요 체크하시거나 내용을 제거하시기 바랍니다." + Environment.NewLine;

                    return false;
                }
			}
            #endregion

            #region 송장정보
            if (this.cbo거래구분.SelectedValue == DBNull.Value || this.cbo거래구분.SelectedValue == null || this.cbo거래구분.SelectedValue.ToString() == "" || this.cbo거래구분.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래구분.Text);
                else
                    this.txt경고메시지.Text += "거래구분은 필수 입력 항목 입니다." + Environment.NewLine;

                this.cbo거래구분.Focus();
                return false;
            }
            
            if (this.dtp발행일자.Text == "" || this.dtp발행일자.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발행일자.Text);
                else
                    this.txt경고메시지.Text += "발행일자는 필수 입력 항목 입니다." + Environment.NewLine;

                this.dtp발행일자.Focus();
                return false;
            }
            
            if (this.cbo출고구분.SelectedValue == DBNull.Value || this.cbo출고구분.SelectedValue == null || this.cbo출고구분.SelectedValue.ToString() == "" || this.cbo출고구분.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고구분.Text);
                else
                    this.txt경고메시지.Text += "출고구분은 필수 입력 항목 입니다." + Environment.NewLine;

                this.cbo출고구분.Focus();
                return false;
            }
            
            if (this.ctx사업장.IsEmpty() || this.ctx사업장.CodeValue == "" || this.ctx사업장.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사업장.Text);
                else
                    this.txt경고메시지.Text += "사업장은 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx사업장.Focus();
                return false;
            }
            
            if (this.ctx영업그룹.IsEmpty() || this.ctx영업그룹.CodeValue == "" || this.ctx영업그룹.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl영업그룹.Text);
                else
                    this.txt경고메시지.Text += "영업그룹은 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx영업그룹.Focus();
                return false;
            }
            
            if (this.ctx매출처.CodeValue == "" || this.ctx매출처.CodeValue == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출처.Text);
                else
                    this.txt경고메시지.Text += "매출처는 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx매출처.Focus();
                return false;
            }

            if (this.txt매출처.Text == "" || this.txt매출처.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("매출처명은 필수 입력 항목 입니다.");
                else
                    this.txt경고메시지.Text += "매출처명은 필수 입력 항목 입니다." + Environment.NewLine;

                this.txt매출처.Focus();
                return false;
            }

            if (this.ctx담당자.IsEmpty() || this.ctx담당자.CodeValue == "" || this.ctx담당자.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
                else
                    this.txt경고메시지.Text += "담당자는 필수 입력 항목 입니다." + Environment.NewLine;

                this.ctx담당자.Focus();
                return false;
            }
            
            if (this.cur외화금액.Text == "" || this.cur외화금액.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl외화금액.Text);
                else
                    this.txt경고메시지.Text += "외화금액은 필수 입력 항목 입니다." + Environment.NewLine;

                this.cur외화금액.Focus();
                return false;
            }
            
            if (this.cboHSCode.SelectedValue == DBNull.Value || this.cboHSCode.SelectedValue == null || this.cboHSCode.SelectedValue.ToString() == "" || this.cboHSCode.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblHSCode.Text);
                else
                    this.txt경고메시지.Text += "HSCODE는 필수 입력 항목 입니다." + Environment.NewLine;

                this.cboHSCode.Focus();
                return false;
            }
            
            if (this.cbo통화.SelectedValue == DBNull.Value || this.cbo통화.SelectedValue == null || this.cbo통화.SelectedValue.ToString() == "" || this.cbo통화.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl통화.Text);
                else
                    this.txt경고메시지.Text += "통화는 필수 입력 항목 입니다." + Environment.NewLine;

                this.cbo통화.Focus();
                return false;
            }
            
            if (this.dtp통관예정일.Text == "" || this.dtp통관예정일.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl통관예정일.Text);
                else
                    this.txt경고메시지.Text += "통관예정일은 필수 입력 항목 입니다." + Environment.NewLine;

                this.dtp통관예정일.Focus();
                return false;
            }
            
            if (this.dtp선적예정일.Text == "" || this.dtp선적예정일.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적예정일.Text);
                else
                    this.txt경고메시지.Text += "선적예정일은 필수 입력 항목 입니다." + Environment.NewLine;

                this.dtp선적예정일.Focus();
                return false;
            }
            
            if (this.cbo선적조건.SelectedValue == DBNull.Value || this.cbo선적조건.SelectedValue == null || this.cbo선적조건.SelectedValue.ToString() == "" || this.cbo선적조건.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적조건.Text);
                else
                    this.txt경고메시지.Text += "선적조건은 필수 입력 항목 입니다." + Environment.NewLine;

                this.cbo선적조건.Focus();
                return false;
            }
            
            if (this.cbo지불조건.SelectedValue == DBNull.Value || this.cbo지불조건.SelectedValue == null || this.cbo지불조건.SelectedValue.ToString() == "" || this.cbo지불조건.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl지불조건.Text);
                else
                    this.txt경고메시지.Text += "지불조건은 필수 입력 항목 입니다." + Environment.NewLine;

                this.cbo지불조건.Focus();
                return false;
            }
            
            if (this.txt선적지.Text == "" || this.txt선적지.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적지.Text);
                else
                    this.txt경고메시지.Text += "선적지는 필수 입력 항목 입니다." + Environment.NewLine;

                this.txt선적지.Focus();
                return false;
            }
            #endregion

            #region 품목정보
            //직접송품 - EIL
            if (D.GetString(this.cboMainCategory.SelectedValue) == "003" &&
                D.GetString(this.cboSubCategory.SelectedValue) == "006" &&
                this._품목정보.DataTable.Select("QT_GIR_STOCK > 0").Length > 0)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("재고품은 직접송품-EIL 로 작성 할 수 없습니다.");
                else
                    this.txt경고메시지.Text += "재고품은 직접송품-EIL 로 작성 할 수 없습니다." + Environment.NewLine;

                return false;
            }
            #endregion

            #region 협조전진행수량 확인
            if (this._biz.협조전진행수량체크(this.txt의뢰번호.Text, this._품목정보.DataTable) == false)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("진행중인 협조전이 있습니다.");
                else
                    this.txt경고메시지.Text += "진행중인 협조전이 있습니다." + Environment.NewLine;

                return false;
            }
            #endregion

            #region 포장예정일 계산
            if (string.IsNullOrEmpty(this.dtp포장예정일.Text) &&
                !string.IsNullOrEmpty(this.dtp출고예정일.Text) &&
                !string.IsNullOrEmpty(this.cboMainCategory.SelectedValue.ToString()) &&
                !string.IsNullOrEmpty(this.cboSubCategory.SelectedValue.ToString()))
            {
                if (this.cboMainCategory.SelectedValue.ToString() == "003" &&
                    this.cboSubCategory.SelectedValue.ToString() == "006")
                {
                    포장예정일 = this.dtp출고예정일.Text;
                }
                else
                {
                    query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ISNULL(NULLIF(MC.CD_SYSDEF, ''), '') AS CD_SYSDEF
FROM CZ_MA_CODEDTL MC 
WHERE MC.CD_COMPANY = 'K100' 
AND MC.CD_FIELD = 'CZ_SA00060' 
AND MC.CD_FLAG2 = '{0}' 
AND ('{0}' = '001' OR MC.CD_FLAG3 = '{1}') 
AND MC.CD_SYSDEF NOT LIKE 'PM%'";

                    dt = DBHelper.GetDataTable(string.Format(query, new string[] { this.cboMainCategory.SelectedValue.ToString(), this.cboSubCategory.SelectedValue.ToString() }));
                    query = @"SELECT NEOE.GIR_DATE('{0}', '{1}', '{2}', '', '{3}')";

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        포장예정일 = DBHelper.ExecuteScalar(string.Format(query, new string[] { this.회사코드,
                                                                                                dt.Rows[0]["CD_SYSDEF"].ToString(),
                                                                                                this.ctx납품처.CodeValue,
                                                                                                this.dtp출고예정일.Text })).ToString().Split('/')[1];
                    }
                    else
                    {
                        포장예정일 = DBHelper.ExecuteScalar(string.Format(query, new string[] { this.회사코드,
                                                                                                string.Empty,
                                                                                                this.ctx납품처.CodeValue,
                                                                                                this.dtp출고예정일.Text })).ToString().Split('/')[1];
                    }
                }

                if (!string.IsNullOrEmpty(포장예정일))
                {
                    this._기본정보.CurrentRow["DT_START"] = 포장예정일;
                    this.dtp포장예정일.Text = 포장예정일;
                }
                else
                {
                    this._기본정보.CurrentRow["DT_START"] = this.dtp출고예정일.Text;
                    this.dtp포장예정일.Text = this.dtp출고예정일.Text;
                }
            }
            #endregion

            this.Btn비고생성_Click(null, null);

            this.ToolBarSaveButtonEnabled = false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!_품목정보.HasNormalRow) return;

                if (MsgAndSave(PageActionMode.Save))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    else
                        this.txt경고메시지.Text += "자료가 정상적으로 저장되었습니다." + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            DataTable dt기본정보, dt송장정보, dt품목정보;

            if (string.IsNullOrEmpty(this.txt의뢰번호.Text) && string.IsNullOrEmpty(this.txt송장번호.Text))
            {
                #region 추가
                this.txt의뢰번호.Text = (string)this.GetSeq(this.회사코드, "SA", "03", this.dtp의뢰일자.Text.Substring(2, 4));
                this.txt송장번호.Text = (string)this.GetSeq(this.회사코드, "TRE", "05", this.dtp발행일자.Text.Substring(0, 6));

                if (this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 이(가) 중복되었습니다.", this.txt의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호가 중복되었습니다." + Environment.NewLine;

                    return false;
                }

                if (this._biz.송장번호중복체크(this.txt송장번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 이(가) 중복되었습니다.", this.txt송장번호.Text);
                    else
                        this.txt경고메시지.Text += "송장번호가 중복되었습니다." + Environment.NewLine;

                    return false;
                }

                #region 기본정보
                this._기본정보.CurrentRow["NO_GIR"] = this.txt의뢰번호.Text;
                this._기본정보.CurrentRow["NO_IMO"] = this.ctx호선번호.CodeValue;

                this._기본정보.CurrentRow["NO_IMO_BILL"] = this.ctx청구호선.CodeValue;

                this._기본정보.CurrentRow["CD_PARTNER"] = this.ctx매출처S.CodeValue;
                this._기본정보.CurrentRow["TP_BUSI"] = D.GetString(this.cbo거래구분.SelectedValue);
                this._기본정보.CurrentRow["YN_RETURN"] = D.GetString(this.cbo출고구분.SelectedValue);

                this._기본정보.CurrentRow["CD_MAIN_CATEGORY"] = D.GetString(this.cboMainCategory.SelectedValue);
                this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = D.GetString(this.cboSubCategory.SelectedValue);

                this._기본정보.CurrentRow["NM_FORWARDER_A"] = this.rboA포워더.Text;
                this._기본정보.CurrentRow["NM_FORWARDER_B"] = this.rboB포워더.Text;
                this._기본정보.CurrentRow["NM_FORWARDER_C"] = this.rboC포워더.Text;

                this._기본정보.CurrentRow["DC_RMK"] = this.txt서류처리.Text;
                this._기본정보.CurrentRow["DC_RMK1"] = this.txt수정취소.Text;
                this._기본정보.CurrentRow["DC_RMK2"] = this.txt매출비고.Text;
                this._기본정보.CurrentRow["DC_RMK3"] = this.txt기포장정보.Text;
                this._기본정보.CurrentRow["DC_RMK4"] = this.txt포장비고.Text;
                this._기본정보.CurrentRow["DC_RMK5"] = this.txtPICKING비고.Text;
                
                this._기본정보.CurrentRow["NM_DELIVERY"] = this.txt수취업체명.Text;
                this._기본정보.CurrentRow["DC_DELIVERY_ADDR"] = this.txt수취인주소.Text;
                this._기본정보.CurrentRow["DC_DELIVERY_TEL"] = this.txt수취인연락처.Text;
                this._기본정보.CurrentRow["DC_DESTINATION"] = this.txt화물도착지점.Text;

                this._기본정보.CurrentRow["YN_REVIEW"] = (this.chk검토필요.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_SHIPPING"] = (this.chk선사서류_쉬핑마크.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_CI"] = (this.chk선사서류_CI.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_RECEIPT"] = (this.chk선사서류_인수증.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_ETC"] = (this.chk기타서류부착.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_REFUND"] = (this.chk관세환급여부.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["NO_GIR_SG"] = this.txt참조번호.Text;
                this._기본정보.CurrentRow["YN_LOADING"] = (this.chk당사발행.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["DC_RMK_ADD"] = this.txt추가요청_서류.Text;

				this._기본정보.CurrentRow["CD_PORT"] = this.cbo선적부두.SelectedValue.ToString();
				this._기본정보.CurrentRow["DC_AGENCY"] = this.txt대리점.Text;
                this._기본정보.CurrentRow["DC_RMK_SHIP"] = this.txt추가요청_선적.Text;
                this._기본정보.CurrentRow["DC_VESSEL"] = this.txt타배선적선명.Text;
                this._기본정보.CurrentRow["DC_SHIP_CRANE"] = this.txt작업선크레인.Text;

                this._기본정보.CurrentRow["YN_DOMESTIC"] = (this.rdo국내선적.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["CD_SHIP_LOCATION"] = this.cbo선적위치.SelectedValue.ToString();
                this._기본정보.CurrentRow["YN_PRE_PHOTO"] = (this.chk포장전.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_POST_PHOTO"] = (this.chk포장후.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["DC_PHOTO"] = this.txt사진촬영.Text;
                this._기본정보.CurrentRow["YN_SPEC_CHECK"] = (this.chk사양확인.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["DC_SPEC"] = this.txt사양확인.Text;
                this._기본정보.CurrentRow["YN_SEPARATE_PACK"] = (this.chk위험물별도포장.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["DC_SEPARATE"] = this.txt위험물별도포장.Text;
                this._기본정보.CurrentRow["YN_UNPACK"] = (this.chk미포장건.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["DC_UNPACK"] = this.txt미포장건.Text;
                this._기본정보.CurrentRow["YN_CERT"] = (this.chk서트부착유무확인.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["YN_UNIDENTIFIABLE"] = (this.chk무지포장진행.Checked == true ? "Y" : "N");
                this._기본정보.CurrentRow["NO_FILE"] = this.txt원본파일번호.Text;
                this._기본정보.CurrentRow["DC_RMK_PACK"] = this.txt추가요청_포장.Text;

                this._기본정보.CurrentRow["TM_DELIVERY"] = this.cbo전달시간.SelectedValue.ToString();

				this._기본정보.CurrentRow.AcceptChanges();
                this._기본정보.CurrentRow.SetAdded();

                dt기본정보 = this._기본정보.GetChanges();

                if (dt기본정보 != null)
                {
                    if (this._biz.기본정보저장(dt기본정보))
                    {
                        this._기본정보.AcceptChanges();
                    }
                }
                #endregion

                #region 송장정보
                this._송장정보.CurrentRow["NO_INV"] = this.txt송장번호.Text;
                this._송장정보.CurrentRow["TP_TRANS"] = this.cbo선적조건.SelectedValue;
                this._송장정보.CurrentRow["TP_TRANSPORT"] = this.cboINCOTERMS지역.SelectedValue;
                this._송장정보.CurrentRow["COND_PAY"] = this.cbo지불조건.SelectedValue;
                this._송장정보.CurrentRow["CD_EXCH"] = this.cbo통화.SelectedValue;
                this._송장정보.CurrentRow["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
                this._송장정보.CurrentRow["FG_LC"] = D.GetString(this.cbo거래구분.SelectedValue);
                this._송장정보.CurrentRow["DESCRIPTION"] = this.txt매출처발주번호.Text;
                this._송장정보.CurrentRow["REMARK"] = this.txt송장비고.Text;

                this._송장정보.CurrentRow.AcceptChanges();
                this._송장정보.CurrentRow.SetAdded();

                dt송장정보 = this._송장정보.GetChanges();

                if (dt송장정보 != null)
                {
                    if (this._biz.송장정보저장(dt송장정보))
                    {
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                #region 품목정보
                dt품목정보 = this._품목정보.DataTable.GetChanges();

                if (dt품목정보 != null)
                {
                    if (this._biz.품목정보저장(dt품목정보, this.txt의뢰번호.Text, this.txt송장번호.Text))
                    {
                        this._품목정보.AcceptChanges();
                    }
                }
                #endregion
                #endregion
            }
            else
            {
                #region 수정
                if (!this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호로 등록된 자료가 없습니다." + Environment.NewLine;

                    return false;
                }

                if (!this._biz.송장번호중복체크(this.txt송장번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt송장번호.Text);
                    else
                        this.txt경고메시지.Text += "송장번호로 등록된 자료가 없습니다." + Environment.NewLine;

                    return false;
                }

                #region 기본정보
                dt기본정보 = this._기본정보.GetChanges();

                if (dt기본정보 != null)
                {
                    if (this._biz.기본정보저장(dt기본정보))
                    {
                        this._기본정보.AcceptChanges();
                    }
                }
                #endregion

                #region 송장정보
                dt송장정보 = this._송장정보.GetChanges();

                if (dt송장정보 != null)
                {
                    if (this._biz.송장정보저장(dt송장정보))
                    {
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                #region 품목정보
                dt품목정보 = this._품목정보.DataTable.GetChanges();

                if (dt품목정보 != null)
                {
                    if (this._biz.품목정보저장(dt품목정보, this.txt의뢰번호.Text, this.txt송장번호.Text))
                    {
                        this._품목정보.AcceptChanges();
                    }
                }
                #endregion
                #endregion
            }

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt1, dt2, dt3;
            string 매출처발주번호, url;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                #region 협조전헤더
                dt1 = _biz.협조전헤더(new object[] { Global.MainFrame.ServerKey,
                                                     this.회사코드,
                                                     Global.MainFrame.LoginInfo.Language,
                                                     this.txt의뢰번호.Text });
                #endregion

                #region 협조전라인
                dt2 = _biz.협조전라인(new object[] { this.회사코드,
                                                     this.txt의뢰번호.Text,
                                                     'Y' });
                #endregion

                매출처발주번호 = string.Empty;

                dt1.Columns.Add("CD_GIR_QR");
                dt1.Columns.Add("CD_QR");

                foreach (DataRow dr in dt1.Rows)
                {
                    dr["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();

                    url = URL.GetShortner("log/pack/write", this.회사코드 + "/" + dr["NO_GIR"].ToString());

                    dr["CD_GIR_QR"] = "V01/D08" + dr["NO_GIR"].ToString() + "/D10 " + url;
                }

                foreach (DataRow dr in ComFunc.getGridGroupBy(dt2.Select(), new string[] { "NO_PO_PARTNER" }, true).Rows)
                {
                    if (!string.IsNullOrEmpty(D.GetString(dr["NO_PO_PARTNER"])))
                        매출처발주번호 += D.GetString(dr["NO_PO_PARTNER"]) + ",";
                }

                dt1.Rows[0]["NO_PO_PARTNER_ALL"] = (string.IsNullOrEmpty(매출처발주번호) == true ? string.Empty : 매출처발주번호.Remove(매출처발주번호.Length - 1));
                dt1.AcceptChanges();

                dt2.Columns.Remove("NM_SUBJECT");

                #region 컷오프타임
                dt3 = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { this.회사코드, this.txt의뢰번호.Text, "Y" });
                #endregion

                reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH", "물류업무협조전", this.회사코드, dt1, dt2);

                reportHelper.SetDataTable(dt1, 1);
                reportHelper.SetDataTable(dt2, 2);
                reportHelper.SetDataTable(dt3, 3);

                Util.RPT_Print(reportHelper);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
            this.임시파일제거();
            return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region ♣ 화면내버튼 클릭
		private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._품목정보.HasNormalRow) return;
                
                dataRowArray = this._품목정보.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    else
                        this.txt경고메시지.Text += "선택된 자료가 없습니다." + Environment.NewLine;
                    return;
                }
                else
                {
                    this._품목정보.Redraw = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }

                    this._품목정보.Redraw = true;
                    this._품목정보.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._품목정보.Redraw = true;
            }
        }

        private void btn제출_Click(object sender, EventArgs e)
        {
            SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

            string 의뢰상태, query;
            DataTable dt;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                #region 포장예정일 확인
                if (this.dtp포장예정일.Text == "" || this.dtp포장예정일.Text == string.Empty)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl포장예정일.Text);
                    else
                        this.txt경고메시지.Text += "포장예정일은 필수 입력 항목 입니다." + Environment.NewLine;

                    this.dtp포장예정일.Focus();
                    return;
                }

                if (D.GetDecimal(this.dtp포장예정일.Text) > D.GetDecimal(this.dtp출고예정일.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.lbl포장예정일.Text, this.lbl출고예정일.Text });
                    else
                        this.txt경고메시지.Text += "포장예정일은 출고예정일보다 작거나 같아야 합니다." + Environment.NewLine;

                    this.dtp포장예정일.Focus();
                    return;
                }
                #endregion
                
                #region 경고마스터
                dt = new DataTable();
                dt.Columns.Add("NO_FILE");
                dt.Columns.Add("NO_LINE");
                dt.Columns.Add("CD_SUPPLIER");
                dt.Columns.Add("NO_DSP");
                dt.Columns.Add("NM_SUBJECT");
                dt.Columns.Add("CD_ITEM_PARTNER");
                dt.Columns.Add("NM_ITEM_PARTNER");
                dt.Columns.Add("CD_ITEM");
                dt.Columns.Add("UM_PU");
                dt.Columns.Add("UM_SA");

                DataRow newRow;

                foreach (DataRow dr in this._품목정보.DataTable.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows))
                {
                    string[] supplierArray = dr["CD_SUPPLIER"].ToString().Split(',');

                    if (supplierArray.Length == 1)
                    {
                        newRow = dt.NewRow();
                        newRow["NO_FILE"] = this.txt의뢰번호.Text;
                        newRow["NO_LINE"] = dr["SEQ_GIR"];
                        newRow["CD_SUPPLIER"] = supplierArray[0];
                        newRow["NO_DSP"] = dr["NO_DSP"];
                        newRow["NM_SUBJECT"] = dr["NM_SUBJECT"];
                        newRow["CD_ITEM_PARTNER"] = dr["CD_ITEM_PARTNER"];
                        newRow["NM_ITEM_PARTNER"] = dr["NM_ITEM_PARTNER"];
                        newRow["CD_ITEM"] = dr["CD_ITEM"];
                        dt.Rows.Add(newRow);
                    }
                    else
                    {
                        foreach (string supplier in supplierArray)
                        {
                            newRow = dt.NewRow();
                            newRow["NO_FILE"] = this.txt의뢰번호.Text;
                            newRow["NO_LINE"] = dr["SEQ_GIR"];
                            newRow["CD_SUPPLIER"] = supplier;
                            newRow["NO_DSP"] = dr["NO_DSP"];
                            newRow["NM_SUBJECT"] = dr["NM_SUBJECT"];
                            newRow["CD_ITEM_PARTNER"] = dr["CD_ITEM_PARTNER"];
                            newRow["NM_ITEM_PARTNER"] = dr["NM_ITEM_PARTNER"];
                            newRow["CD_ITEM"] = dr["CD_ITEM"];
                            dt.Rows.Add(newRow);
                        }
                    }
                }

                WARNING warning = new WARNING(WARNING_TARGET.출고의뢰)
                {
                    매출처코드 = this.ctx매출처S.CodeValue,
                    IMO번호 = this.ctx호선번호.CodeValue,
                    아이템 = dt,
                    SQLDebug = sqlDebug
                };

                warning.조회(true);

                if (warning.경고여부)
                {
                    DialogResult 경고결과 = warning.ShowDialog();

                    if (warning.저장불가 || 경고결과 == DialogResult.Cancel)
                    {
                        UTIL.메세지("작업이 취소되었습니다.", "WK1");
                        return;
                    }
                }
				#endregion

				#region 비용청구방법
				if (this.회사코드 != "S100")
                {
                    query = @"SELECT ISNULL(WD.TP_CHARGE, '001') AS TP_CHARGE
                              FROM CZ_SA_GIRH_WORK_DETAIL WD
                              WHERE WD.CD_COMPANY = '{0}'
                              AND WD.NO_GIR = '{1}'";

                    string 비용처리방법 = DBHelper.GetDataTable(string.Format(query, 회사코드, this.txt의뢰번호.Text)).Rows[0]["TP_CHARGE"].ToString();

                    query = @"SELECT * 
                              FROM CZ_SA_GIRH_CHARGE GC WITH(NOLOCK)
                              WHERE GC.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
                             "AND GC.NO_GIR = '" + this.txt의뢰번호.Text + "'";

                    dt = DBHelper.GetDataTable(query);

                    if (비용처리방법 == "002")
					{
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("비용청구방법 : 수동청구\n부대비용 금액이 등록되어 있지 않습니다.\n저장 후 다시 시도해 주시기 바랍니다.");
                            else
                                this.txt경고메시지.Text += "비용청구방법 : 수동청구\n부대비용 금액이 등록되어 있지 않습니다.\n저장 후 다시 시도해 주시기 바랍니다." + Environment.NewLine;

                            return;
                        }
                    }
                    else
					{
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("비용청구방법 : 자동청구\n부대비용 금액이 등록되어 있습니다.\n저장 후 다시 시도해 주시기 바랍니다.");
                            else
                                this.txt경고메시지.Text += "비용청구방법 : 자동청구\n부대비용 금액이 등록되어 있습니다.\n저장 후 다시 시도해 주시기 바랍니다." + Environment.NewLine;

                            return;
                        }
                    }
                }
                #endregion

                #region 협조전건수 확인
                dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_COUNT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                if (dt.Select("IDX = 2 AND QT_TOTAL >= 180").Length > 0)
                {
                    P_CZ_SA_GIR_COUNT dialog = new P_CZ_SA_GIR_COUNT(false);
                    dialog.ShowDialog();

                    if (dialog.DialogResult == DialogResult.Cancel) return;
                }
				#endregion

				#region 미제출 여부 확인
				의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (!string.IsNullOrEmpty(의뢰상태))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_미제출 건만 제출 할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "미체출 건만 제출 할 수 있습니다." + Environment.NewLine;

                    return;
                }
                #endregion

                #region 의뢰일자 확인 (제출일자와 동일해야 됨)
                if (this.dtp의뢰일자.Text != this.MainFrameInterface.GetStringToday)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._와_은같아야합니다, new string[] { this.lbl의뢰일자.Text, this.DD("제출일자") });
                    else
                        this.txt경고메시지.Text += "제출일자와 의뢰일자는 같아야 합니다." + Environment.NewLine;

                    this.dtp의뢰일자.Text = this.MainFrameInterface.GetStringToday;
                    _기본정보.CurrentRow["DT_GIR"] = this.dtp의뢰일자.Text;
                    this.dtp의뢰일자.Focus();
                    return;
                }
                #endregion

                if (this.회사코드 == "K100" || this.회사코드 == "K200")
				{
                    // 대리점전달 - 물류부 직접전달, 영업부 선전달
                    // 직접송품 - 항송, 해송, 택배, 포워딩직접픽업, 선적대행, 퀵, 화물
                    #region CPR 자동발송 관련
                    if ((this.cboMainCategory.SelectedValue.ToString() == "002" && (this.cboSubCategory.SelectedValue.ToString() == "DIR" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "ADV")) ||
                        (this.cboMainCategory.SelectedValue.ToString() == "003" && (this.cboSubCategory.SelectedValue.ToString() == "001" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "002" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "004" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "005" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "007" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "010" ||
                                                                                    this.cboSubCategory.SelectedValue.ToString() == "011")))
                    {
                        #region 납품처 확인
                        if (string.IsNullOrEmpty(this.ctx납품처.CodeValue))
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("납품처가 지정되어 있지 않습니다.");
                            else
                                this.txt경고메시지.Text += "납품처가 지정되어 있지 않습니다." + Environment.NewLine;

                            return;
                        }
                        #endregion

                        #region 납품처메일주소 확인
                        query = @"SELECT NO_EMAIL 
                                  FROM CZ_MA_DELIVERY WITH(NOLOCK)
                                  WHERE CD_COMPANY = '{0}'
                                  AND CD_PARTNER = '{1}'";

                        dt = DBHelper.GetDataTable(string.Format(query, this.회사코드, this.ctx납품처.CodeValue));

                        if (dt == null || dt.Rows.Count == 0 || string.IsNullOrEmpty(dt.Rows[0]["NO_EMAIL"].ToString()))
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("납품처 메일주소가 지정되어 있지 않습니다.");
                            else
                                this.txt경고메시지.Text += "납품처 메일주소가 지정되어 있지 않습니다." + Environment.NewLine;

                            return;
                        }
                        #endregion

                        // 매출처 : 딘텍싱가폴, DS건, 관세환급건 제외
                        #region CPR 자동발송제외
                        int 의뢰수량, 환급수량;

                        의뢰수량 = D.GetInt(this._품목정보.DataTable.Compute("SUM(QT_GIR)", string.Empty));
                        환급수량 = D.GetInt(this._품목정보.DataTable.Compute("SUM(QT_BL)", string.Empty));

                        if (this.ctx매출처S.CodeValue == "10286" ||
                            this._품목정보.DataTable.Select("NO_SO LIKE 'DS%'").Length > 0 ||
                            환급수량 > 0)
                        {
                            query = @"UPDATE WD
SET WD.YN_EXCLUDE_CPR = 'Y'
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";
                            DBHelper.ExecuteScalar(string.Format(query, this.회사코드, this.txt의뢰번호.Text));
                        }
                        #endregion
                    }
                    else if (this.회사코드 == "K100" &&
                             this.cboMainCategory.SelectedValue.ToString() == "001" && 
                             !string.IsNullOrEmpty(this.txt타배선적선명.Text))
					{
                        if (this.ctx매출처S.CodeValue == "01187") //EVERGREEN MARINE CORP. (TAIWAN) LTD.
                        {
                            query = @"UPDATE WD
SET WD.NO_DELIVERY_EMAIL = 'hfyeh@evergreen-marine.com;parcel@evergreen-marine.com;kmdsls@evergreen-marine.com'
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";
                            DBHelper.ExecuteScalar(string.Format(query, this.회사코드, this.txt의뢰번호.Text));
                        }
                        else if (this.ctx매출처S.CodeValue == "02143") //YANGMING MARINE TRANSPORT CORP
                        {
                            query = @"UPDATE WD
SET WD.NO_DELIVERY_EMAIL = 'log1@dintec.co.kr'
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";
                            DBHelper.ExecuteScalar(string.Format(query, this.회사코드, this.txt의뢰번호.Text));
                        }
					}
                    #endregion

                    // 직접송품 - 택배의 경우 협조내용 필수 값
                    if (this.cboMainCategory.SelectedValue.ToString() == "003" &&
                        this.cboSubCategory.SelectedValue.ToString() == "004" &&
                        string.IsNullOrEmpty(this.cbo협조내용.SelectedValue.ToString()))
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("협조내용이 지정되어 있지 않습니다.");
                        else
                            this.txt경고메시지.Text += "협조내용이 지정되어 있지 않습니다." + Environment.NewLine;

                        return;
                    }

                    // 직접송품 - COURIER (해외)의 경우 납품처 필수 값
                    if (this.cboMainCategory.SelectedValue.ToString() == "003" &&
                        this.cboSubCategory.SelectedValue.ToString() == "003" &&
                        string.IsNullOrEmpty(this.ctx납품처.CodeValue))
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("납품처가 지정되어 있지 않습니다.");
                        else
                            this.txt경고메시지.Text += "납품처가 지정되어 있지 않습니다." + Environment.NewLine;

                        return;
                    }

                    // 무상공급건 대체품 수출 비고
                    #region 상업송장비고 입력 
                    query = @"IF EXISTS (SELECT 1 
		   FROM SA_GIRL GL WITH(NOLOCK)
		   LEFT JOIN MM_EJTP JT WITH(NOLOCK) ON JT.CD_COMPANY = GL.CD_COMPANY AND JT.CD_QTIOTP = GL.TP_GI
		   WHERE GL.CD_COMPANY = '{0}'
		   AND GL.NO_GIR = '{1}'
		   AND ISNULL(JT.YN_AM, '') = 'N')
BEGIN
	UPDATE GD
	SET GD.DC_RMK_CI = (CASE WHEN ISNULL(GD.DC_RMK_CI, '') = '' THEN '90번 대체품 수출로 진행 부탁드립니다.'
																ELSE ISNULL(GD.DC_RMK_CI, '') + CHAR(13) + CHAR(10) + '90번 대체품 수출로 진행 부탁드립니다.' END)
	FROM CZ_SA_GIRH_WORK_DETAIL GD
	WHERE GD.CD_COMPANY = '{0}'
    AND GD.NO_GIR = '{1}'
END";

                    DBHelper.ExecuteScalar(string.Format(query, this.회사코드, this.txt의뢰번호.Text));
					#endregion

					#region 납품처확인
                    if (!string.IsNullOrEmpty(this.ctx납품처.CodeValue) && 
                        !((this.cboMainCategory.SelectedValue.ToString() == "002" && this.cboSubCategory.SelectedValue.ToString() == "ADV") || //대리점전달-영업부 선전달
                          (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "006") || //직접송품-EIL
                          (this.cboMainCategory.SelectedValue.ToString() == "003" && this.cboSubCategory.SelectedValue.ToString() == "010"))) //직접송품-퀵
                    {
                        query = @"SELECT MD.CD_SHIPMENT,
       MC.NM_SYSDEF,
       MC.CD_FLAG2,
       MC.CD_FLAG3
FROM CZ_MA_DELIVERY MD WITH(NOLOCK)
LEFT JOIN CZ_MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = 'K100' AND MC.CD_FIELD = 'CZ_SA00060' AND MC.CD_SYSDEF = MD.CD_SHIPMENT
WHERE MD.CD_COMPANY = '{0}' 
AND MD.CD_PARTNER = '{1}'";

                        dt = DBHelper.GetDataTable(string.Format(query, this.회사코드, this.ctx납품처.CodeValue));

                        if (!string.IsNullOrEmpty(dt.Rows[0]["CD_SHIPMENT"].ToString()))
                        {
                            if (this.cboMainCategory.SelectedValue.ToString() == "001")
                            {
                                if (dt.Rows[0]["CD_FLAG2"].ToString() != this.cboMainCategory.SelectedValue.ToString())
                                {
                                    this.txt경고메시지.Text += "납품처에 지정된 송품방법과 설정한 카테고리 값이 일치하지 않습니다." + Environment.NewLine;
                                    return;
                                }
                            }
                            else
                            {
                                if (dt.Rows[0]["CD_FLAG2"].ToString() != this.cboMainCategory.SelectedValue.ToString() ||
                                    dt.Rows[0]["CD_FLAG3"].ToString() != this.cboSubCategory.SelectedValue.ToString())
                                {
                                    this.txt경고메시지.Text += "납품처에 지정된 송품방법과 설정한 카테고리 값이 일치하지 않습니다." + Environment.NewLine;
                                    return;
                                }
                            }
                        }
                    }
					#endregion
				}

				#region 미입고협조전 여부
				if (!(this.cboMainCategory.SelectedValue.ToString() == "002" && this.cboSubCategory.SelectedValue.ToString() == "ADV"))
				{
                    dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_NOT_IN_CHK", new object[] { this.회사코드, this.txt의뢰번호.Text });

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (this.회사코드 == "K100")
						{
                            query = @"SELECT ISNULL(WD.DTS_CUTOFF, '') AS DTS_CUTOFF
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

                            dt = DBHelper.GetDataTable(string.Format(query, new object[] { this.회사코드, this.txt의뢰번호.Text }));

                            if (string.IsNullOrEmpty(dt.Rows[0]["DTS_CUTOFF"].ToString()))
                            {
                                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                    this.ShowMessage("미입고협조전은 컷오프시간을 입력해야 합니다.");
                                else
                                    this.txt경고메시지.Text += "미입고협조전은 컷오프시간을 입력해야 합니다." + Environment.NewLine;

                                P_CZ_SA_GIR_CUTOFF dialog = new P_CZ_SA_GIR_CUTOFF(this.회사코드, this.txt의뢰번호.Text);
                                dialog.ShowDialog();
                                return;
                            }

                            dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { this.회사코드, this.txt의뢰번호.Text, "N" });

                            if (dt.Select("YN_CHECK = 'Y'").Length > 0)
							{
                                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                    this.ShowMessage("납품 예정 정보가 입력되지 않은 건이 있습니다.");
                                else
                                    this.txt경고메시지.Text += "납품 예정 정보가 입력되지 않은 건이 있습니다." + Environment.NewLine;

                                return;
                            }
                        }
                        else
						{
                            query = @"UPDATE WD
SET WD.DTS_CUTOFF = '{2}' 
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

                            DBHelper.ExecuteScalar(string.Format(query, new object[] { this.회사코드, this.txt의뢰번호.Text, "Y" }));
                        }
                    }
                }
                #endregion

                if (this.SaveData() == true)
                {
                    //직접송품 - COURIER (해외), (주)디에이치엘코리아(DHL)
                    #region DHL Bill 출력 관련
                    if ((this.회사코드 == "K100" || this.회사코드 == "K200") &&
                        this.cboMainCategory.SelectedValue.ToString() == "003" &&
                        this.cboSubCategory.SelectedValue.ToString() == "003" &&
                        (this.ctx납품처.CodeValue == "01107" || this.ctx납품처.CodeValue == "DLV230200274"))
                    {
						#region BAN. 경고
						if (string.IsNullOrEmpty(this.txtBillingAccount.Text))
                        {
                            if (this.ShowMessage("선사 Billing Account Number가 등록되어 있지 않습니다.\n진행하시겠습니까 ? (당사 Account로 진행 됨)", "QY2") != DialogResult.Yes)
                                return;
                        }
						#endregion

						#region DHL Bill 출력 가능 여부 확인
						DataSet ds = this._biz.DHL(new object[] { this.회사코드,
                                                                  this.txt의뢰번호.Text });

                        string result = DHL_xml.DHLShipmentValidationServiceCheck(ds.Tables[0], ds.Tables[1], ds.Tables[2]);

                        if (result != string.Empty)
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage(result);
                            else
                                this.txt경고메시지.Text += result + Environment.NewLine;

                            return;
                        }
						#endregion
					}
                    #endregion

                    #region 부대비용 경고 (LIV 관련, 운송비, 포장비 자동청구)
                    if ((this.회사코드 == "K100" || this.회사코드 == "K200") &&
                        this._품목정보.DataTable.Select("CD_ITEM LIKE 'SD%'").Length > 0)
                    {
                        if (this.ShowMessage("부대비용이 포함 되어 있는 건 입니다.\n비용청구 방법 확인 후 진행 하시기 바랍니다.(이중청구 발생가능)\n진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
                            return;
                    }
					#endregion

					#region EIL건 자동확정 처리
					의뢰상태 = "O";

                    if (D.GetString(this.cboMainCategory.SelectedValue) == "003" &&
                        D.GetString(this.cboSubCategory.SelectedValue) == "006")
                    {
                        if (this.ShowMessage("CZ_EIL건 자동 확정 하시겠습니까?", "QY2") == DialogResult.Yes)
                            의뢰상태 = "R";
                    }

                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                         this.txt의뢰번호.Text,
                                                         "N",
                                                         의뢰상태,
                                                         "Y",
                                                         this.LoginInfo.UserID });
					#endregion

					this.수정여부설정(false);
                    
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn제출.Text);
                    else
                        this.txt경고메시지.Text += "제출작업을 완료 하였습니다." + Environment.NewLine;

                    #region 지불조건에 따른 경고 팝업
                    if (this.cbo지불조건.SelectedValue.ToString() == "101")
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CASH IN ADVANCE 조건\n물품대 입금 확인, 송품비 발생여부 확인 철저");
                        else
                            this.txt경고메시지.Text += "CASH IN ADVANCE 조건\n물품대 입금 확인, 송품비 발생여부 확인 철저" + Environment.NewLine;
                    }
                    else if (this.cbo지불조건.SelectedValue.ToString() == "102")
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CASH ON DELIVERY 조건\n협조전에 캐쉬수급 리마크 기재");
                        else
                            this.txt경고메시지.Text += "CASH ON DELIVERY 조건\n협조전에 캐쉬수급 리마크 기재" + Environment.NewLine;
                    }
					#endregion

					this.입항정보(true);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn제출취소_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "O")
                {
                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                         this.txt의뢰번호.Text,
                                                         "N",
                                                         string.Empty,
                                                         "N",
                                                         this.LoginInfo.UserID });

                    this.수정여부설정(false);

                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn제출취소.Text);
                    else
                        this.txt경고메시지.Text += "제출취소 작업을 완료 하였습니다." + Environment.NewLine;
                }
                else
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_제출 건만 제출취소 할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "제출건만 제출취소 할 수 있습니다." + Environment.NewLine;

                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제요청_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "C")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 삭제요청할 수 없습니다.", this.DD("종결"));
                    else
                        this.txt경고메시지.Text += "종결 상태는 삭제요청할 수 없습니다." + Environment.NewLine;

                    return;
                }
                else if (의뢰상태 == "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_이미 @ 되었습니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "이미 삭제요청 되었습니다." + Environment.NewLine;

                    return;
                }
                else if (의뢰상태 == "R")
                {
                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                          this.txt의뢰번호.Text,
                                                          "N",
                                                          "D",
                                                          "N",
                                                          this.LoginInfo.UserID });

                    this.수정여부설정(false);
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn삭제요청.Text);
                    else
                        this.txt경고메시지.Text += "삭제요청 작업을 완료 하였습니다." + Environment.NewLine;
                }
                else
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_확정상태만 수정할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "확정상태만 수정할 수 있습니다." + Environment.NewLine;

                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제요청취소_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 != "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태가 아닙니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "삭제요청 상태가 아닙니다." + Environment.NewLine;

                    return;
                }

                this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                      this.txt의뢰번호.Text,
                                                      "N",
                                                      "R",
                                                      "N",
                                                      this.LoginInfo.UserID });

                this.수정여부설정(false);
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn삭제요청취소.Text);
                else
                    this.txt경고메시지.Text += "삭제요청취소 작업을 완료하였습니다." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn협조전적용_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_SA_GIR_FORMAT dlg = new P_CZ_SA_GIR_FORMAT(this.ctx매출처S.CodeValue,
                                                                this.ctx매출처S.CodeName,
                                                                this.ctx호선번호.CodeValue,
                                                                this.ctx호선번호.CodeName,
                                                                this.txt호선명.Text,
                                                                false);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    #region 기본정보
                    if (string.IsNullOrEmpty(this.ctx매출처.CodeValue))
                    {
                        this.ctx매출처S.CodeValue = D.GetString(dlg.적용데이터["CD_PARTNER"]);
                        this._기본정보.CurrentRow["CD_PARTNER"] = this.ctx매출처S.CodeValue;

                        this.ctx매출처S.CodeName = D.GetString(dlg.적용데이터["LN_PARTNER"]);
                        this._기본정보.CurrentRow["LN_PARTNER"] = this.ctx매출처S.CodeName;
                    }

                    if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
                    {
                        this.ctx호선번호.CodeValue = D.GetString(dlg.적용데이터["NO_IMO"]);
                        this._기본정보.CurrentRow["NO_IMO"] = this.ctx호선번호.CodeValue;

                        this.ctx호선번호.CodeName = D.GetString(dlg.적용데이터["NO_HULL"]);
                        this._기본정보.CurrentRow["NO_HULL"] = this.ctx호선번호.CodeName;

                        this.txt호선명.Text = D.GetString(dlg.적용데이터["NM_VESSEL"]);
                        this.입항정보(false);
                    }

                    this.cboMainCategory.SelectedValue = D.GetString(dlg.적용데이터["CD_MAIN_CATEGORY"]);
                    this._기본정보.CurrentRow["CD_MAIN_CATEGORY"] = this.cboMainCategory.SelectedValue;
                    SelectionChangeCommitted(this.cboMainCategory, null);

                    this.cboSubCategory.SelectedValue = D.GetString(dlg.적용데이터["CD_SUB_CATEGORY"]);
                    this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = this.cboSubCategory.SelectedValue;
                    SelectionChangeCommitted(this.cboSubCategory, null);

                    this.납품처적용(D.GetString(dlg.적용데이터["CD_DELIVERY_TO"]), D.GetString(dlg.적용데이터["NM_DELIVERY_TO"]), D.GetString(dlg.적용데이터["DC_ADDRESS"]), D.GetString(dlg.적용데이터["NO_TEL"]));

                    this.cbo협조내용.SelectedValue = D.GetString(dlg.적용데이터["CD_RMK"]);
                    this._기본정보.CurrentRow["CD_RMK"] = this.cbo협조내용.SelectedValue;
                    #endregion
                    
                    #region 송장정보
                    if (dlg.송장정보포함 == true)
                    {
                        if (this._송장정보.CurrentRow.RowState != DataRowState.Unchanged)
                        {
                            if (this.ShowMessage("송장데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                                return;
                        }

                        #region 수하인
                        this.ctx수하인.CodeValue = D.GetString(dlg.적용데이터["CD_CONSIGNEE"]);
                        this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                        this.ctx수하인.CodeName = D.GetString(dlg.적용데이터["NM_CONSIGNEE"]);
                        this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;
                        this.txt수하인주소1.Text = D.GetString(dlg.적용데이터["ADDR1_CONSIGNEE"]);
                        this._송장정보.CurrentRow["ADDR1_CONSIGNEE"] = this.txt수하인주소1.Text;
                        this.txt수하인주소2.Text = D.GetString(dlg.적용데이터["ADDR2_CONSIGNEE"]);
                        this._송장정보.CurrentRow["ADDR2_CONSIGNEE"] = this.txt수하인주소2.Text;
                        #endregion

                        this.cboHSCode.SelectedValue = D.GetString(dlg.적용데이터["CD_PRODUCT"]);
                        this._송장정보.CurrentRow["CD_PRODUCT"] = this.cboHSCode.SelectedValue;

                        this.txt도착도시.Text = D.GetString(dlg.적용데이터["PORT_ARRIVER"]);
                        this._송장정보.CurrentRow["PORT_ARRIVER"] = this.txt도착도시.Text;

                        this.ctx도착국가.CodeValue = D.GetString(dlg.적용데이터["ARRIVER_COUNTRY"]);
                        this._송장정보.CurrentRow["ARRIVER_COUNTRY"] = this.ctx도착국가.CodeValue;
                        this.ctx도착국가.CodeName = D.GetString(dlg.적용데이터["NM_ARRIVER_COUNTRY"]);
                        this._송장정보.CurrentRow["NM_ARRIVER_COUNTRY"] = this.ctx도착국가.CodeName;

                        this.txt전화번호.Text = D.GetString(dlg.적용데이터["REMARK1"]);
                        this._송장정보.CurrentRow["REMARK1"] = this.txt전화번호.Text;
                        this.txt이메일.Text = D.GetString(dlg.적용데이터["REMARK2"]);
                        this._송장정보.CurrentRow["REMARK2"] = this.txt이메일.Text;
                        this.txt받는사람.Text = D.GetString(dlg.적용데이터["REMARK3"]);
                        this._송장정보.CurrentRow["REMARK3"] = this.txt받는사람.Text;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn수주적용_Click(object sender, EventArgs e)
        {
            string 매출처발주번호, filter, noLocation, noStockLocation, dtIn, 포장비고, query, message;
            decimal i, qtReq, qtReqStock, qtReqIm, amGir, amGirAmt, amVat, amt;
            int index;
            DataTable dt, dt수주아이템, dt기포장;
            DataRow newRow, tmpRow;
            DataRow[] dataRowArray, 수주RowArray;

            try
            {
                if (!Chk호선번호 || !Chk매출처) return;

                #region 조기경보시스템
                string 경고문구 = string.Empty;
                string 제외여부 = string.Empty;
                string 지불조건제외여부 = string.Empty;

                WarningLevel warningLevel = WarningLevel.정상;

                EalryWarningSystem EWS = new EalryWarningSystem();

                if (EWS.협조전작성가능여부(this.ctx매출처S.CodeValue) == false)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("협조전작성 불가한 매출처 입니다.");
                    else
                        this.txt경고메시지.Text += "협조전작성 불가한 매출처 입니다." + Environment.NewLine;
                    return;
                }

                EWS.미수금확인(this.ctx매출처S.CodeValue, ref warningLevel, ref 경고문구, ref 제외여부, ref 지불조건제외여부);

                if (제외여부 != "Y")
                {
                    if (!string.IsNullOrEmpty(경고문구))
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage(경고문구);
                        else
                            this.txt경고메시지.Text += 경고문구 + Environment.NewLine;
                    }
                    
                    if (warningLevel == WarningLevel.사용불가) return;
                }
                #endregion

                #region 제재호선 확인
                query = string.Format(@"SELECT * 
                                        FROM CZ_MA_CODEDTL MC
				                        WHERE MC.CD_COMPANY = '{0}'
				                        AND MC.CD_FIELD = 'CZ_SA00062'
				                        AND MC.CD_SYSDEF = '{1}'
                                        AND MC.YN_USE = 'Y'", Global.MainFrame.LoginInfo.CompanyCode, this.ctx호선번호.CodeValue);

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("협조전작성 불가한 호선 입니다.");
                    else
                        this.txt경고메시지.Text += "협조전작성 불가한 호선 입니다." + Environment.NewLine;

                    return;
                }
                #endregion

                #region 동일 호선으로 진행중인 건 확인
                query = string.Format(@"SELECT (GH.NO_GIR + ' / 출고예정 : ' + CONVERT(CHAR(10), CONVERT(DATETIME, WD.DT_COMPLETE), 23) + ' / ' + MC.NM_SYSDEF + ' / ' + MC1.NM_SYSDEF) AS MESSAGE
                                        FROM SA_GIRH GH WITH(NOLOCK)
                                        JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
                                        LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00006' AND MC.CD_SYSDEF = WD.CD_MAIN_CATEGORY
                                        LEFT JOIN MA_CODEDTL MC1 WITH(NOLOCK) ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
                                        WHERE GH.CD_COMPANY = '{0}'
                                        AND GH.STA_GIR = 'R'
                                        AND WD.NO_IMO = '{1}'
                                        UNION ALL
                                        SELECT (GH.NO_GIR + ' / 출고예정 : ' + CONVERT(CHAR(10), CONVERT(DATETIME, WD.DT_COMPLETE), 23) + ' / ' + MC.NM_SYSDEF + ' / ' + MC1.NM_SYSDEF) AS MESSAGE
                                        FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
                                        JOIN CZ_SA_GIRH_PACK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
                                        LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00020' AND MC.CD_SYSDEF = WD.CD_PACK_CATEGORY
                                        LEFT JOIN MA_CODEDTL MC1 WITH(NOLOCK) ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
                                        WHERE GH.CD_COMPANY = '{0}'
                                        AND GH.STA_GIR = 'R'
                                        AND WD.NO_IMO = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this.ctx호선번호.CodeValue);

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    message = string.Empty;

                    foreach (DataRow dr in dt.Rows)
                    {
                        message += dr["MESSAGE"] + Environment.NewLine;
                    }

                    if (this.ShowMessage("동일한 호선으로 진행 중인 건이 " + dt.Rows.Count + "건 있습니다." + Environment.NewLine + Environment.NewLine +
                                         message + Environment.NewLine +
                                         "진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return;
                }
                #endregion

                if (this.chk자동제출.Checked == true)
                {
                    if (string.IsNullOrEmpty(this.ctx수주번호.CodeValue))
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("수주번호를 입력 하세요.");
                        else
                            this.txt경고메시지.Text += "수주번호를 입력 하세요." + Environment.NewLine;

                        this.ctx수주번호.Focus();
                        return;
                    }

                    DataSet dataSet = this._biz.자동제출대상검색(new object[] { this.회사코드,
                                                                                this.ctx매출처.CodeValue,
                                                                                this.ctx호선번호.CodeValue,
                                                                                this.ctx수주번호.CodeValue });

                    수주RowArray = dataSet.Tables[0].Select();
                    dt수주아이템 = dataSet.Tables[1];
                }
                else
                {
                    P_CZ_SA_GIR_SUB dlg = new P_CZ_SA_GIR_SUB(this.ctx매출처S.CodeValue,
                                                              this.ctx매출처S.CodeName,
                                                              this.ctx호선번호.CodeValue,
                                                              this.ctx호선번호.CodeName,
                                                              this.txt호선명.Text,
                                                              false,
                                                              this._품목정보.DataTable);

                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    수주RowArray = dlg.수주데이터;
                    dt수주아이템 = dlg.수주아이템데이터;
                }
                
                if (this.ctx매출처.ReadOnly == ReadOnly.None)
                {
                    #region 기본정보
                    this.ctx매출처S.CodeValue = D.GetString(수주RowArray[0]["CD_PARTNER"]);
                    this.ctx매출처S.CodeName = D.GetString(수주RowArray[0]["LN_PARTNER"]);

                    this.ctx호선번호.CodeValue = D.GetString(수주RowArray[0]["NO_IMO"]);
                    this.ctx호선번호.CodeName = D.GetString(수주RowArray[0]["NO_HULL"]);

                    this.txt호선명.Text = D.GetString(수주RowArray[0]["NM_VESSEL"]);
                    this.입항정보(false);

                    this._기본정보.CurrentRow["CD_PLANT"] = D.GetString(dt수주아이템.Rows[0]["CD_PLANT"]);

                    매출처발주번호 = string.Empty;

                    foreach (DataRow dr in 수주RowArray)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(dr["NO_PO_PARTNER"])))
                            매출처발주번호 += D.GetString(dr["NO_PO_PARTNER"]) + ",";
                    }

                    if (dt수주아이템.Select("ISNULL(QT_BL, 0) > 0").Length > 0)
                    {
                        this.chk검토필요.Checked = true;
                        this.chk관세환급여부.Visible = true;
                        this.chk관세환급여부.Checked = true;
                    }
                    else
                    {
                        this.chk관세환급여부.Visible = false;
                        this.chk관세환급여부.Checked = false;
                    }

                    if (dt수주아이템.Select("ISNULL(YN_CONFIRM, 'N') = 'N'").Length > 0)
                        this.bpPanelControl55.Visible = false;
                    else
                        this.bpPanelControl55.Visible = true;

                    foreach(DataRow dr in 수주RowArray)
					{
                        query = @"SELECT CH.NO_SO 
FROM CZ_SA_CLAIMH CH WITH(NOLOCK)
WHERE CH.CD_COMPANY = '{0}'
AND CH.NO_CLAIM = '{1}'";

                        dt = DBHelper.GetDataTable(string.Format(query, this.회사코드, dr["NO_SO"].ToString()));

                        if (dt != null && dt.Rows.Count > 0)
                            this.txt원본파일번호.Text += dt.Rows[0]["NO_SO"].ToString() + ",";
                    }
                    #endregion

                    #region 송장정보
                    this.cbo거래구분.SelectedValue = D.GetString(수주RowArray[0]["TP_BUSI"]);

                    this.ctx영업그룹.CodeValue = D.GetString(수주RowArray[0]["CD_SALEGRP"]);
                    this.ctx영업그룹.CodeName = D.GetString(수주RowArray[0]["NM_SALEGRP"]);

                    this.매출처설정(D.GetString(수주RowArray[0]["CD_PARTNER"]));

                    if (this.회사코드 == "K200")
                        this.수출자설정("09989");
                    else if (this.회사코드 == "S100")
                        this.수출자설정("10286");
                    else if (this.회사코드 == "K300")
                        this.수출자설정("99999");
                    else
                        this.수출자설정("00000");

                    this.txt매출처발주번호.Text = (string.IsNullOrEmpty(매출처발주번호) == true ? string.Empty : 매출처발주번호.Remove(매출처발주번호.Length - 1));

                    this.cbo선적조건.SelectedValue = D.GetString(수주RowArray[0]["TP_TRANS"]);
                    this.cboINCOTERMS지역.SelectedValue = D.GetString(수주RowArray[0]["TP_TRANSPORT"]);
                    this.cbo지불조건.SelectedValue = D.GetString(수주RowArray[0]["COND_PAY"]);

                    this.cbo통화.SelectedValue = D.GetString(수주RowArray[0]["CD_EXCH"]);
                    this.cur외화금액.DecimalValue = (Decimal)dt수주아이템.Compute("SUM(AM_GIR)", string.Empty);
                    #endregion
                }

                #region 품목정보
                i = D.GetDecimal(this._품목정보.DataTable.Compute("MAX(SEQ_GIR)", string.Empty));

                this._품목정보.Redraw = false;
                dt기포장 = new DataTable();
                dt기포장.Columns.Add("NO_GIR");
                dt기포장.Columns.Add("NO_PACK");
                dt기포장.Columns.Add("NO_FILE");
                dt기포장.Columns.Add("DC_PACK");

                dt = dt수주아이템.DefaultView.ToTable(true, "NO_SO", "SEQ_SO");
                index = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

                    filter = "NO_SO = '" + D.GetString(dr["NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(dr["SEQ_SO"]) + "'";

                    dataRowArray = dt수주아이템.Select(filter);

                    if (dataRowArray.Length == 0) continue;

                    tmpRow = dataRowArray[0];

                    qtReq = 0;
                    qtReqStock = 0;
                    qtReqIm = 0;
                    amGir = 0;
                    amGirAmt = 0;
                    amVat = 0;
                    amt = 0;

                    dtIn = string.Empty;
                    noLocation = string.Empty;
                    noStockLocation = string.Empty;

                    foreach (DataRow dr1 in dataRowArray)
                    {
                        qtReq += D.GetDecimal(dr1["QT_REQ"]);
                        qtReqIm += D.GetDecimal(dr1["QT_REQ_IM"]);
                        amGir += D.GetDecimal(dr1["AM_GIR"]);
                        amGirAmt += D.GetDecimal(dr1["AM_GIRAMT"]);
                        amVat += D.GetDecimal(dr1["AM_VAT"]);
                        amt += D.GetDecimal(dr1["AMT"]);

                        if (D.GetDecimal(dtIn) < D.GetDecimal(dr1["DT_IN"]))
                            dtIn = D.GetString(dr1["DT_IN"]);

                        if (D.GetString(dr1["DT_IN"]) == "STOCK")
                        {
                            noStockLocation = D.GetString(dr1["NO_LOCATION"]);
                            qtReqStock += D.GetDecimal(dr1["QT_REQ"]);
                        }
                        else
                        {
                            noLocation = D.GetString(dr1["NO_LOCATION"]);
                        }
                    }

                    DataTable dt1 = this._biz.기포장정보(new object[] { this.회사코드, dr["NO_SO"].ToString(), dr["SEQ_SO"].ToString(), qtReq });

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (dt기포장.Select(string.Format("NO_GIR = '{0}' AND NO_PACK = '{1}'", dr1["NO_GIR"].ToString(),
                                                                                                dr1["NO_PACK"].ToString())).Length == 0)
                            dt기포장.ImportRow(dr1);
                    }

                    #region 품목추가
                    newRow = this._품목정보.DataTable.NewRow();

                    newRow["S"] = "N";
                    newRow["SEQ_GIR"] = ++i;
                    newRow["NO_LINE"] = i;
                    newRow["CD_ITEM"] = tmpRow["CD_ITEM"];
                    newRow["NM_ITEM"] = tmpRow["NM_ITEM"];
                    newRow["NO_DSP"] = tmpRow["NO_DSP"];
                    newRow["NM_SUBJECT"] = tmpRow["NM_SUBJECT"];
                    newRow["CD_ITEM_PARTNER"] = tmpRow["CD_ITEM_PARTNER"];
                    newRow["NM_ITEM_PARTNER"] = tmpRow["NM_ITEM_PARTNER"];
                    newRow["CD_PLANT"] = tmpRow["CD_PLANT"];
                    newRow["TP_ITEM"] = tmpRow["TP_ITEM"];
                    newRow["STND_ITEM"] = tmpRow["STND_ITEM"];
                    newRow["DT_DUEDATE"] = tmpRow["DT_DUEDATE"];
                    newRow["DT_IO"] = dtIn;
                    newRow["DT_REQGI"] = tmpRow["DT_REQGI"];
                    newRow["YN_INSPECT"] = tmpRow["YN_INSPECT"];
                    newRow["CD_SL"] = tmpRow["CD_SL"];
                    newRow["NM_SL"] = tmpRow["NM_SL"];
                    newRow["TP_GI"] = tmpRow["TP_GI"];
                    newRow["NM_GI"] = tmpRow["NM_GI"];
                    newRow["QT_INV"] = tmpRow["QT_INV"];
                    newRow["QT_TAX"] = tmpRow["QT_TAX"];
                    newRow["QT_BL"] = tmpRow["QT_BL"];
                    newRow["CUSTOMS"] = tmpRow["CUSTOMS"];
                    newRow["QT_GIR"] = qtReq;
                    newRow["QT_GIR_STOCK"] = qtReqStock;
                    newRow["QT_GI"] = tmpRow["QT_GI"];
                    newRow["CD_EXCH"] = tmpRow["CD_EXCH"];
                    newRow["UM"] = tmpRow["UM"];
                    newRow["AM_GIR"] = amGir;
                    newRow["AM_GIRAMT"] = amGirAmt;
                    newRow["AM_VAT"] = amVat;
                    newRow["AMT"] = amt;
                    newRow["UNIT"] = tmpRow["UNIT"];
                    newRow["QT_GIR_IM"] = qtReqIm;
                    newRow["GI_PARTNER"] = tmpRow["GI_PARTNER"];
                    newRow["LN_PARTNER"] = tmpRow["NM_GI_PARTNER"];
                    newRow["NO_PROJECT"] = tmpRow["NO_PROJECT"];
                    newRow["NM_PROJECT"] = tmpRow["NM_PROJECT"];
                    newRow["RT_EXCH"] = tmpRow["RT_EXCH"];
                    newRow["RT_VAT"] = tmpRow["RT_VAT"];
                    newRow["NO_SO"] = tmpRow["NO_SO"];
                    newRow["SEQ_SO"] = tmpRow["SEQ_SO"];
                    newRow["CD_SALEGRP"] = tmpRow["CD_SALEGRP"];
                    newRow["NM_SALEGRP"] = tmpRow["NM_SALEGRP"];
                    newRow["TP_VAT"] = tmpRow["TP_VAT"];
                    newRow["NO_EMP"] = tmpRow["NO_EMP"];
                    newRow["TP_IV"] = tmpRow["TP_IV"];
                    newRow["FG_TAXP"] = tmpRow["FG_TAXP"];
                    newRow["TP_BUSI"] = tmpRow["TP_BUSI"];
                    newRow["NO_LC"] = string.Empty;
                    newRow["SEQ_LC"] = 0.0;
                    newRow["FG_LC_OPEN"] = string.Empty;
                    newRow["DC_RMK"] = tmpRow["DC_RMK"];
                    newRow["NO_PO_PARTNER"] = tmpRow["NO_PO_PARTNER"];
                    newRow["NO_POLINE_PARTNER"] = tmpRow["NO_POLINE_PARTNER"];
                    newRow["GIR"] = tmpRow["GIR"];
                    newRow["IV"] = tmpRow["IV"];
                    newRow["CD_WH"] = tmpRow["CD_WH"];
                    newRow["NO_LOCATION"] = noLocation;
                    newRow["NO_LOCATION_STOCK"] = noStockLocation;
                    newRow["SEQ_PROJECT"] = tmpRow["SEQ_PROJECT"];
                    newRow["YN_PICKING"] = tmpRow["YN_PICKING"];
                    newRow["L_CD_USERDEF1"] = string.Empty;
                    newRow["TP_UM_TAX"] = tmpRow["TP_UM_TAX"];
                    newRow["UMVAT_GIR"] = tmpRow["UMVAT_GIR"];
                    newRow["CD_SUPPLIER"] = tmpRow["CD_SUPPLIER"];
                    newRow["NM_SUPPLIER"] = tmpRow["NM_SUPPLIER"];
                    newRow["GRP_ITEM"] = tmpRow["GRP_ITEM"];

                    this._품목정보.DataTable.Rows.Add(newRow);
                    #endregion
                }

                if (dt기포장.Rows.Count > 0)
                {
                    포장비고 = string.Empty;

                    foreach(DataRow dr in dt기포장.Rows)
                        포장비고 += "[" + dr["NO_GIR"].ToString() + "-" + dr["NO_PACK"].ToString() + "/" + dr["NO_FILE"].ToString() + "] " + dr["DC_PACK"].ToString() + " ";

                    포장비고 = 포장비고.Trim();

                    this.txt기포장정보.Text = 포장비고;
                    this._기본정보.CurrentRow["DC_RMK3"] = 포장비고;
                }

                this.OnDataChanged(null, null);

                if (this._품목정보.Rows.Count > 0) this.수정여부설정(false);
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._품목정보.SumRefresh();
                this._품목정보.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P11_ID_MENU = this.Name;
                e.HelpParam.P41_CD_FIELD1 = "MA_B000020";
                e.HelpParam.P42_CD_FIELD2 = "US";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx수주번호검색_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P11_ID_MENU = "CZ_SA_GIR_SUB";
                e.HelpParam.P21_FG_MODULE = "N";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx수주번호검색_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                this.ctx호선번호.CodeValue = D.GetString(e.HelpReturn.Rows[0]["NO_IMO"]);
                this.ctx호선번호.CodeName = D.GetString(e.HelpReturn.Rows[0]["NO_HULL"]);
                this.txt호선명.Text = D.GetString(e.HelpReturn.Rows[0]["NM_VESSEL"]);
                this.입항정보(false);

                this.ctx매출처S.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                this.ctx매출처S.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 수주번호검색(string 수주번호)
        {
            try
            {
                DataTable dt = Global.MainFrame.FillDataTable(@"SELECT MH.NO_IMO, MH.NO_HULL, MH.NM_VESSEL, MP.CD_PARTNER, MP.LN_PARTNER 
                                                                FROM SA_SOH SH WITH(NOLOCK)
                                                                LEFT JOIN CZ_SA_QTNH QH WITH(NOLOCK) ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
                                                                LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = QH.NO_IMO
                                                                LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER "
                                                             + "WHERE SH.CD_COMPANY = '" + this.회사코드 + "' AND SH.NO_SO = '" + 수주번호 + "'");

                if (dt.Rows.Count == 1)
                {
                    this.ctx호선번호.CodeValue = D.GetString(dt.Rows[0]["NO_IMO"]);
                    this.ctx호선번호.CodeName = D.GetString(dt.Rows[0]["NO_HULL"]);
                    this.txt호선명.Text = D.GetString(dt.Rows[0]["NM_VESSEL"]);
                    this.입항정보(false);

                    this.ctx매출처S.CodeValue = D.GetString(dt.Rows[0]["CD_PARTNER"]);
                    this.ctx매출처S.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                }
                else
                {
                    this.ctx호선번호.CodeValue = string.Empty;
                    this.ctx호선번호.CodeName = string.Empty;
                    this.txt호선명.Text = string.Empty;
                    this.입항정보(false);

                    this.ctx매출처S.CodeValue = string.Empty;
                    this.ctx매출처S.CodeName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctxDeliveryTo_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.납품처적용(helpReturn.CodeValue, helpReturn.CodeName, helpReturn.Rows[0]["DC_ADDRESS"].ToString(), helpReturn.Rows[0]["NO_TEL"].ToString());
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 납품처적용(string 납품처코드, string 납품처이름, string 수취인주소, string 수취인전화번호)
        {
            try
            {           
                this.ctx납품처.CodeValue = 납품처코드;
                this._기본정보.CurrentRow["CD_DELIVERY_TO"] = 납품처코드;

                this.ctx납품처.CodeName = 납품처이름;
                this._기본정보.CurrentRow["LN_DELIVERY_TO"] = 납품처이름;

                this.txt수취업체명.Text = 납품처이름;
                this._기본정보.CurrentRow["NM_DELIVERY"] = 납품처이름;

                this.txt수취인주소.Text = 수취인주소;
                this._기본정보.CurrentRow["DC_DELIVERY_ADDR"] = 수취인주소;

                this.txt수취인연락처.Text = 수취인전화번호;
                this._기본정보.CurrentRow["DC_DELIVERY_TEL"] = 수취인전화번호;

                this.chk주소표시.Checked = false;
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx매출처_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.매출처설정(e.HelpReturn.CodeValue);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 매출처설정(string codeValue)
        {
            DataTable dt;

            try
            {
                this.ctx매출처.CodeValue = codeValue;
                this._송장정보.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                
                dt = Global.MainFrame.FillDataTable(@"SELECT LN_PARTNER, 
                                                             DC_ADS1_H,
                                                             DC_ADS1_D 
                                                      FROM MA_PARTNER WITH(NOLOCK) 
                                                      WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                    @"AND CD_PARTNER = '" + this.ctx매출처.CodeValue + "'");

                this.ctx매출처.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this.txt매출처.Text = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this._송장정보.CurrentRow["NM_PARTNER"] = D.GetString(dt.Rows[0]["LN_PARTNER"]);

                this.txt매출처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this.txt매출처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                this._송장정보.CurrentRow["ADDR1_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this._송장정보.CurrentRow["ADDR2_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx수출자_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.수출자설정(e.HelpReturn.CodeValue);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 수출자설정(string codeValue)
        {
            DataTable dt;

            try
            {
                this.ctx수출자.CodeValue = codeValue;
                this._송장정보.CurrentRow["CD_EXPORT"] = this.ctx수출자.CodeValue;
                
                dt = Global.MainFrame.FillDataTable(@"SELECT LN_PARTNER, 
                                                             DC_ADS1_H,
                                                             DC_ADS1_D 
                                                      FROM MA_PARTNER WITH(NOLOCK) 
                                                      WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                    @"AND CD_PARTNER = '" + this.ctx수출자.CodeValue + "'");

                this.ctx수출자.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this.txt수출자주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this.txt수출자주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                this._송장정보.CurrentRow["NM_EXPORT"] = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this._송장정보.CurrentRow["ADDR1_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this._송장정보.CurrentRow["ADDR2_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx수하인_QueryAfter(object sender, BpQueryArgs e)
        {
            DataTable dt;

            try
            {
                if (e.HelpReturn != null)
                {
                    this.ctx수하인.CodeValue = e.HelpReturn.CodeValue;
                    this.ctx수하인.CodeName = e.HelpReturn.CodeName;
                    this.txt수하인.Text = this.ctx수하인.CodeName;
                    this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                    this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;

                    dt = Global.MainFrame.FillDataTable(@"SELECT DC_ADS1_H, 
                                                                 DC_ADS1_D 
                                                          FROM MA_PARTNER WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                        @"AND CD_PARTNER = '" + this.ctx수하인.CodeValue + "' ");

                    this.txt수하인주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this.txt수하인주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                    this._송장정보.CurrentRow["ADDR1_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this._송장정보.CurrentRow["ADDR2_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx착하통지처_QueryAfter(object sender, BpQueryArgs e)
        {
            DataTable dt;
            try
            {
                if (e.HelpReturn != null)
                {
                    this.ctx착하통지처.CodeValue = e.HelpReturn.CodeValue;
                    this.ctx착하통지처.CodeName = e.HelpReturn.CodeName;
                    this.txt착하통지처.Text = this.ctx착하통지처.CodeName;
                    this._송장정보.CurrentRow["CD_NOTIFY"] = this.ctx착하통지처.CodeValue;
                    this._송장정보.CurrentRow["NM_NOTIFY"] = this.ctx착하통지처.CodeName;

                    dt = Global.MainFrame.FillDataTable(@"SELECT DC_ADS1_H, 
                                                                 DC_ADS1_D 
                                                          FROM MA_PARTNER WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                        @"AND CD_PARTNER = '" + this.ctx착하통지처.CodeValue + "' ");
                    
                    this.txt착하통지처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this.txt착하통지처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                    this._송장정보.CurrentRow["ADDR1_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this._송장정보.CurrentRow["ADDR2_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx호선번호_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.txt호선명.Text = D.GetString(e.HelpReturn.Rows[0]["NM_VESSEL"]);
                this.입항정보(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
                {
                    this.txt호선명.Text = string.Empty;
                    this.입항정보(false);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (this.cboMainCategory.Name == name)
                {
                    this.SetSubCategory(D.GetString(this.cboMainCategory.SelectedValue));
                    
                    this.chkPacking.Checked = false;

                    if (!string.IsNullOrEmpty(D.GetString(this.cboMainCategory.SelectedValue)))
                        this.chkPacking.Checked = true;
                }

                this.Set협조내용(D.GetString(this.cboMainCategory.SelectedValue), D.GetString(this.cboSubCategory.SelectedValue));
                this.Set자동제출(D.GetString(this.cboMainCategory.SelectedValue), D.GetString(this.cboSubCategory.SelectedValue));
                this.Set비고설정(D.GetString(this.cboMainCategory.SelectedValue), D.GetString(this.cboSubCategory.SelectedValue));

                this.포워더정보설정();
                this.포워딩비용청구방식설정();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void chk자동제출_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk자동제출.Checked == true)
                {
                    this.bpPanelControl9.Visible = false;
                    this.lbl자동제출.Visible = true;
                    this.nud자동제출.Visible = true;
                    this.lbl자동제출1.Visible = true;

                    this.cboMainCategory.Enabled = false;
                    this.cboSubCategory.Enabled = false;
                    this.btn협조전적용.Enabled = false;
                    this._품목정보.Cols["S"].AllowEditing = false;
                    this.btn삭제.Enabled = false;
                }
                else
                {
                    this.bpPanelControl9.Visible = true;
                    this.lbl자동제출.Visible = false;
                    this.nud자동제출.Visible = false;
                    this.lbl자동제출1.Visible = false;

                    this.cboMainCategory.Enabled = true;
                    this.cboSubCategory.Enabled = true;
                    this.btn협조전적용.Enabled = true;
                    this._품목정보.Cols["S"].AllowEditing = true;
                    this.btn삭제.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt수하인_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txt수하인.Text != this.ctx수하인.CodeName)
                {
                    if (!string.IsNullOrEmpty(this.ctx수하인.CodeValue))
                    {
                        this.ctx수하인.CodeValue = string.Empty;
                        this.ctx수하인.CodeName = string.Empty;
                        this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                        this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;
                    }
                    
                    this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.txt수하인.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt착하통지처_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txt착하통지처.Text != this.ctx착하통지처.CodeName)
                {
                    if (!string.IsNullOrEmpty(this.ctx착하통지처.CodeValue))
                    {
                        this.ctx착하통지처.CodeValue = string.Empty;
                        this.ctx착하통지처.CodeName = string.Empty;
                        this._송장정보.CurrentRow["CD_NOTIFY"] = this.ctx착하통지처.CodeValue;
                        this._송장정보.CurrentRow["NM_NOTIFY"] = this.ctx착하통지처.CodeName;
                    }

                    this._송장정보.CurrentRow["NM_NOTIFY"] = this.txt착하통지처.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx청구호선_CodeChanged(object sender, EventArgs e)
        {
            string query;

            try
            {
                query = @"UPDATE CZ_SA_GIRH_WORK_DETAIL
                          SET NO_IMO_BILL = '" + this.ctx청구호선.CodeValue + "'" + Environment.NewLine +
                         "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND NO_GIR = '" + this.txt의뢰번호.Text + "'";

                DBHelper.ExecuteScalar(query);

                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                else
                    this.txt경고메시지.Text += "자료가 정상적으로 저장되었습니다." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _header_ControlValueChanged
        private void PageDataChanged(object sender, EventArgs e)
        {
            try
            {
                bool bChange = _품목정보.GetChanges() != null ? true : false;

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                bool bChange = (_기본정보.GetChanges() != null ? true : false) || (_송장정보.GetChanges() != null ? true : false);

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 이벤트
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string colname = _품목정보.Cols[e.Col].Name;

                if (colname == "QT_GIR")
                {
                    string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                    string newValue = ((FlexGrid)sender).EditData;

                    if (D.GetDecimal(newValue) <= 0)
                    {
                        _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                        
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CZ_@ 은(는) 0보다 커야합니다.", this.DD("의뢰수량"));
                        else
                            this.txt경고메시지.Text += "의뢰수량은 0보다 커야 합니다." + Environment.NewLine;

                        e.Cancel = true;
                        return;
                    }

                    decimal 출고수량 = D.GetDecimal(_품목정보["QT_GI"]);
                    if (출고수량 > 0 && D.GetDecimal(newValue) < 출고수량)
                    {
                        _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                        
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("의뢰수량이 출고수량보다 작을 수 없습니다.");
                        else
                            this.txt경고메시지.Text += "의뢰수량이 출고수량보다 작을 수 없습니다." + Environment.NewLine;

                        e.Cancel = true;
                        return;
                    }

                    //의뢰수량체크 프로스져를 호출한다.
                    object[] obj = new object[8];
                    obj[0] = this.회사코드;
                    obj[1] = txt의뢰번호.Text;
                    obj[2] = D.GetDecimal(_품목정보[e.Row, "SEQ_GIR"]);
                    obj[3] = _품목정보[e.Row, "NO_SO"].ToString();
                    obj[4] = D.GetDecimal(_품목정보[e.Row, "SEQ_SO"]);
                    obj[5] = _품목정보[e.Row, "NO_LC"].ToString();
                    obj[6] = D.GetDecimal(_품목정보[e.Row, "SEQ_LC"]);
                    obj[7] = D.GetDecimal(newValue);

                    // is수주수량초과허용 = false 일 경우 수주(의뢰) 수량이 초과하려는 경우 수주(의뢰) 수량으로 맞춘다.
                    // is수주수량초과허용 = true  일 경우 수주(의뢰) 수량이 초과허용 가능하다.
                    //decimal qtso_AddAllow = D.GetDecimal(_flex[e.Row, "QT_MINUS"]) + (D.GetDecimal(_flex[e.Row, "QT_MINUS"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));
                    decimal 의뢰한도수량 = D.GetDecimal(_품목정보["QT_SO"]) * D.GetDecimal(_품목정보["RT_PLUS"]) * 0.01M;
                    의뢰한도수량 += D.GetDecimal(_품목정보["QT_SO"]);
                    //D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) + (D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));

                    decimal 분할의뢰수량;
                    bool is입력 = _biz.Check(obj, out 분할의뢰수량);

                    if (!is입력)
                    {
                        bool is허용 = false;
                        if (is수주수량초과허용)
                        {
                            // 범위 허용
                            if (의뢰한도수량 >= D.GetDecimal(newValue) + 분할의뢰수량)
                            {
                                is허용 = true;
                            }
                        }

                        if (!is허용)
                        {
                            _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                            e.Cancel = true;

                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("의뢰수량이 한도수량을 초과하였습니다.");
                            else
                                this.txt경고메시지.Text += "의뢰수량이 한도수량을 초과하였습니다." + Environment.NewLine;
                        }
                    }

                    this.금액계산();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                if (grid["DT_IO"].ToString() == "STOCK" || grid["DT_IO"].ToString() == "CHARGE")
                {
                    e.Cancel = true;
                    return;
                }

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P00_CHILD_MODE = "출고창고";

                        e.Parameter.P61_CODE1 = @"MS.CD_SL AS CODE,
	                                              MS.NM_SL AS NAME";
                        e.Parameter.P62_CODE2 = @"MA_SL MS WITH(NOLOCK)";
                        e.Parameter.P63_CODE3 = "WHERE MS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                "AND MS.CD_SL IN ('SL01', 'VL01', 'VL02')";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid flex;

            try
            {
                flex = ((FlexGrid)sender);

                if (e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed) return;
                if (flex.Cols[e.Col].Name != "QT_TAX" && 
                    flex.Cols[e.Col].Name != "QT_BL" &&
                    flex.Cols[e.Col].Name != "CUSTOMS") return;

                CellStyle style = flex.GetCellStyle(e.Row, e.Col);

                if (style == null)
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                    else
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
                else if (style.Name == "미납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                }
                else if (style.Name == "납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) == 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void SubMenuClick(object sender, EventArgs e)
        {
            string 아이템코드 = string.Empty;
            string 공장코드 = string.Empty;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "현재고조회":
                    for (int i = _품목정보.Row - 2; i < _품목정보.Row + 40; i++)
                    {
                        if (i < _품목정보.Rows.Count - 2)
                        {
                            공장코드 = _품목정보.DataTable.Rows[i]["CD_PLANT"].ToString();
                            아이템코드 += _품목정보.DataTable.Rows[i]["CD_ITEM"].ToString() + "|";
                        }
                        else
                            break;
                    }

                    pur.P_PU_STOCK_SUB m_dlg = new pur.P_PU_STOCK_SUB(공장코드, 아이템코드);
                    m_dlg.ShowDialog(this);
                    break;
            }
        }
        #endregion

        #region ♣ 기타
        private void 금액계산()
        {
            decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["QT_GIR"]));
            decimal 면적2M = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["QT_SQUARE"]));
            decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["UM"]));
            decimal 단가2M = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["UM_SQUARE"]));
            decimal 환율 = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["RT_EXCH"]));

            decimal 총면적 = Unit.수량(DataDictionaryTypes.SA, 의뢰수량 * 면적2M);

            decimal 부가세율 = D.GetDecimal(_품목정보["RT_VAT"]) * 0.01M;
            decimal 외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
            decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * 환율);
            decimal 부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * 부가세율);
            decimal 합계 = 원화금액 + 부가세;

            _품목정보["SUM_SQUARE"] = 총면적;
            _품목정보["AM_GIR"] = 외화금액;
            _품목정보["AM_GIRAMT"] = 원화금액;
            _품목정보["AM_VAT"] = 부가세;
            _품목정보["AMT"] = 합계;

            decimal 재고단위수량 = D.GetDecimal(_품목정보["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(_품목정보["UNIT_SO_FACT"]);
            _품목정보["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, 재고단위수량 * 의뢰수량);
            this.cur외화금액.DecimalValue = (Decimal)this._품목정보.DataTable.Compute("SUM(AM_GIR)", string.Empty);
        }

        private void SetSubCategory(string mainCategory)
        {
            if (string.IsNullOrEmpty(mainCategory) == false)
            {
                this.cboSubCategory.DataSource = this.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00006").Select("CODE = '" + mainCategory + "'")[0].ItemArray[3].ToString());
            }
            else
                this.cboSubCategory.DataSource = null;

            this.cboSubCategory.DisplayMember = "NAME";
            this.cboSubCategory.ValueMember = "CODE";

            this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = null;
        }

        private void Set협조내용(string mainCategory, string subCategory)
        {
            DataTable dt;
            string query;

            if (!string.IsNullOrEmpty(mainCategory) && 
                !string.IsNullOrEmpty(subCategory))
            {
                query = @"SELECT '' AS CODE, '' AS NAME
                              UNION ALL
                              SELECT MC.CD_SYSDEF AS CODE, MC.NM_SYSDEF AS NAME
                              FROM MA_CODEDTL MC WITH(NOLOCK)
                              WHERE MC.CD_COMPANY = '{0}'
                              AND MC.CD_FIELD = 'CZ_SA00032'
                              AND MC.USE_YN = 'Y'
                              AND (MC.CD_SYSDEF = '' OR (MC.CD_FLAG1 = 'W' AND MC.CD_FLAG2 = '{1}' AND MC.CD_FLAG3 = '{2}'))";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                mainCategory,
                                                                subCategory));

                this.cbo협조내용.DataSource = dt;
            }
            else
                this.cbo협조내용.DataSource = null;

            this.cbo협조내용.DisplayMember = "NAME";
            this.cbo협조내용.ValueMember = "CODE";

            this._기본정보.CurrentRow["CD_RMK"] = null;
        }

        private void Set자동제출(string mainCategory, string subCategory)
        {
            if (!this._품목정보.HasNormalRow &&
                ((mainCategory == "002" && subCategory == "DIR") ||
                 (mainCategory == "003" && (subCategory == "004" ||
                                            subCategory == "005"))))
                this.chk자동제출.Enabled = true;
            else
            {
                this.chk자동제출.Checked = false;
                this.chk자동제출.Enabled = false;
            }
            
            this.chk자동제출_CheckedChanged(null, null);
        }

        private void Set비고설정(string mainCategory, string subCategory)
		{
            if (mainCategory == "001" || // 본선선적
                (mainCategory == "003" && subCategory == "010")) // 직접송품-퀵
                this.chk검토필요.Checked = true;
            else
                this.chk검토필요.Checked = false;

            #region 적재허가서
            if (mainCategory == "001") // 본선선적
                this.chk당사발행.Checked = true;
            else
                this.chk당사발행.Checked = false;
            #endregion

			#region 위험물 별도 포장
			if (mainCategory == "001" || // 본선선적
                (mainCategory == "003" && subCategory == "003")) // 직접송품-COURIER (해외)
            {
                this.chk위험물별도포장.Enabled = false;
                this.txt위험물별도포장.ReadOnly = true;
                this.btn위험물적용.Enabled = false;
			}
            else
			{
                this.chk위험물별도포장.Enabled = true;
                this.txt위험물별도포장.ReadOnly = false;
                this.btn위험물적용.Enabled = true;
            }
            #endregion

            #region 납품처
            if ((mainCategory == "003" && subCategory == "004") || // 직접송품-택배
                (mainCategory == "003" && subCategory == "010") || // 직접송품-퀵
                (mainCategory == "002" && subCategory == "DIR") || // 대리점전달-물류부 직접전달
                (mainCategory == "003" && subCategory == "005")) // 직접송품-포워딩 직접픽업
            {
                this.txt수취인주소.Enabled = true;
                this.txt수취인연락처.Enabled = true;
                this.txt화물도착지점.Enabled = false;
                this.txt수취업체명.Enabled = false;
            }
            else if (mainCategory == "003" && subCategory == "011") // 직접송품-화물
			{
                this.txt수취인주소.Enabled = false;
                this.txt수취인연락처.Enabled = true;
                this.txt화물도착지점.Enabled = true;
                this.txt수취업체명.Enabled = true;
            }
            else
			{
                this.txt수취인주소.Enabled = false;
                this.txt수취인연락처.Enabled = false;
                this.txt화물도착지점.Enabled = false;
                this.txt수취업체명.Enabled = false;
            }
			#endregion

			#region 포장방법
            if (mainCategory == "001" || // 본선선적 
                (mainCategory == "002" && subCategory == "DIR") || // 대리점전달-물류부 직접전달
                (mainCategory == "003" && subCategory == "004") || // 직접송품-택배
                (mainCategory == "003" && subCategory == "005") || // 직접송품-포워딩 직접픽업
                (mainCategory == "003" && subCategory == "009") || // 직접송품-하륙대행
                (mainCategory == "003" && subCategory == "007") || // 직접송품-선적대행
                (mainCategory == "003" && subCategory == "010") || // 직접송품-퀵
                (mainCategory == "003" && subCategory == "011")) // 직접송품-화물

            {
                this.rdo해외탁송.Enabled = true;
                this.rdo국내선적.Enabled = true;
			}
            else
			{
                this.rdo해외탁송.Enabled = false;
                this.rdo국내선적.Enabled = false;
            }

            if (mainCategory == "001" || // 본선선적 
                (mainCategory == "003" && subCategory == "009") || // 직접송품-하륙대행
                (mainCategory == "003" && subCategory == "007") || // 직접송품-선적대행
                (mainCategory == "003" && subCategory == "012")) //직접송품-이관

            {
                this.rdo국내선적.Checked = true;
                this.rdo해외탁송.Checked = false;
            }
            else
			{
                this.rdo국내선적.Checked = false;
                this.rdo해외탁송.Checked = true;
            }
            #endregion

            #region 픽업/전달 시간
			if ((mainCategory == "003" && subCategory == "005") || // 직접송품-포워딩 직접픽업
				(mainCategory == "002" && subCategory == "DIR") || // 대리점전달-물류부 직접전달
                (mainCategory == "003" && subCategory == "010") || // 직접송품-퀵
                (mainCategory == "003" && subCategory == "002")) // 직접송품-해송
                this.cbo전달시간.Enabled = true;
			else
				this.cbo전달시간.Enabled = false;
            #endregion

            #region 서류마감 시간
            if ((mainCategory == "003" && subCategory == "002") || // 직접송품-해송
                (mainCategory == "002" && subCategory == "DIR")) // 대리점전달-물류부 직접전달
                this.dtp서류마감.Enabled = true;
            else
                this.dtp서류마감.Enabled = false;
            #endregion

            #region 선적위치
            if (mainCategory == "001" || // 본선선적
                (mainCategory == "002" && subCategory == "DIR")) // 대리점전달-물류부 직접전달
                this.cbo선적위치.Enabled = true;
            else
                this.cbo선적위치.Enabled = false;
            #endregion
        }

		private void 수정여부설정(bool isEnabled)
        {
            string 의뢰상태;
            
            try
            {
                if ((Global.MainFrame.LoginInfo.UserID != "S-304" && 
                     Global.MainFrame.LoginInfo.UserID != "S-180" &&
                     Global.MainFrame.LoginInfo.UserID != "S-587" &&
                     this.회사코드 != Global.MainFrame.LoginInfo.CompanyCode) || this.차수 > 0)
                {
                    this.SetToolBarButtonState(false, false, false, false, false);

                    this.btn제출.Enabled = false;
                    this.btn삭제요청.Enabled = false;
                    this.btn삭제요청취소.Enabled = false;

                    this.chk자동제출.Enabled = false;

                    this.컨트롤활성화(false);
                }
                else
                {
                    의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                    if (this.chk자동제출.Checked == true)
                    {
                        this.btn제출.Enabled = false;
                        this.btn삭제요청.Enabled = false;
                        this.btn삭제요청취소.Enabled = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(의뢰상태))
                            this.btn제출.Enabled = true;
                        else
                            this.btn제출.Enabled = false;

                        if (의뢰상태 == "R")
                            this.btn삭제요청.Enabled = true;
                        else
                            this.btn삭제요청.Enabled = false;

                        if (의뢰상태 == "D")
                            this.btn삭제요청취소.Enabled = true;
                        else
                            this.btn삭제요청취소.Enabled = false;
                    }

					if (Global.MainFrame.LoginInfo.UserID != "S-180" &&
                        Global.MainFrame.LoginInfo.UserID != "S-304" &&
                        Global.MainFrame.LoginInfo.UserID != "S-587" && 
                        (의뢰상태 == "O" || 의뢰상태 == "C" || 의뢰상태 == "D" || 의뢰상태 == "R"))
						this.컨트롤활성화(false);
					else
						this.컨트롤활성화(true);						

                    if (!(의뢰상태 == "O" || 의뢰상태 == "C" || 의뢰상태 == "D" || 의뢰상태 == "R"))
					{
                        this.Set비고설정(D.GetString(this.cboMainCategory.SelectedValue), D.GetString(this.cboSubCategory.SelectedValue));
                        this.Chk검토필요_CheckedChanged(null, null);
                    }

					if (isEnabled == true)
                    {
                        this.ctx수주번호.ReadOnly = ReadOnly.None;
                        this.ctx호선번호.ReadOnly = ReadOnly.None;
                        this.ctx매출처S.ReadOnly = ReadOnly.None;

                        this.cbo거래구분.Enabled = true;
                        this.ctx영업그룹.ReadOnly = ReadOnly.None;
					}
                    else
                    {
                        this.ctx수주번호.ReadOnly = ReadOnly.TotalReadOnly;
                        this.ctx호선번호.ReadOnly = ReadOnly.TotalReadOnly;
                        this.ctx매출처S.ReadOnly = ReadOnly.TotalReadOnly;

                        this.cbo거래구분.Enabled = false;
                        this.ctx영업그룹.ReadOnly = ReadOnly.TotalReadOnly;

						this.chk자동제출.Enabled = false;
                        if (this.chk자동제출.Checked == true)
                        {
                            this.btn수주적용.Enabled = false;
                            this.chk자동제출_CheckedChanged(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 컨트롤활성화(bool 활성여부)
        {
            try
            {
                #region 기본정보
                this.ctx출고의뢰자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx납품처.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);

                this.cboMainCategory.Enabled = 활성여부;
                this.cboSubCategory.Enabled = 활성여부;
                this.cbo협조내용.Enabled = 활성여부;
                this.cbo비용청구방법.Enabled = 활성여부;
                this.chkPacking.Enabled = 활성여부;
                this.chk관세환급여부.Enabled = 활성여부;
                this.rboA포워더.Enabled = 활성여부;
                this.rboB포워더.Enabled = 활성여부;
                this.rboC포워더.Enabled = 활성여부;
                this.rboPrice.Enabled = 활성여부;
                this.rboTT.Enabled = 활성여부;
                this.rboETC.Enabled = 활성여부;
                this.cboFreight.Enabled = 활성여부;
                this.dtp출고예정일.Enabled = 활성여부;
                this.dtp포장예정일.Enabled = 활성여부;
                this.dtp청구예정일.Enabled = 활성여부;
                this.nud자동제출.Enabled = 활성여부;
                
                this.curA포워더운임.ReadOnly = !활성여부;
                this.curB포워더운임.ReadOnly = !활성여부;
                this.curC포워더운임.ReadOnly = !활성여부;
                this.cur중량.ReadOnly = !활성여부;
                this.txt수정취소.ReadOnly = !활성여부;
                this.txt매출비고.ReadOnly = !활성여부;

				this.cbo전달시간.Enabled = 활성여부;
                this.dtp서류마감.Enabled = 활성여부;
                this.txt수취업체명.ReadOnly = !활성여부;
                this.txt수취인주소.ReadOnly = !활성여부;
                this.txt수취인연락처.ReadOnly = !활성여부;
                this.txt화물도착지점.ReadOnly = !활성여부;
                this.chk주소표시.Enabled = 활성여부;

                this.chk검토필요.Enabled = 활성여부;

                this.chk선사서류_쉬핑마크.Enabled = 활성여부;
                this.chk선사서류_CI.Enabled = 활성여부;
                this.chk선사서류_인수증.Enabled = 활성여부;
                this.chk기타서류부착.Enabled = 활성여부;
                this.chk관세환급여부.Enabled = 활성여부;
                this.txt참조번호.ReadOnly = !활성여부;
                this.txt추가요청_서류.ReadOnly = !활성여부;
                this.chk당사발행.Enabled = 활성여부;

                this.cbo선적부두.Enabled = 활성여부;
                this.txt대리점.ReadOnly = !활성여부;
                this.dtp접안일시.Enabled = 활성여부;
                this.dtp출항일시.Enabled = 활성여부;
                this.txt추가요청_선적.ReadOnly = !활성여부;
                this.txt타배선적선명.ReadOnly = !활성여부;
                this.txt작업선크레인.ReadOnly = !활성여부;

                this.rdo국내선적.Enabled = 활성여부;
                this.rdo해외탁송.Enabled = 활성여부;
                this.cbo선적위치.Enabled = 활성여부;
                this.chk포장전.Enabled = 활성여부;
                this.chk포장후.Enabled = 활성여부;
                this.chk사양확인.Enabled = 활성여부;
                this.chk위험물별도포장.Enabled = 활성여부;
                this.chk서트부착유무확인.Enabled = 활성여부;
                this.chk무지포장진행.Enabled = 활성여부;
                this.chk미포장건.Enabled = 활성여부;
                this.txt사진촬영.ReadOnly = !활성여부;
                this.txt위험물별도포장.ReadOnly = !활성여부;
                this.txt사양확인.ReadOnly = !활성여부;
                this.txt원본파일번호.ReadOnly = !활성여부;
                this.txt추가요청_포장.ReadOnly = !활성여부;
                this.txt미포장건.ReadOnly = !활성여부;
                #endregion

                #region 송장정보
                this.ctx매출처.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx사업장.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx수출자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx수하인.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx착하통지처.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx담당자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx원산지.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx도착국가.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);

                this.cbo선적조건.Enabled = 활성여부;
                this.cbo지불조건.Enabled = 활성여부;
                this.cbo통화.Enabled = 활성여부;
                this.cboHSCode.Enabled = 활성여부;
                this.dtp출발예정일.Enabled = 활성여부;
                this.dtp선적예정일.Enabled = 활성여부;
                this.dtp통관예정일.Enabled = 활성여부;

                this.txt매출처.ReadOnly = !활성여부;
                this.txt매출처주소1.ReadOnly = !활성여부;
                this.txt매출처주소2.ReadOnly = !활성여부;
                this.txt수출자주소1.ReadOnly = !활성여부;
                this.txt수출자주소2.ReadOnly = !활성여부;
                this.txt수하인.ReadOnly = !활성여부;
                this.txt수하인주소1.ReadOnly = !활성여부;
                this.txt수하인주소2.ReadOnly = !활성여부;
                this.txt착하통지처.ReadOnly = !활성여부;
                this.txt착하통지처주소1.ReadOnly = !활성여부;
                this.txt착하통지처주소2.ReadOnly = !활성여부;
                this.txt선적지.ReadOnly = !활성여부;
                this.txt도착도시.ReadOnly = !활성여부;
                this.txt전화번호.ReadOnly = !활성여부;
                this.txt이메일.ReadOnly = !활성여부;
                this.txt받는사람.ReadOnly = !활성여부;
                this.txt송장비고.ReadOnly = !활성여부;
                this.chk보험유무.Enabled = 활성여부;
                #endregion

                #region 기타
                this.btn수주적용.Enabled = 활성여부;
                this.btn협조전적용.Enabled = 활성여부;
                this._품목정보.Cols["S"].AllowEditing = 활성여부;
                this._품목정보.Cols["YN_ADD_STOCK"].AllowEditing = 활성여부;
                this._품목정보.Cols["NM_SL"].AllowEditing = 활성여부;
                this.btn삭제.Enabled = 활성여부;
                this.btn입출항정보.Enabled = 활성여부;
                this.btn송품서류등록.Enabled = 활성여부;
                this.btnCPR설정.Enabled = 활성여부;
                this.btn컷오프시간설정.Enabled = 활성여부;
				this.btn비고생성.Enabled = 활성여부;

				this.btn사진촬영적용.Enabled = 활성여부;
                this.btn사양확인적용.Enabled = 활성여부;
                this.btn위험물적용.Enabled = 활성여부;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 포워더정보설정()
        {
            try
            {
                if (D.GetString(this.cboMainCategory.SelectedValue) == "003" && 
                    (D.GetString(this.cboSubCategory.SelectedValue) == "001" || D.GetString(this.cboSubCategory.SelectedValue) == "002"))
                {
                    this.oneGridItem10.Visible = true;
                }
                else
                {
                    this.oneGridItem10.Visible = false;

                    this.rboA포워더.Checked = true;
                    this.rboB포워더.Checked = false;
                    this.rboC포워더.Checked = false;

                    this.curA포워더운임.DecimalValue = 0;
                    this.curB포워더운임.DecimalValue = 0;
                    this.curC포워더운임.DecimalValue = 0;

                    this.cur중량.DecimalValue = 0;

                    this.rboPrice.Checked = true;
                    this.rboTT.Checked = false;
                    this.rboETC.Checked = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 임시변수초기화()
        {
            this.회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
            this.협조전번호 = string.Empty;
            this.차수 = 0;
        }

        private void 부대비용()
        {
            string query, 부대비용;
            DataTable dt;

            try
            {
                query = @"SELECT (CASE GC.CD_ITEM WHEN 'SD0001' THEN 'FREIGHT'
						                          WHEN 'SD0002' THEN 'HANDLING'
						                          WHEN 'SD0003' THEN 'PACKING' END) + ' (' + CONVERT(NVARCHAR, CONVERT(NUMERIC(17, 2), GC.AM_EX_CHARGE)) + ')' AS NM_CHARGE
                          FROM CZ_SA_GIRH_CHARGE GC WITH(NOLOCK)
                          WHERE GC.CD_COMPANY = '{0}'
                          AND GC.NO_GIR = '{1}'";

                dt = DBHelper.GetDataTable(string.Format(query, this.회사코드, this.txt의뢰번호.Text));

                if (dt != null && dt.Rows.Count > 0)
				{
                    부대비용 = "부대비용" + Environment.NewLine;

                    foreach (DataRow dr in dt.Rows)
                    {
                        부대비용 += dr["NM_CHARGE"].ToString() + Environment.NewLine;
                    }

                    this.txt경고메시지.Text += 부대비용 + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 포워딩비용청구방식설정()
        {
            try
            {
                if (회사코드 == "S100") return;

                if (string.IsNullOrEmpty(this.cbo비용청구방법.SelectedValue.ToString()))
                    this.cbo비용청구방법.SetValue("001");

                if (this.cbo비용청구방법.SelectedValue.ToString() == "002")
                    this.btn부대비용.Enabled = true;
                else
                    this.btn부대비용.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "temp"));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 입항정보(bool isShowMessage)
		{
            DataTable dt;

            try
			{
                if (this.회사코드 != "K100" && this.회사코드 != "K200") return;
                
                string query = @"SELECT MH.NO_IMO,
	   VE.NM_VSSL,
	   VE.NM_PRTAG,
	   CONVERT(NVARCHAR(20), CONVERT(DATETIME, LEFT(VE.DTS_ETRYPT, 8) + ' ' + SUBSTRING(VE.DTS_ETRYPT, 9, 2) + ':' + SUBSTRING(VE.DTS_ETRYPT, 11, 2) + ':' + SUBSTRING(VE.DTS_ETRYPT, 13, 2)), 120) AS ARRIVAL
FROM CZ_MA_HULL MH WITH(NOLOCK)
JOIN CZ_SA_VSSL_ETRYNDH VE WITH(NOLOCK) ON VE.CALL_SIGN = MH.CALL_SIGN
WHERE CONVERT(DATETIME, LEFT(VE.DTS_ETRYPT, 8) + ' ' + SUBSTRING(VE.DTS_ETRYPT, 9, 2) + ':' + SUBSTRING(VE.DTS_ETRYPT, 11, 2) + ':' + SUBSTRING(VE.DTS_ETRYPT, 13, 2)) >= GETDATE()
AND MH.NO_IMO = '{0}'";

                dt = DBHelper.GetDataTable(string.Format(query, this.ctx호선번호.CodeValue));

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (isShowMessage)
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage(string.Format(@"Notice !
The vessel {0} calls at {1} on {2}
Please refer to this for outbound delivery request.
", dt.Rows[0]["NM_VSSL"].ToString(),
dt.Rows[0]["NM_PRTAG"].ToString(),
dt.Rows[0]["ARRIVAL"].ToString()));
                        else
                            this.txt경고메시지.Text += string.Format(@"Notice !
The vessel {0} calls at {1} on {2}
Please refer to this for outbound delivery request.
", dt.Rows[0]["NM_VSSL"].ToString(),
dt.Rows[0]["NM_PRTAG"].ToString(),
dt.Rows[0]["ARRIVAL"].ToString()) + Environment.NewLine;
                    }
                    else
                    {
                        this.txt경고메시지.Text += "Notice !" + Environment.NewLine;
                        this.txt경고메시지.Text += string.Format("The vessel {0}", dt.Rows[0]["NM_VSSL"].ToString()) + Environment.NewLine;
                        this.txt경고메시지.Text += string.Format("calls at {0}", dt.Rows[0]["NM_PRTAG"].ToString()) + Environment.NewLine;
                        this.txt경고메시지.Text += string.Format("on {0}", dt.Rows[0]["ARRIVAL"].ToString()) + Environment.NewLine;
                        this.txt경고메시지.Text += "Please refer to this for outbound delivery request." + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 기타 Property
        bool Chk매출처 { get { return !Checker.IsEmpty(this.ctx매출처S, this.DD("매출처")); } }

        bool Chk호선번호 { get { return !Checker.IsEmpty(this.ctx호선번호, this.DD("호선번호")); } }
		#endregion
	}
}