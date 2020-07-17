using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.ConsultingServices.HtmlEditor;

namespace Microsoft.ConsultingServices
{
    public partial class HTMLEditor : UserControl
    {
        private string workingDirectory = string.Empty;


        public string HTML
        {
            set { this.htmlEditorControl.BodyHtml = value; }
            get { return this.htmlEditorControl.DocumentHtml; }
        }

        public HTMLEditor()
        {
            InitializeComponent();
        }

        public void LibraryClick(object sender, EventArgs args)
        {
            btnLinkDocument_Click(sender, args);
        }

        public void ShowHtml(string html, string cssFile)
        {
            this.htmlEditorControl.BodyHtml = html;
            if (!string.IsNullOrEmpty(cssFile))
                this.htmlEditorControl.LinkStyleSheet(cssFile);
        }

        private void bBackground_Click(object sender, System.EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Color color = dialog.Color;
                    this.htmlEditorControl.BodyBackColor = color;
                }
            }
            this.htmlEditorControl.Focus();
        }

        private void bForeground_Click(object sender, System.EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Color color = dialog.Color;
                    this.htmlEditorControl.BodyForeColor = color;
                }
            }
            this.htmlEditorControl.Focus();
        }

        private void bEditHTML_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.HtmlContentsEdit();
            this.htmlEditorControl.Focus();
        }

        private void bStyle_Click(object sender, System.EventArgs e)
        {
            string cssFile = @"default.css";
            if (File.Exists(cssFile))
            {
                this.htmlEditorControl.LinkStyleSheet(cssFile);
                MessageBox.Show(this, cssFile, "Style Sheet Linked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, cssFile, "Style Sheet Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.htmlEditorControl.Focus();
        }

        private void bSaveHtml_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.SaveFilePrompt();
            this.htmlEditorControl.Focus();
        }

        private void bOpenHtml_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.OpenFilePrompt();
            this.htmlEditorControl.Focus();
        }

        private void bHeading_Click(object sender, System.EventArgs e)
        {
            int headingRef = this.listHeadings.SelectedIndex + 1;
            if (headingRef > 0)
            {
                HtmlHeadingType headingType = (HtmlHeadingType)headingRef;
                this.htmlEditorControl.InsertHeading(headingType);
            }
            this.htmlEditorControl.Focus();
        }

        private void bInsertHtml_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.InsertHtmlPrompt();
            this.htmlEditorControl.Focus();
        }

        private void bImage_Click(object sender, System.EventArgs e)
        {

            // set initial value states
            string fileName = string.Empty;
            string filePath = string.Empty;

            // define the file dialog
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Image";
                dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                dialog.CheckFileExists = true;
                if (workingDirectory != String.Empty) dialog.InitialDirectory = workingDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = Path.GetFileName(dialog.FileName);
                    filePath = Path.GetFullPath(dialog.FileName);
                    workingDirectory = Path.GetDirectoryName(dialog.FileName);

                    if (fileName != "")
                    {
                        // have a path for a image I can insert
                        this.htmlEditorControl.InsertImage(filePath);
                    }
                }
            }
            this.htmlEditorControl.Focus();
        }

        private void bBasrHref_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.AutoWordWrap = !this.htmlEditorControl.AutoWordWrap;
            this.htmlEditorControl.Focus();
        }

        private void bPaste_Click(object sender, System.EventArgs e)
        {
            this.htmlEditorControl.InsertTextPrompt();
            this.htmlEditorControl.Focus();
        }

        // set the flat style of the dialog based on the user setting
        private void SetFlatStyleSystem(Control parent)
        {
            // iterate through all controls setting the flat style
            foreach (Control control in parent.Controls)
            {
                // Only these controls have a FlatStyle property
                ButtonBase button = control as ButtonBase;
                GroupBox group = control as GroupBox;
                Label label = control as Label;
                TextBox textBox = control as TextBox;
                if (button != null) button.FlatStyle = FlatStyle.System;
                else if (group != null) group.FlatStyle = FlatStyle.System;
                else if (label != null) label.FlatStyle = FlatStyle.System;

                // Set contained controls FlatStyle, too
                SetFlatStyleSystem(control);
            }

        }

        private void EditorTestForm_Load(object sender, System.EventArgs e)
        {
            SetFlatStyleSystem(this);
        }

        private void htmlEditorControl_HtmlException(object sender, Microsoft.ConsultingServices.HtmlEditor.HtmlExceptionEventArgs args)
        {
            // obtain the message and operation
            // concatenate the message with any inner message
            string operation = args.Operation;
            Exception ex = args.ExceptionObject;
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                if (ex.InnerException.Message != null)
                {
                    message = string.Format("{0}\n{1}", message, ex.InnerException.Message);
                }
            }
            // define the title for the internal message box
            string title;
            if (operation == null || operation == string.Empty)
            {
                title = "Unknown Error";
            }
            else
            {
                title = operation + " Error";
            }
            // display the error message box
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void htmlEditorControl_HtmlNavigation(object sender, Microsoft.ConsultingServices.HtmlEditor.HtmlNavigationEventArgs e)
        {
            e.Cancel = false;
        }

        private void listHeadings_SelectedIndexChanged(object sender, EventArgs e)
        {
            bHeading_Click(sender, e);
        }

        private void btnLinkDocument_Click(object sender, EventArgs e)
        {
            this.htmlEditorControl.InsertLinkToDocumentPrompt();
            //MessageBox.Show("Not implemented in this version");
        }
    }
}
