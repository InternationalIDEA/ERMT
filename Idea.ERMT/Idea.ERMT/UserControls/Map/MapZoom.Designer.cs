namespace Idea.ERMT.UserControls
{
    partial class MapZoom
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
            this.pbZoomOut = new System.Windows.Forms.PictureBox();
            this.pbZoomIn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomIn)).BeginInit();
            this.SuspendLayout();
            // 
            // pbZoomOut
            // 
            this.pbZoomOut.BackgroundImage = global::Idea.ERMT.Properties.Resources.zoomout;
            this.pbZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbZoomOut.InitialImage = null;
            this.pbZoomOut.Location = new System.Drawing.Point(0, 0);
            this.pbZoomOut.Name = "pbZoomOut";
            this.pbZoomOut.Size = new System.Drawing.Size(15, 15);
            this.pbZoomOut.TabIndex = 0;
            this.pbZoomOut.TabStop = false;
            this.pbZoomOut.Click += new System.EventHandler(this.pbZoomOut_Click);
            // 
            // pbZoomIn
            // 
            this.pbZoomIn.BackgroundImage = global::Idea.ERMT.Properties.Resources.zoomin;
            this.pbZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbZoomIn.Location = new System.Drawing.Point(17, 0);
            this.pbZoomIn.Name = "pbZoomIn";
            this.pbZoomIn.Size = new System.Drawing.Size(15, 15);
            this.pbZoomIn.TabIndex = 1;
            this.pbZoomIn.TabStop = false;
            this.pbZoomIn.Click += new System.EventHandler(this.pbZoomIn_Click);
            // 
            // MapZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pbZoomIn);
            this.Controls.Add(this.pbZoomOut);
            this.Name = "MapZoom";
            this.Size = new System.Drawing.Size(34, 17);
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbZoomOut;
        private System.Windows.Forms.PictureBox pbZoomIn;
    }
}
