using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bytescout.PDFExtractor;
using SelectPdf;

namespace DX
{
	public class PDF
	{
		static readonly string RegistrationName = "dykim@dintec.co.kr";
		static readonly string RegistrationKey = "6585-166F-FF29-D53B-F0C8-F2DF-0DA"; 



		public static void Optimize(string sourPdf, string destPdf)
		{
			DocumentOptimizer optimizer = new DocumentOptimizer(RegistrationName, RegistrationKey);

			//optimizer.OptimizeDocument(sourPdf, destPdf, new OptimizationOptions());
			optimizer.OptimizeDocument(sourPdf, destPdf);
		}

		public static void ImageToPdf(string imgFile)
		{
			GlobalProperties.LicenseKey = "JQ4UBRcQFAUWFBYcBRQdCxUFFhQLFBcLHBwcHA==";
			PdfDocument doc = new PdfDocument();
			PdfPage page = doc.AddPage();

			// get image path
			//string imgFile = @"d:\test.jpeg";

			// define a rendering result object
			PdfRenderingResult result;

			// create image element from file path with real image size
			PdfImageElement img1 = new PdfImageElement(0, 0, imgFile);
			result = page.Add(img1);

			// create image element from system Image object with fixed size in pdf 
			// (aspect ratio not preserved)
			//System.Drawing.Image img = System.Drawing.Image.FromFile(imgFile);
			//PdfImageElement img2 = new PdfImageElement(0,
			//	result.PdfPageLastRectangle.Bottom + 50, 100, 100, img);
			//result = page.Add(img2);

			// add text element
			//PdfFont font1 = doc.AddFont(PdfStandardFont.Helvetica);
			//font1.Size = 10;
			//PdfTextElement text1 = new PdfTextElement(0,
			//	result.PdfPageLastRectangle.Bottom + 50, Helper.SomeText(), font1);
			//page.Add(text1);

			// add image over text (no transparency)
			//PdfImageElement img3 = new PdfImageElement(0,
			//	result.PdfPageLastRectangle.Bottom + 50, imgFile);
			//result = page.Add(img3);

			//// add image over text (with transparency) next to the previous image
			//PdfImageElement img4 = new PdfImageElement(
			//	result.PdfPageLastRectangle.Right + 30,
			//	result.PdfPageLastRectangle.Top, imgFile);
			//img4.Transparency = 50;
			//result = page.Add(img4);

			// Pdf 파일이름
			string pdfFile = imgFile.Substring(0, imgFile.LastIndexOf(".")) + ".pdf";

			// save pdf document
			doc.Save(pdfFile);

			// close pdf document
			doc.Close();
		}





		public static void Merge(string destPdf, params string[] sourcePdf)
		{
			using (DocumentMerger merger = new DocumentMerger(RegistrationName, RegistrationKey))
			{
				merger.Merge(sourcePdf, destPdf);
			}
		}

		public static void RemovePage(string sourPdf, string destPdf, int pageNum)
		{
			using (DocumentSplitter splitter = new DocumentSplitter(RegistrationName, RegistrationKey))
			{
				splitter.RemovePage(sourPdf, destPdf, pageNum);
			}	
		}

		public static void RemovePageRange(string sourPdf, string destPdf, int startPage, int endPage)
		{
			using (DocumentSplitter splitter = new DocumentSplitter(RegistrationName, RegistrationKey))
			{
				splitter.RemovePageRange(sourPdf, destPdf, startPage, endPage);
			}
		}

		public static int GetPageCount(string fileName)
		{
			using (TextExtractor extractor = new TextExtractor(RegistrationName, RegistrationKey))
			{
				extractor.LoadDocumentFromFile(fileName);
				return extractor.GetPageCount();
			}		
		}

		//public static int GetPageCount(string fileName)
		//{
		//	TextExtractor extractor = new TextExtractor(RegistrationName, RegistrationKey);
		//	extractor.LoadDocumentFromFile(fileName);
		//	return extractor.GetPageCount();
		//}

		public static void ExtractPageRange(string sourPdf, string destPdf, int startPage, int endPage)
		{
			using (DocumentSplitter splitter = new DocumentSplitter(RegistrationName, RegistrationKey))
			{
				splitter.ExtractPageRange(sourPdf, destPdf, startPage, endPage);
			}
		}












		public static void 추출(string sourPdf, string destPdf, int 시작페이지, int 종료페이지)
		{
			using (DocumentSplitter splitter = new DocumentSplitter(RegistrationName, RegistrationKey))
			{
				splitter.ExtractPageRange(sourPdf, destPdf, 시작페이지, 종료페이지);
			}
		}

		public static int 페이지수(string PDF파일)
		{
			TextExtractor extractor = new TextExtractor(RegistrationName, RegistrationKey);
			extractor.LoadDocumentFromFile(PDF파일);
			return extractor.GetPageCount();
		}



		public static string 텍스트(string PDF파일)
		{			
			TextExtractor extractor = new TextExtractor(RegistrationName, RegistrationKey);
			extractor.LoadDocumentFromFile(PDF파일);
			
			string text = extractor.GetText();
			extractor.Dispose();

			return text;
		}

		
	}
}
