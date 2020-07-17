namespace Idea.ERMT.UserControls
{
    partial class MapLegendItem
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
            this.pnlLegendItemColor = new System.Windows.Forms.Panel();
            this.lblLegendItemValue = new System.Windows.Forms.Label();
            this.lblCumulativeTextColor = new System.Windows.Forms.Label();
            this.pnlLegendItemColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLegendItemColor
            // 
            this.pnlLegendItemColor.Controls.Add(this.lblCumulativeTextColor);
            this.pnlLegendItemColor.Location = new System.Drawing.Point(1, 2);
            this.pnlLegendItemColor.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLegendItemColor.Name = "pnlLegendItemColor";
            this.pnlLegendItemColor.Size = new System.Drawing.Size(17, 15);
            this.pnlLegendItemColor.TabIndex = 0;
            // 
            // lblLegendItemValue
            // 
            this.lblLegendItemValue.AutoSize = true;
            this.lblLegendItemValue.Location = new System.Drawing.Point(17, 4);
            this.lblLegendItemValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblLegendItemValue.Name = "lblLegendItemValue";
            this.lblLegendItemValue.Size = new System.Drawing.Size(13, 13);
            this.lblLegendItemValue.TabIndex = 1;
            this.lblLegendItemValue.Text = "1";
            // 
            // lblCumulativeTextColor
            // 
            this.lblCumulativeTextColor.AutoSize = true;
            this.lblCumulativeTextColor.Location = new System.Drawing.Point(3, 2);
            this.lblCumulativeTextColor.Name = "lblCumulativeTextColor";
            this.lblCumulativeTextColor.Size = new System.Drawing.Size(14, 13);
            this.lblCumulativeTextColor.TabIndex = 0;
            this.lblCumulativeTextColor.Text = "#";
            // 
            // MapLegendItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLegendItemColor);
            this.Controls.Add(this.lblLegendItemValue);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MapLegendItem";
            this.Size = new System.Drawing.Size(33, 19);
            this.pnlLegendItemColor.ResumeLayout(false);
            this.pnlLegendItemColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLegendItemColor;
        private System.Windows.Forms.Label lblLegendItemValue;
        private System.Windows.Forms.Label lblCumulativeTextColor;
    }
}
