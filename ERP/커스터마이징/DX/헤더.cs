using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;

namespace DX
{
	public class 헤더
	{		
		object 이전값;
		bool 변경이벤트동작 = true;
		bool 헤더변경됨 = false;
		bool 신규여부 = true;
		
		TableLayoutPanel cntr;
		Control pk;
		Control[] req;

		Dictionary<Control, object> 기본값 = new Dictionary<Control, object>();


		public void 기본키(Control 기본키) => pk = 기본키;

		public void 필수값(params Control[] 필수값) => req = 필수값;

		/// <summary>
		/// 클리어 후 기본값으로 지정, 특히 체크박스나 라디오버튼은 필요
		/// </summary>
		public void 기본값추가(Control 컨트롤, object 기본값)
		{
			this.기본값.Add(컨트롤, 기본값);
		}

		public void 컨테이너(TableLayoutPanel 컨테이너)
		{
			cntr = 컨테이너;

			foreach (Control c in cntr.컨트롤())
			{
				string 태그 = c.태그();
				if (태그 == "" || 태그 == "VIEWBOX") continue;

				if		(c is BpComboBox		cbm) {  }
				else if	(c is BpCodeTextBox		ctx) { ctx.CodeChanged += 추가버튼사용; }
				else if (c is CheckBox			chk) { chk.CheckedChanged += 추가버튼사용; }
				else if (c is ComboBox			cbo) { cbo.SelectedIndexChanged += 추가버튼사용; }
				else if (c is CurrencyTextBox	cur) { cur.GotFocus += Cur_GotFocus; cur.Validated += Cur_Validated; cur.TextChanged += Cur_TextChanged; }
				else if (c is DatePicker		dtp) { dtp.DateChanged += 추가버튼사용; }
				else if (c is DropDownComboBox	ddc) { ddc.SelectedIndexChanged += 추가버튼사용; }
				else if (c is Label				lbl) { }
				else if (c is RadioButton		rdo) { rdo.CheckedChanged += 추가버튼사용; }
				else if (c is TextBoxExt		tbx) { tbx.GotFocus += Tbx_GotFocus; tbx.Validated += Tbx_Validated; tbx.TextChanged += Tbx_TextChanged; }
			}
		}

		public void 클리어()
		{
			if (cntr == null) return;
			변경이벤트동작 = false;

			foreach (Control c in cntr.컨트롤())
			{
				string 태그 = c.태그();
				if (태그 == "" || 태그 == "VIEWBOX") continue;

				if		(c is BpComboBox		cbm) { cbm.클리어(); }
				else if	(c is BpCodeTextBox		ctx) { ctx.값(""); ctx.글(""); }
				else if (c is CheckBox			chk) { chk.Checked = 기본값.ContainsKey(chk) ? (bool)기본값[chk] : false; }
				else if (c is ComboBox			cbo) { cbo.SelectedIndex = 0; }
				else if (c is CurrencyTextBox	cur) { cur.DecimalValue = 0; }
				else if (c is DatePicker		dtp) { dtp.Text = ""; }
				else if (c is DropDownComboBox	ddc) { ddc.SelectedIndex = 0; }
				else if (c is Label				lbl) { lbl.Text = ""; }
				else if (c is RadioButton		rdo) { 라디오버튼기본값컨트롤(rdo).Checked = true; }
				else if (c is TextBoxExt		tbx) { tbx.Text = ""; }
			}

			커밋();			
			변경이벤트동작 = true;
			신규여부 = true;	// 클리어 한 경우에는 추가 모드로 인식함
		}

		private RadioButton 라디오버튼기본값컨트롤(RadioButton rdo)
		{
			// 기본값으로 설정된 라디오버튼이 있는지 찾아봄 (true, false인지는 상관없음)
			foreach (RadioButton r in rdo.Parent.컨트롤<RadioButton>())
			{
				if (기본값.ContainsKey(r))
					return r;
			}

			// 없으면 첫번째꺼 리턴
			RadioButton[] rdos = rdo.Parent.컨트롤<RadioButton>();	// 더존꺼 쓰면 ext인데도 이 메소드를 타는데 여기에서는 안잡혀서 카운트 0이 뜸
			return rdos.Length > 0 ? rdo.Parent.컨트롤<RadioButton>()[0] : rdo;
		}

		public void 초기화() => 클리어();


		public void 바인딩(DataTable 데이터테이블)
		{
			if (데이터테이블.존재())
				바인딩(데이터테이블.Rows[0]);
			else
				클리어();
		}

