namespace Idea.ERMT.UserControls
{
    partial class MapLegend
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
            this.tlpLegends = new System.Windows.Forms.TableLayoutPanel();
            this.lblFactorInfo = new System.Windows.Forms.Label();
            this.flpLegendItems = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpLegends.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLegends
            // 
            this.tlpLegends.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpLegends.ColumnCount = 1;
            this.tlpLegends.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLegends.Controls.Add(this.lblFactorInfo, 0, 0);
            this.tlpLegends.Controls.Add(this.flpLegendItems, 0, 1);
            this.tlpLegends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLegends.Location = new System.Drawing.Point(0, 0);
            this.tlpLegends.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLegends.Name = "tlpLegends";
            this.tlpLegends.RowCount = 2;
            this.tlpLegends.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpLegends.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLegends.Size = new System.Drawing.Size(337, 68);
            this.tlpLegends.TabIndex = 0;
            // 
            // lblFactorInfo
            // 
            this.lblFactorInfo.Location = new System.Drawing.Point(4, 1);
            this.lblFactorInfo.Name = "lblFactorInfo";
            this.lblFactorInfo.Size = new System.Drawing.Size(329, 40);
            this.lblFactorInfo.TabIndex = 0;
            this.lblFactorInfo.Text = "Factor info";
            this.lblFactorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpLegendItems
            // 
            this.flpLegendItems.AutoSize = true;
            this.flpLegendItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpLegendItems.Location = new System.Drawing.Point(1, 42);
            this.flpLegendItems.Margin = new System.Windows.Forms.Padding(0);
            this.flpLegendItems.Name = "flpLegendItems";
            this.flpLegendItems.Size = new System.Drawing.Size(335, 25);
            this.flpLegendItems.TabIndex = 1;
            // 
            // MapLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 68);
            this.Controls.Add(this.tlpLegends);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MapLegend";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MapLegend";
            this.tlpLegends.ResumeLayout(false);
            this.tlpLegends.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLegends;
        private System.Windows.Forms.Label lblFactorInfo;
        private System.Windows.Forms.FlowLayoutPanel flpLegendItems;
    }
}