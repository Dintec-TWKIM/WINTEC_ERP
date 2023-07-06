using System.Drawing;

namespace Dintec
{
	public class Icons
	{
		//global::cz.Properties.Resources.empty__20x6_
		//public static Image Empty_12x6
		public static Image Empty_12x6 { get { return global::Dintec.Properties.Resources.empty_12x6; } }
		public static Image Empty_20x6 { get { return global::Dintec.Properties.Resources.empty_20x6; } }
		public static Image FolderExpand { get { return global::Dintec.Properties.Resources.folder_expand; } }
		public static Image IconPdf { get { return global::Dintec.Properties.Resources.icon_pdf_16x16; } }
		public static Image Unknown { get { return global::Dintec.Properties.Resources.icon_document_16x16; } }
		public static Image Popup { get { return global::Dintec.Properties.Resources.icon_view_detail_16x16; } }

		public static Image Arrow_16x16
		{
			get
			{
				//Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_arrow, new Size(16, 16));
				return new Bitmap(global::Dintec.Properties.Resources.icon_arrow, new Size(16, 16));
			}
		}

		public static Image GetExtension(string extension)
		{
			extension = extension.Replace(".", "");
			extension = extension.ToLower();

			if (extension.In("xls", "xlsx"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_excel_csv_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("htm", "html"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_html_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("pdf"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_pdf_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("gif", "jpg", "jpeg", "png", "tif", "tiff"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_images_16x16, new Size(16, 16));
				return bitmap;
			}
			else if (extension.In("msg"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_email_16x16, new Size(16, 16));
				return bitmap;
			}

			return null;
		}

		public static Image GetExtensionForCell(string extension)
		{
			extension = extension.ToLower();

			if (extension.In("xls", "xlsx"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_excel_16_18x16);
				return bitmap;
			}
			else if (extension.In("pdf"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_pdf_16_18x16);
				return bitmap;
			}
			else if (extension.In("htm", "html"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_html_16_18x16);
				return bitmap;
			}
			else if (extension.In("gif", "jpg", "jpeg", "png", "tif", "tiff"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_image_16_18x16);
				return bitmap;
			}
			else if (extension.In("msg"))
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_email_16_18x16);
				return bitmap;
			}
			else
			{
				Bitmap bitmap = new Bitmap(global::Dintec.Properties.Resources.icon_document_16_18x16);
				return bitmap;
			}

			return null;

		}
	}
}
