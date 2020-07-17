namespace Idea.ERMT.UserControls
{
    partial class FactorPreview
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
            this.htmlEditorControl1 = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.SuspendLayout();
            // 
            // htmlEditorControl1
            // 
            this.htmlEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlEditorControl1.InnerText = null;
            this.htmlEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.htmlEditorControl1.Name = "htmlEditorControl1";
            this.htmlEditorControl1.ReadOnly = true;
            this.htmlEditorControl1.Size = new System.Drawing.Size(820, 444);
            this.htmlEditorControl1.TabIndex = 26;
            this.htmlEditorControl1.ToolbarDock = System.Windows.Forms.DockStyle.None;
            // 
            // FactorPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 444);
            this.Controls.Add(this.htmlEditorControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FactorPreview";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.ConsultingServices.HtmlEditorControl htmlEditorControl1;
    }
}