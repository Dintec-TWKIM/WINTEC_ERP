using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_MA_DELIVERY_REG : PageBase
	{
		P_CZ_MA_DELIVERY_REG_BIZ _biz = new P_CZ_MA_DELIVERY_REG_BIZ();

		public P_CZ_MA_DELIVERY_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			UGrant ugrant = new UGrant();

			if (ugrant.GrantButton("P_CZ_MA_DELIVERY_REG", "CONFIRM") == false ||
				Global.MainFrame.LoginInfo.CompanyCode == "W100")
			{
				bpc회사.Enabled = false;
				bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode,
									 Global.MainFrame.LoginInfo.CompanyName);

			}

			cbo택배사.DataBind(GetDb.Code("CZ_SA00067"), true);

			string query = "SELECT CD_SYSDEF AS CODE, NM_SYSDEF AS NAME FROM CZ_MA_CODEDTL WHERE CD_FIELD = 'CZ_SA00060' AND CD_SYSDEF LIKE 'WM%'";
			DataTable codeDT = DBMgr.GetDataTable(query);

			cbo송품방법.DataBind(codeDT, true);

			ugrant.GrantButtonVisible("P_CZ_MA_DELIVERY_REG", "CONFIRM", btn승인);
			ugrant.GrantButtonVisible("P_CZ_MA_DELIVERY_REG", "REJECT", btn반려);
			//ugrant.GrantButtonVisible("P_CZ_MA_DELIVERY_REG", "ROLLBACK", btn취소);

			InitGrid();
			InitEvent();
		}


		private void InitGrid()
		{
			MainGrids = new FlexGrid[] { flex };

			flex.BeginSetting(1, 1, true);

			flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			flex.SetCol("YN_USE", "처리여부", 40, false, CheckTypeEnum.Y_N);
			flex.SetCol("NM_COMPANY", "신청회사", 150);
			flex.SetCol("NM_PARTNER_GRP", "납품처그룹", 160);
			flex.SetCol("CD_PARTNER", "납품처코드", 90);
			flex.SetCol("LN_PARTNER", "납품처명", 160);
			flex.SetCol("EN_PARTNER", "영문명", 160);
			flex.SetCol("NO_POST", "우편번호", 60);
			flex.SetCol("DC_ADDRESS", "주소", 160);
			flex.SetCol("DC_ADDRESS1", "상세주소", 160);
			flex.SetCol("NO_TEL", "전화번호", 60);
			flex.SetCol("NM_PIC", "담당자", 60);
			flex.SetCol("CD_TAEGBAE", "택배사", 60);
			flex.SetCol("CD_SHIPMENT", "송품방법", false);
			flex.SetCol("NM_SHIPMENT", "송품방법", 60);
			flex.SetCol("NO_TEL1", "휴대전화", false);
			flex.SetCol("NO_TEL2", "FAX번호", 60);
			flex.SetCol("TP_PERIOD", "발송주기", false);
			flex.SetCol("YN_MON", "월", false);
			flex.SetCol("YN_TUE", "화", false);
			flex.SetCol("YN_WED", "수", false);
			flex.SetCol("YN_THU", "목", false);
			flex.SetCol("YN_FRI", "금", false);
			flex.SetCol("NO_EMAIL", "E-MAIL", 160);
			flex.SetCol("DAY_WORK", "작업소요일", 70);
			flex.SetCol("NM_INSERT", "등록자", 80);
			flex.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("NM_UPDATE", "수정자", 80);
			flex.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
			flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

			//flex.VerifyPrimaryKey = new string[] { "LN_PARTNER" };

			flex.Cols["DAY_WORK"].TextAlign = TextAlignEnum.CenterCenter;



			flex.SetDummyColumn("S");
			flex.SetOneGridBinding(new object[] { },  this.one기본정보);

			flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdo매일, this.rdo매주 }, new string[] { "DAY", "WEK" });
			flex.SetBindningCheckBox(chk월요일, "Y", "N");
			flex.SetBindningCheckBox(chk화요일, "Y", "N");
			flex.SetBindningCheckBox(chk수요일, "Y", "N");
			flex.SetBindningCheckBox(chk목요일, "Y", "N");
			flex.SetBindningCheckBox(chk금요일, "Y", "N");

			flex.SettingVersion = "1.1.0.21";
			flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			splitContainer1.SplitterDistance = splitContainer1.Width - 700;

			cbo처리여부.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "처리", "미처리" }, true);
			cbo처리여부.DisplayMember = "NAME";
			cbo처리여부.ValueMember = "CODE";
		}

		private void InitEvent()
		{
			btn본사주소.Click += Btn본사주소_Click;
			btn반려.Click += Btn반려_Click;
			btn승인.Click += Btn승인_Click;

			flex.AfterRowChange += Flex_AfterRowChange;


			rdo매일.CheckedChanged += RadioButton_CheckedChanged;
			rdo매주.CheckedChanged += RadioButton_CheckedChanged;

			tbx작업소요일.KeyPress += Tbx작업소요일_KeyPress;
		}

		private void Tbx작업소요일_KeyPress(object sender, KeyPressEventArgs e)
		{
			//숫자만 입력되도록 필터링             
			if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자(0제외)와 백스페이스를 제외한 나머지를 바로 처리             
			{
				e.Handled = true;
			}

		
		}

		private void Flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			if(!string.IsNullOrEmpty(flex["CD_COMPANY"].ToString()) && !string.IsNullOrEmpty(flex["CD_PARTNER"].ToString()))
			{
				string query = "SELECT LN_PARTNER FROM CZ_MA_DELIVERY WHERE CD_COMPANY = '"+ flex["CD_COMPANY"].ToString() + "' AND CD_PARTNER IN ( ";
				query =query + " SELECT MAP.CD_PARTNER_NEW FROM CZ_MA_DELIVERY AS A";
				query = query + " LEFT JOIN CZ_MA_DELIVERY_MAP		AS MAP ON A.CD_COMPANY = MAP.CD_COMPANY AND A.CD_PARTNER = MAP.CD_PARTNER_OLD";
				query = query + " LEFT JOIN CZ_MA_DELIVERY			AS Z ON A.CD_COMPANY = Z.CD_COMPANY AND MAP.CD_PARTNER_NEW = Z.CD_PARTNER";
				query = query + " WHERE A.CD_COMPANY = '" + flex["CD_COMPANY"].ToString() + "'";
				query = query + " AND A.CD_PARTNER = '" + flex["CD_PARTNER"].ToString() + "')";
				DataTable dt = DBMgr.GetDataTable(query);



				if(dt.Rows.Count > 0)
				{
					tbx매칭뉴.Text = dt.Rows[0]["LN_PARTNER"].ToString();
				}
				else
				{
					tbx매칭뉴.Text = "";
				}

			}
		}


		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (rdo매일.Checked)
				{
					chk월요일.Enabled = false;
					chk화요일.Enabled = false;
					chk수요일.Enabled = false;
					chk목요일.Enabled = false;
					chk금요일.Enabled = false;

					chk월요일.Checked = false;
					chk화요일.Checked = false;
					chk수요일.Checked = false;
					chk목요일.Checked = false;
					chk금요일.Checked = false;
				}
				else
				{
					chk월요일.Enabled = true;
					chk화요일.Enabled = true;
					chk수요일.Enabled = true;
					chk목요일.Enabled = true;
					chk금요일.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#region 조회
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				flex.Binding = _biz.Search(new object[] { bpc회사.QueryWhereIn_Pipe,
																	 D.GetString(cbo처리여부.SelectedValue),
																	 txt납품처명S.Text, txt영문명S.Text, tbx코드S.Text });

				if (!flex.HasNormalRow)
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);

			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion 조회

		#region 추가
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!BeforeAdd()) return;

				flex.Rows.Add();
				flex.Row = flex.Rows.Count - 1;

				flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				flex["NM_COMPANY"] = Global.MainFrame.LoginInfo.CompanyName;
				flex["TP_PERIOD"] = "DAY";
				flex["YN_USE"] = "N";

				flex.AddFinished();
				flex.Col = 1;
				flex.Focus();
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
			DataTable dt = flex.GetChanges();

			if (dt.Select().AsEnumerable().Where(x => Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", x["NO_EMAIL"].ToString()))) == 0).Count() > 0)
			{
				ShowMessage("메일주소 형식이 올바르지 않습니다.");
				return false;
			}
			else if (string.IsNullOrEmpty(dt.Rows[0]["LN_PARTNER"].ToString()))
			{
				this.ShowMessage("거래처명을 입력해주세요.");
				return false;
			}
			else if (this.flex.GetChanges() != null &&
				this.flex.GetChanges().Select("TP_PERIOD = 'WEK' AND ISNULL(YN_MON, 'N') = 'N' AND ISNULL(YN_TUE, 'N') = 'N' AND ISNULL(YN_WED, 'N') = 'N' AND ISNULL(YN_THU, 'N') = 'N' AND ISNULL(YN_FRI, 'N') = 'N'").Length > 0)
			{
				this.ShowMessage("발송주기 매주 선택시 요일 선택은 필수 입니다.");
				return false;
			}
			else if (dt.Rows[0]["YN_USE"].ToString().Equals("Y"))
			{
				if (!base.SaveData() || !base.Verify()) return false;
				if (flex.IsDataChanged == false) return false;

				DataTable dt1 = flex.GetChanges();

				foreach (DataRow dr in dt1.Rows)
				{
					if (dr.RowState == DataRowState.Added)
					{
						dr["CD_PARTNER"] = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "16", Global.MainFrame.GetStringToday.Substring(2, 4));
					}

					if (dr["DAY_WORK"].ToString() == "0")
					{
						dr["DAY_WORK"] = DBNull.Value;
					}
				}


				string xml = Util.GetTO_Xml(dt1);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_DELIVERY_REG_MAIL_XML", new object[] { xml });


				flex.AcceptChanges();

				OnToolBarSearchButtonClicked(null, null);


				//ShowMessage("거래처명을 제외한 항목이 변경 되었습니다.");
				return false;
			}

			return base.BeforeSave();
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!BeforeSave()) return;

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
			if (!base.SaveData() || !base.Verify() ) return false;
			if (flex.IsDataChanged == false) return false;

			

			DataTable dt = flex.GetChanges();

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.RowState == DataRowState.Added)
				{
					dr["CD_PARTNER"] = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "16", Global.MainFrame.GetStringToday.Substring(2, 4));
				}
			}




			string xml = Util.GetTO_Xml(dt);
			DBMgr.ExecuteNonQuery("SP_CZ_MA_DELIVERY_REG_XML", new object[] { xml });


			flex.AcceptChanges();

			//Sync(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["CD_PARTNER"]));

			OnToolBarSearchButtonClicked(null, null);

			return true;
		}

		#endregion 저장

		#region 삭제
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			int index;
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!BeforeDelete() || !flex.HasNormalRow) return;

				dataRowArray = flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					index = 0;
					flex.Redraw = false;
					foreach (DataRow dr in dataRowArray)
					{
						MsgControl.ShowMsg("잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });


						if (dr["YN_USE"].ToString().Equals("Y"))
						{
							ShowMessage("승인 처리 된 납품처는 삭제할 수 없습니다.");
							MsgControl.CloseMsg();
							return;
						}

						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				flex.Redraw = true;
				MsgControl.CloseMsg();
			}
		}

		#endregion 삭제


		private void Sync(string cdCompany, string cdPartner)
		{
			string connectStr = "Server=113.130.254.143; Database=NEOE; Uid=sa; Password=skm0828!";
			string query = string.Empty;

			string cdCompany2 = string.Empty;
			string cdPartner2 = string.Empty;


			if (cdCompany != "S100")
			{
				if (cdCompany.Equals("K100"))
				{
					cdCompany2 = "K200";
					cdPartner2 = (string)this.GetSeq("K200", "CZ", "16", Global.MainFrame.GetStringToday.Substring(2, 4));
				}
				else if (cdCompany.Equals("K200"))
				{
					cdCompany2 = "K100";
					cdPartner2 = (string)this.GetSeq("K100", "CZ", "16", Global.MainFrame.GetStringToday.Substring(2, 4));
				}

				
				query = @"BEGIN TRAN

							 INSERT INTO NEOE.CZ_MA_DELIVERY
							(
								  CD_COMPANY
								, CD_PARTNER
								, LN_PARTNER
								, EN_PARTNER
								, NO_POST
								, DC_ADDRESS
								, DC_ADDRESS1
								, NO_TEL
								, NM_PIC 
								, NO_TEL1
								, NO_TEL2
								, NO_EMAIL
								, YN_USE
								, DC_RMK
								, ID_INSERT
								, DTS_INSERT
							)
							SELECT 
								  '{2}'
								, '{3}'
								, LN_PARTNER
								, EN_PARTNER
								, NO_POST
								, DC_ADDRESS
								, DC_ADDRESS1
								, NO_TEL
								, NM_PIC
								, NO_TEL1
								, NO_TEL2
								, NO_EMAIL
								, YN_USE
								, DC_RMK
								, ID_INSERT
								, DTS_INSERT
							FROM NEOE.CZ_MA_DELIVERY
							WHERE CD_COMPANY = '{0}'
							AND CD_PARTNER = '{1}'

							  COMMIT";

				query = string.Format(query, cdCompany, cdPartner, cdCompany2, cdPartner2);

				SqlConnection sqlConn = new SqlConnection(connectStr);

				using (SqlConnection conn = new SqlConnection())
				{
					conn.ConnectionString = connectStr;
					conn.Open();

					SqlCommand sqlComm = new SqlCommand(query, conn);
					sqlComm.ExecuteNonQuery();
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("동기화"));

				flex.AcceptChanges();

				OnToolBarSearchButtonClicked(null, null);

			}
		}

		#region 버튼
		private void Btn승인_Click(object sender, EventArgs e)
		{
			string contents;
			int index;
			DataRow[] dataRowArray;

			try
			{
				if (this.flex.HasNormalRow == false) return;
				if (this.ShowMessage("승인 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

				dataRowArray = this.flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					index = 0;
					foreach (DataRow dr in dataRowArray)
					{
						MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

						bool result = _biz.Confirm(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["CD_PARTNER"]), "C");

						contents = @"** 납품처등록 알림

신청번호 : {0}
거래처명 : {1}

위와 같이 납품처 등록이 완료 되었습니다.


※ 본 쪽지는 발신 전용 입니다.";

						contents = string.Format(contents, D.GetString(dr["CD_PARTNER"]),
														   D.GetString(dr["LN_PARTNER"]));

						if (D.GetString(dr["CD_COMPANY"]) != "W100")
							Messenger.SendMSG(new string[] { D.GetString(dr["ID_INSERT"]) }, contents);


						// 동기화
						//Sync(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["CD_PARTNER"]));
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn승인.Text);
				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		private void Btn반려_Click(object sender, EventArgs e) 
		{
			string contents;
			int index;
			DataRow[] dataRowArray;

			try
			{
				if (this.flex.HasNormalRow == false) return;
				if (this.ShowMessage("반려 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

				dataRowArray = this.flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					index = 0;
					foreach (DataRow dr in dataRowArray)
					{
						MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

						_biz.Confirm(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["CD_PARTNER"]), "R");

						contents = @"** 납품처등록 반려 알림

신청번호 : {0}
거래처명 : {1}


납품처 등록이 반려 되었습니다.


※ 본 쪽지는 발신 전용 입니다.";

						contents = string.Format(contents, D.GetString(dr["CD_PARTNER"]),
														   D.GetString(dr["LN_PARTNER"]));

						if (D.GetString(dr["CD_COMPANY"]) != "W100")
							Messenger.SendMSG(new string[] { D.GetString(dr["ID_INSERT"]) }, contents);
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn반려.Text);
				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		private void Btn본사주소_Click(object sender, EventArgs e)
		{
			try
			{
				object obj1 = LoadHelpWindow("P_MA_POST", new object[] { MainFrameInterface,
																			   string.Empty });

				if (((Form)obj1).ShowDialog() == DialogResult.OK && obj1 is IHelpWindow)
				{
					meb본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[0]) + D.GetString(((IHelpWindow)obj1).ReturnValues[1]);
					txt본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[2]).Trim();
					tbx본사주소상세.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[3]);
					tbx본사주소상세.Focus();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion 버튼
	}
}
