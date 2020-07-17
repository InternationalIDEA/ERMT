using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using ImageAlignOption = Microsoft.ConsultingServices.HtmlEditor.ImageAlignOption;

namespace Microsoft.ConsultingServices.HtmlEditor
{

	/// <summary>
	/// Form used to enter an Html Image attribute
	/// Consists of Href, Text and Image Alignment
	/// </summary>
	internal class EnterImageForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button bInsert;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.Label labelHref;
		private System.Windows.Forms.TextBox hrefText;
		private System.Windows.Forms.TextBox hrefLink;
		private System.Windows.Forms.Label labelAlign;
		private System.Windows.Forms.ComboBox listAlign;

		private System.ComponentModel.Container components = null;

		public EnterImageForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// define the text for the alignment
			this.listAlign.Items.AddRange(Enum.GetNames(typeof(ImageAlignOption)));

			// ensure default value set for target
			this.listAlign.SelectedIndex = 4;

		} //EnterHrefForm


		// property for the text to display
		public string ImageText
		{
			get
			{
				return this.hrefText.Text;
			}
			set
			{
				this.hrefText.Text = value;
			}

		} //ImageText

		// property for the href for the image
		public string ImageLink
		{
			get
			{
				return this.hrefLink.Text.Trim();
			}
			set
			{
				this.hrefLink.Text = value.Trim();
			}

		} //ImageLink

		//property for the image align
		public ImageAlignOption ImageAlign
		{
			get
			{
				return (ImageAlignOption)this.listAlign.SelectedIndex;
			}
			set
			{
				this.listAlign.SelectedIndex = (int)value;
			}
		}


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterImageForm));
            this.bInsert = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.labelHref = new System.Windows.Forms.Label();
            this.hrefText = new System.Windows.Forms.TextBox();
            this.hrefLink = new System.Windows.Forms.TextBox();
            this.labelAlign = new System.Windows.Forms.Label();
            this.listAlign = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bInsert
            // 
            this.bInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInsert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bInsert.Location = new System.Drawing.Point(231, 101);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(96, 27);
            this.bInsert.TabIndex = 4;
            this.bInsert.Text = "Insert Image";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(336, 101);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 27);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(10, 46);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(38, 27);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "Text:";
            // 
            // labelHref
            // 
            this.labelHref.Location = new System.Drawing.Point(10, 9);
            this.labelHref.Name = "labelHref";
            this.labelHref.Size = new System.Drawing.Size(38, 27);
            this.labelHref.TabIndex = 4;
            this.labelHref.Text = "Href:";
            // 
            // hrefText
            // 
            this.hrefText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefText.Location = new System.Drawing.Point(58, 46);
            this.hrefText.Name = "hrefText";
            this.hrefText.Size = new System.Drawing.Size(365, 22);
            this.hrefText.TabIndex = 2;
            // 
            // hrefLink
            // 
            this.hrefLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefLink.Location = new System.Drawing.Point(58, 9);
            this.hrefLink.Name = "hrefLink";
            this.hrefLink.Size = new System.Drawing.Size(365, 22);
            this.hrefLink.TabIndex = 1;
            // 
            // labelAlign
            // 
            this.labelAlign.Location = new System.Drawing.Point(10, 92);
            this.labelAlign.Name = "labelAlign";
            this.labelAlign.Size = new System.Drawing.Size(38, 27);
            this.labelAlign.TabIndex = 7;
            this.labelAlign.Text = "Align:";
            // 
            // listAlign
            // 
            this.listAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listAlign.Location = new System.Drawing.Point(58, 92);
            this.listAlign.Name = "listAlign";
            this.listAlign.Size = new System.Drawing.Size(145, 24);
            this.listAlign.TabIndex = 3;
            // 
            // EnterImageForm
            // 
            this.AcceptButton = this.bInsert;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(432, 136);
            this.Controls.Add(this.listAlign);
            this.Controls.Add(this.labelAlign);
            this.Controls.Add(this.hrefLink);
            this.Controls.Add(this.hrefText);
            this.Controls.Add(this.labelHref);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterImageForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Enter Image";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
