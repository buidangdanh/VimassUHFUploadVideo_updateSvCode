namespace VimassUHFUploadVideo
{
    partial class AutoVpass
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 20F);
            this.label10.Location = new System.Drawing.Point(25, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 32);
            this.label10.TabIndex = 21;
            this.label10.Text = "label10";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // AutoVpass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 292);
            this.Controls.Add(this.label10);
            this.Name = "AutoVpass";
            this.Text = "AutoVpass";
            this.Load += new System.EventHandler(this.AutoVpass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
    }
}