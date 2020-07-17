namespace Idea.ERMT.UserControls
{
    partial class ElectoralCycleBulletList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElectoralCycleBulletList));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.addDeleteTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.upDownTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.bulletsListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.addDeleteTableLayoutPanel.SuspendLayout();
            this.upDownTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.addDeleteTableLayoutPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.upDownTableLayoutPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bulletsListBox, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // addDeleteTableLayoutPanel
            // 
            resources.ApplyResources(this.addDeleteTableLayoutPanel, "addDeleteTableLayoutPanel");
            this.tableLayoutPanel1.SetColumnSpan(this.addDeleteTableLayoutPanel, 2);
            this.addDeleteTableLayoutPanel.Controls.Add(this.btnAdd, 0, 0);
            this.addDeleteTableLayoutPanel.Controls.Add(this.btnDelete, 1, 0);
            this.addDeleteTableLayoutPanel.Name = "addDeleteTableLayoutPanel";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // upDownTableLayoutPanel
            // 
            resources.ApplyResources(this.upDownTableLayoutPanel, "upDownTableLayoutPanel");
            this.upDownTableLayoutPanel.Controls.Add(this.btnUp, 0, 0);
            this.upDownTableLayoutPanel.Controls.Add(this.btnDown, 0, 1);
            this.upDownTableLayoutPanel.Name = "upDownTableLayoutPanel";
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // bulletsListBox
            // 
            this.bulletsListBox.DisplayMember = "Text";
            resources.ApplyResources(this.bulletsListBox, "bulletsListBox");
            this.bulletsListBox.FormattingEnabled = true;
            this.bulletsListBox.Name = "bulletsListBox";
            this.bulletsListBox.ValueMember = "IDPhaseBullet";
            this.bulletsListBox.SelectedIndexChanged += new System.EventHandler(this.bulletsListBox_SelectedIndexChanged);
            this.bulletsListBox.DoubleClick += new System.EventHandler(this.bulletsListBox_DoubleClick);
            // 
            // ElectoralCycleBulletList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ElectoralCycleBulletList";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.addDeleteTableLayoutPanel.ResumeLayout(false);
            this.upDownTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel addDeleteTableLayoutPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TableLayoutPanel upDownTableLayoutPanel;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ListBox bulletsListBox;
    }
}
