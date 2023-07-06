using System.Windows.Forms;
using Bytescout.BarCodeReader;
using Duzon.Common.Forms;

namespace DX
{
	public class BARCODE
	{
		static readonly string RegistrationName = "1_DEV_BYTESCOUT_BARCODE_READER_SDK_SMALL_BUSINESS_DESKTOP_DEVELOPER_LICENSE_UNLIM_PAGES_MNT_UNTIL_JANUARY_22_2022_DYKIM@DINTEC.CO.KR";
		static readonly string RegistrationKey = "005C-2B78-FAC7-0C87-D527-F71F-535";

		string fileName;
		FoundBARCODE[] foundBarcodes;

		public string FileName
		{
			get
			{
				return fileName;
			}
		}

		public FoundBARCODE[] Items
		{
			get
			{
				return foundBarcodes;
			}
		}

		public bool QRCode
		{
			get; set;
		}

		public bool DataMatrix
		{
			get; set;
		}

		public bool Code128
		{
			get; set;
		}

		public DialogResult OpenFileDialog()
		{
			OpenFileDialog f = new OpenFileDialog() { Filter = Global.MainFrame.DD("Pdf 파일") + "|*.pdf;" };

			if (f.ShowDialog() == DialogResult.OK)
			{
				fileName = f.FileName;
				return DialogResult.OK;
			}
			else
				return DialogResult.Cancel;
		}

		public void Read()
		{
			Reader reader = new Reader(RegistrationName, RegistrationKey);
			
			if (QRCode)			
				reader.BarcodeTypesToFind.QRCode = true;

			if (DataMatrix)
			reader.BarcodeTypesToFind.DataMatrix = true;

			if (Code128)
				reader.BarcodeTypesToFind.Code128 = true;

			FoundBarcode[] barcodes = reader.ReadFrom(fileName);    // Page가 다른데서 쓰는거랑 얘만 달러서 헷갈리므로 유저함수에 다시 넣어줌
			foundBarcodes = new FoundBARCODE[barcodes.Length];

			for (int i = 0; i < barcodes.Length; i++)
				foundBarcodes[i] = new FoundBARCODE(barcodes[i].Page, barcodes[i].Value);
		}
	}
}
