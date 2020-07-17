namespace Idea.ERMT.UserControls
{
    partial class IndexUserControl
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
        protected override void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpIndexUserControl = new System.Windows.Forms.TableLayoutPanel();
            this.wbIndex = new System.Windows.Forms.WebBrowser();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ttIUC = new System.Windows.Forms.ToolTip(this.components);
            this.tlpIndexUserControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpIndexUserControl
            // 
            this.tlpIndexUserControl.ColumnCount = 3;
            this.tlpIndexUserControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpIndexUserControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpIndexUserControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpIndexUserControl.Controls.Add(this.wbIndex, 0, 1);
            this.tlpIndexUserControl.Controls.Add(this.btnBack, 1, 0);
            this.tlpIndexUserControl.Controls.Add(this.btnForward, 2, 0);
            this.tlpIndexUserControl.Controls.Add(this.btnPrint, 0, 0);
            this.tlpIndexUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIndexUserControl.Location = new System.Drawing.Point(0, 0);
            this.tlpIndexUserControl.Name = "tlpIndexUserControl";
            this.tlpIndexUserControl.RowCount = 2;
            this.tlpIndexUserControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpIndexUserControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpIndexUserControl.Size = new System.Drawing.Size(691, 436);
            this.tlpIndexUserControl.TabIndex = 1;
            // 
            // wbIndex
            // 
            this.tlpIndexUserControl.SetColumnSpan(this.wbIndex, 3);
            this.wbIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbIndex.Location = new System.Drawing.Point(3, 53);
            this.wbIndex.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbIndex.Name = "wbIndex";
            this.wbIndex.Size = new System.Drawing.Size(685, 380);
            this.wbIndex.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.Location = new System.Drawing.Point(53, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(44, 44);
            this.btnBack.TabIndex = 2;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnForward.Location = new System.Drawing.Point(103, 3);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(44, 44);
            this.btnForward.TabIndex = 3;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Location = new System.Drawing.Point(3, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(44, 44);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // IndexUserControl
            // 
            this.Controls.Add(this.tlpIndexUserControl);
            this.Name = "IndexUserControl";
            this.Size = new System.Drawing.Size(691, 436);
            this.Load += new System.EventHandler(this.IndexUserControl_Load);
            this.tlpIndexUserControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbIndex;
        private System.Windows.Forms.TableLayoutPanel tlpIndexUserControl;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolTip ttIUC;

    }
}
