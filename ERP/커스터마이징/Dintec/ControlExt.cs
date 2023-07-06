using Duzon.Common.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Controls;
using System.Windows;
using Duzon.Common.BpControls;

namespace Dintec
{
	public static class ControlExt
	{
		public static void SetTextBoxDefault(Control control)
		{
			foreach (var o in GetAll(control, typeof(TextBoxExt)))
			{
				TextBoxExt textBox = (TextBoxExt)o;

				if (textBox.Name == "tbx포커스")
				{ 
					textBox.Left = -1000;
					continue;
				}

				if (!textBox.Multiline)
				{
					textBox.Multiline = true;
					textBox.Height = 20;
					textBox.KeyDown += TextBox_KeyDown;
					textBox.TextChanged += TextBox_TextChanged;
				}

				textBox.Enter += TextBox_GotFocus;
				textBox.Leave += TextBox_LostFocus;
			}		
		}

		public static IEnumerable<Control> GetAll(Control control, Type type = null)
		{
			var controls = control.Controls.Cast<Control>();
			//check the all value, if true then get all the controls
			//otherwise get the controls of the specified type
			if (type == null)
				return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls);
			else
				return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
		}

		private static void TextBox_GotFocus(object sender, EventArgs e)
		{
			TextBoxExt textBox = (TextBoxExt)sender;

			if (textBox.Tag == null || textBox.Tag.ToString() == "")
				textBox.Tag = "BorderColor:" + textBox.BorderColor.ToArgb();

			textBox.BorderColor = Constant.FocusedBorderColor;
		}

		private static void TextBox_LostFocus(object sender, EventArgs e)
		{
			TextBoxExt textBox = (TextBoxExt)sender;

			if (textBox.Tag.ToString().IndexOf("BorderColor:") == 0)
			{
				textBox.BorderColor = Color.FromArgb(GetTo.Int(textBox.Tag.ToString().Replace("BorderColor:", "")));
				textBox.Tag = "";
			}
			else
				textBox.BorderColor = Constant.UnfocusedBorderColor;
		}
		
		private static void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				e.SuppressKeyPress = true;
		}

		private static void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBoxExt textBox = (TextBoxExt)sender;

			if (textBox.Text.IndexOf(Environment.NewLine) >= 0)
				textBox.Text = textBox.Text.Substring(0, textBox.Text.IndexOf(Environment.NewLine));
		}
	}
}
