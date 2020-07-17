namespace Idea.ERMT.Setup.UI
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.btnServer = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbEN = new System.Windows.Forms.PictureBox();
            this.pbFR = new System.Windows.Forms.PictureBox();
            this.pbES = new System.Windows.Forms.PictureBox();
            this.pbAR = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbES)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnServer
            // 
            this.btnServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(10)))));
            this.btnServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.btnServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServer.ForeColor = System.Drawing.Color.White;
            this.btnServer.Location = new System.Drawing.Point(53, 270);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(149, 66);
            this.btnServer.TabIndex = 1;
            this.btnServer.Text = "SERVER";
            this.btnServer.UseVisualStyleBackColor = false;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnClient
            // 
            this.btnClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(10)))));
            this.btnClient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.btnClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClient.ForeColor = System.Drawing.Color.White;
            this.btnClient.Location = new System.Drawing.Point(245, 270);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(149, 66);
            this.btnClient.TabIndex = 2;
            this.btnClient.Text = "CLIENT";
            this.btnClient.UseVisualStyleBackColor = false;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // lblInfo1
            // 
            this.lblInfo1.Location = new System.Drawing.Point(39, 151);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(383, 62);
            this.lblInfo1.TabIndex = 3;
            this.lblInfo1.Text = resources.GetString("lblInfo1.Text");
            this.lblInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo2
            // 
            this.lblInfo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo2.Location = new System.Drawing.Point(32, 210);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(390, 53);
            this.lblInfo2.TabIndex = 4;
            this.lblInfo2.Text = "Please select which version to install. Previous versions and data will be remove" +
    "d.";
            this.lblInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Idea.ERMT.Setup.UI.Properties.Resources.ERMTool_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(23, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(411, 108);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pbEN
            // 
            this.pbEN.BackgroundImage = global::Idea.ERMT.Setup.UI.Properties.Resources.language_en;
            this.pbEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbEN.Location = new System.Drawing.Point(119, 127);
            this.pbEN.Name = "pbEN";
            this.pbEN.Size = new System.Drawing.Size(35, 24);
            this.pbEN.TabIndex = 5;
            this.pbEN.TabStop = false;
            this.pbEN.Click += new System.EventHandler(this.pbEN_Click);
            // 
            // pbFR
            // 
            this.pbFR.BackgroundImage = global::Idea.ERMT.Setup.UI.Properties.Resources.language_fr;
            this.pbFR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbFR.Location = new System.Drawing.Point(239, 127);
            this.pbFR.Name = "pbFR";
            this.pbFR.Size = new System.Drawing.Size(35, 24);
            this.pbFR.TabIndex = 6;
            this.pbFR.TabStop = false;
            this.pbFR.Click += new System.EventHandler(this.pbFR_Click);
            // 
            // pbES
            // 
            this.pbES.BackgroundImage = global::Idea.ERMT.Setup.UI.Properties.Resources.language_es;
            this.pbES.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbES.Location = new System.Drawing.Point(179, 127);
            this.pbES.Name = "pbES";
            this.pbES.Size = new System.Drawing.Size(35, 24);
            this.pbES.TabIndex = 7;
            this.pbES.TabStop = false;
            this.pbES.Click += new System.EventHandler(this.pbES_Click);
            // 
            // pbAR
            // 
            this.pbAR.BackgroundImage = global::Idea.ERMT.Setup.UI.Properties.Resources.language_ar2;
            this.pbAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAR.Location = new System.Drawing.Point(299, 127);
            this.pbAR.Name = "pbAR";
            this.pbAR.Size = new System.Drawing.Size(35, 24);
            this.pbAR.TabIndex = 8;
            this.pbAR.TabStop = false;
            this.pbAR.Click += new System.EventHandler(this.pbAR_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(452, 347);
            this.Controls.Add(this.pbAR);
            this.Controls.Add(this.pbES);
            this.Controls.Add(this.pbFR);
            this.Controls.Add(this.pbEN);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.lblInfo1);
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Electoral Risk Management Tool Installer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbES)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.PictureBox pbEN;
        private System.Windows.Forms.PictureBox pbFR;
        private System.Windows.Forms.PictureBox pbES;
        private System.Windows.Forms.PictureBox pbAR;
    }
}

