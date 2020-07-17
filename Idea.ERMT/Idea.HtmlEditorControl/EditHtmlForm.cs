using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.ConsultingServices.HtmlEditor
{

	/// <summary>
	/// Form used to Edit or View Html contents
	/// If a property RedOnly is true contents are considered viewable
	/// No Html parsing is performed on the resultant data
	/// </summary>
	internal class EditHtmlForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox htmlText;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
		private System.ComponentModel.Container components = null;

		// read only property for the form
		private bool _readOnly;

		// string values for the form title
		private const string editCommand = "Cancel";
		private const string viewCommand = "Close";

		// form initializer
		public EditHtmlForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// ensure content is empty
			this.htmlText.Text = string.Empty;
			this.ReadOnly = true;

		} //EditHtmlForm

		// option to modify the caption of the display
		public void SetCaption(string caption)
		{
			this.Text = caption;
		}

		// property to set and get the HTML contents
		public string HTML
		{
			get
			{
				return this.htmlText.Text.Trim();
			}
			set
			{
				this.htmlText.Text = (value != null)?value.Trim():string.Empty;
				this.htmlText.SelectionStart = 0;
				this.htmlText.SelectionLength = 0;
			}

		} //HTML

		// property that determines if the html is editable
		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly = value;
				this.bOK.Visible = !_readOnly;
				this.htmlText.ReadOnly = _readOnly;
				this.bCancel.Text = _readOnly?viewCommand:editCommand;
			}

		} //ReadOnly


		// Clean up any resources being used.
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );

		} //Dispose

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditHtmlForm));
			this.htmlText = new System.Windows.Forms.TextBox();
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// htmlText
			// 
			this.htmlText.AcceptsReturn = true;
			this.htmlText.AcceptsTab = true;
			this.htmlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.htmlText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.htmlText.Location = new System.Drawing.Point(8, 8);
			this.htmlText.Multiline = true;
			this.htmlText.Name = "htmlText";
			this.htmlText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.htmlText.Size = new System.Drawing.Size(576, 320);
			this.htmlText.TabIndex = 0;
			this.htmlText.Text = "";
			// 
			// bOK
			// 
			this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOK.Location = new System.Drawing.Point(416, 336);
			this.bOK.Name = "bOK";
			this.bOK.TabIndex = 1;
			this.bOK.Text = "OK";
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(504, 336);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 2;
			this.bCancel.Text = "Cancel";
			// 
			// EditHtmlForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(592, 366);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.htmlText);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditHtmlForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Html";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
