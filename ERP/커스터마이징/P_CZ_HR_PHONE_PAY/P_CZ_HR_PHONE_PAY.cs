using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Data.SqlClient;
using System.IO;
using Duzon.ERPU.Utils;
using System.Diagnostics;
using Duzon.ERPU.Grant;

namespace cz
{
	public partial class P_CZ_HR_PHONE_PAY : PageBase
	{

		#region 생성자
		P_CZ_HR_PHONE_PAY_BIZ _biz = new P_CZ_HR_PHONE_PAY_BIZ();

		private DirectoryInfo dirInfo = null;

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		public string NM_KOR { get; set; }

		public string NM_CC { get; set; }

		public decimal allTotal { get; set; }

		public string basicTotal { get; set; }

		public string roamingTotal { get; set; }

		public string statement { get; set; }

		public string YM = string.Empty;

		public string rdoUse { get; set; }

		public string GUBUN { get; set; }

		public string NO_DOCU { get; set; }

		public string ST_STAT { get; set; }

		public string NM_STAT { get; set; }

		Image fileIcon = null;

		#endregion 생성자


		#region 초기화
		public P_CZ_HR_PHONE_PAY()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			NM_KOR = Global.MainFrame.LoginInfo.UserName;
			NM_CC = Global.MainFrame.LoginInfo.CostCenterName;
			InitializeComponent();
		}


		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();

