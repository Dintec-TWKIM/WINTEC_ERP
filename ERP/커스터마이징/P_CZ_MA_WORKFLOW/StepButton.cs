using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Controls;

namespace cz
{
    public partial class StepButton : UserControl
    {
        public string StepText
        {
            get
            {
                return this.button.Text;
            }
            set
            {
                this.button.Text = value;
            }
        }

        public Image StepImage
        {
            get
            {
                return this.button.Image;
            }
            set
            {
                this.button.Image = value;
            }
        }

        public string StepCount
        {
            get
            {
                return this.label.Text;
            }
            set
            {
                this.label.Text = value;
            }
        }

        public Color StepBackColor
        {
            set
            {
                this.button.BackColor = value;
                this.label.BackColor = value;
            }
        }

        public string StepName
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        public StepButton()
        {
            InitializeComponent();

            this.button.Click += new EventHandler(this.button_Click);
            this.button.MouseEnter += new EventHandler(this.button_MouseEnter);
            this.button.MouseLeave += new EventHandler(this.button_MouseLeave);
            this.label.Click += new EventHandler(this.button_Click);
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            this.button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            this.button.FlatAppearance.BorderSize = 2;
            this.label.BackColor = this.button.FlatAppearance.MouseOverBackColor;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            this.button.FlatAppearance.BorderColor = Color.White;
            this.button.FlatAppearance.BorderSize = 0;
            this.label.BackColor = this.button.BackColor;
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
