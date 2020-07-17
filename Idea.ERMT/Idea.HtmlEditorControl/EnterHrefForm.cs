using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NavigateActionOption = Microsoft.ConsultingServices.HtmlEditor.NavigateActionOption;

namespace Microsoft.ConsultingServices.HtmlEditor
{

	/// <summary>
	/// Form used to enter an Html Anchor attribute
	/// Consists of Href, Text and Target Frame
	/// </summary>
	internal class EnterHrefForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button bInsert;
		private System.Windows.Forms.Button bRemove;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.Label labelHref;
		private System.Windows.Forms.TextBox hrefText;
		private System.Windows.Forms.TextBox hrefLink;
		private System.Windows.Forms.Label labelTarget;
		private System.Windows.Forms.ComboBox listTargets;

		private System.ComponentModel.Container components = null;

		public EnterHrefForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// define the text for the targets
			this.listTargets.Items.AddRange(Enum.GetNames(typeof(NavigateActionOption)));

			// ensure default value set for target
			this.listTargets.SelectedIndex = 0;

		} //EnterHrefForm


		// property for the text to display
		public string HrefText
		{
			get
			{
				return this.hrefText.Text;
			}
			set
			{
				this.hrefText.Text = value;
			}

		} //HrefText

		//property for the href target
		public NavigateActionOption HrefTarget
		{
			get
			{
				return (NavigateActionOption)this.listTargets.SelectedIndex;
			}
		}

		// property for the href for the text
		public string HrefLink
		{
			get
			{
				return this.hrefLink.Text.Trim();
			}
			set
			{
				this.hrefLink.Text = value.Trim();
			}

		} //HrefLink


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EnterHrefForm));
			this.bInsert = new System.Windows.Forms.Button();
			this.bRemove = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.labelText = new System.Windows.Forms.Label();
			this.labelHref = new System.Windows.Forms.Label();
			this.hrefText = new System.Windows.Forms.TextBox();
			this.hrefLink = new System.Windows.Forms.TextBox();
			this.labelTarget = new System.Windows.Forms.Label();
			this.listTargets = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// bInsert
			// 
			this.bInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bInsert.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.bInsert.Location = new System.Drawing.Point(184, 106);
			this.bInsert.Name = "bInsert";
			this.bInsert.TabIndex = 0;
			this.bInsert.Text = "Insert Href";
			// 
			// bRemove
			// 
			this.bRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bRemove.DialogResult = System.Windows.Forms.DialogResult.No;
			this.bRemove.Location = new System.Drawing.Point(264, 106);
			this.bRemove.Name = "bRemove";
			this.bRemove.Size = new System.Drawing.Size(80, 23);
			this.bRemove.TabIndex = 1;
			this.bRemove.Text = "Remove Href";
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(352, 106);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 2;
			this.bCancel.Text = "Cancel";
			// 
			// labelText
			// 
			this.labelText.Location = new System.Drawing.Point(8, 16);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(40, 23);
			this.labelText.TabIndex = 3;
			this.labelText.Text = "Text:";
			// 
			// labelHref
			// 
			this.labelHref.Location = new System.Drawing.Point(8, 48);
			this.labelHref.Name = "labelHref";
			this.labelHref.Size = new System.Drawing.Size(40, 23);
			this.labelHref.TabIndex = 4;
			this.labelHref.Text = "Href:";
			// 
			// hrefText
			// 
			this.hrefText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.hrefText.Location = new System.Drawing.Point(56, 16);
			this.hrefText.Name = "hrefText";
			this.hrefText.ReadOnly = true;
			this.hrefText.Size = new System.Drawing.Size(368, 20);
			this.hrefText.TabIndex = 5;
			this.hrefText.Text = "";
			// 
			// hrefLink
			// 
			this.hrefLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.hrefLink.Location = new System.Drawing.Point(56, 48);
			this.hrefLink.Name = "hrefLink";
			this.hrefLink.Size = new System.Drawing.Size(368, 20);
			this.hrefLink.TabIndex = 6;
			this.hrefLink.Text = "";
			// 
			// labelTarget
			// 
			this.labelTarget.Location = new System.Drawing.Point(8, 80);
			this.labelTarget.Name = "labelTarget";
			this.labelTarget.Size = new System.Drawing.Size(40, 23);
			this.labelTarget.TabIndex = 7;
			this.labelTarget.Text = "Target:";
			// 
			// listTargets
			// 
			this.listTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.listTargets.Location = new System.Drawing.Point(56, 80);
			this.listTargets.Name = "listTargets";
			this.listTargets.Size = new System.Drawing.Size(121, 21);
			this.listTargets.TabIndex = 8;
			// 
			// EnterHrefForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(432, 136);
			this.Controls.Add(this.listTargets);
			this.Controls.Add(this.labelTarget);
			this.Controls.Add(this.hrefLink);
			this.Controls.Add(this.hrefText);
			this.Controls.Add(this.labelHref);
			this.Controls.Add(this.labelText);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bRemove);
			this.Controls.Add(this.bInsert);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EnterHrefForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Enter Href";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
