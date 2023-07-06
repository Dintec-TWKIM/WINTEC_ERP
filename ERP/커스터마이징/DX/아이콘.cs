using System.Drawing;
using Dintec;

namespace DX
{
	public class 아이콘
	{
		public static Image Empty_12x6 => Properties.Resources.empty_12x6;
		public static Image Empty_20x6 => Properties.Resources.empty_20x6;
		public static Image FolderExpand => Properties.Resources.folder_expand;
		public static Image IconPdf => Properties.Resources.icon_pdf_16x16;
		public static Image Unknown => Properties.Resources.icon_document_16x16;
		public static Image Popup => Properties.Resources.icon_view_detail_16x16;

		public static Image Arrow_16x16 => new Bitmap(Properties.Resources.icon_arrow, new Size(16, 16));

		public static Image 회전_18x18 => Properties.Resources.rotate_18x18;
		public static Image 가로뒤집기_18x18 => Properties.Resources.flip_x_18x18;
		public static Image 세로뒤집기_18x18 => Properties.Resources.flip_y_18x18;
		public static Image 더하기_18x18 => Properties.Resources.plus_18x18;
		public static Image 빼기_18x18 => Properties.Resources.minus_18x18;
		public static Image 이전_18x18 => Properties.Resources.prev_18x18;
		public static Image 다음_18x18 => Properties.Resources.next_18x18;

		public static Image 찾기_20x20 => Properties.Resources.find_20x20;

		public static Image 이미지없음 => Properties.Resources.no_image;


		public static Image 동그라미_초록_12x9 => Properties.Resources.icon_circle_green_9_12x9;
		public static Image 동그라미_초록_15x9 => Properties.Resources.icon_circle_green_9_15x9;


		public static Image 확장자(string extension)
		{
			extension = extension.Replace(".", "");
			extension = extension.ToLower();

			if (extension.In("xls", "xlsx"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_excel_csv_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("htm", "html"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_html_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("pdf"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_pdf_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("gif", "jpg", "jpeg", "png", "tif", "tiff"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_images_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("msg"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_email_16x16, new Size(16, 16));
				return bitmap;
			}

			return null;
		}

		public static Image GetExtensionForCell(string extension)
		{
			extension = extension.ToLower();

			if (extension.In("xls", "xlsx"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_excel_16_18x16);
				return bitmap;
			}
			else if (extension.In("pdf"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_pdf_16_18x16);
				return bitmap;
			}
			else if (extension.In("htm", "html"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_html_16_18x16);
				return bitmap;
			}
			else if (extension.In("gif", "jpg", "jpeg", "png", "tif", "tiff"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_image_16_18x16);
				return bitmap;
			}
			else if (extension.In("msg"))
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_email_16_18x16);
				return bitmap;
			}
			else
			{
				Bitmap bitmap = new Bitmap(Properties.Resources.icon_document_16_18x16);
				return bitmap;
			}

			return null;

		}
	}
}
