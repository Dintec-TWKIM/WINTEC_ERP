using System.Windows.Forms;

namespace DX
{
	public class 파일선택
	{
		readonly OpenFileDialog ofd;

		public 파일선택()
		{
			ofd = new OpenFileDialog();
		}

		public 파일선택(파일유형 파일유형)
		{
			ofd = new OpenFileDialog();
			this.파일유형 = 파일유형;
		}

		public bool 다중선택 { get; set; } = false;

		public 파일유형 파일유형 { get; set; }

		public string 파일이름 => ofd.FileName;

		public string[] 파일이름s => ofd.FileNames;
		
		public bool 열기()
		{
			string 표시 = "";
			string 필터 = "";

			if (파일유형.메일.포함(파일유형))	{ 표시 += ",메일"		; 필터 += ";*.msg"; }
			if (파일유형.이미지.포함(파일유형))	{ 표시 += ",이미지"	; 필터 += ";*.jpg;*.jpeg;*.png"; }
			if (파일유형.Html.포함(파일유형))	{ 표시 += ",Html"	; 필터 += ";*.htm;*.html"; }
			if (파일유형.Pdf.포함(파일유형))	{ 표시 += ",Pdf"		; 필터 += ";*.pdf"; }
			
			if (표시.시작(",")) 표시 = 표시.Substring(1);
			if (필터.시작(";")) 필터 = 필터.Substring(1);

			ofd.Filter = 표시 + "|" + 필터;
			ofd.Multiselect = 다중선택;

			return ofd.ShowDialog() == DialogResult.OK;			
		}
	}
}
