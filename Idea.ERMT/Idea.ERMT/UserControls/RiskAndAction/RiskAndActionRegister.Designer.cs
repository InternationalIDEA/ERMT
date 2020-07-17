using Idea.Facade;
namespace Idea.ERMT.UserControls
{
    partial class RiskAndActionRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiskAndActionRegister));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllChildRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllRegionsOnThisLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deselectAllChildRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllRegionsOnThisLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllChildRegionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllRegionsOnThisLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deselectAllChildRegionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllRegionsOnThisLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAlerts = new Idea.ERMT.UserControls.DataGridViewExtended();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElectoralPhase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvRegions = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.TreeMenu.SuspendLayout();
            this.NodeMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeMenu
            // 
            resources.ApplyResources(this.TreeMenu, "TreeMenu");
            this.TreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllChildRegionsToolStripMenuItem,
            this.selectAllRegionsOnThisLevelToolStripMenuItem,
            this.toolStripSeparator1,
            this.deselectAllChildRegionsToolStripMenuItem,
            this.deselectAllRegionsOnThisLevelToolStripMenuItem});
            this.TreeMenu.Name = "TreeMenu";
            // 
            // selectAllChildRegionsToolStripMenuItem
            // 
            resources.ApplyResources(this.selectAllChildRegionsToolStripMenuItem, "selectAllChildRegionsToolStripMenuItem");
            this.selectAllChildRegionsToolStripMenuItem.Name = "selectAllChildRegionsToolStripMenuItem";
            this.selectAllChildRegionsToolStripMenuItem.Click += new System.EventHandler(this.selectAllChildRegionsToolStripMenuItem_Click);
            // 
            // selectAllRegionsOnThisLevelToolStripMenuItem
            // 
            resources.ApplyResources(this.selectAllRegionsOnThisLevelToolStripMenuItem, "selectAllRegionsOnThisLevelToolStripMenuItem");
            this.selectAllRegionsOnThisLevelToolStripMenuItem.Name = "selectAllRegionsOnThisLevelToolStripMenuItem";
            this.selectAllRegionsOnThisLevelToolStripMenuItem.Click += new System.EventHandler(this.selectAllRegionsOnThisLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // deselectAllChildRegionsToolStripMenuItem
            // 
            resources.ApplyResources(this.deselectAllChildRegionsToolStripMenuItem, "deselectAllChildRegionsToolStripMenuItem");
            this.deselectAllChildRegionsToolStripMenuItem.Name = "deselectAllChildRegionsToolStripMenuItem";
            this.deselectAllChildRegionsToolStripMenuItem.Click += new System.EventHandler(this.deselectAllChildRegionsToolStripMenuItem_Click);
            // 
            // deselectAllRegionsOnThisLevelToolStripMenuItem
            // 
            resources.ApplyResources(this.deselectAllRegionsOnThisLevelToolStripMenuItem, "deselectAllRegionsOnThisLevelToolStripMenuItem");
            this.deselectAllRegionsOnThisLevelToolStripMenuItem.Name = "deselectAllRegionsOnThisLevelToolStripMenuItem";
            this.deselectAllRegionsOnThisLevelToolStripMenuItem.Click += new System.EventHandler(this.deselectAllRegionsOnThisLevelToolStripMenuItem_Click);
            // 
            // NodeMenu
            // 
            resources.ApplyResources(this.NodeMenu, "NodeMenu");
            this.NodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllChildRegionsToolStripMenuItem1,
            this.selectAllRegionsOnThisLevelToolStripMenuItem1,
            this.toolStripSeparator2,
            this.deselectAllChildRegionsToolStripMenuItem1,
            this.deselectAllRegionsOnThisLevelToolStripMenuItem1});
            this.NodeMenu.Name = "NodeMenu";
            // 
            // selectAllChildRegionsToolStripMenuItem1
            // 
            resources.ApplyResources(this.selectAllChildRegionsToolStripMenuItem1, "selectAllChildRegionsToolStripMenuItem1");
            this.selectAllChildRegionsToolStripMenuItem1.Name = "selectAllChildRegionsToolStripMenuItem1";
            this.selectAllChildRegionsToolStripMenuItem1.Click += new System.EventHandler(this.selectAllChildRegionsToolStripMenuItem_Click);
            // 
            // selectAllRegionsOnThisLevelToolStripMenuItem1
            // 
            resources.ApplyResources(this.selectAllRegionsOnThisLevelToolStripMenuItem1, "selectAllRegionsOnThisLevelToolStripMenuItem1");
            this.selectAllRegionsOnThisLevelToolStripMenuItem1.Name = "selectAllRegionsOnThisLevelToolStripMenuItem1";
            this.selectAllRegionsOnThisLevelToolStripMenuItem1.Click += new System.EventHandler(this.selectAllRegionsOnThisLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // deselectAllChildRegionsToolStripMenuItem1
            // 
            resources.ApplyResources(this.deselectAllChildRegionsToolStripMenuItem1, "deselectAllChildRegionsToolStripMenuItem1");
            this.deselectAllChildRegionsToolStripMenuItem1.Name = "deselectAllChildRegionsToolStripMenuItem1";
            this.deselectAllChildRegionsToolStripMenuItem1.Click += new System.EventHandler(this.deselectAllChildRegionsToolStripMenuItem_Click);
            // 
            // deselectAllRegionsOnThisLevelToolStripMenuItem1
            // 
            resources.ApplyResources(this.deselectAllRegionsOnThisLevelToolStripMenuItem1, "deselectAllRegionsOnThisLevelToolStripMenuItem1");
            this.deselectAllRegionsOnThisLevelToolStripMenuItem1.Name = "deselectAllRegionsOnThisLevelToolStripMenuItem1";
            this.deselectAllRegionsOnThisLevelToolStripMenuItem1.Click += new System.EventHandler(this.deselectAllRegionsOnThisLevelToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.dgvAlerts, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // dgvAlerts
            // 
            resources.ApplyResources(this.dgvAlerts, "dgvAlerts");
            this.dgvAlerts.AllowUserToAddRows = false;
            this.dgvAlerts.AllowUserToDeleteRows = false;
            this.dgvAlerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlerts.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlerts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Model,
            this.Title,
            this.ElectoralPhase,
            this.Created,
            this.Modified,
            this.Status});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlerts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlerts.MultiSelect = false;
            this.dgvAlerts.Name = "dgvAlerts";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlerts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlerts.RowTemplate.Height = 24;
            this.dgvAlerts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlerts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlerts_CellDoubleClick);
            this.dgvAlerts.SelectionChanged += new System.EventHandler(this.dgvAlerts_SelectionChanged);
            this.dgvAlerts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAlerts_KeyDown);
            // 
            // Code
            // 
            resources.ApplyResources(this.Code, "Code");
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Model
            // 
            resources.ApplyResources(this.Model, "Model");
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // ElectoralPhase
            // 
            resources.ApplyResources(this.ElectoralPhase, "ElectoralPhase");
            this.ElectoralPhase.Name = "ElectoralPhase";
            this.ElectoralPhase.ReadOnly = true;
            // 
            // Created
            // 
            resources.ApplyResources(this.Created, "Created");
            this.Created.Name = "Created";
            this.Created.ReadOnly = true;
            // 
            // Modified
            // 
            resources.ApplyResources(this.Modified, "Modified");
            this.Modified.Name = "Modified";
            this.Modified.ReadOnly = true;
            // 
            // Status
            // 
            resources.ApplyResources(this.Status, "Status");
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnPrint, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddNew, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnEdit, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Tag = "";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAddNew
            // 
            resources.ApplyResources(this.btnAddNew, "btnAddNew");
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Tag = "";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Tag = "";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rbAll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbInactive);
            this.panel1.Controls.Add(this.rbActive);
            this.panel1.Name = "panel1";
            // 
            // rbAll
            // 
            resources.ApplyResources(this.rbAll, "rbAll");
            this.rbAll.Checked = true;
            this.rbAll.Name = "rbAll";
            this.rbAll.TabStop = true;
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.Click += new System.EventHandler(this.rbAll_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // rbInactive
            // 
            resources.ApplyResources(this.rbInactive, "rbInactive");
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // rbActive
            // 
            resources.ApplyResources(this.rbActive, "rbActive");
            this.rbActive.Name = "rbActive";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.CheckedChanged += new System.EventHandler(this.rbActive_CheckedChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.tvRegions);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Name = "panel2";
            // 
            // tvRegions
            // 
            resources.ApplyResources(this.tvRegions, "tvRegions");
            this.tvRegions.CheckBoxes = true;
            this.tvRegions.ContextMenuStrip = this.TreeMenu;
            this.tvRegions.HideSelection = false;
            this.tvRegions.Name = "tvRegions";
            this.tvRegions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvRegions_AfterCheck);
            this.tvRegions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvRegions_MouseUp);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RiskAndActionRegister
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RiskAndActionRegister";
            this.Load += new System.EventHandler(this.RiskAndActionRegister_Load);
            this.TreeMenu.ResumeLayout(false);
            this.NodeMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Idea.ERMT.UserControls.DataGridViewExtended dgvAlerts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvRegions;
        private System.Windows.Forms.ContextMenuStrip TreeMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllChildRegionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectAllChildRegionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllRegionsOnThisLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectAllRegionsOnThisLevelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip NodeMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllChildRegionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deselectAllChildRegionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllRegionsOnThisLevelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deselectAllRegionsOnThisLevelToolStripMenuItem1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElectoralPhase;
        private System.Windows.Forms.DataGridViewTextBoxColumn Created;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modified;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;

    }
}
