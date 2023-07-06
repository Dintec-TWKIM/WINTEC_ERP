using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using DX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_LOG_QTN_REG : PageBase
	{
		DHL_xml dhlxml = new DHL_xml();

		public P_CZ_SA_LOG_QTN_REG()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			this.페이지초기화();

			ctx담당자.CodeValue = 상수.사원번호;
			ctx담당자.CodeName = 상수.사원이름;

			ctx담당자검색.CodeValue = 상수.사원번호;
			ctx담당자검색.CodeName = 상수.사원이름;

			tbx전송상태.ReadOnly = true;
			tbx견적번호.ReadOnly = true;

			cbo문의종류.DataBind(GetDb.Code("CZ_SA00063"), true);
			cbo수신처.DataBind(GetDb.Code("CZ_SA00064"), true);

			InitGrid();
			InitEvent();
		}

		#region 〓〓〓〓〓〓〓〓〓〓 세팅 〓〓〓〓〓〓〓〓〓〓

		protected override void InitPaint()
		{
			spc메인.SplitterDistance = spc메인.Width - 900;
		}

		private void InitGrid()
		{
			DataTable dtYN2 = new DataTable();
			dtYN2.Columns.Add("CODE");
			dtYN2.Columns.Add("NAME");
			dtYN2.Rows.Add("Y", DD("포함"));
			dtYN2.Rows.Add("N", DD("제외"));

			MainGrids = this.컨트롤<FlexGrid>();

			// 헤드
			grd헤드.세팅시작(2);
			grd헤드.컬럼세팅("NO_KEY", "견적번호", 90, 정렬.가운데);
			grd헤드.컬럼세팅("DT_SEND", "제출일자", 100, 포맷.날짜);
			grd헤드.컬럼세팅("NM_EMP", "담당자", 80, 정렬.가운데);
			grd헤드.컬럼세팅("CD_FORWARDER", "문의종류", 90, 정렬.가운데);

			grd헤드.컬럼세팅("CD_PARTNER", "수신", "수신처", 180, 정렬.왼쪽);
			grd헤드.컬럼세팅("MAIL_LIST", "수신", "메일", 200, 정렬.왼쪽);
			grd헤드.컬럼세팅("MAIL_STATUS_NM", "상태", 100, 정렬.가운데);

			grd헤드.컬럼세팅("PORT", "회신", "Port", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("CARGO_READY", "회신", "C/Ready", 80, 정렬.가운데);
			grd헤드.컬럼세팅("R_AM_EX", "회신", "외화", 100, 포맷.외화단가);
			grd헤드.컬럼세팅("R_AM_KR", "회신", "원화", 100, 포맷.원화단가);

			grd헤드.컬럼세팅("DHL_ZONE", "DHL", "ZONE", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_WEIGHT", "DHL", "무게", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_VWEIGHT", "DHL", "V/W", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_WIDTH", "DHL", "가로", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_LENGTH", "DHL", "길이", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_HEIGHT", "DHL", "높이", 100, 정렬.왼쪽);
			grd헤드.컬럼세팅("DHL_AM_KR", "DHL", "운임", 100, 정렬.왼쪽);

			grd헤드.컬럼세팅("R_DC_RMK", "비고", false);
			grd헤드.컬럼세팅("DHL_ZONE_CODE", "ZONE CODE", false);
			grd헤드.컬럼세팅("MAIL_STATUS", "상태코드", false);
			grd헤드.컬럼세팅("YN_MSDS", "MSDS", false);
			grd헤드.컬럼세팅("CD_COMPANY", "회사", false);
			grd헤드.컬럼세팅("ID_INSERT", "등록자", false);
			grd헤드.컬럼세팅("NO_EMP", "담당자코드", false);
			grd헤드.컬럼세팅("FILE_PATH_CODE", "첨부파일코드", false);
			grd헤드.컬럼세팅("CD_FILE_KIND", "파일종류", false);

			grd헤드.데이터맵("YN_MSDS", dtYN2);
			grd헤드.데이터맵("CD_PARTNER", 코드.코드관리("CZ_SA00064"));
			grd헤드.데이터맵("CD_FORWARDER", 코드.코드관리("CZ_SA00063"));

			grd헤드.에디트컬럼("CD_FORWARDER", "CD_PARTNER", "MAIL_LIST");

			grd헤드.기본키("NO_KEY", "CD_COMPANY");
			grd헤드.패널바인딩(lay헤드);
			grd헤드.세팅종료("23.02.09.04", false);

			// 라인
			grd라인.세팅시작(2);

			grd라인.컬럼세팅("NO_FILE", "파일번호", 120, 정렬.가운데);
			grd라인.컬럼세팅("NM_VESSEL", "선명", 150, 정렬.가운데);
			grd라인.컬럼세팅("PORT_LOADING", "출발지", 120, 정렬.가운데);
			grd라인.컬럼세팅("PORT_ARRIVER", "도착지", 120, 정렬.가운데);
			grd라인.컬럼세팅("DC_RMK_SIZE", "결과비고", 400, 정렬.가운데);
			grd라인.컬럼세팅("NM_KOR", "담당자", 80, 정렬.가운데);
			grd라인.컬럼세팅("DC_RMK_TEXT2", "물류비고", 400, 정렬.가운데);
			grd라인.컬럼세팅("WEIGHT", "무게", 80, 포맷.소수);
			grd라인.컬럼세팅("YN_PACK", "포장여부", 80, 정렬.가운데);

			grd라인.컬럼세팅("MAIL_BODY", "본문", false);
			grd라인.컬럼세팅("MAIL_SUBJECT", "제목", false);
			grd라인.컬럼세팅("NO_KEY", "견적번호", false);
			grd라인.컬럼세팅("CD_COMPANY", "회사", false);
			grd라인.컬럼세팅("NO_EMP", "담당자", false);

			grd라인.기본키("NO_KEY", "CD_COMPANY");
			grd라인.세팅종료("23.02.09.01", false);

			grd라인.에디트컬럼("NO_FILE", "DC_RMK_TEXT2", "PORT_LOADING", "PORT_ARRIVER", "DC_RMK_SIZE");
			grd라인.복사붙여넣기(Grd라인_AfterEdit);

			// 아이템
			grd아이템.세팅시작(1);
			grd아이템.컬럼세팅("NO_FILE", "파일번호", false);
			grd아이템.컬럼세팅("NO_LINE", "순번", 80, 정렬.가운데);
			grd아이템.컬럼세팅("NM_ITEM", "품목명", 200, 정렬.가운데);
			grd아이템.컬럼세팅("CD_ITEM", "품목코드", 100, 정렬.가운데);
			grd아이템.컬럼세팅("QT", "수량", 70, 포맷.수량);
			grd아이템.컬럼세팅("UNIT", "단위", 70, 정렬.가운데);
			grd아이템.컬럼세팅("WEIGHT", "무게", 100, 포맷.소수);
			//grd아이템.컬럼세팅("YN_PACK", "포장여부", 100, 정렬.가운데);

			grd아이템.기본키("NO_FILE");
			grd아이템.세팅종료("23.01.17.01", false);
		}

		private void InitEvent()
		{
			btn첨부파일.Click += Btn첨부파일_Click;
			btn메일발송.Click += Btn메일발송_Click;
			btn추가.Click += Btn추가_Click;
			btn삭제.Click += Btn삭제_Click;
			btn메일생성.Click += Btn메일생성_Click; ;
			btn확정.Click += Btn확정_Click;
			btn계산.Click += Btn계산_Click;
			btn회신.Click += Btn회신_Click;
			btn회신메일첨부.Click += Btn회신메일첨부_Click;

			cbo문의종류.SelectedValueChanged += Cbo문의종류_SelectedValueChanged;
			cbo수신처.SelectedValueChanged += Cbo수신처_SelectedValueChanged;

			tbxDHL총운임.TextChanged += TbxDHL총운임_TextChanged;

			grd라인.AfterEdit += Grd라인_AfterEdit;
			grd라인.AfterRowChange += Grd라인_AfterRowChange;
			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;

			tbx제목.TextChanged += Tbx제목_TextChanged;
			tbx본문.TextChanged += Tbx본문_TextChanged;
			tbx첨부파일.TextChanged += Tbx첨부파일_TextChanged;
			dtp출고가능일.CalendarDateChanged += Dtp출고가능일_CalendarDateChanged;

			ctxDHLZONE.QueryBefore += CtxDHLZONE_QueryBefore;
		}

		private void Btn회신메일첨부_Click(object sender, EventArgs e)
		{
			string fileName = string.Empty;

			try
			{
				if (!this.grd헤드.HasNormalRow)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					if (grd헤드["NO_KEY"].ToString().Equals("NEW"))
					{
						유틸.메세지("저장 후 첨부파일을 등록해주세요.");
					}
					else
					{
						string nokeyStr = grd헤드["NO_KEY"].ToString();
						string companyStr = grd헤드["CD_COMPANY"].ToString();
						string noempStr = grd헤드["NO_EMP"].ToString();

						string file_code = companyStr + "_" + noempStr + "_" + nokeyStr + "2";
						P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(상수.회사코드, "CZ", "P_CZ_SA_LOG_QTN_REG2", file_code, "P_CZ_SA_LOG_QTN_REG2");
						m_dlg.ShowDialog(this);

						fileName = SearchFileInfo(상수.회사코드, file_code, "P_CZ_SA_LOG_QTN_REG2");

						if (!string.IsNullOrEmpty(fileName))
						{
							tbx회신메일.Text = fileName;

							//string query = "UPDATE CZ_SA_LOG_QTNH SET FILE_PATH_CODE = '" + fileName + "' WHERE CD_COMPANY = '" + companyStr + "' AND NO_KEY = '" + nokeyStr + "'";
							//TSQL.실행(query);
						}
						else
						{
							tbx회신메일.Text = "";
							//string query = "UPDATE CZ_SA_LOG_QTNH SET FILE_PATH_CODE = '' WHERE CD_COMPANY = '" + companyStr + "' AND NO_KEY = '" + nokeyStr + "'";
							//TSQL.실행(query);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Tbx첨부파일_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbx첨부파일.Text))
				btn회신.Enabled = false;
			else
				btn회신.Enabled = true;
		}

		private void Btn회신_Click(object sender, EventArgs e)
		{
			try
			{
				string 회사코드 = 상수.회사코드;
				string 견적번호 = tbx견적번호.Text;
				string 문의종류 = cbo문의종류.Text;
				// RPA QUEUE 입력
				InsertQue(회사코드, 견적번호, 문의종류, "FW_REPLY", "8");

				string query = @"UPDATE CZ_SA_LOG_QTNH SET MAIL_STATUS = 'R' WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_KEY = '" + 견적번호 + "'";
				TSQL.실행(query);

				유틸.메세지("회신 요청을 완료하였습니다.");

				OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 세팅 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 버튼 〓〓〓〓〓〓〓〓〓〓

		private void Btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (grd라인.Rows.Count <= 2)
				{
					if (string.IsNullOrEmpty(grd헤드["NO_KEY"].ToString()) || grd헤드["NO_KEY"].ToString().Equals("NEW"))
					{
						유틸.메세지("저장 후 아이템 추가 가능합니다.");
					}
					else
					{
						grd라인.행추가();
						grd라인["CD_COMPANY"] = 상수.회사코드;
						grd라인["NO_KEY"] = grd헤드["NO_KEY"];
						grd라인.행추가완료();
					}
				}
				else
				{
					유틸.메세지("복수 문의 불가합니다.");
				}
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.grd라인.HasNormalRow) return;
				this.grd라인.Rows.Remove(grd라인.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn확정_Click(object sender, EventArgs e)
		{
			DataTable dt = grd라인.GetChanges();

			if (dt != null)
			{
				메일내용(dt);
			}
			string 키값 = grd라인["NO_KEY"].ToString();

			string xml = Util.GetTO_Xml(dt);
			TSQL.실행("PS_CZ_SA_LOG_QTNL_XML", new object[] { xml });

			string query = @"
UPDATE CZ_SA_LOG_QTNL
	SET YN_CHECK = 'Y'
WHERE 1=1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_KEY = @NO_KEY

UPDATE CZ_SA_LOG_QTNL
	SET MAIL_BODY = REPLACE(MAIL_BODY, CHAR(10), CHAR(13) + CHAR(10))
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_KEY = @NO_KEY
	AND MAIL_BODY LIKE '%' + CHAR(10) + '%'
	AND MAIL_BODY NOT LIKE '%' + CHAR(13) + CHAR(10) + '%'

UPDATE CZ_SA_LOG_QTNL
	SET MAIL_BODY = REPLACE(MAIL_BODY, CHAR(10), CHAR(13) + CHAR(10))
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_KEY = @NO_KEY
	AND MAIL_BODY LIKE '%' + CHAR(10) + '%'
	AND MAIL_BODY NOT LIKE '%' + CHAR(13) + CHAR(10) + '%'";

			DBMgr dbm = new DBMgr();
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", 상수.회사코드);
			dbm.AddParameter("@NO_KEY", grd라인["NO_KEY"].ToString());
			dbm.ExecuteNonQuery();

			grd라인.AcceptChanges();

			유틸.메세지("확정 되었습니다.");

			tbx견적번호검색.Text = 키값;

			OnToolBarSearchButtonClicked(null, null);
		}

		private void Btn메일생성_Click(object sender, EventArgs e)
		{
			if (grd헤드["MAIL_STATUS"].ToString().Equals("N"))
			{
				if (grd라인.Rows.Count > 2) // 헤드부분 포함하여 3번째 ROW가 첫번째임
				{
					메일내용_grd();
				}
				else
				{
					유틸.메세지("파일번호 입력 후 메일 작성 가능합니다.");
				}
			}
			else
			{
				유틸.메세지("메일 발송 이력이 조회되었습니다.\r\n항목 추가 바랍니다.");
			}
		}

		private void Btn메일발송_Click(object sender, EventArgs e)
		{
			if (grd헤드["MAIL_STATUS"].ToString().Equals("N"))
			{
				if (grd라인.Rows.Count > 2)
				{
					string query = "SELECT * FROM CZ_SA_LOG_QTNL WHERE CD_COMPANY = '" + grd라인["CD_COMPANY"].ToString() + "' AND NO_KEY = '" + grd라인["NO_KEY"].ToString() + "' AND YN_CHECK = 'Y'";

					DataTable dt = TSQL.결과(query);

					if (dt.Rows.Count > 0)
					{
						SendMail(grd라인["CD_COMPANY"].ToString(), grd라인["NO_KEY"].ToString(), grd헤드["NO_EMP"].ToString(), false);
					}
					else
					{
						유틸.메세지("확정 후 메일 발송 가능합니다.");
					}
				}
				else
				{
					유틸.메세지("메일 내용이 생성되지 않았습니다.");
				}
			}
			else
			{
				유틸.메세지("메일 발송 이력이 조회되었습니다.\r\n항목 추가 바랍니다.");
			}
		}

		private void Btn첨부파일_Click(object sender, EventArgs e)
		{
			string fileName = string.Empty;

			try
			{
				if (!this.grd헤드.HasNormalRow)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					if (grd헤드["NO_KEY"].ToString().Equals("NEW"))
					{
						유틸.메세지("저장 후 첨부파일을 등록해주세요.");
					}
					else
					{
						string nokeyStr = grd헤드["NO_KEY"].ToString();
						string companyStr = grd헤드["CD_COMPANY"].ToString();
						string noempStr = grd헤드["NO_EMP"].ToString();

						string file_code = companyStr + "_" + noempStr + "_" + nokeyStr;
						P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(상수.회사코드, "CZ", "P_CZ_SA_LOG_QTN_REG", file_code, "P_CZ_SA_LOG_QTN_REG");
						m_dlg.ShowDialog(this);

						fileName = SearchFileInfo(상수.회사코드, file_code, "P_CZ_SA_LOG_QTN_REG");

						if (!string.IsNullOrEmpty(fileName))
						{
							tbx첨부파일.Text = fileName;

							string query = "UPDATE CZ_SA_LOG_QTNH SET FILE_PATH_CODE = '" + fileName + "' WHERE CD_COMPANY = '" + companyStr + "' AND NO_KEY = '" + nokeyStr + "'";

							TSQL.실행(query);
						}
						else
						{
							tbx첨부파일.Text = "";

							string query = "UPDATE CZ_SA_LOG_QTNH SET FILE_PATH_CODE = '' WHERE CD_COMPANY = '" + companyStr + "' AND NO_KEY = '" + nokeyStr + "'";

							TSQL.실행(query);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn계산_Click(object sender, EventArgs e)
		{
			string vw가로 = tbxDHL가로.Text;
			string vw길이 = tbxDHL길이.Text;
			string vw높이 = tbxDHL높이.Text;

			double _vw무게 = 0;
			string vw무게 = string.Empty;

			if (!string.IsNullOrEmpty(vw가로) && !string.IsNullOrEmpty(vw길이) && !string.IsNullOrEmpty(vw높이))
			{
				_vw무게 = Math.Round(Convert.ToDouble(vw가로) * Convert.ToDouble(vw길이) * Convert.ToDouble(vw높이) / 5000);
				vw무게 = Convert.ToString(_vw무게);

				tbxvw무게.Text = vw무게;
			}

			string 입력지역명 = ctxDHLZONE.CodeName.ToString();
			string 입력지역코드 = ctxDHLZONE.CodeValue.ToString();
			string 입력무게 = tbxDHL무게.Text;
			string 도시 = tbxCity.Text;


			if (string.IsNullOrEmpty(입력지역명))
			{
				유틸.메세지("DHL ZONE을 선택해주세요.");
				return;
			}

			if (string.IsNullOrEmpty(입력무게) && string.IsNullOrEmpty(vw무게))
			{
				유틸.메세지("무게 or 사이즈를 입력해주세요.");
				return;
			}


			//string 지역 = DHL_ZONE(입력지역명);
			
			if(string.IsNullOrEmpty(도시))
				도시 = DHL_도시(입력지역코드);

			DataTable dttest = new DataTable();
			
			string getStr = DHL_xml.DHL_Capability(입력무게, vw높이, vw길이, vw가로, 입력지역코드, 도시,"");

			double _총운임 = Convert.ToDouble(getStr);

			tbxDHL총운임.Text = string.Format("{0:N0}", _총운임);
			


			#region 포워딩비용관리

			//			string query = string.Format(@"SELECT TOP 1
			//	   SL.WEIGHT,
			//	   PH.RT_FSC,
			//       ROUND(SL.AM_ZONE1 + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) AS AM_ZONE,
			//	   (CASE WHEN SL.WEIGHT > 30 THEN ROUND(SL.AM_ZONE{3} * 2.50, 0) ELSE SL.AM_ZONE1 END) AS AM_CHARGE,
			//       (CASE WHEN SL.WEIGHT > 30 THEN ROUND(ROUND(SL.AM_ZONE{3} + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) * {2}, 0)

			//								 ELSE ROUND(SL.AM_ZONE{3} + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) END) AS AM_FULL
			//FROM CZ_MA_TARIFF_DHL_PAY_H PH WITH(NOLOCK)
			//JOIN CZ_MA_TARIFF_DHL_STD_L SL WITH(NOLOCK) ON SL.CD_COMPANY = PH.CD_COMPANY AND SL.DT_YEAR = SUBSTRING(PH.DT_MONTH, 1, 4) AND SL.TP_TARIFF = '{4}'
			//WHERE PH.CD_COMPANY = '{0}'
			//AND PH.DT_MONTH = '{1}'
			//AND SL.WEIGHT <= {2}
			//ORDER BY SL.WEIGHT DESC", 상수.회사코드, DateTime.Now.ToString("yyyyMM"), 입력무게,지역, "001" );

			//			string query2 = string.Format(@"SELECT TOP 1
			//	   SL.WEIGHT,
			//	   PH.RT_FSC,
			//       ROUND(SL.AM_ZONE1 + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) AS AM_ZONE,
			//	   (CASE WHEN SL.WEIGHT > 30 THEN ROUND(SL.AM_ZONE{3} * 2.50, 0) ELSE SL.AM_ZONE1 END) AS AM_CHARGE,
			//       (CASE WHEN SL.WEIGHT > 30 THEN ROUND(ROUND(SL.AM_ZONE{3} + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) * {2}, 0)

			//								 ELSE ROUND(SL.AM_ZONE{3} + (SL.AM_ZONE{3} * (PH.RT_FSC / 100)), 0) END) AS AM_FULL
			//FROM CZ_MA_TARIFF_DHL_PAY_H PH WITH(NOLOCK)
			//JOIN CZ_MA_TARIFF_DHL_STD_L SL WITH(NOLOCK) ON SL.CD_COMPANY = PH.CD_COMPANY AND SL.DT_YEAR = SUBSTRING(PH.DT_MONTH, 1, 4) AND SL.TP_TARIFF = '{4}'
			//WHERE PH.CD_COMPANY = '{0}'
			//AND PH.DT_MONTH = '{1}'
			//AND SL.WEIGHT <= {2}
			//ORDER BY SL.WEIGHT DESC", 상수.회사코드, DateTime.Now.ToString("yyyyMM"), 입력무게, 지역, "002");

			//			DataTable dt = TSQL.결과(query);
			//			DataTable dt2 = TSQL.결과(query2);

			//double _총운임 = 0;
			//double _할총운임 = 0;

			//_총운임 = Convert.ToDouble(dt.Rows[0]["AM_FULL"].ToString());
			//_할총운임 = Convert.ToDouble(dt2.Rows[0]["AM_FULL"].ToString());

			//tbxDHL총운임.Text = string.Format("{0:N0}", _총운임);
			//tbxDHL운임할인.Text = string.Format("{0:N0}", _할총운임);

			#endregion 포워딩비용관리

			//string 청구중량 = string.Empty;
			//string 변경청구중량 = string.Empty;
			//string 유류할증 = string.Empty;

			//string 할청구중량 = string.Empty;
			//string 할변경청구중량 = string.Empty;
			//string 할유류할증 = string.Empty;

			//string 총운임 = string.Empty;
			//string 중량물 = string.Empty;
			//string 수수료 = string.Empty;
			//string 서비스요율 = string.Empty;

			//double _총운임 = 0;
			//double _할총운임 = 0;
			//double _청구중량 = 0;
			//double _할청구중량 = 0;
			//double _변경청구중량 = 0;
			//double _할변경청구중량 = 0;
			//double _유류할증 = 0;
			//double _할유류할증 = 0;
			//double _수수료 = 0;
			//double _중량물 = 0;
			//double _서비스요율 = 0;

			//string query = "SELECT RT_FSC FROM CZ_MA_TARIFF_DHL_PAY_H WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND DT_MONTH = '" + DateTime.Now.ToString("yyyyMM") + "'";

			//DataTable dt = TSQL.결과(query);

			//if (dt.Rows.Count > 0)
			//{
			//	서비스요율 = dt.Rows[0][0].ToString().Trim();
			//	_서비스요율 = Convert.ToDouble(서비스요율);
			//}

			//double _무게 = 0;
			//double _최종무게 = 0;
			//double _무게차이 = 0;

			////			string 지역 = DHL_ZONE(입력지역명);
			//string 지역텍스트 = 입력지역명;
			//string 무게 = 입력무게;
			//string 무게차이 = string.Empty;
			//string 최종무게 = string.Empty;

			//_무게 = Math.Round(Convert.ToDouble(무게));

			//if (_무게 > _vw무게)
			//	무게 = Convert.ToString(_무게);
			//else
			//{
			//	무게 = Convert.ToString(_vw무게);
			//}

			//_무게 = Math.Round(Convert.ToDouble(무게));

			//if (30.5 <= _무게 && _무게 < 70.5)
			//	_최종무게 = 30.5;
			//else if (70.5 <= _무게 && _무게 < 300.5)
			//	_최종무게 = 70.5;
			//else if (300.5 <= _무게)
			//	_최종무게 = 300.5;
			//else
			//	_최종무게 = _무게;

			//if(_무게 == 0)
			//{
			//	_무게 = 1;
			//	_최종무게 = _무게;
			//}

			//최종무게 = Convert.ToString(_최종무게);
			//무게 = Convert.ToString(_무게);


			//if (_무게 != _최종무게)
			//{
			//	_무게차이 = _무게 - _최종무게;
			//	무게차이 = Convert.ToString(_무게차이);
			//	최종무게 = Convert.ToString(_최종무게);

			//	변경청구중량 = DHL_TARIFF(지역, 최종무게);
			//	_변경청구중량 = Convert.ToDouble(변경청구중량) * _무게차이;

			//	청구중량 = DHL_TARIFF(지역, "30");
			//	_청구중량 = Convert.ToDouble(청구중량) + _변경청구중량;

			//	할변경청구중량 = DHL_TARIFF_DC(지역, 최종무게);
			//	_할변경청구중량 = Convert.ToDouble(할변경청구중량) * _무게차이;

			//	할청구중량 = DHL_TARIFF_DC(지역, "30");
			//	_할청구중량 = Convert.ToDouble(할청구중량) + _할변경청구중량;
			//}
			//else
			//{
			//	청구중량 = DHL_TARIFF(지역, 무게);
			//	_청구중량 = Convert.ToDouble(청구중량);

			//	할청구중량 = DHL_TARIFF_DC(지역, 무게);
			//	_할청구중량 = Convert.ToDouble(할청구중량);
			//}

			//if (_무게 >= 70)
			//	_중량물 = 115000;
			//else
			//	_중량물 = 0;

			//// 호주, 뉴질랜드
			//var regionStr = new List<string> { "Australia", "New Zealand" };
			//var commonStr = regionStr.FindAll(x => 지역텍스트.Contains(x));
			//if (commonStr.Count > 0)
			//{
			//	_수수료 = 3400;
			//}

			//if (지역텍스트.ToLower().Contains("china") || 지역텍스트.ToLower().Contains("hong kong"))
			//{
			//	_수수료 = 1200;
			//}

			//if (_수수료 == 0)
			//{
			//	_수수료 = 1700;
			//}

			//_유류할증 = (_청구중량 + _중량물 + _수수료) * _서비스요율;
			//_할유류할증 = (_할청구중량 + _중량물 + _수수료) * _서비스요율;

			//// Last
			//_총운임 = Math.Round((_청구중량 * 0.8) + _유류할증 + _중량물 + _수수료);
			//_할총운임 = Math.Round((_할청구중량 * 0.8) + _할유류할증 + _중량물 + _수수료);

			//총운임 = Convert.ToString(_총운임);

			//tbxDHL총운임.Text = string.Format("{0:N0}", _총운임);
			//tbxDHL운임할인.Text = string.Format("{0:N0}", _할총운임);
			//tbxDHL총운임.Text = Convert.ToString(_총운임);
			//tbxDHL운임한글.Text = TransNum2Han(_총운임);
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 버튼 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 메일 〓〓〓〓〓〓〓〓〓〓

		public void 메일내용(DataTable dt)
		{
			if (dt != null && dt.Rows.Count > 0)
			{
				string 문의종류 = cbo문의종류.Text;
				string 출발지 = dt.Rows[0]["PORT_LOADING"].ToString();
				string 도착지 = dt.Rows[0]["PORT_ARRIVER"].ToString();
				string 견적번호 = grd라인["NO_KEY"].ToString();
				string 선명 = dt.Rows[0]["NM_VESSEL"].ToString();
				string 패킹정보 = grd라인["DC_RMK_SIZE"].ToString();
				string 준비일자 = dtp출고가능일.StartDate.ToString("yyyy-MM-dd") + " ~ " + dtp출고가능일.EndDate.ToString("yyyy-MM-dd");
				string 파일번호 = grd라인["NO_FILE"].ToString();

				string 수신처 = cbo수신처.Text;
				string 담당자 = ctx담당자.GetCodeName();

				tbx제목.Text = "[" + 문의종류 + " 운임 견적요청] 출발지(" + 출발지 + ") ⇒ 도착지(" + 도착지 + ") / 견적번호: " + 견적번호 + " / 선명: " + 선명 + " / " + 파일번호;
				tbx본문.Text = string.Format(@"수신: {0}
발신: {1}

- 귀사의 일익 번창하심을 기원합니다.

- 하기 내용 참고하시어 {6} 운임 견적 및 스케줄 회신 부탁드립니다.

--------------------------------------------------------
> 선명: {2}

> 출발지: {7}

> 도착지: {4}

> 출고준비일자: {5}

> 패킹정보: {3}
--------------------------------------------------------

※ 회신시 아래 양식 사용 요청드립니다.
(위 아래로 [===] 포함하여 작성 부탁드립니다.)
=========================================================
PORT:
Cargo ready:
청구예상비용:
스케줄:
상세내용:
=========================================================

감사합니다.
", 수신처, "딘텍 / " + 담당자, 선명, 패킹정보, 도착지, 준비일자, 문의종류, 출발지);
			}
		}

		public void 메일내용_grd()
		{
			string 문의종류 = cbo문의종류.Text;
			string 출발지 = grd라인["PORT_LOADING"].ToString();
			string 도착지 = grd라인["PORT_ARRIVER"].ToString();
			string 견적번호 = grd라인["NO_KEY"].ToString();
			string 선명 = grd라인["NM_VESSEL"].ToString();
			string 패킹정보 = grd라인["DC_RMK_SIZE"].ToString();
			string 준비일자 = dtp출고가능일.StartDate.ToString("yyyy-MM-dd") + " ~ " + dtp출고가능일.EndDate.ToString("yyyy-MM-dd");
			string 파일번호 = grd라인["NO_FILE"].ToString();

			string 수신처 = cbo수신처.Text;
			string 담당자 = ctx담당자.GetCodeName();

			tbx제목.Text = "[" + 문의종류 + " 운임 견적요청] 출발지(" + 출발지 + ") ⇒ 도착지(" + 도착지 + ") / 견적번호: " + 견적번호 + " / 선명: " + 선명 + " / " + 파일번호;
			tbx본문.Text = string.Format(@"수신: {0}
발신: {1}

- 귀사의 일익 번창하심을 기원합니다.

- 하기 내용 참고하시어 {6} 운임 견적 및 스케줄 회신 부탁드립니다.

--------------------------------------------------------
> 선명: {2}

> 출발지: {7}

> 도착지: {4}

> 출고준비일자: {5}

> 패킹정보: {3}
--------------------------------------------------------

※ 회신시 아래 양식 사용 요청드립니다.
(위 아래로 [===] 포함하여 작성 부탁드립니다.)
=========================================================
PORT:
Cargo ready:
청구예상비용:
스케줄:
상세내용:
=========================================================

감사합니다.
", 수신처, "딘텍 / " + 담당자, 선명, 패킹정보, 도착지, 준비일자, 문의종류, 출발지);
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 메일 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 이벤트 〓〓〓〓〓〓〓〓〓〓

		private void TbxDHL총운임_TextChanged(object sender, EventArgs e)
		{
			//string 총운임 = string.Empty;
			//총운임 = tbxDHL총운임.Text.Replace(",", "");

			//if (!string.IsNullOrEmpty(총운임))
			//{
			//	double _총운임 = Convert.ToDouble(총운임);

			//	tbxDHL운임할인.Text = TransNum2Han(_총운임);
			//}
			//else
			//{
			//	tbxDHL운임할인.Text = "";
			//}
		}

		private void CtxDHLZONE_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P00_CHILD_MODE = "DHL ZONE";

			// 첫번째 문자부터 첫빈칸까지를 컬럼이름으로 가져가므로 주석 가져가도록 처리해줌
			e.HelpParam.P61_CODE1 = @"--
	  CD_SYSDEF	AS CODE
	, NM_SYSDEF	AS NAME";

			e.HelpParam.P62_CODE2 = @"
CZ_MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_FIELD = 'CZ_SA00054'
	AND YN_USE = 'Y'
	ORDER BY NM_SYSDEF";

			string a = e.HelpParam.ToString();
		}

		private void Dtp출고가능일_CalendarDateChanged(object sender, Duzon.Common.Controls.CalendarDateChangeEventArgs e)
		{
		}

		private void Tbx제목_TextChanged(object sender, EventArgs e)
		{
			grd라인["MAIL_SUBJECT"] = tbx제목.Text;
		}

		private void Tbx본문_TextChanged(object sender, EventArgs e)
		{
			grd라인["MAIL_BODY"] = tbx본문.Text;
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			string 키값 = string.Empty;

			btn확정.Enabled = true;
			btn확정.BackColor = Color.Maroon;

			if (!string.IsNullOrEmpty(grd헤드["NO_KEY"].ToString()) && !grd헤드["NO_KEY"].ToString().Equals("NEW"))
			{
				키값 = grd헤드["NO_KEY"].ToString();
			}

			if (!grd헤드["MAIL_STATUS"].ToString().Equals("N"))
			{
				btn확정.Enabled = false;
			}

			if (string.IsNullOrEmpty(grd헤드["DHL_ZONE"].ToString()))
			{
				ctxDHLZONE.CodeName = grd헤드["DHL_ZONE"].ToString();
			}
			else
			{
				ctxDHLZONE.Clear();
			}

			if (!string.IsNullOrEmpty(키값))
			{
				DataTable dt = TSQL.실행<DataTable>("PS_CZ_SA_LOG_QTNL_S2", 상수.회사코드, 키값);

				grd라인.Binding = dt;

				if (dt.Rows.Count == 0)
				{
					grd아이템.ClearData();

					tbx제목.Text = "";
					tbx본문.Text = "";
				}
				else
				{
					tbx제목.Text = dt.Rows[0]["MAIL_SUBJECT"].ToString();
					tbx본문.Text = dt.Rows[0]["MAIL_BODY"].ToString();
				}
			}
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			string 키값 = string.Empty;

			if (!string.IsNullOrEmpty(grd라인["NO_FILE"].ToString()))
			{
				키값 = grd라인["NO_FILE"].ToString();
			}

			if (!string.IsNullOrEmpty(키값))
			{
				DataTable dt = TSQL.실행<DataTable>("PS_CZ_SA_LOG_QTN_ITEM", 상수.회사코드, 키값);
				grd아이템.Binding = dt;
				double weightD = 0;

				for (int r = 0; r < dt.Rows.Count; r++)
				{
					if (!string.IsNullOrEmpty(dt.Rows[r]["WEIGHT"].ToString()))
					{
						weightD += double.Parse(dt.Rows[r]["WEIGHT"].ToString());
					}
				}

				tbxDHL무게.Text = string.Format("{0:F1}", weightD);

				//if (!string.IsNullOrEmpty(grd라인["WEIGHT"].ToString()))
				//{
				//	double weightD = double.Parse(grd라인["WEIGHT"].ToString());
				//	if (string.IsNullOrEmpty(grd헤드["DHL_WEIGHT"].ToString()))
				//		tbxDHL무게.Text = string.Format("{0:F1}", weightD);
				//}
			}
		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			//if (grd헤드.HasNormalRow)
			//{
			//	DataTable dt = this.grd헤드.GetChanges();
			//	string xml = Util.GetTO_Xml(dt);
			//	TSQL.실행("PS_CZ_SA_LOG_QTNH_XML", new object[] { xml });
			//}
			//
			//string query = "select 'LQ-'+RIGHT('0000' + CONVERT(varchar,MAX(CONVERT(INT, RIGHT(NO_KEY,5)))),5) FROM CZ_SA_LOG_QTNH";
			//
			//DataTable dt2 = TSQL.결과(query);
			//
			//if (dt2.Rows.Count > 0)
			//{
			//	grd라인["NO_FILE"] = dt2.Rows[0][0].ToString();
			//}
			//
			//TSQL sql = new TSQL("PS_CZ_SA_LOG_QTNH_S");
			//sql.변수.추가("@CD_COMPANY", 상수.회사코드);
			//sql.변수.추가("@NO_KEY", dt2.Rows[0][0].ToString());
			//sql.변수.추가("@NO_EMP", 상수.사원번호);
			//DataTable dt3 = sql.결과();
			//grd헤드.바인딩(dt3);
			//
			//
			//
			//grd헤드.AcceptChanges();
			//

			string 컬럼 = grd라인.컬럼이름(e.Col);
			string 새값 = grd라인[e.Row, e.Col].문자();

			if (컬럼 == "NO_FILE")
			{
				새값 = 새값.한글을영어().대문자();

				if (string.IsNullOrEmpty(새값))
					return;

				grd라인[e.Row, 컬럼] = 새값;

				if (TSQL.실행<DataTable>("PS_CZ_SA_LOG_QTNL_S", 상수.회사코드, 새값) is DataTable dt && dt.Rows.Count > 0)
				{
					grd라인[e.Row, "WEIGHT"] = dt.Rows[0]["WEIGHT"];
					grd라인[e.Row, "NM_VESSEL"] = dt.Rows[0]["NM_VESSEL"];
					grd라인[e.Row, "NM_KOR"] = dt.Rows[0]["NM_KOR"];
					grd라인[e.Row, "NO_EMP"] = dt.Rows[0]["NO_EMP"];
					grd라인[e.Row, "DC_RMK_TEXT2"] = dt.Rows[0]["DC_RMK_TEXT2"];
					grd라인[e.Row, "DC_RMK_SIZE"] = dt.Rows[0]["DC_RMK_SIZE"];
					grd라인[e.Row, "YN_PACK"] = dt.Rows[0]["YN_PACK"];
					grd라인[e.Row, "PORT_LOADING"] = dt.Rows[0]["PORT_LOADING"];
					grd라인[e.Row, "PORT_ARRIVER"] = dt.Rows[0]["PORT_ARRIVER"];

					메일내용(dt);
				}
				else
				{
					for (int j = grd라인.Cols.Fixed; j < grd라인.Cols.Count; j++)
					{
						if (!grd라인.컬럼이름(j).포함(grd라인.기본키()))
							grd라인[e.Row, j] = DBNull.Value;
					}
				}

				if (!string.IsNullOrEmpty(grd라인["NO_FILE"].ToString()))
				{
					새값 = grd라인["NO_FILE"].ToString();
				}

				if (!string.IsNullOrEmpty(새값))
				{
					DataTable dtItem = TSQL.실행<DataTable>("PS_CZ_SA_LOG_QTN_ITEM", 상수.회사코드, 새값);
					grd아이템.Binding = dtItem;
				}
			}
		}

		private void Cbo수신처_SelectedValueChanged(object sender, EventArgs e)
		{
			if (grd헤드.Rows.Count > 0)
			{
				string CodeNm = grd헤드["CD_PARTNER"].ToString();
				string result, query = string.Empty;

				query = "SELECT * FROM CZ_MA_CODEDTL WHERE CD_FIELD = 'CZ_SA00064' AND CD_SYSDEF = '" + CodeNm + "'";

				DataTable dt = TSQL.결과(query);

				if (dt.Rows.Count > 0)
				{
					grd헤드["MAIL_LIST"] = dt.Rows[0]["CD_FLAG1"].ToString();
					tbx메일리스트.Text = dt.Rows[0]["CD_FLAG1"].ToString();
				}
			}
		}

		private void Cbo문의종류_SelectedValueChanged(object sender, EventArgs e)
		{
			//cbo수신처.Clear();

			//if (cbo문의종류.SelectedValue.ToString() == "001")
			//{
			//	cbo수신처.DataBind(GetDb.Code("CZ_SA00064").Select("CD_FLAG5='해송'")), false);
			//}
			//else if (cbo문의종류.SelectedValue.ToString() == "003")
			//{
			//	cbo수신처.바인딩(코드.코드관리("CZ_SA00064").선택("CD_FLAG4='항송'"), false);
			//}
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 이벤트 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 기타 〓〓〓〓〓〓〓〓〓〓

		public string DHL_ZONE(string 지역)
		{
			string query = @"SELECT CD_FLAG1 FROM CZ_MA_CODEDTL WHERE CD_FIELD = 'CZ_SA00054' AND NM_SYSDEF = '" + 지역 + "'";

			DataTable dt = TSQL.결과(query);

			return dt.Rows[0][0].ToString();
		}

		public string DHL_도시(string 지역)
		{
			string query = string.Format(@"SELECT TOP 1 PORT_ARRIVER, COUNT(*) AS duplicate_count
FROM CZ_TR_INVH
WHERE 1=1
  AND CD_COMPANY = 'K100'
  AND ARRIVER_COUNTRY = (
SELECT CD_SYSDEF FROM MA_CODEDTL
WHERE 1=1
AND CD_FIELD = 'MA_B000020'
AND CD_COMPANY = 'K100'
AND CD_FLAG1 = '{0}')
  AND REMARK1 IS NOT NULL
GROUP BY PORT_ARRIVER
ORDER BY duplicate_count DESC",지역);

			DataTable dt = TSQL.결과(query);

			return dt.Rows[0][0].ToString();
		}


		public string DHL_TARIFF(string zone, string 무게)
		{
			string query = @"SELECT CD_FLAG" + zone + " FROM CZ_MA_CODEDTL WHERE CD_FIELD  = 'CZ_SA00066' AND NM_SYSDEF = '" + 무게 + "'";

			DataTable dt = TSQL.결과(query);

			return dt.Rows[0][0].ToString();
		}

		public string DHL_TARIFF_DC(string zone, string 무게)
		{
			string query = @"SELECT CD_FLAG" + zone + " FROM CZ_MA_CODEDTL WHERE CD_FIELD  = 'CZ_SA00071' AND NM_SYSDEF = '" + 무게 + "'";

			DataTable dt = TSQL.결과(query);

			return dt.Rows[0][0].ToString();
		}

		// 금액 한글로 변환
		public string TransNum2Han(double num)
		{
			string[] han = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
			string[] unit = new string[] { "천", "백", "십", "" };
			string[] unit2 = new string[] { "", "만", "억", "조" };
			string result = num.ToString();
			if (result.Length > 16)
				return "금액이 너무 많습니다.";
			// 네자리마다 단위수2(만,억,조)가 바뀌고 네자리마다 단위수1(천,백,십)이 바뀌므로 네자리씩 나눈다.
			ArrayList spilt = new ArrayList();
			for (int i = 0; result.Length > 0; i++)
			{
				int spiltLength;
				if (result.Length > 4) { spiltLength = 4; }
				else { spiltLength = result.Length; }
				spilt.Add(result.Substring(result.Length - spiltLength, spiltLength));
				result = result.Substring(0, result.Length - spiltLength);
			}
			for (int i = 0; i < spilt.Count; i++)
			{
				string str = (string)spilt[i];
				// 네자리씩 나눌때 네자리가 되지않으면 원만한 처리를 위해서 '0'을 채워 네자리로 만든다.
				if (str.Length < 4) str = str.PadLeft(4, '0');
				if (Int32.Parse(str) != 0) result = unit2[i] + result;
				// 네자리로 나뉜값을 '천','백','십'단위를 구분하는 루틴
				for (int j = 3; j >= 0; j--)
				{
					string unitFlag = unit[j];
					if (str.Substring(j, 1) == "0") unitFlag = "";
					result = han[Int32.Parse(str.Substring(j, 1))] + unitFlag + result;
				}
			}
			return result + "원";
		}

		public bool SendMail(string companyCode, string noKey, string noEmp, bool boAuto)
		{
			DataRow headRow = SQL.GetDataTable("PS_CZ_SA_LOG_QTNH_S3", companyCode, noKey, noEmp).Rows[0];
			DataRow bodyRow = SQL.GetDataTable("PS_CZ_SA_LOG_QTNL_S2", companyCode, noKey).Rows[0];

			// 딘텍 담당자 정보
			string empMail = (string)headRow["NO_EMAIL"];

			// 메일 주소
			string from = "forwarder@dintec.co.kr";
			string to = headRow["MAIL_LIST"].ToString();
			string cc = empMail;

			// 제목
			string title = bodyRow["MAIL_SUBJECT"].ToString();

			// 기본파일
			string[] files1 = null;

			// 본문
			string body = bodyRow["MAIL_BODY"].ToString();

			// 메일발송
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(from, to, cc, "", title, files1, files1, body, noKey, noEmp, boAuto);

			// 메일 발송에 성공한 경우 수신자 업데이트
			if (f.ShowDialog() == DialogResult.OK)
			{
				// 발송일 업데이트
				string query = @"
UPDATE CZ_SA_LOG_QTNH SET
	MAIL_STATUS		 = 'S'
,	DT_SEND = '" + DateTime.Now.ToString("yyyyMMdd") + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_KEY = '" + noKey + @"'
";

				DataTable dtSend = SQL.GetDataTable(query);

				if (grd헤드.DataTable != null)
					grd헤드.DataTable.Select("NO_KEY = '" + noKey + "'")[0]["DT_SEND"] = DateTime.Now.ToString("yyyyMMdd");

				tbx견적번호검색.Text = "";

				return true;
			}
			else
			{
				return false;
			}
		}

		public string SearchFileInfo(string companyCode, string fileCode, string menuCode)
		{
			string query, result = string.Empty;

			try
			{
				query = "SELECT MAX(FILE_NAME) AS FILE_PATH_MNG " +
						"FROM MA_FILEINFO " +
						"WHERE CD_COMPANY = '" + companyCode + "' " +
						"AND CD_MODULE = 'CZ' " +
						"AND ID_MENU = 'P_CZ_SA_LOG_QTN_REG' " +
						"AND CD_FILE = '" + fileCode + "'";

				result = D.GetString(Global.MainFrame.ExecuteScalar(query));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return result;
		}

		private void InsertQue(string cdCompany, string noFile, string cdPartner, string cdRPA, string noBot)
		{
			string query = @"EXEC PX_CZ_RPA_WORK_QUEUE_4 @CD_COMPANY = @CD_COMPANY , @CD_RPA = @CD_RPA, @NO_FILE = @NO_FILE, @CD_PARTNER = @CD_PARTNER, @NO_BOTS = @NO_BOTS";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", cdCompany);
			dbm.AddParameter("@CD_RPA", cdRPA);
			dbm.AddParameter("@NO_FILE", noFile);
			dbm.AddParameter("@CD_PARTNER", cdPartner);
			dbm.AddParameter("@NO_BOTS", noBot);

			dbm.ExecuteNonQuery();
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 기타 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 조회 〓〓〓〓〓〓〓〓〓〓

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			string 검색값 = tbx견적번호검색.Text;
			string 담당자_검색 = ctx담당자검색.CodeValue.ToString();

			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				TSQL sql = new TSQL("PS_CZ_SA_LOG_QTNH_S");
				sql.변수.추가("@CD_COMPANY", 상수.회사코드);
				sql.변수.추가("@NO_KEY", 검색값);
				sql.변수.추가("@NO_EMP", 담당자_검색);
				DataTable dt = sql.결과();
				grd헤드.바인딩(dt);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 조회 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 추가 〓〓〓〓〓〓〓〓〓〓

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			try
			{
				if (grd헤드.데이터테이블("NO_KEY = 'NEW'").존재())
					유틸.메세지("이미 작성중인 항목이 있습니다.");

				// 추가
				grd헤드.행추가();
				grd헤드["CD_COMPANY"] = 상수.회사코드;
				grd헤드["NO_KEY"] = "NEW";
				//grd헤드["DT_SEND"] = 유틸.오늘(0);
				grd헤드["NO_EMP"] = 상수.사원번호;
				grd헤드["MAIL_STATUS"] = "N"; //NEW
				grd헤드.행추가완료();

				grd라인.ClearData();
				grd아이템.ClearData();

				tbx제목.Text = "";
				tbx본문.Text = "";

				//Btn추가_Click(null, null);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 추가 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 저장 〓〓〓〓〓〓〓〓〓〓

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
			if (!base.SaveData() || !this.Verify()) return false;

			if (grd헤드.HasNormalRow)
			{
				DataTable dt = this.grd헤드.GetChanges();
				string xml = Util.GetTO_Xml(dt);
				//TSQL.실행("PS_CZ_SA_LOG_QTNH_XML", new object[] { xml });

				DataTable dtR = TSQL.결과("PS_CZ_SA_LOG_QTNH_XML", new object[] { xml });

				if (dtR.Rows.Count > 0)
					tbx견적번호검색.Text = dtR.Rows[0][0].ToString();
				else
					tbx견적번호검색.Text = "";
			}

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();

			OnToolBarSearchButtonClicked(null, null);

			return true;
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 저장 〓〓〓〓〓〓〓〓〓〓

		#region 〓〓〓〓〓〓〓〓〓〓 삭제 〓〓〓〓〓〓〓〓〓〓

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.grd헤드.HasNormalRow) return;
				this.grd헤드.Rows.Remove(grd헤드.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#endregion 〓〓〓〓〓〓〓〓〓〓 삭제 〓〓〓〓〓〓〓〓〓〓
	}
}