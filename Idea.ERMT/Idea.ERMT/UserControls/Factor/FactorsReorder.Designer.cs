namespace Idea.ERMT.UserControls
{
    partial class FactorsReorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactorsReorder));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbInternalFactors = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbExternalFactors = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownInternalFactors = new System.Windows.Forms.Button();
            this.btnUpInternalFactors = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownExternalFactors = new System.Windows.Forms.Button();
            this.btnUpExternalFactors = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lbInternalFactors, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbExternalFactors, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lbInternalFactors
            // 
            resources.ApplyResources(this.lbInternalFactors, "lbInternalFactors");
            this.lbInternalFactors.AllowDrop = true;
            this.lbInternalFactors.FormattingEnabled = true;
            this.lbInternalFactors.Name = "lbInternalFactors";
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
            // lbExternalFactors
            // 
            resources.ApplyResources(this.lbExternalFactors, "lbExternalFactors");
            this.lbExternalFactors.AllowDrop = true;
            this.lbExternalFactors.FormattingEnabled = true;
            this.lbExternalFactors.Name = "lbExternalFactors";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnDownInternalFactors, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnUpInternalFactors, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnDownInternalFactors
            // 
            resources.ApplyResources(this.btnDownInternalFactors, "btnDownInternalFactors");
            this.btnDownInternalFactors.Name = "btnDownInternalFactors";
            this.btnDownInternalFactors.UseVisualStyleBackColor = true;
            this.btnDownInternalFactors.Click += new System.EventHandler(this.btnDownInternalFactors_Click);
            // 
            // btnUpInternalFactors
            // 
            resources.ApplyResources(this.btnUpInternalFactors, "btnUpInternalFactors");
            this.btnUpInternalFactors.Name = "btnUpInternalFactors";
            this.btnUpInternalFactors.UseVisualStyleBackColor = true;
            this.btnUpInternalFactors.Click += new System.EventHandler(this.btnUpInternalFactors_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnDownExternalFactors, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnUpExternalFactors, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnDownExternalFactors
            // 
            resources.ApplyResources(this.btnDownExternalFactors, "btnDownExternalFactors");
            this.btnDownExternalFactors.Name = "btnDownExternalFactors";
            this.btnDownExternalFactors.UseVisualStyleBackColor = true;
            this.btnDownExternalFactors.Click += new System.EventHandler(this.btnDownExternalFactors_Click);
            // 
            // btnUpExternalFactors
            // 
            resources.ApplyResources(this.btnUpExternalFactors, "btnUpExternalFactors");
            this.btnUpExternalFactors.Name = "btnUpExternalFactors";
            this.btnUpExternalFactors.UseVisualStyleBackColor = true;
            this.btnUpExternalFactors.Click += new System.EventHandler(this.btnUpExternalFactors_Click);
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
            // FactorsReorder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FactorsReorder";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lbInternalFactors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbExternalFactors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDownInternalFactors;
        private System.Windows.Forms.Button btnUpInternalFactors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnDownExternalFactors;
        private System.Windows.Forms.Button btnUpExternalFactors;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
