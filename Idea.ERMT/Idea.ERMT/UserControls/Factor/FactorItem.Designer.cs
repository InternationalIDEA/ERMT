namespace Idea.ERMT.UserControls
{
    partial class FactorItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactorItem));
            this.chkFactor = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nuMax = new System.Windows.Forms.NumericUpDown();
            this.nuMin = new System.Windows.Forms.NumericUpDown();
            this.nuInt = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nuWeight = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nuMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFactor
            // 
            resources.ApplyResources(this.chkFactor, "chkFactor");
            this.chkFactor.Name = "chkFactor";
            this.toolTip1.SetToolTip(this.chkFactor, resources.GetString("chkFactor.ToolTip"));
            this.chkFactor.UseVisualStyleBackColor = true;
            this.chkFactor.CheckedChanged += new System.EventHandler(this.chkFactor_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // nuMax
            // 
            resources.ApplyResources(this.nuMax, "nuMax");
            this.nuMax.Name = "nuMax";
            this.toolTip1.SetToolTip(this.nuMax, resources.GetString("nuMax.ToolTip"));
            this.nuMax.ValueChanged += new System.EventHandler(this.nuMin_ValueChanged);
            // 
            // nuMin
            // 
            resources.ApplyResources(this.nuMin, "nuMin");
            this.nuMin.Name = "nuMin";
            this.toolTip1.SetToolTip(this.nuMin, resources.GetString("nuMin.ToolTip"));
            this.nuMin.ValueChanged += new System.EventHandler(this.nuMin_ValueChanged);
            // 
            // nuInt
            // 
            resources.ApplyResources(this.nuInt, "nuInt");
            this.nuInt.DecimalPlaces = 1;
            this.nuInt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nuInt.Name = "nuInt";
            this.toolTip1.SetToolTip(this.nuInt, resources.GetString("nuInt.ToolTip"));
            this.nuInt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nuInt.ValueChanged += new System.EventHandler(this.nuMin_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // nuWeight
            // 
            resources.ApplyResources(this.nuWeight, "nuWeight");
            this.nuWeight.Name = "nuWeight";
            this.toolTip1.SetToolTip(this.nuWeight, resources.GetString("nuWeight.ToolTip"));
            this.nuWeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nuWeight.ValueChanged += new System.EventHandler(this.nuWeight_ValueChanged);
            // 
            // FactorItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nuWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nuInt);
            this.Controls.Add(this.nuMin);
            this.Controls.Add(this.nuMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkFactor);
            this.Name = "FactorItem";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.nuMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFactor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nuMax;
        private System.Windows.Forms.NumericUpDown nuMin;
        private System.Windows.Forms.NumericUpDown nuInt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuWeight;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
