namespace Idea.ERMT.UserControls
{
    partial class MarkerPick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPick));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMapDatesRange = new System.Windows.Forms.TableLayoutPanel();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.lblSelectTypeMarker = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbSelectTypeMarker = new System.Windows.Forms.ComboBox();
            this.pbSymbol = new System.Windows.Forms.PictureBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMarkerTitleColor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMapDatesRange.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSymbol)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tlpMapDatesRange, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSelectTypeMarker, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtText, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tlpMapDatesRange
            // 
            resources.ApplyResources(this.tlpMapDatesRange, "tlpMapDatesRange");
            this.tlpMapDatesRange.Controls.Add(this.dtpDateFrom, 1, 0);
            this.tlpMapDatesRange.Controls.Add(this.dtpDateTo, 3, 0);
            this.tlpMapDatesRange.Controls.Add(this.lblDateTo, 2, 0);
            this.tlpMapDatesRange.Controls.Add(this.label1, 0, 0);
            this.tlpMapDatesRange.Name = "tlpMapDatesRange";
            // 
            // dtpDateFrom
            // 
            resources.ApplyResources(this.dtpDateFrom, "dtpDateFrom");
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpDateFrom.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            // 
            // dtpDateTo
            // 
            resources.ApplyResources(this.dtpDateTo, "dtpDateTo");
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpDateTo.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            // 
            // lblDateTo
            // 
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Name = "lblDateTo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // lblSelectTypeMarker
            // 
            resources.ApplyResources(this.lblSelectTypeMarker, "lblSelectTypeMarker");
            this.lblSelectTypeMarker.Name = "lblSelectTypeMarker";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.txtLongitude, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtLatitude, 1, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // txtLongitude
            // 
            resources.ApplyResources(this.txtLongitude, "txtLongitude");
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Leave += new System.EventHandler(this.txtLongitude_Leave);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtLatitude
            // 
            resources.ApplyResources(this.txtLatitude, "txtLatitude");
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Leave += new System.EventHandler(this.txtLatitude_Leave);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.cbSelectTypeMarker, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbSymbol, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // cbSelectTypeMarker
            // 
            resources.ApplyResources(this.cbSelectTypeMarker, "cbSelectTypeMarker");
            this.cbSelectTypeMarker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectTypeMarker.FormattingEnabled = true;
            this.cbSelectTypeMarker.Name = "cbSelectTypeMarker";
            this.cbSelectTypeMarker.SelectedIndexChanged += new System.EventHandler(this.cbSelectTypeMarker_SelectedIndexChanged);
            // 
            // pbSymbol
            // 
            resources.ApplyResources(this.pbSymbol, "pbSymbol");
            this.pbSymbol.Name = "pbSymbol";
            this.pbSymbol.TabStop = false;
            // 
            // txtText
            // 
            resources.ApplyResources(this.txtText, "txtText");
            this.txtText.Name = "txtText";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlMarkerTitleColor);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTitle);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pnlMarkerTitleColor
            // 
            resources.ApplyResources(this.pnlMarkerTitleColor, "pnlMarkerTitleColor");
            this.pnlMarkerTitleColor.Name = "pnlMarkerTitleColor";
            this.pnlMarkerTitleColor.Click += new System.EventHandler(this.pnlMarkerTitleColor_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtTitle
            // 
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.Name = "txtTitle";
            // 
            // MarkerPick
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MarkerPick";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpMapDatesRange.ResumeLayout(false);
            this.tlpMapDatesRange.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSymbol)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSelectTypeMarker;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbSelectTypeMarker;
        private System.Windows.Forms.PictureBox pbSymbol;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.TableLayoutPanel tlpMapDatesRange;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlMarkerTitleColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
    }
}
