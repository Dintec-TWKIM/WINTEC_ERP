using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using DX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.Windows.Print;
using System.Diagnostics;

namespace cz
{
	public partial class P_CZ_MAILBAGLIST : PageBase
	{

		P_CZ_MAILBAGLIST_BIZ _biz = new P_CZ_MAILBAGLIST_BIZ();

		int int_본사마감시간 = 1900;
		int int_양산마감시간 = 1745;
		string No_Emp_Send = "S-623";

		//이름가져오는
		public string NM_EMP { get; set; }

		public P_CZ_MAILBAGLIST()
		{
			try
			{
				NM_EMP = Global.MainFrame.LoginInfo.UserName;
				InitializeComponent();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitLoad() // 기본세팅
		{
			base.InitLoad();
			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid() // 그리드 컬럼세팅
		{
			this.MainGrids = new FlexGrid[] { this.flex리스트, this.flex작성};
			this.flex리스트.DetailGrids = new FlexGrid[] {this.flex작성 };

			#region 헤더_리스트
			//flex에 보여주는
			this.flex리스트.BeginSetting(1, 1, false);
			this.flex리스트.SetCol("DT_ACCT", "작성일", 100, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this.flex리스트.SetCol("NM_SYSDEF", "발송위치", 150);
			this.flex리스트.ExtendLastCol = true;

			this.flex리스트.SettingVersion = "23.04.25.07";
			this.flex리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 라인_작성
			this.flex작성.BeginSetting(1, 1, true);

			this.flex작성.SetCol("NM_ITEM", "내용물", 500);
			this.flex작성.SetCol("QT", "수량", 40);
			this.flex작성.SetCol("NM_KOR_SEND", "발송자", 60, false);
			this.flex작성.SetCol("NM_KOR_RECEIVE", "수신자", 60, false);
			this.flex작성.SetCol("NM_KOR_INSPECT", "확인자", 60, false);
			this.flex작성.SetCol("DC_RMK", "비고", 250);

			this.flex작성.SetOneGridBinding(new object[] { }, new IUParentControl[] { this.oneGrid2 });
			
			//마지막 셀을 끝까지
			this.flex작성.ExtendLastCol = true;

			this.flex작성.SettingVersion = "23.06.29.01";
			this.flex작성.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

		}


		protected override void InitPaint() // 데이터값 셋팅
		{
			base.InitPaint();

			//왼쪽리스트 조회기간
			this.dtp작성일.Text = Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd");

			this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddDays(-10).ToString("yyyyMMdd");
			this.dtp조회기간.EndDateToString = Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd");

			this.btn추가.Enabled = false;
			this.btn삭제.Enabled = false;
			this.btn확인.Enabled = false;

		}
		private void InitEvent() // 발생 이벤트 세팅
		{
			this.flex리스트.AfterRowChange += new RangeEventHandler(flex리스트_AfterRowChange);

			this.btn추가.Click += new EventHandler(btn추가_Click);
			this.btn삭제.Click += new EventHandler(btn삭제_Click);
			this.btn확인.Click += new EventHandler(btn확인_Click);

			this.rdo본사.Click += new EventHandler(rdo_Click);
			this.rdo양산.Click += new EventHandler(rdo_Click);

			this.btn쪽지.Click += new EventHandler(btn쪽지_Click);

			this.ctx발송자.QueryBefore += ctx사원_QueryBefore;
			this.ctx수신자.QueryBefore += ctx사원_QueryBefore;
			this.ctx확인자.QueryBefore += ctx사원_QueryBefore;
		}

		private void ctx사원_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.UserParams = "출장자;H_CZ_MA_CUSTOMIZE_SUB";
			e.HelpParam.P11_ID_MENU = "H_MA_EMP_SUB";
			e.HelpParam.P21_FG_MODULE = "N";
			e.HelpParam.MultiHelp = false;
		}

		//헤드 row 선택시 라인의 flex변경
		private void flex리스트_AfterRowChange(object sender, EventArgs e)
		{
			try
			{
				if (!this.flex리스트.HasNormalRow) return;

				object[] objArray = new object[] {  this.flex리스트["DT_ACCT"].ToString() ,
													this.flex리스트["CD_SEND"].ToString()};
				this.flex작성.Binding = this._biz.Search_작성(objArray);

			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//라인 row추가
		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable dt = this.flex작성.DataTable;

				this.flex작성.Rows.Add();
				this.flex작성.Row = flex작성.Rows.Count - 1;

				this.flex작성["CD_SEND"] = this.flex리스트["CD_SEND"];
				this.flex작성["DT_ACCT"] = this.flex리스트["DT_ACCT"];
				if (dt.Compute("MAX(SEQ)", "").ToString() == "") this.flex작성["SEQ"] = 1;
				else this.flex작성["SEQ"] = 1 + Convert.ToInt32(dt.Compute("MAX(SEQ)", ""));
				this.flex작성["QT"] = 1;
				this.flex작성["NO_EMP_SEND"] = Global.MainFrame.LoginInfo.UserID;
				this.flex작성["NM_KOR_SEND"] = Global.MainFrame.LoginInfo.UserName;
				this.flex작성["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;
				this.flex작성["DTS_INSERT"] = Convert.ToString(Global.MainFrame.GetDateTimeToday().ToString("yyyyMMddHHmmss"));
				
				this.flex작성.AddFinished();

			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//라인 row삭제
		private void btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flex작성.HasNormalRow) return;

				this.flex작성.Rows.Remove(this.flex작성.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//확인버튼 클릭시 로그인한 이름 삽입
		private void btn확인_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flex작성.HasNormalRow) return;

				this.flex작성["NO_EMP_INSPECT"] = Global.MainFrame.LoginInfo.UserID;
				this.flex작성["NM_KOR_INSPECT"] = Global.MainFrame.LoginInfo.UserName;

				this.flex작성.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		//로그인한 아이디로 쪽지보내기 테스트용
		private void btn쪽지_Click(object sender, EventArgs e)
		{
			try
			{

				//string query = @"
				//				SELECT *
				//				FROM CZ_MA_CODEDTL
				//				WHERE CD_FIELD = 'CZ_MM00004'";

				//DataSet ds = TSQL.실행<DataSet>(query);
				//int 순번 = ds.Tables[0].Rows[0][0].정수();
				//string test = ds.Tables[0].Rows[1][2].ToString();
				//int test1 = ds.Tables[0].Rows[1][2].정수();
				//this.ShowMessage(test);
				//this.ShowMessage(test1.ToString());
				//P_CZ_MAILBAGLIST_SUB dialog = new P_CZ_MAILBAGLIST_SUB();
				//dialog.ShowDialog();
				//this._biz.Msg_Send(new object[] { Send_NO_EMP });
				//this._biz.Msg_Send2(new object[] { Send_NO_EMP });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		//본사,양산 라디오 버튼 클릭시 조회버튼 클릭
		private void rdo_Click(object sender, EventArgs e)
		{
			this.OnToolBarSearchButtonClicked(null, null);
		}
		//조회버튼 클릭이 본사인지 양산인지 확인후 검색
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (this.btn추가.Enabled == false)
				{
					this.btn추가.Enabled = true;
					this.btn삭제.Enabled = true;
					this.btn확인.Enabled = true;
				}

				string sendcompany = "";

				if (rdo본사.Checked)
				{
					sendcompany = "01";
					if (Convert.ToInt32(Global.MainFrame.GetDateTimeToday().ToString("HHmm")) >= int_본사마감시간
						&& this.dtp작성일.Text == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd"))
					{
						this.ShowMessage("금일 마감시간은 오후7시 입니다\n추가작성은 담당자에게 연락 후 추가부탁드립니다\n안된다면 명일에 입력 부탁드립니다\n담당자 : 박일현조장");
					}
				}
				else
				{
					sendcompany = "02";
					if (Convert.ToInt32(Global.MainFrame.GetDateTimeToday().ToString("HHmm")) >= int_양산마감시간
						&& this.dtp작성일.Text == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd"))
					{
						this.ShowMessage("금일 마감시간은 오후5시45분 입니다\n추가작성은 담당자에게 연락 후 추가부탁드립니다\n안된다면 명일에 입력 부탁드립니다\n담당자 : 박일현조장");
					}
				}

				this.flex리스트.Binding = this._biz.Search_리스트(new object[] {	sendcompany,
																			this.dtp조회기간.StartDateToString,
																			this.dtp조회기간.EndDateToString	});

				if (!this.flex리스트.HasNormalRow)
					this.ShowMessage(PageResultMode.SearchNoData);

				this.flex리스트.AcceptChanges();

			}
			catch(Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//헤드 row추가
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);
				if (!this.BeforeAdd()) return;

				DataTable dt = this.flex리스트.DataTable;

				if (Int32.Parse(this.dtp작성일.Text) > Int32.Parse(Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd")))
				{
					this.ShowMessage("금일 이후는 작성할수 없습니다");
				}
				else if (dt.Compute("MAX(DT_ACCT)", "").ToString() == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd") 
						&& this.dtp작성일.Text == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd"))
				{
					this.ShowMessage("오늘은 이미 추가되있습니다.");
				}
				else
						{
					this.flex리스트.Rows.Add();
					this.flex리스트.Row = this.flex리스트.Rows.Count - 1;

					this.flex리스트["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					this.flex리스트["NO_EMP"] = Global.MainFrame.LoginInfo.UserID;
					this.flex리스트["DT_ACCT"] = this.dtp작성일.Text;

					if (rdo본사.Checked)
					{
						this.flex리스트["CD_SEND"] = "01";
						this.flex리스트["NM_SYSDEF"] = "본사에서 발송";
					}
					if (rdo양산.Checked)
					{
						this.flex리스트["CD_SEND"] = "02";
						this.flex리스트["NM_SYSDEF"] = "양산에서 발송";
					}

					this.flex리스트.AddFinished();
					this.flex리스트.Focus();

					this.btn추가_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		//헤드 row 삭제
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (DialogResult.OK == MessageBox.Show("왼쪽에있는 리스트를 지우시겠습니까?", "리스트삭제", MessageBoxButtons.OKCancel))
				{
					base.OnToolBarDeleteButtonClicked(sender, e);
					if (!this.flex리스트.HasNormalRow) return;
					this.flex리스트.Rows.Remove(this.flex리스트.Row);
				}
				else;

			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//저장 버튼클릭시
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (this.MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		//저장본문
		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			if (!this.flex리스트.IsDataChanged && !this.flex작성.IsDataChanged) return false;

			if (this.ctx수신자.값() == "")
			{
				this.ShowMessage("수신자 등록이 안되어있습니다\n상단에서 적어주시기 바랍니다");
				return false;
			}

			DataTable dt = this.flex리스트.DataTable;
			DataTable changes_H = this.flex리스트.GetChanges();
			DataTable changes1_L = this.flex작성.GetChanges();

			if (changes_H == null && changes1_L == null) return true;

			if (changes_H != null && changes1_L == null) //이 경우는 없음
			{
				if (!this._biz.Save_H(changes_H)) return false;
				this.flex리스트.AcceptChanges();
			}
			if ( changes_H == null && changes1_L != null)
			{
				if (!this._biz.Save_L(changes1_L)) return false;
				this.flex작성.AcceptChanges();
			}
			if (changes_H != null && changes1_L != null)
			{
				if (!this._biz.Save_H(changes_H)) return false;
				if (!this._biz.Save_L(changes1_L)) return false;
				this.flex리스트.AcceptChanges();
				this.flex작성.AcceptChanges();
			}

			this.OnToolBarSearchButtonClicked(null, null);

			if(rdo본사.Checked)
			{
				if (Convert.ToInt32(Global.MainFrame.GetDateTimeToday().ToString("HHmm")) >= int_본사마감시간
					&& this.dtp작성일.Text == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd"))
				{
					this.ShowMessage("담당자에게 쪽지가 자동으로 발송되었습니다(본사)");
					this._biz.Msg_Send(new object[] { No_Emp_Send ,"01" });
				}
			}
			else if(rdo양산.Checked)
			{
				if (Convert.ToInt32(Global.MainFrame.GetDateTimeToday().ToString("HHmm")) >= int_양산마감시간
					&& this.dtp작성일.Text == Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd"))
				{
					this.ShowMessage("담당자에게 쪽지가 자동으로 발송되었습니다(양산)");
					this._biz.Msg_Send(new object[] { No_Emp_Send,"02" });
				}
			}

			return true;
		}
		//프린터
		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				ReportHelper reportHelper;
				base.OnToolBarPrintButtonClicked(sender, e);

				if (!this.flex작성.HasNormalRow) return;

				DataTable dt = this.flex작성.DataTable.Select(string.Format("DT_ACCT = '{0}' AND NM_SYSDEF = '{1}'",
											this.flex작성["DT_ACCT"].ToString(),
											this.flex작성["NM_SYSDEF"].ToString())).ToDataTable();

				reportHelper = new ReportHelper("R_CZ_MAILBAGLIST", "행낭물류대장리스트");
				reportHelper.SetDataTable(dt, 1);
				reportHelper.Print();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		} 
	}
}
