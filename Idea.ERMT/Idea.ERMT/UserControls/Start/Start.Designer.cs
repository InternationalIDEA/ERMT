namespace Idea.ERMT.UserControls
{
    partial class Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnKnowledgeResources = new System.Windows.Forms.Button();
            this.btnPreventionAndMitigation = new System.Windows.Forms.Button();
            this.btnOpenModel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbModels = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKnowledgeResources
            // 
            resources.ApplyResources(this.btnKnowledgeResources, "btnKnowledgeResources");
            this.btnKnowledgeResources.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnKnowledgeResources.FlatAppearance.BorderSize = 0;
            this.btnKnowledgeResources.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnKnowledgeResources.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnKnowledgeResources.Name = "btnKnowledgeResources";
            this.toolTip1.SetToolTip(this.btnKnowledgeResources, resources.GetString("btnKnowledgeResources.ToolTip"));
            this.btnKnowledgeResources.UseVisualStyleBackColor = true;
            this.btnKnowledgeResources.Click += new System.EventHandler(this.btnKnowledgeResources_Click);
            this.btnKnowledgeResources.MouseEnter += new System.EventHandler(this.btnKnowledgeResources_MouseEnter);
            this.btnKnowledgeResources.MouseLeave += new System.EventHandler(this.btnKnowledgeResources_MouseLeave);
            // 
            // btnPreventionAndMitigation
            // 
            resources.ApplyResources(this.btnPreventionAndMitigation, "btnPreventionAndMitigation");
            this.btnPreventionAndMitigation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPreventionAndMitigation.FlatAppearance.BorderSize = 0;
            this.btnPreventionAndMitigation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnPreventionAndMitigation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnPreventionAndMitigation.Name = "btnPreventionAndMitigation";
            this.toolTip1.SetToolTip(this.btnPreventionAndMitigation, resources.GetString("btnPreventionAndMitigation.ToolTip"));
            this.btnPreventionAndMitigation.UseVisualStyleBackColor = true;
            this.btnPreventionAndMitigation.Click += new System.EventHandler(this.btnPreventionAndMitigation_Click);
            this.btnPreventionAndMitigation.MouseEnter += new System.EventHandler(this.btnPreventionAndMitigation_MouseEnter);
            this.btnPreventionAndMitigation.MouseLeave += new System.EventHandler(this.btnPreventionAndMitigation_MouseLeave);
            // 
            // btnOpenModel
            // 
            resources.ApplyResources(this.btnOpenModel, "btnOpenModel");
            this.btnOpenModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnOpenModel.FlatAppearance.BorderSize = 0;
            this.btnOpenModel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnOpenModel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnOpenModel.Name = "btnOpenModel";
            this.toolTip1.SetToolTip(this.btnOpenModel, resources.GetString("btnOpenModel.ToolTip"));
            this.btnOpenModel.UseVisualStyleBackColor = true;
            this.btnOpenModel.Click += new System.EventHandler(this.btnOpenModel_Click);
            this.btnOpenModel.MouseEnter += new System.EventHandler(this.btnOpenModel_MouseEnter);
            this.btnOpenModel.MouseLeave += new System.EventHandler(this.btnOpenModel_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.Controls.Add(this.btnKnowledgeResources, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPreventionAndMitigation, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenModel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbModels, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // cbModels
            // 
            resources.ApplyResources(this.cbModels, "cbModels");
            this.cbModels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel1.SetColumnSpan(this.cbModels, 5);
            this.cbModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModels.FormattingEnabled = true;
            this.cbModels.Name = "cbModels";
            this.toolTip1.SetToolTip(this.cbModels, resources.GetString("cbModels.ToolTip"));
            this.cbModels.SelectedIndexChanged += new System.EventHandler(this.cbModels_SelectedIndexChanged);
            // 
            // Start
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Start";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.Start_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPreventionAndMitigation;
        private System.Windows.Forms.Button btnOpenModel;
        private System.Windows.Forms.Button btnKnowledgeResources;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbModels;

    }
}
