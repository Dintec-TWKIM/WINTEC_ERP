using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dintec;
using Duzon.Common.Forms;
using System.Drawing;
using DataMatrix.net;
using ThoughtWorks.QRCode.Codec;
using System.Windows.Forms;
using Bytescout.BarCodeReader;
using DXQR;
using System.Text.RegularExpressions;

namespace DX
{
	public class QR
	{
		static readonly string RegistrationName = "1_DEV_BYTESCOUT_BARCODE_READER_SDK_SMALL_BUSINESS_DESKTOP_DEVELOPER_LICENSE_UNLIM_PAGES_MNT_UNTIL_JANUARY_22_2022_DYKIM@DINTEC.CO.KR";
		static readonly string RegistrationKey = "005C-2B78-FAC7-0C87-D527-F71F-535";
		readonly List<QRDATA> qrdata;

		public string 파일이름 { get; set; }

		public List<QRDATA> Items => qrdata;

		public QRDATA this[int i] => qrdata[i];

		public int Count => qrdata.Count;

		public 읽기유형 읽기유형 { get; set; } = new 읽기유형();

		public bool 쇼트너 { get; set; } = false;	// 딘텍쇼트너 타입인지

		public QR()
		{
			qrdata = new List<QRDATA>();
		}

		public void 읽기()
		{
			Reader reader = new Reader(RegistrationName, RegistrationKey);
			reader.BarcodeTypesToFind.QRCode = 읽기유형.QR코드;
			reader.BarcodeTypesToFind.DataMatrix = 읽기유형.데이터매트릭스;
			reader.BarcodeTypesToFind.Code128 = 읽기유형.바코드;
			
			// 옵션
			reader.PDFReadingMode = PDFReadingMode.ExtractEmbeddedImagesOnly;
			//reader.ImagePreprocessingFilters.AddContrast(40);

			// QR저장
			FoundBarcode[] barcodes = reader.ReadFrom(파일이름);    // Page가 다른데서 쓰는거랑 얘만 달러서 헷갈리므로 유저함수에 다시 넣어줌 (0부터 시작하면 헛갈리니 1부터 시작하도록)
			
			//if (쇼트너)
			//{
				Dictionary<int, string> dict = new Dictionary<int, string>();

				// ***** 1. 전체 페이지에서 dintec 쇼트너를 먼저 찾아서 저장함
				foreach (FoundBarcode bar in barcodes)
				{
					int page = bar.Page + 1;
					string value = bar.Value;

					if (value.발생("dint.ec", "dintec.kr"))
						dict.Add(page, value.분할("/").마지막());
				}

				// ***** 2. 쇼트너로 등록된 페이지는 스킵하고 나머지 QR 저장
				foreach (FoundBarcode bar in barcodes)
				{
					int page = bar.Page + 1;
					string value = bar.Value;

					if (!dict.Keys.발생(page))
					{
						if (value.Length > 8)							continue;	// 8글자 초과 탈락
						if (!Regex.IsMatch(value, "^[a-zA-Z0-9]*$"))	continue;	// 영문,숫자 외 다른문자 포함 탈락
						if (dict.Values.발생(value))						continue;   // 동일한거 있으면 탈락

						// 모든 역경을 이겨 냈으면 등록
						dict.Add(page, value);
					}
				}

				// ***** 3. 페이지순으로 정렬한다음 qrdata 저장
				foreach (KeyValuePair<int, string> d in dict.OrderBy(x => x.Key))
					qrdata.Add(new QRDATA(d.Key, d.Value));
			//}
		}






































		public static void 이미지_데이터매트릭스(string 내용, string 파일이름)
		{

			DmtxImageEncoder encoder = new DmtxImageEncoder();
			//DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();


			//string fileName = "encodedImg.png";
			//DmtxImageEncoder encoder = new DmtxImageEncoder();

			//options.ModuleSize = 5;
			//options.MarginSize = 4;
			//options.BackColor = Color.White;
			//options.ForeColor = Color.Green;
			//Bitmap encodedBitmap = encoder.EncodeImage(testVal);
			//encodedBitmap.Save(fileName, ImageFormat.Png);
			//string fileName = "download.png";
			//DmtxImageEncoder encoder = new DmtxImageEncoder();
			//DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
			//options.ModuleSize = 8;
			//options.MarginSize = 4;
			//options.BackColor = Color.White;
			//options.ForeColor = Color.Green;
			//Bitmap encodedBitmap = encoder.EncodeImage(testVal);
			//encodedBitmap.Save(fileName, ImageFormat.Png);


			Image qrImage = encoder.EncodeImage(내용);
			qrImage.Save(파일이름);
						//string a = "";
		}

		

		public static void 이미지_QR코드(string 내용, string 파일이름)
		{
			QRCodeEncoder qrCode = new QRCodeEncoder();
			//qrCode.QRCodeVersion = 40;	// 좀더 알아보고 수정하기
			qrCode.QRCodeVersion = 4;
			qrCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

			Image qrImage = qrCode.Encode(내용);
			qrImage.Save(파일이름);
		}





		public static void 데이터매트릭스(string 내용, string 파일이름)
		{
			DmtxImageEncoder encoder = new DmtxImageEncoder();
			Image qrImage = encoder.EncodeImage(내용);
			qrImage.Save(파일이름);
		}

		public static void 바코드(string 내용, string 파일이름)
		{			
			Image qrImage = GenCode128.Code128Rendering.MakeBarcodeImage(내용, 2, true);
			qrImage.Save(파일이름);
		}
	}

	

	public class QRDATA
	{
		public int 페이지 { get; }

		public string 값 { get; }

		public string 인코딩 { get; }

		public QRDATA(int page, string value)
		{
			페이지 = page;
			값 = value;
			인코딩 = 값;
			인코딩 = 인코딩.Replace("http://", "");
			인코딩 = 인코딩.Replace("dint.ec/", "");
			인코딩 = 인코딩.Replace("dintec.kr/", "");
		}
	}
}

namespace DXQR
{
	public class 읽기유형
	{
		public bool 데이터매트릭스 { get; set; } = false;

		public bool 바코드 { get; set; } = false;

		public bool QR코드 { get; set; } = false;
	}
}