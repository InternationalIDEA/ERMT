namespace Idea.ERMT.UserControls
{
    partial class MapLegendMarkerItem
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
            this.lblLegendItemValue = new System.Windows.Forms.Label();
            this.pbMarkerTypeImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMarkerTypeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLegendItemValue
            // 
            this.lblLegendItemValue.AutoSize = true;
            this.lblLegendItemValue.Location = new System.Drawing.Point(38, 11);
            this.lblLegendItemValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblLegendItemValue.Name = "lblLegendItemValue";
            this.lblLegendItemValue.Size = new System.Drawing.Size(13, 13);
            this.lblLegendItemValue.TabIndex = 1;
            this.lblLegendItemValue.Text = "1";
            // 
            // pbMarkerTypeImage
            // 
            this.pbMarkerTypeImage.Location = new System.Drawing.Point(1, 2);
            this.pbMarkerTypeImage.Name = "pbMarkerTypeImage";
            this.pbMarkerTypeImage.Size = new System.Drawing.Size(34, 34);
            this.pbMarkerTypeImage.TabIndex = 2;
            this.pbMarkerTypeImage.TabStop = false;
            // 
            // MapLegendMarkerItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMarkerTypeImage);
            this.Controls.Add(this.lblLegendItemValue);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MapLegendMarkerItem";
            this.Size = new System.Drawing.Size(150, 38);
            ((System.ComponentModel.ISupportInitialize)(this.pbMarkerTypeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLegendItemValue;
        private System.Windows.Forms.PictureBox pbMarkerTypeImage;
    }
}
