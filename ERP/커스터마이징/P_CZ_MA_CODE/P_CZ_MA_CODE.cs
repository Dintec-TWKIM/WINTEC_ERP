using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using P_CZ_MA_CODE;
using System.Data.SqlClient;

namespace cz
{
	public partial class P_CZ_MA_CODE : PageBase
	{
		#region 생성자 및 초기화
		P_CZ_MA_CODE_BIZ _biz = new P_CZ_MA_CODE_BIZ();

		public string CD_COMPANY { get; set; }
		public string CD_FIELD { get; set; }
		#endregion 생성자 및 초기화

		public P_CZ_MA_CODE()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}


		private void InitControl()
		{
			this.ctx모듈.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			flexGridH.AfterRowChange += new RangeEventHandler(flexGridH_AfterRowChange);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);
			btn코드동기화.Click += new EventHandler(btn코드동기화_Click);
			btnSync.Click += new EventHandler(btnSync_Click);

			MainGrids = new FlexGrid[] { flexGridH, flexGridL1, flexGridL2 };
			flexGridH.DetailGrids = new FlexGrid[] { flexGridL1, flexGridL2 };

			pnl시스템.Visible = true;
			splitContainer3.SplitterDistance = 305;

			tbx_row_1.Visible = false;
			tbx_row_2.Visible = false;
			tbx_row_3.Visible = false;
			tbx_row_4.Visible = false;
			tbx_row_5.Visible = false;
			tbx_row_6.Visible = false;
			tbx_row_7.Visible = false;
			tbx_row_8.Visible = false;
			tbx_row_9.Visible = false;
			tbx_row_10.Visible = false;
		}



		private void InitGrid()
		{
			DataTable dtYN = new DataTable();
			dtYN.Columns.Add("CODE");
			dtYN.Columns.Add("NAME");
			dtYN.Rows.Add("Y", DD("사용"));
			dtYN.Rows.Add("N", DD("미사용"));

			Util.SetDB_CODE(cbxUse, dtYN, true);

			#region flexGridH
			flexGridH.BeginSetting(1, 1, true);

			flexGridH.SetCol("CD_FIELD", "구분코드", 120);
			flexGridH.SetCol("NM_FIELD", "구분코드명", 200);
			flexGridH.SetCol("NM_FIELD_E", "구분코드명(EN)", false);
			flexGridH.SetCol("FG1_SYSCODE", "SYS코드여부", 80);

			flexGridH.Cols["FG1_SYSCODE"].TextAlign = TextAlignEnum.CenterCenter;

			flexGridH.SettingVersion = "1.1.1.12";
			flexGridH.SetAlternateRow();
			flexGridH.SetMalgunGothic();
			flexGridH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexGridH


			#region flexGridL1
			flexGridL1.BeginSetting(1, 1, true);

			flexGridL1.SetCol("CD_SYSDEF", "구분코드", 100);
			flexGridL1.SetCol("NM_SYSDEF", "구분코드명", 200);
			flexGridL1.SetCol("NM_SYSDEF_L1", "구분코드명(EN)", 100);
			flexGridL1.SetCol("NM_SYSDEF_L2", "구분코드명(CH)", false);
			flexGridL1.SetCol("NM_SYSDEF_L3", "구분코드명(JP)", false);
			flexGridL1.SetCol("USE_YN", "사용여부", 100);
			flexGridL1.SetCol("CD_FLAG1", "관련1", 200);
			flexGridL1.SetCol("CD_FLAG2", "관련2", 200);
			flexGridL1.SetCol("CD_FLAG3", "관련3", 200);
			flexGridL1.SetCol("NM_SYSDEF_CH", "", false);
			flexGridL1.SetCol("NM_SYSDEF_JP", "", false);
			flexGridL1.SetCol("NM_SYSDEF_L4", "", false);
			flexGridL1.SetCol("NM_SYSDEF_L5", "", false);
			flexGridL1.SetCol("NM_USERDEF_L1", "", false);
			flexGridL1.SetCol("NM_USERDEF_L2", "", false);

			flexGridL1.SetDataMap("USE_YN", dtYN, "CODE", "NAME");

			flexGridL1.Cols["CD_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;
			flexGridL1.Cols["NM_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;
			flexGridL1.Cols["USE_YN"].TextAlign = TextAlignEnum.CenterCenter;

			flexGridL1.SettingVersion = "1.1.1.15";
			flexGridL1.SetAlternateRow();
			flexGridL1.SetMalgunGothic();
			flexGridL1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexGridL1


			#region flexxGridL2


			flexGridL2.BeginSetting(1, 1, true);

			flexGridL2.SetCol("CD_COMPANY", "회사", false);
			flexGridL2.SetCol("CD_FIELD", "구분코드", false);
			flexGridL2.SetCol("FG1_SYSCODE", "SYS코드여부", false);
			flexGridL2.SetCol("CD_SYSDEF", "구분코드", 100);
			flexGridL2.SetCol("NM_SYSDEF", "구분코드명", 200);
			flexGridL2.SetCol("NM_SYSDEF_L1", "구분코드명(EN)", false);
			flexGridL2.SetCol("NM_SYSDEF_L2", "구분코드명(CH)", false);
			flexGridL2.SetCol("NM_SYSDEF_L3", "구분코드명(JP)", false);
			flexGridL2.SetCol("USE_YN", "사용여부", 100);
			flexGridL2.SetCol("CD_FLAG1", "관련1", 250);
			flexGridL2.SetCol("CD_FLAG2", "관련2", 250);
			flexGridL2.SetCol("CD_FLAG3", "관련3", 250);
			flexGridL2.SetCol("CD_FLAG4", "관련4", 250);
			flexGridL2.SetCol("CD_FLAG5", "관련5", 250);
			flexGridL2.SetCol("CD_FLAG6", "관련6", 250);
			flexGridL2.SetCol("CD_FLAG7", "관련7", 250);
			flexGridL2.SetCol("CD_FLAG8", "관련8", 250);
			flexGridL2.SetCol("CD_FLAG9", "관련9", 250);
			flexGridL2.SetCol("CD_FLAG10", "관련10", 250);
			//flexGridL2.SetCol("NM_SYSDEF_E", "구분코드명(EN)", 100);
			//flexGridL2.SetCol("NM_SYSDEF_CH", "구분코드명(CH)", false);
			//flexGridL2.SetCol("NM_SYSDEF_JP", "구분코드명(JP)", false);
			flexGridL2.SetCol("NM_SYSDEF_L4", "", false);
			flexGridL2.SetCol("NM_SYSDEF_L5", "", false);
			flexGridL2.SetCol("NM_USERDEF_L1", "", false);
			flexGridL2.SetCol("NM_USERDEF_L2", "", false);
			flexGridL2.SetCol("DC_RMK", "비고", 300);


			flexGridL2.SetDataMap("USE_YN", dtYN, "CODE", "NAME");
			flexGridL2.SetOneGridBinding(new object[] { }, oneL);

			flexGridL2.Cols["CD_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;
			flexGridL2.Cols["NM_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;
			flexGridL2.Cols["USE_YN"].TextAlign = TextAlignEnum.CenterCenter;


			//flexGridL2.SettingVersion = "1.1.1.02";
			flexGridL2.SetDefault("19.07.15.03", SumPositionEnum.None);
			flexGridL2.SetAlternateRow();
			flexGridL2.SetMalgunGothic();
			//flexGridL2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexxGridL2

		}

		private void InitEvent()
		{

		}


		#region 조회
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		 {
			try
			{
				DataTable dt = null;

				if (chk전용.Checked)
				{
					dt = DBHelper.GetDataTable("P_CZ_MA_CODE_SELECT", new object[] { CD_COMPANY, ctx모듈.CodeValue, tbx검색.Text, "" });
				}
				else
				{
					dt = DBHelper.GetDataTable("UP_MA_CODE_SELECT", new object[] { CD_COMPANY, ctx모듈.CodeValue, tbx검색.Text, "" });
				}

				flexGridH.Redraw = false;

				flexGridH.Binding = dt;

				flexGridH.Redraw = true;

				if (!flexGridH.HasNormalRow)
				{
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		private void flexGridH_AfterRowChange(object sender, RangeEventArgs e)
		{
			if (!string.IsNullOrEmpty(flexGridH["CD_FIELD"].ToString()))
				CD_FIELD = flexGridH["CD_FIELD"].ToString();

			DataTable dtL = null;
			DataTable dtL2 = null;

			DataRow[] arrRows = null;

			if (chk전용.Checked)
			{
				dtL = DBMgr.GetDataTable("UP_MA_CODE_SELECT1", new object[] { CD_COMPANY, CD_FIELD });
				dtL2 = DBMgr.GetDataTable("P_CZ_MA_CODE_SELECT1_USERD", new object[] { CD_COMPANY, CD_FIELD });

				if (!string.IsNullOrEmpty(tbx_row_1.Text))
				{
					arrRows = dtL2.Select("CD_FLAG1='" + tbx_row_1.Text + "'");

					if(arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_2.Text))
				{
					arrRows = dtL2.Select("CD_FLAG2='" + tbx_row_2.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_3.Text))
				{
					arrRows = dtL2.Select("CD_FLAG3='" + tbx_row_3.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_4.Text))
				{
					arrRows = dtL2.Select("CD_FLAG4='" + tbx_row_4.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_5.Text))
				{
					arrRows = dtL2.Select("CD_FLAG5='" + tbx_row_5.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_6.Text))
				{
					arrRows = dtL2.Select("CD_FLAG6='" + tbx_row_6.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_7.Text))
				{
					arrRows = dtL2.Select("CD_FLAG7='" + tbx_row_7.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_8.Text))
				{
					arrRows = dtL2.Select("CD_FLAG8='" + tbx_row_8.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_9.Text))
				{
					arrRows = dtL2.Select("CD_FLAG9='" + tbx_row_9.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}

				if (!string.IsNullOrEmpty(tbx_row_10.Text))
				{
					arrRows = dtL2.Select("CD_FLAG10='" + tbx_row_10.Text + "'");

					if (arrRows.Length > 0)
						dtL2 = arrRows.CopyToDataTable();
				}


			}
			else
			{
				dtL = DBMgr.GetDataTable("UP_MA_CODE_SELECT1", new object[] { CD_COMPANY, CD_FIELD });
				dtL2 = DBMgr.GetDataTable("P_CZ_MA_CODE_SELECT1_USER", new object[] { CD_COMPANY, CD_FIELD });

				arrRows = dtL2.Select("CD_FLAG1='" + tbx_row_1.Text + "'");
				dtL2 = arrRows.CopyToDataTable();
			}

			flexGridL1.Binding = dtL;
			flexGridL2.Binding = dtL2;
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
				flexGridH.Rows.Add();
				flexGridH.Row = flexGridH.Rows.Count - 1;

				flexGridH.AddFinished();

				flexGridH.AllowEditing = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				//DataTable dtL = flexGridL2.DataTable;

				flexGridL2.Rows.Add();
				flexGridL2.Row = flexGridL2.Rows.Count - 1;
				flexGridL2["USE_YN"] = "Y";
				flexGridL2["CD_FIELD"] = flexGridH["CD_FIELD"].ToString();

				if (!chk전용.Checked)
					flexGridL2["FG1_SYSCODE"] = "N";

				//flexGridL2.Rows[flexGridL2.Rows.Count - 1]["CD_FIELD"] = flexGridH["CD_FIELD"].ToString();
				//flexGridL2.Rows[flexGridL2.Rows.Count - 1]["FG1_SYSCODE"] = flexGridH["FG1_SYSCODE"].ToString();

				flexGridL2.AddFinished();

				flexGridL2.AllowEditing = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
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
			if (chk전용.Checked)
			{
				if (!string.IsNullOrEmpty(flexGridH["CD_FIELD"].ToString()))
					flexGridL2["CD_FIELD"] = flexGridH["CD_FIELD"].ToString();

				if (!base.SaveData() || !this.Verify()) return false;

				DataTable dtH = this.flexGridH.GetChanges();
				DataTable dtL = this.flexGridL2.GetChanges();

				string xmlH = Util.GetTO_Xml(dtH);
				string xmlL = Util.GetTO_Xml(dtL);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_CODE_XML", new object[] { xmlH, xmlL });
			}
			else
			{
				//flexGridL1["NM_SYSDEF_CH"] = "";
				//flexGridL1["NM_SYSDEF_JP"] = "";

				DataTable dtL1 = flexGridL1.GetChanges();
				_biz.SaveL1(dtL1);
				flexGridL1.AcceptChanges();


				DataTable dt = flexGridL2.GetChanges();
				_biz.Save(dt);
				flexGridL2.AcceptChanges();
			}

			flexGridH.AcceptChanges();
			flexGridL2.AcceptChanges();

			OnToolBarSearchButtonClicked(null, null);

			return true;
		}

		#endregion 저장

		#region  삭제

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexGridH.HasNormalRow) return;
				this.flexGridH.Rows.Remove(flexGridH.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		private void btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexGridL2.HasNormalRow) return;
				this.flexGridL2.Rows.Remove(flexGridL2.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#endregion 삭제

		#region 기타
		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000039";
		}


		private void btn코드동기화_Click(object sender, EventArgs e)
		{
			string query;
			if (!CD_COMPANY.Equals("W100"))
			{
				try
				{
					if (string.IsNullOrEmpty(CD_FIELD)) return;

					if (chk전용.Checked)
					{
						query = @"BEGIN TRAN

						  DELETE FROM CZ_MA_CODE
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'
						  
						  DELETE FROM CZ_MA_CODEDTL
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'

						  INSERT INTO CZ_MA_CODE
						  (
							CD_FIELD,
							CD_COMPANY,
							NM_FIELD,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 MC.CD_COMPANY,
								 CD.NM_FIELD,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM CZ_MA_CODE CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'

						  INSERT INTO CZ_MA_CODEDTL
						  (
							CD_FIELD,
							CD_SYSDEF,
							CD_COMPANY,
							NM_SYSDEF,
							YN_USE,
							CD_FLAG1,
							CD_FLAG2,
							CD_FLAG3,
                            CD_FLAG4,
                            CD_FLAG5,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 CD.CD_SYSDEF,
								 MC.CD_COMPANY,
								 CD.NM_SYSDEF,
								 CD.YN_USE,
								 CD.CD_FLAG1,
								 CD.CD_FLAG2,
								 CD.CD_FLAG3,
                                 CD.CD_FLAG4,
                                 CD.CD_FLAG5,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM CZ_MA_CODEDTL CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'
						  
						  COMMIT";
					}
					else
					{
						query = @"BEGIN TRAN

						  DELETE FROM MA_CODE
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'
						  
						  DELETE FROM MA_CODEDTL
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'

						  INSERT INTO MA_CODE
						  (
							CD_FIELD,
							CD_COMPANY,
							NM_FIELD,
							FG1_SYSCODE,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 MC.CD_COMPANY,
								 CD.NM_FIELD,
								 CD.FG1_SYSCODE,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM MA_CODE CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'

						  INSERT INTO MA_CODEDTL
						  (
							CD_FIELD,
							CD_SYSDEF,
							CD_COMPANY,
							FG1_SYSCODE,
							NM_SYSDEF,
							USE_YN,
							CD_FLAG1,
							CD_FLAG2,
							CD_FLAG3,
							NM_SYSDEF_E,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 CD.CD_SYSDEF,
								 MC.CD_COMPANY,
								 CD.FG1_SYSCODE,
								 CD.NM_SYSDEF,
								 CD.USE_YN,
								 CD.CD_FLAG1,
								 CD.CD_FLAG2,
								 CD.CD_FLAG3,
								 CD.NM_SYSDEF_E,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM MA_CODEDTL CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'
						  
						  COMMIT";
					}

					query = string.Format(query, CD_FIELD, Global.MainFrame.LoginInfo.CompanyCode);

					DBHelper.ExecuteScalar(query);

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("동기화"));
				}
				catch (Exception ex)
				{
					this.MsgEnd(ex);
				}
			}
		}

		// 윈텍 동기화 버튼
		private void btnSync_Click(object sender, EventArgs e)
		{
			DataTable dt = null;
			System.Data.DataSet ds = new System.Data.DataSet();
			string connectStr = "Server=113.130.254.143; Database=NEOE; Uid=sa; Password=skm0828!";
			string query = string.Empty;

			query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE FROM MES.NEOE.NEOE.CZ_MA_CODEDTL

INSERT INTO MES.NEOE.NEOE.CZ_MA_CODEDTL
(CD_COMPANY, CD_FIELD, CD_SYSDEF, NM_SYSDEF, NM_SYSDEF_E, CD_FLAG1, CD_FLAG2, CD_FLAG3, CD_FLAG4, CD_FLAG5, CD_FLAG6, CD_FLAG7, CD_FLAG8, YN_USE, DC_RMK, ID_INSERT, DTS_INSERT, ID_UPDATE, DTS_UPDATE)
SELECT CD_COMPANY, CD_FIELD, CD_SYSDEF, NM_SYSDEF, NM_SYSDEF_E, CD_FLAG1, CD_FLAG2, CD_FLAG3, CD_FLAG4, CD_FLAG5, CD_FLAG6, CD_FLAG7, CD_FLAG8,CD_FLAG9,CD_FLAG10, YN_USE, DC_RMK, ID_INSERT, DTS_INSERT, ID_UPDATE, DTS_UPDATE
FROM NEOE.NEOE.CZ_MA_CODEDTL
WHERE CD_COMPANY = 'W100'";

			SqlConnection sqlConn = new SqlConnection(connectStr);

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = connectStr;
				conn.Open();

				SqlCommand sqlComm = new SqlCommand(query, conn);
				sqlComm.ExecuteNonQuery();
			}

			this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("동기화"));
		}
		#endregion 기타


		private void flexGridL2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();
				int index = flexGridL2.Row;  // 시작인덱스 저장 (행이 클립보다 많은 경우는 .Row가 안바뀌지만 클립보드가 더 많은 경우에는 .Row가 바뀌므로 미리 저장함)

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = index + i;
					int j = 0;

					for (int col = flexGridL2.Col; col < flexGridL2.Cols.Count; col++)
					{
						// 클립보드 넘어가는 순간 제외
						if (j == clipboard.GetLength(1))
							break;

						// 비허용 컬럼
						if (!flexGridL2.Cols[col].Visible || !flexGridL2.Cols[col].AllowEditing)
							continue;

						flexGridL2[row, col] = clipboard[i, j];
						j++;
					}

					// 마지막 행이면 종료
					if (i == clipboard.GetLength(0) - 1)
						break;

					// 클립보드는 아직 남았는데 그리드의 마지막 행인 경우 행 추가
					if (row == flexGridL2.Rows.Count - 1)
					{
						// 행 추가
						flexGridL2.Rows.Add();
						//flexL.Row = flexL.Rows.Count - 1;

						flexGridL2["USE_YN"] = "Y";
						flexGridL2["CD_FIELD"] = flexGridH["CD_FIELD"].ToString();

						if (!chk전용.Checked)
							flexGridL2["FG1_SYSCODE"] = "N";
					}
				}

				flexGridL2.AddFinished();
			}
		}

		private void chk전용_CheckedChanged(object sender, EventArgs e)
		{
			if(chk전용.Checked)
			{
				pnl시스템.Visible = false;
				splitContainer3.SplitterDistance = 0;

				tbx_row_1.Visible = true;
				tbx_row_2.Visible = true;
				tbx_row_3.Visible = true;
				tbx_row_4.Visible = true;
				tbx_row_5.Visible = true;
				tbx_row_6.Visible = true;
				tbx_row_7.Visible = true;
				tbx_row_8.Visible = true;
				tbx_row_9.Visible = true;
				tbx_row_10.Visible = true;
			}
			else
			{
				pnl시스템.Visible = true;
				splitContainer3.SplitterDistance = 305;

				tbx_row_1.Visible = false;
				tbx_row_2.Visible = false;
				tbx_row_3.Visible = false;
				tbx_row_4.Visible = false;
				tbx_row_5.Visible = false;
				tbx_row_6.Visible = false;
				tbx_row_7.Visible = false;
				tbx_row_8.Visible = false;
				tbx_row_9.Visible = false;
				tbx_row_10.Visible = false;
			}
		}
	}
}