		public void 바인딩(DataRow 데이터행)
		{
			변경이벤트동작 = false;

			foreach (Control c in cntr.컨트롤())
			{
				string 태그 = c.태그();
				if (태그 == "" || 태그 == "VIEWBOX") continue;

				try
				{
					if		(c is BpComboBox		cbm) { cbm.바인딩(데이터행[태그.분할(",")[0]], 데이터행[태그.분할(",")[1]]); }
					else if	(c is BpCodeTextBox		ctx) { ctx.값(데이터행[태그.분할(",")[0]]); ctx.글(데이터행[태그.분할(",")[1]]); }
					else if (c is CheckBox			chk) { chk.Checked = 데이터행[태그].문자() == "Y"; }
					else if (c is ComboBox			cbo) { cbo.값(데이터행[태그]); }
					else if (c is CurrencyTextBox	cur) { cur.DecimalValue = 데이터행[태그].실수(); }
					else if (c is DatePicker		dtp) { dtp.Text = 데이터행[태그].문자(); }
					else if (c is DropDownComboBox	ddc) { ddc.값(데이터행[태그]); }
					else if (c is Label				lbl) { lbl.Text = 데이터행[태그].문자(); }
					else if (c is RadioButton		rdo) { rdo.값(데이터행[태그.분할(",")[0]]); }
					else if (c is TextBoxExt		tbx) { tbx.Text = 데이터행[태그].문자(); }
				}
				catch (IndexOutOfRangeException)
				{
					throw new Exception("바인딩 오류 : " + 태그);
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			
			커밋();
			변경이벤트동작 = true;
		}

		/// <summary>
		/// 헤더 데이터테이블 리턴, 헤더 변경됐던 안됐던 헤더 정보 리턴
		/// </summary>
		public DataTable 데이터테이블()
		{			
			// 데이터테이블 만들기
			DataTable dt = new DataTable();
			dt.Rows.Add();

			foreach (Control c in cntr.컨트롤())
			{
				string 태그 = c.태그();
				if (태그 == "" || 태그 == "VIEWBOX") continue;

				// 태그가 ,로 이루어진 얘들도 있어서 앞 부분만 가져옴
				태그 = 태그.분할(",")[0];

				// 태그가 이미 추가되어있다면 패스 (라디오버튼이 동일 태그가 여러 콘트롤에 들어감)
				if (dt.Columns.Contains(태그))
					continue;
				else
					dt.Columns.Add(태그);

				if		(c is BpComboBox		cbm) { dt.Rows[0][태그] = cbm.값(); }
				else if	(c is BpCodeTextBox		ctx) { dt.Rows[0][태그] = ctx.값(); }
				else if (c is CheckBox			chk) { dt.Rows[0][태그] = chk.Checked ? "Y" : "N"; }
				else if (c is ComboBox			cbo) { dt.Rows[0][태그] = cbo.값(); }
				else if (c is CurrencyTextBox	cur) { dt.Rows[0][태그] = cur.DecimalValue; }
				else if (c is DatePicker		dtp) { dt.Rows[0][태그] = dtp.Text; }
				else if (c is DropDownComboBox	ddc) { dt.Rows[0][태그] = ddc.값(); }
				else if (c is Label				lbl) { dt.Rows[0][태그] = lbl.Text; }
				else if (c is RadioButton		rdo) { dt.Rows[0][태그] = rdo.선택라디오().태그().분할(",")[1]; }
				else if (c is TextBoxExt		tbx) { dt.Rows[0][태그] = tbx.Text; }
			}

			// pk 필드 젤 앞으로 끌어오기
			if (pk != null)
				dt.Columns[pk.태그()].SetOrdinal(0);

			// 회사코드 추가
			if (!dt.Columns.Contains("CD_COMPANY"))
				dt.컬럼추가("CD_COMPANY", 상수.회사코드, 0);			

			// 추가 or 수정 상태값 설정
			dt.AcceptChanges();
			if (신규여부)		dt.Rows[0].SetAdded();
			else			dt.Rows[0].SetModified();

			return dt;
		}

		/// <summary>
		/// 헤더 데이터테이블 리턴, 헤더 변경된게 없으면 null 리턴
		/// </summary>
		public DataTable 데이터테이블_수정() => !헤더변경됨 ? null : 데이터테이블();



		public bool 유효성검사()
		{			
			foreach (Control c in req)
			{
				if		(c is BpCodeTextBox		ctx) { if (ctx.값() == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
				else if (c is ComboBox			cbo) { if (cbo.값() == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
				else if (c is DatePicker		dtp) { if (dtp.값() == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
				else if (c is DropDownComboBox	ddc) { if (ddc.값() == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
				else if (c is Label				lbl) { if (lbl.Text == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
				else if (c is TextBoxExt		tbx) { if (tbx.값() == "") 메시지.경고발생(메시지코드._은는필수입력항목입니다, 라벨(c).Text); }
			}

			return true;
		}

		private Label 라벨(Control 컨트롤)
		{			
			Control pnl = 컨트롤.Parent;	// 윈도우 기본 패널인지 더존 패널ext인지 모름
			TableLayoutPanel lay = (TableLayoutPanel)pnl.Parent;
			int row = lay.GetRow(pnl);
			int col = lay.GetColumn(pnl);
								
			return (Label)lay.GetControlFromPosition(col - 1, row);			
		}






		public void 커밋()
		{
			이전값 = null;
			헤더변경됨 = false;
			신규여부 = false;

			if (cntr.GetContainerControl() is PageBase pb)
				pb.ToolBarSaveButtonEnabled = false;
		}

		public void 변경이벤트시작() => 변경이벤트동작 = true;

		public void 변경이벤트중지() => 변경이벤트동작 = false;

		private void 추가버튼사용(object sender, EventArgs e)
		{	
			// PK에 해당하는 컨트롤은 제외
			if (pk == (Control)sender)
				return;

			if (변경이벤트동작)
			{
				헤더변경됨 = true;

				// 현재 페이지를 가져옴
				Control con = (sender as Control).GetContainerControl() as Control;
				PageBase pb;

				if (con is SplitContainer)
					pb = con.GetContainerControl() as PageBase;	// 이상하게 스플릿은 최상위로 지가 잡혀버림
				else
					pb = con as PageBase;

				//PageBase pb = (sender as Control).GetContainerControl() as PageBase;

				// 더 상위 페이지가 있는지 확인 (대표적으로 견적등록)
				if (pb.Parent.GetContainerControl() is PageBase)
					(pb.Parent.GetContainerControl() as PageBase).ToolBarSaveButtonEnabled = true;
				else				
					pb.ToolBarSaveButtonEnabled = true;
			}
		}

		/// <summary>
		/// 헤더 강제 변경 (수정 모드 돌입)
		/// </summary>
		public void 변경됨() => 추가버튼사용(cntr, null);	// 컨트롤 뭐 하나라도 넘겨야 하므로 젤 만만한 컨테이너 넘겨줌


		private void Cur_GotFocus(object sender, EventArgs e) => 이전값 = ((CurrencyTextBox)sender).DecimalValue;

		private void Cur_Validated(object sender, EventArgs e)
		{
			// 이전값이 null이 아니란 얘기는 일단 포커싱 됐단거고 값 비교해서 변경된 경우만 추가버튼사용
			if (이전값 != null && 이전값.실수() != ((CurrencyTextBox)sender).DecimalValue)
				추가버튼사용(sender, e);

			// 사용하고 난 후 이전값 초기화 (변경안되고 아웃포커싱 될수도 있으므로 무조건 초기화)
			이전값 = null;
		}

		private void Tbx_TextChanged(object sender, EventArgs e)
		{
			// 포커싱 없이 외부 변경 (코드에서 값 변경하는 경우)
			if (이전값 == null)
				추가버튼사용(sender, e);
		}

		private void Tbx_GotFocus(object sender, EventArgs e) => 이전값 = ((TextBoxExt)sender).Text;

		private void Tbx_Validated(object sender, EventArgs e)
		{
			// 이전값이 null이 아니란 얘기는 일단 포커싱 됐단거고 값 비교해서 변경된 경우만 추가버튼사용
			if (이전값 != null && 이전값.문자() != ((TextBoxExt)sender).Text)
				추가버튼사용(sender, e);

			// 사용하고 난 후 이전값 초기화 (변경안되고 아웃포커싱 될수도 있으므로 무조건 초기화)
			이전값 = null;
		}

		private void Cur_TextChanged(object sender, EventArgs e)
		{
			// 포커싱 없이 외부 변경 (코드에서 값 변경하는 경우)
			if (이전값 == null)
				추가버튼사용(sender, e);
		}

		public void 사용(bool 사용)
		{
			// PK에 해당하는 컨트롤은 제외

			// 패널 찾아서 사용 여부 처리함 (패널은 따로 만들어 놓은게 있어서 그거 동작시킴)
			//foreach (var c in GetAll(t, typeof(PanelExt)))
			//{
			//	((PanelExt)c).사용(사용);
			//}

			foreach (Control c in cntr.컨트롤<PanelExt>())
			{
				// 기본키는 별도로 사용여부 컨트롤 하자
				if (pk.Parent == c)
					continue;

				((PanelExt)c).사용(사용);
			}
		}


		
	}
}