			fileIcon = imageList1.Images[0];
		}

		private void InitControl()
		{
			dtp급여반영월.Text = Util.GetToday().Substring(0, 6);

			YM = dtp급여반영월.Text;

			txt사번.Text = NO_EMP;
			txt성명.Text = NM_KOR;
			txt부서.Text = NM_CC;

			Util.SetCON_ReadOnly(pnl사번, true);
			Util.SetCON_ReadOnly(pnl성명, true);
			Util.SetCON_ReadOnly(pnl부서, true);


			DataTable dt = this._biz.UserSearch(new object[] {
				YM,
				NO_EMP
			});

			foreach (DataRow r in dt.Rows)
			{
				GUBUN = r["GUBUN"].ToString();
			}

			if (string.IsNullOrEmpty(GUBUN))
			{
				cur명세서요금.Enabled = false;
				cur국제전화1.Enabled = false;
				cur기본요금.Enabled = false;
				cur국내통화료.Enabled = false;
				txt비고.Enabled = false;

				Util.ShowMessage("등록되지 않은 사용자 입니다.");

				return;
			}
			else
			{
				cur명세서요금.Enabled = true;
				cur국제전화1.Enabled = true;
				cur기본요금.Enabled = true;
				cur국내통화료.Enabled = true;
				txt비고.Enabled = true;
			}


			UGrant ugrant = new UGrant();

			if (ugrant.GrantButton("P_CZ_SA_PHONE_PAY", "CONFIRM"))
			{
				ctx사원.Visible = true;
				btn승인.Visible = true;
			}
			else
			{
				ctx사원.Visible = false;
				btn승인.Visible = false;
				lbl_사원.Visible = false;
			}

		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH };

			_flexH.BeginSetting(1, 1, true);

			_flexH.SetCol("YM", "지급반영월", 80, false, typeof(string), FormatTpType.YEAR_MONTH);
			_flexH.SetCol("NM_SYSDEF", "결재상태", 80, false);
			_flexH.SetCol("ST_STAT", "결재", false);
			_flexH.SetCol("NO_EMP", "사번", false);
			_flexH.SetCol("NM_CC", "코스트센터", 80, false);
			_flexH.SetCol("NM_DUTY_RANK", "직급", 80, false); // 직위 CD_DUTY_RANK, HR_H000002
			_flexH.SetCol("NM_KOR", "이름", 80, false);
			_flexH.SetCol("NM_TITLE", "제목", 280, false);
			_flexH.SetCol("DT_REG", "작성일", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			_flexH.SetCol("NO_DOCU", "문서번호", false);
			_flexH.SetCol("AM_TOTAL", "명세서요금", 100, true, typeof(decimal), FormatTpType.MONEY);
			_flexH.SetCol("AM_BASIC", "기본요금", 100, true, typeof(decimal), FormatTpType.MONEY);
			_flexH.SetCol("AM_ADD1", "국내통화료", 100, true, typeof(decimal), FormatTpType.MONEY);
			_flexH.SetCol("AM_ROAMING1", "국제통화료", 100, true, typeof(decimal), FormatTpType.MONEY);
			_flexH.SetCol("AM", "합계", 120, true, typeof(decimal), FormatTpType.MONEY);
			_flexH.SetCol("YN_USE", "지원금유형", false);
			_flexH.SetCol("FILE_PATH_MNG", "첨부파일", 80, false);
			_flexH.SetCol("DC_RMK", "비고", 200);
			_flexH.SetCol("YN_STAT", "STAT", false);
			_flexH.Cols["NM_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;
			_flexH.Cols["FILE_PATH_MNG"].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexH.Cols["NM_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;
			_flexH.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter; 
			_flexH.Cols["NM_CC"].TextAlign = TextAlignEnum.CenterCenter;


			this._flexH.SetOneGridBinding(new object[] { this.txt제목 }, this.oneL);

			_flexH.SettingVersion = "0.0.0.1";
			_flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn초기화.Click += new EventHandler(btn초기화_Click);
			btn첨부파일.Click += new EventHandler(btn첨부파일_Click);
			btn전자결재.Click += new EventHandler(btn전자결재_Click);

			btn승인.Click +=new EventHandler(btn승인_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			_flexH.DoubleClick += new EventHandler(flexH_DoubleClick);
			_flexH.Click += new EventHandler(flexH_Click);


			ctx사원.CodeChanged +=new EventHandler(ctx사원_CodeChanged);
			//cur명세서요금.TextChanged += new EventHandler(cur명세서요금_TextChanged);
			//cur기본요금.TextChanged += new EventHandler(cur기본요금_TextChanged);
		   //cur국내통화료.TextChanged +=new EventHandler(cur국내통화료_TextChanged);
			//cur국제전화1.TextChanged +=new EventHandler(cur국제전화1_TextChanged);
			//txt비고.TextChanged += new EventHandler(txt비고_TextChanged);

		}
		#endregion 초기화

		
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

				if (!string.IsNullOrEmpty(ctx사원.CodeValue.ToString()))
					NO_EMP = ctx사원.CodeValue;
				else
					NO_EMP = Global.MainFrame.LoginInfo.UserID;


				this._flexH.Binding = this._biz.Search(new object[] { 
																	  CD_COMPANY,
																	  NO_EMP,
																	  dtp급여반영월.Text
																	   });

				if (!_flexH.HasNormalRow)
				{
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					btn초기화_Click(null, null);

					GUBUN = string.Empty;
					YM = dtp급여반영월.Text;


					DataTable dt = this._biz.UserSearch(new object[] {
				YM,
				NO_EMP
			});

					foreach (DataRow r in dt.Rows)
					{
						GUBUN = r["GUBUN"].ToString();
					}

					if (string.IsNullOrEmpty(GUBUN))
					{
						cur명세서요금.Enabled = false;
						cur국제전화1.Enabled = false;
						cur기본요금.Enabled = false;
						cur국내통화료.Enabled = false;
						txt비고.Enabled = false;

						Util.ShowMessage("등록되지 않은 사용자 입니다.");

						return;
					}
					else
					{
						cur명세서요금.Enabled = true;
						cur국제전화1.Enabled = true;
						cur기본요금.Enabled = true;
						cur국내통화료.Enabled = true;
						txt비고.Enabled = true;
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(ctx사원.CodeValue.ToString()))
						ST_STAT = "1";
					else
						ST_STAT = _flexH["ST_STAT"].ToString();

					flexH_Click(null, null);
				}
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
				GUBUN = string.Empty;
				YM = dtp급여반영월.Text;

				DataTable dt = this._biz.UserSearch(new object[] {
				YM,
				NO_EMP
			});

				foreach (DataRow r in dt.Rows)
				{
					GUBUN = r["GUBUN"].ToString();
				}

				if (string.IsNullOrEmpty(GUBUN))
				{
					cur명세서요금.Enabled = false;
					cur국제전화1.Enabled = false;
					cur기본요금.Enabled = false;
					cur국내통화료.Enabled = false;
					txt비고.Enabled = false;

					Util.ShowMessage("등록되지 않은 사용자 입니다.");

					return;

				}
				else
				{
					cur명세서요금.Enabled = true;
					cur국제전화1.Enabled = true;
					cur기본요금.Enabled = true;
					cur국내통화료.Enabled = true;
					txt비고.Enabled = true;
				}

					string query = "SELECT * FROM CZ_HR_PHONE_PAY WHERE CD_COMPANY = '" + CD_COMPANY + "' AND YM = '" + YM + "' AND NO_EMP = '" + NO_EMP + "'";
					DataTable dtDB = DBMgr.GetDataTable(query);
					DataTable dtTB = _flexH.DataTable;

					if (dtDB.Rows.Count > 0 || dtTB.Select("YM = '" + YM + "' AND NO_EMP = '" + NO_EMP + "'").Length > 0) {
						if (!dtDB.Rows[0]["NM_TITLE"].ToString().Contains("자동등록"))
						{
							_flexH.DataSource = dtDB;
							ShowMessage("해당월은 이미 작성하였습니다.");
						}
						else
						{
							ShowMessage("해당월은 이미 자동등록되었습니다.");
						}
							 return; 
					}


					string _amtotal = cur명세서요금.Text;
					string _ambasic = cur기본요금.Text;
					string _amadd = cur국내통화료.Text;
					string _amroaming = cur국제전화1.Text;
					string _dcrmk = txt비고.Text;



					if (!string.IsNullOrEmpty(GUBUN))
					{
						this._flexH.Rows.Add();
						this._flexH.Row = _flexH.Rows.Count - 1;
					}
					else
					{
						return;
					}

					string nmTitle = NM_KOR + " - " + YM.Substring(0,4)+"년"+YM.Substring(4,2)+"월" + " 휴대폰요금지급신청";

					this._flexH["YM"] = YM;
					this._flexH["DT_REG"] = System.DateTime.Now.ToString("yyyy-MM-dd");
					this._flexH["NM_TITLE"] = nmTitle;
					this._flexH["AM_TOTAL"] = _amtotal;
					this._flexH["AM_BASIC"] = _ambasic;
					this._flexH["AM_ADD1"] = _amadd;
					this._flexH["AM_ROAMING1"] = _amroaming;
					this._flexH["YN_PAY"] = "N";
					this._flexH["NO_EMP"] = NO_EMP;
					this._flexH["NM_KOR"] = NM_KOR;
					this._flexH["NM_CC"] = NM_CC;
					this._flexH["YN_USE"] = rdoUse;
					this._flexH["DC_RMK"] = _dcrmk;

					txt제목.Text = nmTitle;

					Calculator();

					this._flexH["AM"] = allTotal;

					this._flexH.AddFinished();
					this.cur명세서요금.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion 추가


		#region 삭제
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;
				if (!this._flexH.HasNormalRow)
					return;

				this._flexH.Rows.Remove(this._flexH.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		public void btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;
				if (!this._flexH.HasNormalRow)
					return;

				string ST_STAT = _flexH["ST_STAT"].ToString();

				if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
				if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

				this._flexH.Rows.Remove(this._flexH.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion 삭제


		#region 저장

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}
		
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSave()) return;

				if (this.MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !this.Verify())
				return false;

			if (this._flexH.IsDataChanged == false) return false;

			if (_flexH.Rows.Count > 1)
			{
				_flexH["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				ST_STAT = _flexH["ST_STAT"].ToString();
				_flexH["AM"] = lbl합계.Text;
                if (NO_EMP.Equals("S-001") || NO_EMP.Equals("S-002") || NO_EMP.Equals("S-279") || NO_EMP.Equals("S-231"))
				{
					_flexH["YN_STAT"] = "Y";
				}
				
				Calculator();
			}
			

			if (!ST_STAT.Equals("1") || !ST_STAT.Equals("0"))
			{
				DataTable dt = this._flexH.GetChanges();
				this._biz.Save(dt);
				this._flexH.AcceptChanges();

				OnToolBarSearchButtonClicked(null, null);
				return true;
			}
			else
			{
				Util.ShowMessage("결재 진행 중이므로 수정할 수 없습니다." + "\r\n결재상태 : " + NM_STAT);
				return false;
			}
		}

		#endregion 저장


		#region 버튼
		//초기화
		private void btn초기화_Click(object sender, EventArgs e)
		{
			this.cur국제전화1.Clear();
			this.cur기본요금.Clear();
			this.cur국내통화료.Clear();
			this.cur명세서요금.Clear();
			this.txt비고.Clear();

			this.lbl명세.Text = "0";
			this.lbl기타요금.Text = "0";
			this.lbl로밍.Text = "0";
			this.lbl합계.Text = "0";
			this.txt제목.Clear();
			this.tbx첨부파일.Clear();
		}

		#region 파일첨부
		private void btn첨부파일_Click(object sender, EventArgs e)
		{
			   string fileName = string.Empty;

			try
			{
				if (!this._flexH.HasNormalRow)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					YM = dtp급여반영월.Text;

					string _ym = this._flexH["YM"].ToString();
					string _noemp = string.Empty;

					if (!string.IsNullOrEmpty(ctx사원.CodeValue.ToString()))
						_noemp = ctx사원.CodeValue;
					else
						_noemp = this._flexH["NO_EMP"].ToString();


					string _company = this._flexH["CD_COMPANY"].ToString();

					string file_code = D.GetString("HP" + _ym + "_" + _noemp + "_" + _company); //파일 PK설정 
					P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_HR_PHONE_PAY", file_code, "P_CZ_HR_PHONE_PAY");
					m_dlg.ShowDialog(this);
					
					fileName = this._biz.SearchFileInfo(Global.MainFrame.LoginInfo.CompanyCode, file_code);

					if (!string.IsNullOrEmpty(fileName))
					{
						tbx첨부파일.Text = fileName;
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion 파일첨부

		#region  전자결재
		
		//전자결재     ST_STAT 값 : 2:미상신, 0:진행중, 1:종결, -1:반려, 3:취소(삭제)
		private void btn전자결재_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(tbx첨부파일.Text))
				{
					YM = dtp급여반영월.Text;
					// 결재 상태 체크
					string query = @"
SELECT
	A.NO_DOCU, B.ST_STAT
FROM	  CZ_HR_PHONE_PAY	AS A
LEFT JOIN FI_GWDOCU			AS B ON A.NO_DOCU = B.NO_DOCU
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.YM = '" + YM + @"'
	AND A.NO_EMP = '" + NO_EMP + "'";

					DataTable dt = DBMgr.GetDataTable(query);
					string NO_DOCU = dt.Rows[0]["NO_DOCU"].ToString();
					string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

					if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
					if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

					// html 만들기
					string html = @"
<div class='header'>
  ※ 휴대폰 사용요금 지급요청서
</div>
<table width='90%'>
  <tr>
	<th width='25%'>명세요금</th>
	<th width='25%'>국내통화</th>
	<th width='25%'>국제통화</th>
	<th width='25%'>기본요금</th>
  </tr>";

					html += @"
  <tr>
	<td class='col1' style='text-align:center'>" + lbl명세.Text + " 원"+@"</td>
	<td class='col2' style='text-align:center'>" + lbl기타요금.Text + " 원"+@"</td>
	<td class='col3' style='text-align:center'>" + string.Format("{0:#,###}", lbl로밍.Text) + " 원" + @"</td>
	<td class='col4' style='text-align:center'>" + string.Format("{0:#,###}", cur기본요금.Text) + " 원"+@"</td>
  </tr>";

					DataTable dtL = _flexH.DataTable;
					html += @"
   <tr> 
	<th colspan='3'>명세서 요금</th>
	<td colspan='1' class='sum'>" + string.Format("{0:#,###}", lbl명세.Text) + @" 원</td>
  </tr>
  <tr>
	<th colspan='3'>국내통화료 합계</th>
	<td colspan='1' class='sum'>" + string.Format("{0:#,###}", lbl기타요금.Text) + @" 원</td>
  </tr>
  <tr>
	<th colspan='3'>국제통화료 합계</th>
	<td colspan='1' class='sum'>" + string.Format("{0:#,###}", lbl로밍.Text) + @" 원</td>
  </tr>
  <tr>
	<th colspan='3'>당월 지급액</th>
	<td colspan='1' class='sum fin'>" + string.Format("{0:#,###}", lbl합계.Text) + @" 원</td>
  </tr>
  <tr>
	<td colspan='4' class='rmk'>" + txt비고.Text + @" </td>
  </tr>
</table>";

					// html 업데이트 및 전자결재 팝업			
					query = @"
UPDATE CZ_HR_PHONE_PAY SET
	NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND YM = '" + YM + @"'
	AND NO_EMP = '" + NO_EMP + "'";

					GroupWare.Save(txt제목.Text, html, NO_DOCU, 1011, query, true);
				}
				else
				{
					Util.ShowMessage("관련 첨부파일을 등록하세요");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#endregion 전자결재
		
		//파일첨부 컬럼DoubleClick _ 파일열기
		public void flexH_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				//string fileName = _flexH["FILE_PATH_MNG"].ToString();
				if (this._flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG")
				{
					파일보기();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public void btn승인_Click(object sender, EventArgs e)
		{

			string no_emp_new = _flexH["NO_EMP"].ToString();
				DataTable dt = this._flexH.GetChanges();
				this._biz.SaveState(CD_COMPANY, YM, no_emp_new);
				this._flexH.AcceptChanges();

				OnToolBarSearchButtonClicked(null, null);

				Util.ShowMessage("승인 완료 되었습니다.");
		}


		private void flexH_Click(object sender, EventArgs e)
		{
			try
			{
				if (_flexH.Rows.Count > 1)
				{
					string _ym = this._flexH["YM"].ToString();

					string _noemp = string.Empty;

					if (!string.IsNullOrEmpty(ctx사원.CodeValue.ToString()))
						_noemp = ctx사원.CodeValue;
					else
						_noemp = this._flexH["NO_EMP"].ToString();

					string _company = Global.MainFrame.LoginInfo.CompanyCode;

					YM = dtp급여반영월.Text;
					string file_code = D.GetString("HP" + _ym + "_" + _noemp + "_" + _company); //파일 PK설정 
					tbx첨부파일.Text = _biz.SearchFileInfo(Global.MainFrame.LoginInfo.CompanyCode, file_code);

					if (!string.IsNullOrEmpty(tbx첨부파일.Text))
					{
						this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG"].Index, fileIcon);
					}
					else
					{
						this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG"].Index, null);
					}


					//cur국제전화1.Text = _flexH["AM_ROAMING1"].ToString();
					//cur기본요금.Text = _flexH["AM_BASIC"].ToString();
					//cur국내통화료.Text = _flexH["AM_ADD1"].ToString();
					//cur명세서요금.Text = _flexH["AM_TOTAL"].ToString();
					//txt제목.Text = _flexH["NM_TITLE"].ToString();
					//txt비고.Text = _flexH["DC_RMK"].ToString();

					Calculator();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}





		#endregion 버튼


		#region 파일열기
		private void 파일보기()
		{
			try
			{
				YM = dtp급여반영월.Text;

				string file_code = D.GetString("HP" + YM + "_" + NO_EMP + "_" + Global.MainFrame.LoginInfo.CompanyCode); //첨부파일 PK설정 

				string str1 = Path.Combine(Application.StartupPath, "temp");
				string string3 = D.GetString(this.tbx첨부파일.Text);
				string fileName = Path.Combine(str1, string3);

				this.dirInfo = new DirectoryInfo(str1);

				if (!this.dirInfo.Exists)
					this.dirInfo.Create();

				FileUploader.DownloadFile(string3, str1, "upload/P_CZ_HR_PHONE_PAY", file_code);

				string string4 = D.GetString(new FileInfo(fileName).LastWriteTime);
				Process process = new Process();
				process.StartInfo.FileName = str1 + "\\" + string3;
				process.Start();
				process.WaitForExit();

			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion 파일열기


		#region 계산 메소드
		private void Calculator()
		{
			YM = dtp급여반영월.Text;
			DataTable dtG = this._biz.UserSearch(new object[] {
				YM,
				NO_EMP
			});

			foreach (DataRow r in dtG.Rows)
			{
				GUBUN = r["GUBUN"].ToString();
			}

			DataTable dt = _flexH.DataTable;

			//decimal AM_BASIC = Util.GetTO_Decimal(dt.Compute("SUM(AM_BASIC)", "YM='" + YM + "'"));
			//decimal AM_ADD1 = Util.GetTO_Decimal(dt.Compute("SUM(AM_ADD1)", "YM='" + YM + "'"));
			//decimal AM_ROAMING1 = Util.GetTO_Decimal(dt.Compute("SUM(AM_ROAMING1)", "YM='" + YM + "'"));
			//decimal AM_TOTAL = Util.GetTO_Decimal(dt.Compute("SUM(AM_TOTAL)", "YM='" + YM + "'"));

			if (!string.IsNullOrEmpty(ctx사원.CodeValue.ToString()))
				NO_EMP = ctx사원.CodeValue;
			else
				NO_EMP = _flexH["NO_EMP"].ToString();

			decimal AM_BASIC = Util.GetTO_Decimal(dt.Compute("SUM(AM_BASIC)", "NO_EMP='" + NO_EMP + "'"));
			decimal AM_ADD1 = Util.GetTO_Decimal(dt.Compute("SUM(AM_ADD1)", "NO_EMP='" + NO_EMP + "'"));
			decimal AM_ROAMING1 = Util.GetTO_Decimal(dt.Compute("SUM(AM_ROAMING1)", "NO_EMP='" + NO_EMP + "'"));
			decimal AM_TOTAL = Util.GetTO_Decimal(dt.Compute("SUM(AM_TOTAL)", "NO_EMP='" + NO_EMP + "'"));

			decimal AM_ADD = AM_ADD1;
			decimal AM_ROAMING = AM_ROAMING1;
			decimal AM = AM_TOTAL;

			decimal AM_ALL = 0;
			decimal AM_CH = Util.GetTO_Decimal(dt.Compute("SUM(AM)", "YM='" + YM + "'"));
			AM_ROAMING = Convert.ToDecimal(Convert.ToDouble(AM_ROAMING) * 1.1);
			AM_ROAMING = Math.Floor(AM_ROAMING / 10) * 10;
			decimal _AM_BASIC = Convert.ToDecimal(Convert.ToDouble(AM_BASIC + AM_ADD) * 1.1);

			// 001  전체지급 명세요금전체
			// 002  기본지급 max 50000 + 로밍
			// 003  기타지급 로밍

			if (GUBUN.Equals("001"))
			{
				// 전체지급은 명세서 금액대로 지급
				AM_ALL = AM_TOTAL;
			}
			else if (GUBUN.Equals("002"))
			{

                // 자동 지급 이후로는 무조건 5만원 + 로밍비

				//기본요금+국내통화료 * 1.1 < 50000
                //if (_AM_BASIC < 50000)
                //{
                //    AM_ALL = _AM_BASIC + AM_ROAMING;
                //}
                //else
                //{
					// 기본지급은 로밍 제외 맥스 50000 
					if (AM_TOTAL <= 50000)
					{
						// 명세서금액이 50000이거나 50000이 안될때엔 명세서 금액 전액 지급
						AM_ALL = AM_TOTAL;
					}
					else
					{
						//명세서 금액이 50000 초과시

						//명세서 - 로밍비
						AM = AM_TOTAL - AM_ROAMING;


						// 기본지급금이 50000이기 때문에
						if (AM > 50000)
						{
							// 기본지급금이 50000 이상이 계산 될 시에는 맥스값이 50000으로 FIX 하여 지급금 계산
							AM_ALL = 50000 + AM_ROAMING;
						}
						else
						{
							//기본지급금이 50000 미만일 경우에는 기존기본지급금 + 로밍비 지원  
							AM_ALL = AM + AM_ROAMING; // 여기서 AM_ALL은 AM_TOTAL보다 작아야 한다.

							//계산이 끝나고 명세서 금액보다 큰 금액이 나오면 버그!
							if (AM_ALL > AM_TOTAL)
							{
								Util.ShowMessage("계산 실패!!");
							}
						}
                    //}
				}
			}
			else //003
			{
				// 지급금액은 로밍비
				AM_ALL = AM_ROAMING;
			}

			AM_ALL = Math.Floor(AM_ALL / 10) * 10;

			

			if (AM_TOTAL < AM_ALL)
			{
				AM_ALL = AM_TOTAL;
			}

			allTotal = AM_ALL;

			lbl명세.Text = string.Format("{0:#,##0}", Math.Floor(AM_TOTAL));
			lbl기타요금.Text = string.Format("{0:#,##0}", Math.Floor(AM_ADD));
			lbl로밍.Text = string.Format("{0:#,##0}", Math.Floor(AM_ROAMING));
			lbl합계.Text = string.Format("{0:#,##0}", Math.Floor(AM_ALL));

			if (!(AM_CH == AM_ALL))
			{
				_flexH["AM"] = lbl합계.Text;
			}
		}
	   
		#endregion 메소드


		#region 이벤트
		public void cur명세서요금_TextChanged(object sender, EventArgs e)
		{
			if (_flexH.Rows.Count > 1)
			{
				decimal origin = Util.GetTO_Decimal(_flexH["AM_TOTAL"].ToString());
				decimal after = Util.GetTO_Decimal(cur명세서요금.Text);

				if (!(origin == after))
				{
					_flexH["AM_TOTAL"] = cur명세서요금.Text;
					Calculator();
				}
			}
		}

		public void cur기본요금_TextChanged(object sender, EventArgs e)
		{
			if (_flexH.Rows.Count > 1)
			{
				decimal origin = Util.GetTO_Decimal(_flexH["AM_BASIC"].ToString());
				decimal after = Util.GetTO_Decimal(cur기본요금.Text);

				if (!(origin == after))
				{
					_flexH["AM_BASIC"] = cur기본요금.Text;
					Calculator();
				}
			}
		}

		public void cur국내통화료_TextChanged(object sender, EventArgs e)
		{
			if (_flexH.Rows.Count > 1)
			{
				decimal origin = Util.GetTO_Decimal(_flexH["AM_ADD1"].ToString());
				decimal after = Util.GetTO_Decimal(cur국내통화료.Text);

				if (!(origin == after))
				{
					_flexH["AM_ADD1"] = cur국내통화료.Text;
					Calculator();
				}
			}
		}


		public void cur국제전화1_TextChanged(object sender, EventArgs e)
		{
			if (_flexH.Rows.Count > 1)
			{
				decimal origin = Util.GetTO_Decimal(_flexH["AM_ROAMING1"].ToString());
				decimal after = Util.GetTO_Decimal(cur국제전화1.Text);

				if (!(origin == after))
				{
					_flexH["AM_ROAMING1"] = cur국제전화1.Text;
					Calculator();
				}
			}
		}

		public void txt비고_TextChanged(object sender, EventArgs e)
		{
			if (_flexH.Rows.Count > 1)
			{
				  _flexH["DC_RMK"] = txt비고.Text;
			}
		}

		public void ctx사원_CodeChanged(object sender, EventArgs e)
		{
			txt사번.Text = ctx사원.CodeValue;
			NO_EMP = txt사번.Text;
			txt성명.Text = ctx사원.CodeName;
			NM_KOR = txt성명.Text;
			txt부서.Text = "";
		}


		#endregion 이벤트


		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PHONE_PAY));
			this.spcM = new System.Windows.Forms.SplitContainer();
			this.tlayH = new System.Windows.Forms.TableLayoutPanel();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx사원 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl_사원 = new Duzon.Common.Controls.LabelExt();
			this.dtp급여반영월 = new Duzon.Common.Controls.DatePicker();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt제목S = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.tlayL = new System.Windows.Forms.TableLayoutPanel();
			this.pnl합계 = new Duzon.Common.Controls.PanelEx();
			this.lbl합계 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lbl로밍 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lbl기타요금 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl명세 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pnl제목2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn초기화 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneH = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl부서 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt부서 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.pnl성명 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt성명 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.pnl사번 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt사번 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl인도기간 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl제목 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt제목 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt10 = new Duzon.Common.Controls.LabelExt();
			this.oneL = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem8 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt11 = new Duzon.Common.Controls.LabelExt();
			this.cur명세서요금 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl명세서요금 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem11 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.cur기본요금 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl기본요금 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt6 = new Duzon.Common.Controls.LabelExt();
			this.cur국내통화료 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl국내통화료 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem12 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt8 = new Duzon.Common.Controls.LabelExt();
			this.cur국제전화1 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl국제통화료 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl20 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl비고 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem10 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx첨부파일 = new Duzon.Common.Controls.TextBoxExt();
			this.btn첨부파일 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.btn전자결재 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.lbl관련파일 = new Duzon.Common.Controls.LabelExt();
			this.btn파일첨부 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btn승인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spcM)).BeginInit();
			this.spcM.Panel1.SuspendLayout();
			this.spcM.Panel2.SuspendLayout();
			this.spcM.SuspendLayout();
			this.tlayH.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.tlayL.SuspendLayout();
			this.pnl합계.SuspendLayout();
			this.pnl제목2.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.pnl부서.SuspendLayout();
			this.pnl성명.SuspendLayout();
			this.pnl사번.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.pnl제목.SuspendLayout();
			this.oneGridItem8.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur명세서요금)).BeginInit();
			this.oneGridItem6.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem11.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur기본요금)).BeginInit();
			this.oneGridItem7.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur국내통화료)).BeginInit();
			this.oneGridItem12.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur국제전화1)).BeginInit();
			this.oneGridItem5.SuspendLayout();
			this.bpPanelControl20.SuspendLayout();
			this.oneGridItem10.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.spcM);
			this.mDataArea.Size = new System.Drawing.Size(1461, 579);
			// 
			// spcM
			// 
			this.spcM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcM.Location = new System.Drawing.Point(0, 0);
			this.spcM.Name = "spcM";
			// 
			// spcM.Panel1
			// 
			this.spcM.Panel1.Controls.Add(this.tlayH);
			// 
			// spcM.Panel2
			// 
			this.spcM.Panel2.Controls.Add(this.tlayL);
			this.spcM.Size = new System.Drawing.Size(1461, 579);
			this.spcM.SplitterDistance = 891;
			this.spcM.TabIndex = 1;
			// 
			// tlayH
			// 
			this.tlayH.ColumnCount = 1;
			this.tlayH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayH.Controls.Add(this._flexH, 0, 1);
			this.tlayH.Controls.Add(this.oneGrid1, 0, 0);
			this.tlayH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayH.Location = new System.Drawing.Point(0, 0);
			this.tlayH.Name = "tlayH";
			this.tlayH.RowCount = 2;
			this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlayH.Size = new System.Drawing.Size(891, 579);
			this.tlayH.TabIndex = 0;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(3, 72);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(885, 504);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 1;
			this._flexH.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
			this.oneGridItem1,
			this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(885, 63);
			this.oneGrid1.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(875, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx사원);
			this.bpPanelControl2.Controls.Add(this.lbl_사원);
			this.bpPanelControl2.Controls.Add(this.dtp급여반영월);
			this.bpPanelControl2.Controls.Add(this.labelExt3);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(488, 23);
			this.bpPanelControl2.TabIndex = 17;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// ctx사원
			// 
			this.ctx사원.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx사원.Location = new System.Drawing.Point(195, 0);
			this.ctx사원.Name = "ctx사원";
			this.ctx사원.Size = new System.Drawing.Size(177, 21);
			this.ctx사원.TabIndex = 7;
			this.ctx사원.TabStop = false;
			this.ctx사원.Tag = "NO_EMP";
			this.ctx사원.Text = "bpCodeTextBox1";
			// 
			// lbl_사원
			// 
			this.lbl_사원.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl_사원.Location = new System.Drawing.Point(130, 0);
			this.lbl_사원.Name = "lbl_사원";
			this.lbl_사원.Size = new System.Drawing.Size(65, 23);
			this.lbl_사원.TabIndex = 6;
			this.lbl_사원.Text = "사원";
			this.lbl_사원.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp급여반영월
			// 
			this.dtp급여반영월.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp급여반영월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp급여반영월.Dock = System.Windows.Forms.DockStyle.Left;
			this.dtp급여반영월.Location = new System.Drawing.Point(65, 0);
			this.dtp급여반영월.Mask = "####/##";
			this.dtp급여반영월.MaskBackColor = System.Drawing.Color.White;
			this.dtp급여반영월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp급여반영월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp급여반영월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp급여반영월.Modified = true;
			this.dtp급여반영월.Name = "dtp급여반영월";
			this.dtp급여반영월.ShowUpDown = true;
			this.dtp급여반영월.Size = new System.Drawing.Size(65, 21);
			this.dtp급여반영월.TabIndex = 5;
			this.dtp급여반영월.Tag = "DT_START";
			this.dtp급여반영월.Value = new System.DateTime(((long)(0)));
			// 
			// labelExt3
			// 
			this.labelExt3.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt3.Location = new System.Drawing.Point(0, 0);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(65, 23);
			this.labelExt3.TabIndex = 4;
			this.labelExt3.Text = "지급반영월";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(875, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt제목S);
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(488, 23);
			this.bpPanelControl1.TabIndex = 18;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt제목S
			// 
			this.txt제목S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt제목S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt제목S.Dock = System.Windows.Forms.DockStyle.Left;
			this.txt제목S.Location = new System.Drawing.Point(65, 0);
			this.txt제목S.Name = "txt제목S";
			this.txt제목S.Size = new System.Drawing.Size(359, 21);
			this.txt제목S.TabIndex = 10;
			this.txt제목S.Tag = "";
			// 
			// labelExt2
			// 
			this.labelExt2.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt2.Location = new System.Drawing.Point(0, 0);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 23);
			this.labelExt2.TabIndex = 1;
			this.labelExt2.Text = "제목";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tlayL
			// 
			this.tlayL.ColumnCount = 1;
			this.tlayL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayL.Controls.Add(this.pnl합계, 0, 4);
			this.tlayL.Controls.Add(this.pnl제목2, 0, 1);
			this.tlayL.Controls.Add(this.oneH, 0, 0);
			this.tlayL.Controls.Add(this.oneL, 0, 2);
			this.tlayL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayL.Location = new System.Drawing.Point(0, 0);
			this.tlayL.Name = "tlayL";
			this.tlayL.RowCount = 5;
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayL.Size = new System.Drawing.Size(566, 579);
			this.tlayL.TabIndex = 0;
			// 
			// pnl합계
			// 
			this.pnl합계.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
			this.pnl합계.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl합계.ColorA = System.Drawing.Color.Empty;
			this.pnl합계.ColorB = System.Drawing.Color.Empty;
			this.pnl합계.Controls.Add(this.lbl합계);
			this.pnl합계.Controls.Add(this.label6);
			this.pnl합계.Controls.Add(this.lbl로밍);
			this.pnl합계.Controls.Add(this.label4);
			this.pnl합계.Controls.Add(this.lbl기타요금);
			this.pnl합계.Controls.Add(this.label1);
			this.pnl합계.Controls.Add(this.lbl명세);
			this.pnl합계.Controls.Add(this.label3);
			this.pnl합계.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl합계.Location = new System.Drawing.Point(3, 293);
			this.pnl합계.Name = "pnl합계";
			this.pnl합계.Size = new System.Drawing.Size(560, 40);
			this.pnl합계.TabIndex = 17;
			// 
			// lbl합계
			// 
			this.lbl합계.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl합계.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl합계.ForeColor = System.Drawing.Color.Blue;
			this.lbl합계.Location = new System.Drawing.Point(728, 0);
			this.lbl합계.Name = "lbl합계";
			this.lbl합계.Size = new System.Drawing.Size(140, 38);
			this.lbl합계.TabIndex = 31;
			this.lbl합계.Text = "0";
			this.lbl합계.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.AliceBlue;
			this.label6.Dock = System.Windows.Forms.DockStyle.Left;
			this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label6.Location = new System.Drawing.Point(633, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95, 38);
			this.label6.TabIndex = 25;
			this.label6.Text = "합계";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl로밍
			// 
			this.lbl로밍.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl로밍.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl로밍.Location = new System.Drawing.Point(517, 0);
			this.lbl로밍.Name = "lbl로밍";
			this.lbl로밍.Size = new System.Drawing.Size(116, 38);
			this.lbl로밍.TabIndex = 24;
			this.lbl로밍.Tag = "";
			this.lbl로밍.Text = "0";
			this.lbl로밍.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.AliceBlue;
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(422, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 38);
			this.label4.TabIndex = 23;
			this.label4.Text = "국제통화료";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl기타요금
			// 
			this.lbl기타요금.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl기타요금.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl기타요금.Location = new System.Drawing.Point(306, 0);
			this.lbl기타요금.Name = "lbl기타요금";
			this.lbl기타요금.Size = new System.Drawing.Size(116, 38);
			this.lbl기타요금.TabIndex = 22;
			this.lbl기타요금.Tag = "";
			this.lbl기타요금.Text = "0";
			this.lbl기타요금.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.AliceBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(211, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 38);
			this.label1.TabIndex = 21;
			this.label1.Text = "국내통화료";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl명세
			// 
			this.lbl명세.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl명세.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl명세.Location = new System.Drawing.Point(95, 0);
			this.lbl명세.Name = "lbl명세";
			this.lbl명세.Size = new System.Drawing.Size(116, 38);
			this.lbl명세.TabIndex = 20;
			this.lbl명세.Tag = "";
			this.lbl명세.Text = "0";
			this.lbl명세.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.AliceBlue;
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 38);
			this.label3.TabIndex = 5;
			this.label3.Text = "명세요금";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnl제목2
			// 
			this.pnl제목2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl제목2.Controls.Add(this.btn삭제);
			this.pnl제목2.Controls.Add(this.btn초기화);
			this.pnl제목2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl제목2.LeftImage = null;
			this.pnl제목2.Location = new System.Drawing.Point(3, 72);
			this.pnl제목2.Name = "pnl제목2";
			this.pnl제목2.Padding = new System.Windows.Forms.Padding(1);
			this.pnl제목2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl제목2.PatternImage = null;
			this.pnl제목2.RightImage = null;
			this.pnl제목2.Size = new System.Drawing.Size(560, 27);
			this.pnl제목2.TabIndex = 9;
			this.pnl제목2.TitleText = "휴대폰 요금 등록";
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(485, 4);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 5;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn초기화
			// 
			this.btn초기화.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn초기화.BackColor = System.Drawing.Color.White;
			this.btn초기화.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn초기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn초기화.Location = new System.Drawing.Point(405, 4);
			this.btn초기화.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn초기화.Name = "btn초기화";
			this.btn초기화.Size = new System.Drawing.Size(70, 19);
			this.btn초기화.TabIndex = 4;
			this.btn초기화.TabStop = false;
			this.btn초기화.Text = "초기화";
			this.btn초기화.UseVisualStyleBackColor = false;
			// 
			// oneH
			// 
			this.oneH.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneH.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
			this.oneGridItem3,
			this.oneGridItem4});
			this.oneH.Location = new System.Drawing.Point(3, 3);
			this.oneH.Name = "oneH";
			this.oneH.Size = new System.Drawing.Size(560, 63);
			this.oneH.TabIndex = 0;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.pnl부서);
			this.oneGridItem3.Controls.Add(this.pnl성명);
			this.oneGridItem3.Controls.Add(this.pnl사번);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 0;
			// 
			// pnl부서
			// 
			this.pnl부서.Controls.Add(this.txt부서);
			this.pnl부서.Controls.Add(this.labelExt9);
			this.pnl부서.Location = new System.Drawing.Point(486, 1);
			this.pnl부서.Name = "pnl부서";
			this.pnl부서.Size = new System.Drawing.Size(240, 23);
			this.pnl부서.TabIndex = 17;
			this.pnl부서.Text = "bpPanelControl5";
			// 
			// txt부서
			// 
			this.txt부서.BackColor = System.Drawing.SystemColors.Control;
			this.txt부서.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt부서.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt부서.Location = new System.Drawing.Point(84, 1);
			this.txt부서.Name = "txt부서";
			this.txt부서.Size = new System.Drawing.Size(143, 21);
			this.txt부서.TabIndex = 10;
			this.txt부서.Tag = "";
			// 
			// labelExt9
			// 
			this.labelExt9.Location = new System.Drawing.Point(17, 4);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(65, 16);
			this.labelExt9.TabIndex = 1;
			this.labelExt9.Text = "코스트센터";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl성명
			// 
			this.pnl성명.Controls.Add(this.txt성명);
			this.pnl성명.Controls.Add(this.labelExt7);
			this.pnl성명.Location = new System.Drawing.Point(244, 1);
			this.pnl성명.Name = "pnl성명";
			this.pnl성명.Size = new System.Drawing.Size(240, 23);
			this.pnl성명.TabIndex = 16;
			this.pnl성명.Text = "bpPanelControl4";
			// 
			// txt성명
			// 
			this.txt성명.BackColor = System.Drawing.SystemColors.Control;
			this.txt성명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt성명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt성명.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt성명.Location = new System.Drawing.Point(97, 0);
			this.txt성명.Name = "txt성명";
			this.txt성명.Size = new System.Drawing.Size(143, 21);
			this.txt성명.TabIndex = 11;
			this.txt성명.Tag = "";
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(65, 16);
			this.labelExt7.TabIndex = 1;
			this.labelExt7.Text = "성명";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl사번
			// 
			this.pnl사번.Controls.Add(this.txt사번);
			this.pnl사번.Controls.Add(this.lbl인도기간);
			this.pnl사번.Location = new System.Drawing.Point(2, 1);
			this.pnl사번.Name = "pnl사번";
			this.pnl사번.Size = new System.Drawing.Size(240, 23);
			this.pnl사번.TabIndex = 15;
			this.pnl사번.Text = "bpPanelControl7";
			// 
			// txt사번
			// 
			this.txt사번.BackColor = System.Drawing.SystemColors.Control;
			this.txt사번.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사번.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사번.Location = new System.Drawing.Point(84, 1);
			this.txt사번.Name = "txt사번";
			this.txt사번.Size = new System.Drawing.Size(143, 21);
			this.txt사번.TabIndex = 9;
			this.txt사번.Tag = "";
			// 
			// lbl인도기간
			// 
			this.lbl인도기간.Location = new System.Drawing.Point(17, 4);
			this.lbl인도기간.Name = "lbl인도기간";
			this.lbl인도기간.Size = new System.Drawing.Size(65, 16);
			this.lbl인도기간.TabIndex = 1;
			this.lbl인도기간.Text = "사번";
			this.lbl인도기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.pnl제목);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 1;
			// 
			// pnl제목
			// 
			this.pnl제목.Controls.Add(this.txt제목);
			this.pnl제목.Controls.Add(this.labelExt10);
			this.pnl제목.Location = new System.Drawing.Point(2, 1);
			this.pnl제목.Name = "pnl제목";
			this.pnl제목.Size = new System.Drawing.Size(715, 23);
			this.pnl제목.TabIndex = 0;
			this.pnl제목.Text = "bpPanelControl6";
			// 
			// txt제목
			// 
			this.txt제목.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.txt제목.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt제목.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt제목.Location = new System.Drawing.Point(84, 1);
			this.txt제목.Name = "txt제목";
			this.txt제목.Size = new System.Drawing.Size(627, 21);
			this.txt제목.TabIndex = 8;
			this.txt제목.Tag = "NM_TITLE";
			// 
			// labelExt10
			// 
			this.labelExt10.Location = new System.Drawing.Point(17, 4);
			this.labelExt10.Name = "labelExt10";
			this.labelExt10.Size = new System.Drawing.Size(65, 16);
			this.labelExt10.TabIndex = 1;
			this.labelExt10.Text = "제목";
			this.labelExt10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneL
			// 
			this.oneL.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneL.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
			this.oneGridItem8,
			this.oneGridItem6,
			this.oneGridItem11,
			this.oneGridItem7,
			this.oneGridItem12,
			this.oneGridItem5,
			this.oneGridItem10});
			this.oneL.Location = new System.Drawing.Point(3, 105);
			this.oneL.Name = "oneL";
			this.oneL.Size = new System.Drawing.Size(560, 182);
			this.oneL.TabIndex = 10;
			// 
			// oneGridItem8
			// 
			this.oneGridItem8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem8.Controls.Add(this.bpPanelControl5);
			this.oneGridItem8.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem8.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem8.Name = "oneGridItem8";
			this.oneGridItem8.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem8.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem8.TabIndex = 0;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.labelExt11);
			this.bpPanelControl5.Controls.Add(this.cur명세서요금);
			this.bpPanelControl5.Controls.Add(this.lbl명세서요금);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(872, 23);
			this.bpPanelControl5.TabIndex = 15;
			this.bpPanelControl5.Text = "bpPanelControl7";
			// 
			// labelExt11
			// 
			this.labelExt11.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt11.Location = new System.Drawing.Point(297, 0);
			this.labelExt11.Name = "labelExt11";
			this.labelExt11.Size = new System.Drawing.Size(63, 23);
			this.labelExt11.TabIndex = 17;
			this.labelExt11.Text = "(총 요금)";
			this.labelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cur명세서요금
			// 
			this.cur명세서요금.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cur명세서요금.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur명세서요금.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur명세서요금.DecimalValue = new decimal(new int[] {
			0,
			0,
			0,
			0});
			this.cur명세서요금.Dock = System.Windows.Forms.DockStyle.Left;
			this.cur명세서요금.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur명세서요금.Location = new System.Drawing.Point(107, 0);
			this.cur명세서요금.Name = "cur명세서요금";
			this.cur명세서요금.NullString = "0";
			this.cur명세서요금.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur명세서요금.Size = new System.Drawing.Size(190, 21);
			this.cur명세서요금.TabIndex = 15;
			this.cur명세서요금.Tag = "AM_TOTAL";
			this.cur명세서요금.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl명세서요금
			// 
			this.lbl명세서요금.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl명세서요금.Location = new System.Drawing.Point(0, 0);
			this.lbl명세서요금.Name = "lbl명세서요금";
			this.lbl명세서요금.Size = new System.Drawing.Size(107, 23);
			this.lbl명세서요금.TabIndex = 1;
			this.lbl명세서요금.Text = "명세서요금";
			this.lbl명세서요금.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem6
			// 
			this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem6.Controls.Add(this.bpPanelControl3);
			this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem6.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem6.Name = "oneGridItem6";
			this.oneGridItem6.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem6.TabIndex = 1;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.labelExt4);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(544, 23);
			this.bpPanelControl3.TabIndex = 16;
			this.bpPanelControl3.Text = "bpPanelControl7";
			// 
			// labelExt4
			// 
			this.labelExt4.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt4.Location = new System.Drawing.Point(0, 0);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(216, 23);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "※ 요금상세내역 금액 기준 기입";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem11
			// 
			this.oneGridItem11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem11.Controls.Add(this.bpPanelControl12);
			this.oneGridItem11.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem11.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem11.Name = "oneGridItem11";
			this.oneGridItem11.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem11.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem11.TabIndex = 2;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.labelExt5);
			this.bpPanelControl12.Controls.Add(this.cur기본요금);
			this.bpPanelControl12.Controls.Add(this.lbl기본요금);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(544, 23);
			this.bpPanelControl12.TabIndex = 17;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// labelExt5
			// 
			this.labelExt5.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt5.Location = new System.Drawing.Point(297, 0);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(156, 23);
			this.labelExt5.TabIndex = 16;
			this.labelExt5.Text = "(부가세 10% 자동 적용됨)";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cur기본요금
			// 
			this.cur기본요금.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cur기본요금.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur기본요금.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur기본요금.DecimalValue = new decimal(new int[] {
			0,
			0,
			0,
			0});
			this.cur기본요금.Dock = System.Windows.Forms.DockStyle.Left;
			this.cur기본요금.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur기본요금.Location = new System.Drawing.Point(107, 0);
			this.cur기본요금.Name = "cur기본요금";
			this.cur기본요금.NullString = "0";
			this.cur기본요금.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur기본요금.Size = new System.Drawing.Size(190, 21);
			this.cur기본요금.TabIndex = 15;
			this.cur기본요금.Tag = "AM_BASIC";
			this.cur기본요금.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl기본요금
			// 
			this.lbl기본요금.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl기본요금.Location = new System.Drawing.Point(0, 0);
			this.lbl기본요금.Name = "lbl기본요금";
			this.lbl기본요금.Size = new System.Drawing.Size(107, 23);
			this.lbl기본요금.TabIndex = 1;
			this.lbl기본요금.Text = "월정액";
			this.lbl기본요금.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem7
			// 
			this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem7.Controls.Add(this.bpPanelControl11);
			this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem7.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem7.Name = "oneGridItem7";
			this.oneGridItem7.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem7.TabIndex = 3;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.labelExt6);
			this.bpPanelControl11.Controls.Add(this.cur국내통화료);
			this.bpPanelControl11.Controls.Add(this.lbl국내통화료);
			this.bpPanelControl11.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(872, 23);
			this.bpPanelControl11.TabIndex = 15;
			this.bpPanelControl11.Text = "bpPanelControl7";
			// 
			// labelExt6
			// 
			this.labelExt6.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt6.Location = new System.Drawing.Point(297, 0);
			this.labelExt6.Name = "labelExt6";
			this.labelExt6.Size = new System.Drawing.Size(156, 23);
			this.labelExt6.TabIndex = 17;
			this.labelExt6.Text = "(부가세 10% 자동 적용됨)";
			this.labelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cur국내통화료
			// 
			this.cur국내통화료.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur국내통화료.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur국내통화료.DecimalValue = new decimal(new int[] {
			0,
			0,
			0,
			0});
			this.cur국내통화료.Dock = System.Windows.Forms.DockStyle.Left;
			this.cur국내통화료.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur국내통화료.Location = new System.Drawing.Point(107, 0);
			this.cur국내통화료.Name = "cur국내통화료";
			this.cur국내통화료.NullString = "0";
			this.cur국내통화료.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur국내통화료.Size = new System.Drawing.Size(190, 21);
			this.cur국내통화료.TabIndex = 15;
			this.cur국내통화료.Tag = "AM_ADD1";
			this.cur국내통화료.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl국내통화료
			// 
			this.lbl국내통화료.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl국내통화료.Location = new System.Drawing.Point(0, 0);
			this.lbl국내통화료.Name = "lbl국내통화료";
			this.lbl국내통화료.Size = new System.Drawing.Size(107, 23);
			this.lbl국내통화료.TabIndex = 1;
			this.lbl국내통화료.Text = "국내통화료";
			this.lbl국내통화료.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem12
			// 
			this.oneGridItem12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem12.Controls.Add(this.bpPanelControl10);
			this.oneGridItem12.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem12.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem12.Name = "oneGridItem12";
			this.oneGridItem12.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem12.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem12.TabIndex = 4;
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.labelExt8);
			this.bpPanelControl10.Controls.Add(this.cur국제전화1);
			this.bpPanelControl10.Controls.Add(this.lbl국제통화료);
			this.bpPanelControl10.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(544, 23);
			this.bpPanelControl10.TabIndex = 17;
			this.bpPanelControl10.Text = "bpPanelControl4";
			// 
			// labelExt8
			// 
			this.labelExt8.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt8.Location = new System.Drawing.Point(297, 0);
			this.labelExt8.Name = "labelExt8";
			this.labelExt8.Size = new System.Drawing.Size(156, 23);
			this.labelExt8.TabIndex = 18;
			this.labelExt8.Text = "(부가세 10% 자동 적용됨)";
			this.labelExt8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cur국제전화1
			// 
			this.cur국제전화1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur국제전화1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur국제전화1.DecimalValue = new decimal(new int[] {
			0,
			0,
			0,
			0});
			this.cur국제전화1.Dock = System.Windows.Forms.DockStyle.Left;
			this.cur국제전화1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur국제전화1.Location = new System.Drawing.Point(107, 0);
			this.cur국제전화1.Name = "cur국제전화1";
			this.cur국제전화1.NullString = "0";
			this.cur국제전화1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur국제전화1.Size = new System.Drawing.Size(190, 21);
			this.cur국제전화1.TabIndex = 17;
			this.cur국제전화1.Tag = "AM_ROAMING1";
			this.cur국제전화1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl국제통화료
			// 
			this.lbl국제통화료.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl국제통화료.Location = new System.Drawing.Point(0, 0);
			this.lbl국제통화료.Name = "lbl국제통화료";
			this.lbl국제통화료.Size = new System.Drawing.Size(107, 23);
			this.lbl국제통화료.TabIndex = 16;
			this.lbl국제통화료.Text = "국제통화료";
			this.lbl국제통화료.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl20);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 5;
			// 
			// bpPanelControl20
			// 
			this.bpPanelControl20.Controls.Add(this.txt비고);
			this.bpPanelControl20.Controls.Add(this.lbl비고);
			this.bpPanelControl20.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl20.Name = "bpPanelControl20";
			this.bpPanelControl20.Size = new System.Drawing.Size(873, 23);
			this.bpPanelControl20.TabIndex = 15;
			this.bpPanelControl20.Text = "bpPanelControl7";
			// 
			// txt비고
			// 
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Dock = System.Windows.Forms.DockStyle.Left;
			this.txt비고.Location = new System.Drawing.Point(107, 0);
			this.txt비고.Name = "txt비고";
			this.txt비고.Size = new System.Drawing.Size(441, 21);
			this.txt비고.TabIndex = 9;
			this.txt비고.Tag = "DC_RMK";
			// 
			// lbl비고
			// 
			this.lbl비고.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl비고.Location = new System.Drawing.Point(0, 0);
			this.lbl비고.Name = "lbl비고";
			this.lbl비고.Size = new System.Drawing.Size(107, 23);
			this.lbl비고.TabIndex = 1;
			this.lbl비고.Text = "비고";
			this.lbl비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem10
			// 
			this.oneGridItem10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem10.Controls.Add(this.bpPanelControl4);
			this.oneGridItem10.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem10.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem10.Name = "oneGridItem10";
			this.oneGridItem10.Size = new System.Drawing.Size(550, 23);
			this.oneGridItem10.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem10.TabIndex = 6;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.tbx첨부파일);
			this.bpPanelControl4.Controls.Add(this.btn첨부파일);
			this.bpPanelControl4.Controls.Add(this.labelExt1);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(872, 22);
			this.bpPanelControl4.TabIndex = 15;
			this.bpPanelControl4.Text = "bpPanelControl7";
			// 
			// tbx첨부파일
			// 
			this.tbx첨부파일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx첨부파일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx첨부파일.Dock = System.Windows.Forms.DockStyle.Left;
			this.tbx첨부파일.Location = new System.Drawing.Point(107, 0);
			this.tbx첨부파일.Name = "tbx첨부파일";
			this.tbx첨부파일.ReadOnly = true;
			this.tbx첨부파일.Size = new System.Drawing.Size(190, 21);
			this.tbx첨부파일.TabIndex = 9;
			this.tbx첨부파일.TabStop = false;
			this.tbx첨부파일.Tag = "";
			// 
			// btn첨부파일
			// 
			this.btn첨부파일.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn첨부파일.BackColor = System.Drawing.Color.White;
			this.btn첨부파일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn첨부파일.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn첨부파일.Location = new System.Drawing.Point(337, 1);
			this.btn첨부파일.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn첨부파일.Name = "btn첨부파일";
			this.btn첨부파일.Size = new System.Drawing.Size(70, 19);
			this.btn첨부파일.TabIndex = 12;
			this.btn첨부파일.TabStop = false;
			this.btn첨부파일.Text = "첨부파일";
			this.btn첨부파일.UseVisualStyleBackColor = false;
			// 
			// labelExt1
			// 
			this.labelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt1.Location = new System.Drawing.Point(0, 0);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(107, 22);
			this.labelExt1.TabIndex = 1;
			this.labelExt1.Text = "첨부파일";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn전자결재
			// 
			this.btn전자결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전자결재.BackColor = System.Drawing.Color.White;
			this.btn전자결재.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전자결재.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전자결재.Location = new System.Drawing.Point(1386, 10);
			this.btn전자결재.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전자결재.Name = "btn전자결재";
			this.btn전자결재.Size = new System.Drawing.Size(70, 19);
			this.btn전자결재.TabIndex = 12;
			this.btn전자결재.TabStop = false;
			this.btn전자결재.Text = "전자결재";
			this.btn전자결재.UseVisualStyleBackColor = false;
			// 
			// lbl관련파일
			// 
			this.lbl관련파일.Location = new System.Drawing.Point(17, 4);
			this.lbl관련파일.Name = "lbl관련파일";
			this.lbl관련파일.Size = new System.Drawing.Size(84, 16);
			this.lbl관련파일.TabIndex = 1;
			this.lbl관련파일.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn파일첨부
			// 
			this.btn파일첨부.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn파일첨부.BackColor = System.Drawing.Color.White;
			this.btn파일첨부.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn파일첨부.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn파일첨부.Location = new System.Drawing.Point(504, 2);
			this.btn파일첨부.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn파일첨부.Name = "btn파일첨부";
			this.btn파일첨부.Size = new System.Drawing.Size(70, 19);
			this.btn파일첨부.TabIndex = 12;
			this.btn파일첨부.TabStop = false;
			this.btn파일첨부.Text = "파일첨부";
			this.btn파일첨부.UseVisualStyleBackColor = false;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "FILE_ICON.gif");
			// 
			// btn승인
			// 
			this.btn승인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn승인.BackColor = System.Drawing.Color.White;
			this.btn승인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인.Location = new System.Drawing.Point(1303, 10);
			this.btn승인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인.Name = "btn승인";
			this.btn승인.Size = new System.Drawing.Size(70, 19);
			this.btn승인.TabIndex = 13;
			this.btn승인.TabStop = false;
			this.btn승인.Text = "승인";
			this.btn승인.UseVisualStyleBackColor = false;
			// 
			// P_CZ_HR_PHONE_PAY
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn승인);
			this.Controls.Add(this.btn전자결재);
			this.Name = "P_CZ_HR_PHONE_PAY";
			this.Size = new System.Drawing.Size(1461, 619);
			this.TitleText = "P_CZ_HR_PHONE_PAY";
			this.Controls.SetChildIndex(this.btn전자결재, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn승인, 0);
			this.mDataArea.ResumeLayout(false);
			this.spcM.Panel1.ResumeLayout(false);
			this.spcM.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spcM)).EndInit();
			this.spcM.ResumeLayout(false);
			this.tlayH.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.tlayL.ResumeLayout(false);
			this.pnl합계.ResumeLayout(false);
			this.pnl제목2.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.pnl부서.ResumeLayout(false);
			this.pnl부서.PerformLayout();
			this.pnl성명.ResumeLayout(false);
			this.pnl성명.PerformLayout();
			this.pnl사번.ResumeLayout(false);
			this.pnl사번.PerformLayout();
			this.oneGridItem4.ResumeLayout(false);
			this.pnl제목.ResumeLayout(false);
			this.pnl제목.PerformLayout();
			this.oneGridItem8.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur명세서요금)).EndInit();
			this.oneGridItem6.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem11.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.bpPanelControl12.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur기본요금)).EndInit();
			this.oneGridItem7.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl11.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur국내통화료)).EndInit();
			this.oneGridItem12.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.bpPanelControl10.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur국제전화1)).EndInit();
			this.oneGridItem5.ResumeLayout(false);
			this.bpPanelControl20.ResumeLayout(false);
			this.bpPanelControl20.PerformLayout();
			this.oneGridItem10.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
