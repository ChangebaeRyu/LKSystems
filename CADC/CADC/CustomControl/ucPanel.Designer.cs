namespace CADC.CustomControl
{
    partial class ucPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDown = new System.Windows.Forms.Label();
            this.btnTitle = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDown.ForeColor = System.Drawing.Color.White;
            this.btnDown.Location = new System.Drawing.Point(25, 66);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(30, 30);
            this.btnDown.TabIndex = 3;
            this.btnDown.Tag = "3";
            this.btnDown.Text = "▼";
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnTitle
            // 
            this.btnTitle.BackColor = System.Drawing.SystemColors.Control;
            this.btnTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTitle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTitle.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTitle.ForeColor = System.Drawing.Color.Black;
            this.btnTitle.Location = new System.Drawing.Point(3, 0);
            this.btnTitle.Name = "btnTitle";
            this.btnTitle.Size = new System.Drawing.Size(144, 30);
            this.btnTitle.TabIndex = 2;
            this.btnTitle.Text = "DRIVE - 1";
            this.btnTitle.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUp.ForeColor = System.Drawing.Color.White;
            this.btnUp.Location = new System.Drawing.Point(95, 66);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(30, 30);
            this.btnUp.TabIndex = 3;
            this.btnUp.Tag = "3";
            this.btnUp.Text = "▲";
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.CornflowerBlue;
            this.labName.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labName.ForeColor = System.Drawing.Color.White;
            this.labName.Location = new System.Drawing.Point(3, 33);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(75, 30);
            this.labName.TabIndex = 6;
            this.labName.Text = "희망온도";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labValue
            // 
            this.labValue.BackColor = System.Drawing.Color.CornflowerBlue;
            this.labValue.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labValue.ForeColor = System.Drawing.Color.White;
            this.labValue.Location = new System.Drawing.Point(82, 33);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(65, 30);
            this.labValue.TabIndex = 6;
            this.labValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnTitle);
            this.Name = "ucPanel";
            this.Size = new System.Drawing.Size(150, 100);
            this.Load += new System.EventHandler(this.ucPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label btnDown;
        public System.Windows.Forms.Button btnTitle;
        public System.Windows.Forms.Label btnUp;
        public System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labValue;
    }
}
