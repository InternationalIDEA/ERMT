namespace Idea.ERMT.UserControls
{
    partial class EditRegion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRegion));
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.tlpScroll = new System.Windows.Forms.TableLayoutPanel();
            this.tvRegions = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmEditRegions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditRegionChangeName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditRegionAddChildRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRegionAddRoads = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRegionAddPOI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditRegionDeleteRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRegionDeleteAllChildRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpBase.SuspendLayout();
            this.tlpScroll.SuspendLayout();
            this.cmEditRegions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBase
            // 
            resources.ApplyResources(this.tlpBase, "tlpBase");
            this.tlpBase.Controls.Add(this.winformsMap1, 1, 0);
            this.tlpBase.Controls.Add(this.tlpScroll, 0, 0);
            this.tlpBase.Name = "tlpBase";
            // 
            // winformsMap1
            // 
            resources.ApplyResources(this.winformsMap1, "winformsMap1");
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790D;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.DesktopEdition.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // tlpScroll
            // 
            resources.ApplyResources(this.tlpScroll, "tlpScroll");
            this.tlpScroll.Controls.Add(this.tvRegions, 0, 2);
            this.tlpScroll.Controls.Add(this.label1, 0, 0);
            this.tlpScroll.Controls.Add(this.label2, 0, 1);
            this.tlpScroll.Name = "tlpScroll";
            // 
            // tvRegions
            // 
            resources.ApplyResources(this.tvRegions, "tvRegions");
            this.tlpScroll.SetColumnSpan(this.tvRegions, 2);
            this.tvRegions.HideSelection = false;
            this.tvRegions.Name = "tvRegions";
            this.tvRegions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvRegions_NodeMouseClick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmEditRegions
            // 
            resources.ApplyResources(this.cmEditRegions, "cmEditRegions");
            this.cmEditRegions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditRegionChangeName,
            this.toolStripSeparator1,
            this.tsmiEditRegionAddChildRegion,
            this.tsmiEditRegionAddRoads,
            this.tsmiEditRegionAddPOI,
            this.toolStripSeparator2,
            this.tsmiEditRegionDeleteRegion,
            this.tsmiEditRegionDeleteAllChildRegions});
            this.cmEditRegions.Name = "contextMenuStrip1";
            // 
            // tsmiEditRegionChangeName
            // 
            resources.ApplyResources(this.tsmiEditRegionChangeName, "tsmiEditRegionChangeName");
            this.tsmiEditRegionChangeName.Name = "tsmiEditRegionChangeName";
            this.tsmiEditRegionChangeName.Click += new System.EventHandler(this.tsmiEditRegionChangeName_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // tsmiEditRegionAddChildRegion
            // 
            resources.ApplyResources(this.tsmiEditRegionAddChildRegion, "tsmiEditRegionAddChildRegion");
            this.tsmiEditRegionAddChildRegion.Name = "tsmiEditRegionAddChildRegion";
            this.tsmiEditRegionAddChildRegion.Click += new System.EventHandler(this.tsmiEditRegionAddChildRegion_Click);
            // 
            // tsmiEditRegionAddRoads
            // 
            resources.ApplyResources(this.tsmiEditRegionAddRoads, "tsmiEditRegionAddRoads");
            this.tsmiEditRegionAddRoads.Name = "tsmiEditRegionAddRoads";
            this.tsmiEditRegionAddRoads.Click += new System.EventHandler(this.tsmiEditRegionAddRoadsAndPOI_Click);
            // 
            // tsmiEditRegionAddPOI
            // 
            resources.ApplyResources(this.tsmiEditRegionAddPOI, "tsmiEditRegionAddPOI");
            this.tsmiEditRegionAddPOI.Name = "tsmiEditRegionAddPOI";
            this.tsmiEditRegionAddPOI.Click += new System.EventHandler(this.tsmiEditRegionAddRoadsAndPOI_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tsmiEditRegionDeleteRegion
            // 
            resources.ApplyResources(this.tsmiEditRegionDeleteRegion, "tsmiEditRegionDeleteRegion");
            this.tsmiEditRegionDeleteRegion.Name = "tsmiEditRegionDeleteRegion";
            this.tsmiEditRegionDeleteRegion.Click += new System.EventHandler(this.tsmiEditRegionDeleteRegion_Click);
            // 
            // tsmiEditRegionDeleteAllChildRegions
            // 
            resources.ApplyResources(this.tsmiEditRegionDeleteAllChildRegions, "tsmiEditRegionDeleteAllChildRegions");
            this.tsmiEditRegionDeleteAllChildRegions.Name = "tsmiEditRegionDeleteAllChildRegions";
            this.tsmiEditRegionDeleteAllChildRegions.Click += new System.EventHandler(this.tsmiEditRegionDeleteAllChildRegions_Click);
            // 
            // EditRegion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpBase);
            this.Name = "EditRegion";
            this.tlpBase.ResumeLayout(false);
            this.tlpScroll.ResumeLayout(false);
            this.tlpScroll.PerformLayout();
            this.cmEditRegions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.TableLayoutPanel tlpScroll;
        private ThinkGeo.MapSuite.DesktopEdition.WinformsMap winformsMap1;
        private System.Windows.Forms.ContextMenuStrip cmEditRegions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionAddRoads;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionAddPOI;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionAddChildRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionChangeName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionDeleteRegion;
        private System.Windows.Forms.TreeView tvRegions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRegionDeleteAllChildRegions;
    }
}
