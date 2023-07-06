namespace cz
{
    partial class StepButton
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new Duzon.Common.Controls.LabelExt();
            this.button = new Duzon.Common.Controls.ButtonExt();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.BorderColor = System.Drawing.Color.Transparent;
            this.label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label.Location = new System.Drawing.Point(30, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(53, 17);
            this.label.TabIndex = 1;
            this.label.Text = "0";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.Color.Transparent;
            this.button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button.FlatAppearance.BorderSize = 0;
            this.button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button.ForeColor = System.Drawing.Color.Black;
            this.button.Image = global::cz.Properties.Resources.고객문의등록;
            this.button.Location = new System.Drawing.Point(0, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(113, 81);
            this.button.TabIndex = 0;
            this.button.Text = "고객문의등록";
            this.button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button.UseVisualStyleBackColor = false;
            // 
            // StepButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label);
            this.Controls.Add(this.button);
            this.Name = "StepButton";
            this.Size = new System.Drawing.Size(113, 81);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.ButtonExt button;
        private Duzon.Common.Controls.LabelExt label;
    }
}
