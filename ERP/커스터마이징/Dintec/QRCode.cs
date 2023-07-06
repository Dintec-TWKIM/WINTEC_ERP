using System.Drawing;
using ThoughtWorks.QRCode.Codec;

namespace Dintec
{
	public class QRCode
	{
		public static void Generate(string text, string fileName)
		{
			QRCodeEncoder qrCode = new QRCodeEncoder();
			//qrCode.QRCodeVersion = 40;	// 좀더 알아보고 수정하기
			qrCode.QRCodeVersion = 4;
			qrCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

			Image qrImage = qrCode.Encode(text);
			qrImage.Save(fileName);

			string a = "";
		}
	}
}
