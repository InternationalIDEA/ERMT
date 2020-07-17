namespace Idea.ERMT.UserControls
{
    partial class ElectoralCycleModifyPhase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElectoralCycleModifyPhase));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPhase = new System.Windows.Forms.Label();
            this.cbPhases = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPhaseName = new System.Windows.Forms.Label();
            this.txtPhaseName = new System.Windows.Forms.TextBox();
            this.columnsTabControl = new System.Windows.Forms.TabControl();
            this.Col1BulletsTabPage = new System.Windows.Forms.TabPage();
            this.column1BulletList = new Idea.ERMT.UserControls.ElectoralCycleBulletList();
            this.Col1TextTabPage = new System.Windows.Forms.TabPage();
            this.col1HtmlEditorControl = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.Col2BulletsTabPage = new System.Windows.Forms.TabPage();
            this.column2BulletList = new Idea.ERMT.UserControls.ElectoralCycleBulletList();
            this.Col2TextTabPage = new System.Windows.Forms.TabPage();
            this.col2HtmlEditorControl = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.Col3BulletsTabPage = new System.Windows.Forms.TabPage();
            this.column3BulletList = new Idea.ERMT.UserControls.ElectoralCycleBulletList();
            this.Col3TextTabPage = new System.Windows.Forms.TabPage();
            this.col3HtmlEditorControl = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.practitionersTipsTabPage = new System.Windows.Forms.TabPage();
            this.practitionersTipsHtmlEditorControl = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.columnsTabControl.SuspendLayout();
            this.Col1BulletsTabPage.SuspendLayout();
            this.Col1TextTabPage.SuspendLayout();
            this.Col2BulletsTabPage.SuspendLayout();
            this.Col2TextTabPage.SuspendLayout();
            this.Col3BulletsTabPage.SuspendLayout();
            this.Col3TextTabPage.SuspendLayout();
            this.practitionersTipsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblPhase, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbPhases, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblPhaseName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPhaseName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.columnsTabControl, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblPhase
            // 
            resources.ApplyResources(this.lblPhase, "lblPhase");
            this.lblPhase.Name = "lblPhase";
            // 
            // cbPhases
            // 
            resources.ApplyResources(this.cbPhases, "cbPhases");
            this.cbPhases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhases.FormattingEnabled = true;
            this.cbPhases.Name = "cbPhases";
            this.cbPhases.SelectedIndexChanged += new System.EventHandler(this.cbPhases_SelectedIndexChanged);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPhaseName
            // 
            resources.ApplyResources(this.lblPhaseName, "lblPhaseName");
            this.lblPhaseName.Name = "lblPhaseName";
            // 
            // txtPhaseName
            // 
            resources.ApplyResources(this.txtPhaseName, "txtPhaseName");
            this.txtPhaseName.Name = "txtPhaseName";
            // 
            // columnsTabControl
            // 
            resources.ApplyResources(this.columnsTabControl, "columnsTabControl");
            this.tableLayoutPanel1.SetColumnSpan(this.columnsTabControl, 3);
            this.columnsTabControl.Controls.Add(this.Col1BulletsTabPage);
            this.columnsTabControl.Controls.Add(this.Col1TextTabPage);
            this.columnsTabControl.Controls.Add(this.Col2BulletsTabPage);
            this.columnsTabControl.Controls.Add(this.Col2TextTabPage);
            this.columnsTabControl.Controls.Add(this.Col3BulletsTabPage);
            this.columnsTabControl.Controls.Add(this.Col3TextTabPage);
            this.columnsTabControl.Controls.Add(this.practitionersTipsTabPage);
            this.columnsTabControl.Name = "columnsTabControl";
            this.columnsTabControl.SelectedIndex = 0;
            // 
            // Col1BulletsTabPage
            // 
            resources.ApplyResources(this.Col1BulletsTabPage, "Col1BulletsTabPage");
            this.Col1BulletsTabPage.Controls.Add(this.column1BulletList);
            this.Col1BulletsTabPage.Name = "Col1BulletsTabPage";
            this.Col1BulletsTabPage.UseVisualStyleBackColor = true;
            // 
            // column1BulletList
            // 
            resources.ApplyResources(this.column1BulletList, "column1BulletList");
            this.column1BulletList.Bullets = null;
            this.column1BulletList.Name = "column1BulletList";
            this.column1BulletList.PhaseBulletsIDsToDelete = ((System.Collections.Generic.List<int>)(resources.GetObject("column1BulletList.PhaseBulletsIDsToDelete")));
            // 
            // Col1TextTabPage
            // 
            resources.ApplyResources(this.Col1TextTabPage, "Col1TextTabPage");
            this.Col1TextTabPage.Controls.Add(this.col1HtmlEditorControl);
            this.Col1TextTabPage.Name = "Col1TextTabPage";
            this.Col1TextTabPage.UseVisualStyleBackColor = true;
            // 
            // col1HtmlEditorControl
            // 
            resources.ApplyResources(this.col1HtmlEditorControl, "col1HtmlEditorControl");
            this.col1HtmlEditorControl.InnerText = null;
            this.col1HtmlEditorControl.Name = "col1HtmlEditorControl";
            // 
            // Col2BulletsTabPage
            // 
            resources.ApplyResources(this.Col2BulletsTabPage, "Col2BulletsTabPage");
            this.Col2BulletsTabPage.Controls.Add(this.column2BulletList);
            this.Col2BulletsTabPage.Name = "Col2BulletsTabPage";
            this.Col2BulletsTabPage.UseVisualStyleBackColor = true;
            // 
            // column2BulletList
            // 
            resources.ApplyResources(this.column2BulletList, "column2BulletList");
            this.column2BulletList.Bullets = null;
            this.column2BulletList.Name = "column2BulletList";
            this.column2BulletList.PhaseBulletsIDsToDelete = ((System.Collections.Generic.List<int>)(resources.GetObject("column2BulletList.PhaseBulletsIDsToDelete")));
            // 
            // Col2TextTabPage
            // 
            resources.ApplyResources(this.Col2TextTabPage, "Col2TextTabPage");
            this.Col2TextTabPage.Controls.Add(this.col2HtmlEditorControl);
            this.Col2TextTabPage.Name = "Col2TextTabPage";
            this.Col2TextTabPage.UseVisualStyleBackColor = true;
            // 
            // col2HtmlEditorControl
            // 
            resources.ApplyResources(this.col2HtmlEditorControl, "col2HtmlEditorControl");
            this.col2HtmlEditorControl.InnerText = null;
            this.col2HtmlEditorControl.Name = "col2HtmlEditorControl";
            // 
            // Col3BulletsTabPage
            // 
            resources.ApplyResources(this.Col3BulletsTabPage, "Col3BulletsTabPage");
            this.Col3BulletsTabPage.Controls.Add(this.column3BulletList);
            this.Col3BulletsTabPage.Name = "Col3BulletsTabPage";
            this.Col3BulletsTabPage.UseVisualStyleBackColor = true;
            // 
            // column3BulletList
            // 
            resources.ApplyResources(this.column3BulletList, "column3BulletList");
            this.column3BulletList.Bullets = null;
            this.column3BulletList.Name = "column3BulletList";
            this.column3BulletList.PhaseBulletsIDsToDelete = ((System.Collections.Generic.List<int>)(resources.GetObject("column3BulletList.PhaseBulletsIDsToDelete")));
            // 
            // Col3TextTabPage
            // 
            resources.ApplyResources(this.Col3TextTabPage, "Col3TextTabPage");
            this.Col3TextTabPage.Controls.Add(this.col3HtmlEditorControl);
            this.Col3TextTabPage.Name = "Col3TextTabPage";
            this.Col3TextTabPage.UseVisualStyleBackColor = true;
            // 
            // col3HtmlEditorControl
            // 
            resources.ApplyResources(this.col3HtmlEditorControl, "col3HtmlEditorControl");
            this.col3HtmlEditorControl.InnerText = null;
            this.col3HtmlEditorControl.Name = "col3HtmlEditorControl";
            // 
            // practitionersTipsTabPage
            // 
            resources.ApplyResources(this.practitionersTipsTabPage, "practitionersTipsTabPage");
            this.practitionersTipsTabPage.Controls.Add(this.practitionersTipsHtmlEditorControl);
            this.practitionersTipsTabPage.Name = "practitionersTipsTabPage";
            this.practitionersTipsTabPage.UseVisualStyleBackColor = true;
            // 
            // practitionersTipsHtmlEditorControl
            // 
            resources.ApplyResources(this.practitionersTipsHtmlEditorControl, "practitionersTipsHtmlEditorControl");
            this.practitionersTipsHtmlEditorControl.InnerText = null;
            this.practitionersTipsHtmlEditorControl.Name = "practitionersTipsHtmlEditorControl";
            // 
            // ElectoralCycleModifyPhase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ElectoralCycleModifyPhase";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.columnsTabControl.ResumeLayout(false);
            this.Col1BulletsTabPage.ResumeLayout(false);
            this.Col1TextTabPage.ResumeLayout(false);
            this.Col2BulletsTabPage.ResumeLayout(false);
            this.Col2TextTabPage.ResumeLayout(false);
            this.Col3BulletsTabPage.ResumeLayout(false);
            this.Col3TextTabPage.ResumeLayout(false);
            this.practitionersTipsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPhase;
        private System.Windows.Forms.ComboBox cbPhases;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPhaseName;
        private System.Windows.Forms.TextBox txtPhaseName;
        private System.Windows.Forms.TabControl columnsTabControl;
        private System.Windows.Forms.TabPage Col1BulletsTabPage;
        private System.Windows.Forms.TabPage Col1TextTabPage;
        private Microsoft.ConsultingServices.HtmlEditorControl col1HtmlEditorControl;
        private System.Windows.Forms.TabPage Col2BulletsTabPage;
        private System.Windows.Forms.TabPage Col2TextTabPage;
        private Microsoft.ConsultingServices.HtmlEditorControl col2HtmlEditorControl;
        private System.Windows.Forms.TabPage Col3BulletsTabPage;
        private System.Windows.Forms.TabPage Col3TextTabPage;
        private Microsoft.ConsultingServices.HtmlEditorControl col3HtmlEditorControl;
        private System.Windows.Forms.TabPage practitionersTipsTabPage;
        private Microsoft.ConsultingServices.HtmlEditorControl practitionersTipsHtmlEditorControl;
        private ElectoralCycleModifyPhase electoralCycleBulletList21;
        private Idea.ERMT.UserControls.ElectoralCycleBulletList column1BulletList;
        private ElectoralCycleBulletList column2BulletList;
        private ElectoralCycleBulletList column3BulletList;
    }
}
