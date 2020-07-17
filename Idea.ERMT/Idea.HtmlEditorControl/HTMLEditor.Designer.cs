namespace Microsoft.ConsultingServices
{
    partial class HTMLEditor
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
            this.btnLinkDocument = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.htmlEditorControl = new Microsoft.ConsultingServices.HtmlEditorControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bEditHTML = new System.Windows.Forms.Button();
            this.bHeading = new System.Windows.Forms.Button();
            this.bInsertHtml = new System.Windows.Forms.Button();
            this.bPaste = new System.Windows.Forms.Button();
            this.listHeadings = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLinkDocument
            // 
            this.btnLinkDocument.Location = new System.Drawing.Point(3, 183);
            this.btnLinkDocument.Name = "btnLinkDocument";
            this.btnLinkDocument.Size = new System.Drawing.Size(88, 23);
            this.btnLinkDocument.TabIndex = 27;
            this.btnLinkDocument.Text = "Link Document";
            this.btnLinkDocument.UseVisualStyleBackColor = true;
            this.btnLinkDocument.Visible = false;
            this.btnLinkDocument.Click += new System.EventHandler(this.btnLinkDocument_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.htmlEditorControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(636, 302);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // htmlEditorControl
            // 
            this.htmlEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlEditorControl.InnerText = null;
            this.htmlEditorControl.Location = new System.Drawing.Point(3, 3);
            this.htmlEditorControl.Name = "htmlEditorControl";
            this.htmlEditorControl.Size = new System.Drawing.Size(530, 296);
            this.htmlEditorControl.TabIndex = 26;
            this.htmlEditorControl.HtmlNavigation += new Microsoft.ConsultingServices.HtmlEditor.HtmlNavigationEventHandler(this.htmlEditorControl_HtmlNavigation);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.Controls.Add(this.bEditHTML, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bHeading, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.btnLinkDocument, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.bInsertHtml, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.bPaste, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.listHeadings, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(539, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 296);
            this.tableLayoutPanel2.TabIndex = 27;
            // 
            // bEditHTML
            // 
            this.bEditHTML.Location = new System.Drawing.Point(3, 3);
            this.bEditHTML.Name = "bEditHTML";
            this.bEditHTML.Size = new System.Drawing.Size(88, 23);
            this.bEditHTML.TabIndex = 3;
            this.bEditHTML.Text = "Edit HTML";
            this.bEditHTML.Visible = false;
            this.bEditHTML.Click += new System.EventHandler(this.bEditHTML_Click);
            // 
            // bHeading
            // 
            this.bHeading.Location = new System.Drawing.Point(3, 213);
            this.bHeading.Name = "bHeading";
            this.bHeading.Size = new System.Drawing.Size(88, 23);
            this.bHeading.TabIndex = 15;
            this.bHeading.Text = "Set Heading";
            this.bHeading.Visible = false;
            this.bHeading.Click += new System.EventHandler(this.bHeading_Click);
            // 
            // bInsertHtml
            // 
            this.bInsertHtml.Location = new System.Drawing.Point(3, 33);
            this.bInsertHtml.Name = "bInsertHtml";
            this.bInsertHtml.Size = new System.Drawing.Size(88, 23);
            this.bInsertHtml.TabIndex = 16;
            this.bInsertHtml.Text = "Insert Html";
            this.bInsertHtml.Visible = false;
            this.bInsertHtml.Click += new System.EventHandler(this.bInsertHtml_Click);
            // 
            // bPaste
            // 
            this.bPaste.Location = new System.Drawing.Point(3, 93);
            this.bPaste.Name = "bPaste";
            this.bPaste.Size = new System.Drawing.Size(88, 23);
            this.bPaste.TabIndex = 19;
            this.bPaste.Text = "Insert Text";
            this.bPaste.Visible = false;
            this.bPaste.Click += new System.EventHandler(this.bPaste_Click);
            // 
            // listHeadings
            // 
            this.listHeadings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listHeadings.Items.AddRange(new object[] {
            "H1",
            "H2",
            "H3",
            "H4",
            "H5"});
            this.listHeadings.Location = new System.Drawing.Point(3, 153);
            this.listHeadings.MaxDropDownItems = 5;
            this.listHeadings.Name = "listHeadings";
            this.listHeadings.Size = new System.Drawing.Size(88, 21);
            this.listHeadings.TabIndex = 14;
            this.listHeadings.Visible = false;
            this.listHeadings.SelectedIndexChanged += new System.EventHandler(this.listHeadings_SelectedIndexChanged);
            // 
            // HTMLEditor
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HTMLEditor";
            this.Size = new System.Drawing.Size(636, 302);
            this.Load += new System.EventHandler(this.EditorTestForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlEditorControl htmlEditorControl;
        private System.Windows.Forms.Button btnLinkDocument;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bEditHTML;
        private System.Windows.Forms.Button bHeading;
        private System.Windows.Forms.Button bInsertHtml;
        private System.Windows.Forms.Button bPaste;
        private System.Windows.Forms.ComboBox listHeadings;
    }
}
