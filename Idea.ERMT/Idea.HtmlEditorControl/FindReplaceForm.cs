using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.ConsultingServices.HtmlEditor
{

	/// <summary>
	/// Form designed to control Find and Replace operations
	/// Find and Replace operations performed by the user control class
	/// Delegates need to be defined to reference the class instances
	/// </summary> 
	internal class FindReplaceForm : System.Windows.Forms.Form
	{

		private System.Windows.Forms.TabPage tabFind;
		private System.Windows.Forms.TabPage tabReplace;
		private System.Windows.Forms.Label labelFind;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.TextBox textFind;
		private System.Windows.Forms.Button bFindNext;
		private System.Windows.Forms.Label labelReplace;
		private System.Windows.Forms.Button bReplaceAll;
		private System.Windows.Forms.Button bReplace;
		private System.Windows.Forms.Button bOptions;
		private System.Windows.Forms.CheckBox optionMatchCase;
		private System.Windows.Forms.CheckBox optionMatchWhole;
		private System.Windows.Forms.TextBox textReplace;
		private System.Windows.Forms.Panel panelOptions;
		private System.Windows.Forms.Panel panelInput;

		private System.ComponentModel.Container components = null;

		// private variables defining the state of the form
		private bool options = false;
		private bool findNotReplace  = true;
		private string findText;
		private string replaceText;

		// internal delegate reference
		private FindReplaceResetDelegate FindReplaceReset;
        private FindReplaceOneDelegate FindReplaceOne;
		private FindReplaceAllDelegate FindReplaceAll;
		private FindFirstDelegate FindFirst;
		private FindNextDelegate FindNext;


		// public constructor that defines the required delegates
		// delegates must be defined for the find and replace to operate
		public FindReplaceForm(string initText, FindReplaceResetDelegate resetDelegate, FindFirstDelegate findFirstDelegate, FindNextDelegate findNextDelegate, FindReplaceOneDelegate replaceOneDelegate, FindReplaceAllDelegate replaceAllDelegate)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Define the initial state of the form assuming a Find command to be displayed first
			DefineFindWindow(findNotReplace);
			DefineOptionsWindow(options);

			// ensure buttons not initially enabled
			this.bFindNext.Enabled = false;
			this.bReplace.Enabled = false;
			this.bReplaceAll.Enabled = false;

			// save the delegates used to perform find and replcae operations
			this.FindReplaceReset = resetDelegate;
			this.FindFirst = findFirstDelegate;
			this.FindNext = findNextDelegate;
			this.FindReplaceOne = replaceOneDelegate;
			this.FindReplaceAll = replaceAllDelegate;

			// define the original text
			this.textFind.Text = initText;

		} //FindReplaceForm


		// setup the properties based on the find or repalce functionality
		private void DefineFindWindow(bool find)
		{
			this.textReplace.Visible = !find;
			this.labelReplace.Visible = !find;
			this.bReplace.Visible = !find;
			this.bReplaceAll.Visible = !find;
			this.textFind.Focus();

		} //DefineFindWindow


		// define if the options dialog is shown
		private void DefineOptionsWindow(bool options)
		{
			if (options)
			{
				// Form displayed with the options shown
				this.bOptions.Text = "Less Options";
				this.panelOptions.Visible = true;
				this.tabControl.Height = 216;
				this.Height = 264;
				this.optionMatchCase.Focus();
			}
			else
			{
				// Form displayed without the options shown
				this.bOptions.Text = "More Options";
				this.panelOptions.Visible = false;
				this.tabControl.Height = 152;
				this.Height = 200;
				this.textFind.Focus();
			}

		} //DefineOptionsWindow


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FindReplaceForm));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabFind = new System.Windows.Forms.TabPage();
			this.tabReplace = new System.Windows.Forms.TabPage();
			this.labelFind = new System.Windows.Forms.Label();
			this.bCancel = new System.Windows.Forms.Button();
			this.textFind = new System.Windows.Forms.TextBox();
			this.bFindNext = new System.Windows.Forms.Button();
			this.labelReplace = new System.Windows.Forms.Label();
			this.textReplace = new System.Windows.Forms.TextBox();
			this.bReplaceAll = new System.Windows.Forms.Button();
			this.bReplace = new System.Windows.Forms.Button();
			this.bOptions = new System.Windows.Forms.Button();
			this.optionMatchCase = new System.Windows.Forms.CheckBox();
			this.optionMatchWhole = new System.Windows.Forms.CheckBox();
			this.panelOptions = new System.Windows.Forms.Panel();
			this.panelInput = new System.Windows.Forms.Panel();
			this.tabControl.SuspendLayout();
			this.panelOptions.SuspendLayout();
			this.panelInput.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabFind);
			this.tabControl.Controls.Add(this.tabReplace);
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.ShowToolTips = true;
			this.tabControl.Size = new System.Drawing.Size(440, 32);
			this.tabControl.TabIndex = 0;
			this.tabControl.TabStop = false;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabFind
			// 
			this.tabFind.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabFind.Location = new System.Drawing.Point(4, 22);
			this.tabFind.Name = "tabFind";
			this.tabFind.Size = new System.Drawing.Size(432, 6);
			this.tabFind.TabIndex = 0;
			this.tabFind.Text = "Find";
			this.tabFind.ToolTipText = "Find Text";
			// 
			// tabReplace
			// 
			this.tabReplace.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabReplace.Location = new System.Drawing.Point(4, 22);
			this.tabReplace.Name = "tabReplace";
			this.tabReplace.Size = new System.Drawing.Size(432, 6);
			this.tabReplace.TabIndex = 1;
			this.tabReplace.Text = "Replace";
			this.tabReplace.ToolTipText = "Find and Replace Text";
			// 
			// labelFind
			// 
			this.labelFind.Location = new System.Drawing.Point(8, 16);
			this.labelFind.Name = "labelFind";
			this.labelFind.Size = new System.Drawing.Size(96, 23);
			this.labelFind.TabIndex = 0;
			this.labelFind.Text = "Find What:";
			// 
			// bCancel
			// 
			this.bCancel.BackColor = System.Drawing.SystemColors.Control;
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(344, 80);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 4;
			this.bCancel.Text = "Cancel";
			// 
			// textFind
			// 
			this.textFind.Location = new System.Drawing.Point(112, 16);
			this.textFind.Name = "textFind";
			this.textFind.Size = new System.Drawing.Size(296, 20);
			this.textFind.TabIndex = 1;
			this.textFind.Text = "";
			this.textFind.TextChanged += new System.EventHandler(this.textFind_TextChanged);
			// 
			// bFindNext
			// 
			this.bFindNext.BackColor = System.Drawing.SystemColors.Control;
			this.bFindNext.Location = new System.Drawing.Point(264, 80);
			this.bFindNext.Name = "bFindNext";
			this.bFindNext.TabIndex = 3;
			this.bFindNext.Text = "Find Next";
			this.bFindNext.Click += new System.EventHandler(this.bFindNext_Click);
			// 
			// labelReplace
			// 
			this.labelReplace.Location = new System.Drawing.Point(8, 48);
			this.labelReplace.Name = "labelReplace";
			this.labelReplace.Size = new System.Drawing.Size(96, 23);
			this.labelReplace.TabIndex = 0;
			this.labelReplace.Text = "Replace  With:";
			// 
			// textReplace
			// 
			this.textReplace.Location = new System.Drawing.Point(112, 48);
			this.textReplace.Name = "textReplace";
			this.textReplace.Size = new System.Drawing.Size(296, 20);
			this.textReplace.TabIndex = 2;
			this.textReplace.Text = "";
			this.textReplace.TextChanged += new System.EventHandler(this.textReplace_TextChanged);
			// 
			// bReplaceAll
			// 
			this.bReplaceAll.BackColor = System.Drawing.SystemColors.Control;
			this.bReplaceAll.Location = new System.Drawing.Point(176, 80);
			this.bReplaceAll.Name = "bReplaceAll";
			this.bReplaceAll.TabIndex = 7;
			this.bReplaceAll.Text = "Replace All";
			this.bReplaceAll.Click += new System.EventHandler(this.bReplaceAll_Click);
			// 
			// bReplace
			// 
			this.bReplace.BackColor = System.Drawing.SystemColors.Control;
			this.bReplace.Location = new System.Drawing.Point(96, 80);
			this.bReplace.Name = "bReplace";
			this.bReplace.TabIndex = 6;
			this.bReplace.Text = "Replace";
			this.bReplace.Click += new System.EventHandler(this.bReplace_Click);
			// 
			// bOptions
			// 
			this.bOptions.BackColor = System.Drawing.SystemColors.Control;
			this.bOptions.Location = new System.Drawing.Point(8, 80);
			this.bOptions.Name = "bOptions";
			this.bOptions.TabIndex = 5;
			this.bOptions.Text = "Options";
			this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
			// 
			// optionMatchCase
			// 
			this.optionMatchCase.Location = new System.Drawing.Point(8, 8);
			this.optionMatchCase.Name = "optionMatchCase";
			this.optionMatchCase.Size = new System.Drawing.Size(240, 24);
			this.optionMatchCase.TabIndex = 8;
			this.optionMatchCase.Text = "Match Exact Case";
			// 
			// optionMatchWhole
			// 
			this.optionMatchWhole.Location = new System.Drawing.Point(8, 32);
			this.optionMatchWhole.Name = "optionMatchWhole";
			this.optionMatchWhole.Size = new System.Drawing.Size(240, 24);
			this.optionMatchWhole.TabIndex = 9;
			this.optionMatchWhole.Text = "Match Whole Word Only";
			// 
			// panelOptions
			// 
			this.panelOptions.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panelOptions.Controls.Add(this.optionMatchCase);
			this.panelOptions.Controls.Add(this.optionMatchWhole);
			this.panelOptions.Location = new System.Drawing.Point(16, 152);
			this.panelOptions.Name = "panelOptions";
			this.panelOptions.Size = new System.Drawing.Size(424, 64);
			this.panelOptions.TabIndex = 8;
			// 
			// panelInput
			// 
			this.panelInput.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panelInput.Controls.Add(this.labelFind);
			this.panelInput.Controls.Add(this.textFind);
			this.panelInput.Controls.Add(this.labelReplace);
			this.panelInput.Controls.Add(this.textReplace);
			this.panelInput.Controls.Add(this.bOptions);
			this.panelInput.Controls.Add(this.bReplace);
			this.panelInput.Controls.Add(this.bReplaceAll);
			this.panelInput.Controls.Add(this.bFindNext);
			this.panelInput.Controls.Add(this.bCancel);
			this.panelInput.Location = new System.Drawing.Point(16, 40);
			this.panelInput.Name = "panelInput";
			this.panelInput.Size = new System.Drawing.Size(424, 112);
			this.panelInput.TabIndex = 9;
			// 
			// FindReplaceForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(458, 224);
			this.Controls.Add(this.panelOptions);
			this.Controls.Add(this.panelInput);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindReplaceForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Find and Replace";
			this.tabControl.ResumeLayout(false);
			this.panelOptions.ResumeLayout(false);
			this.panelInput.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		// define the visibility of the options
		// based on the user clicking the options button
		private void bOptions_Click(object sender, System.EventArgs e)
		{
			options = !options;
			DefineOptionsWindow(options);

		} //OptionsClick


		// set the state of the form based on the index slected
		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.tabControl.SelectedIndex == 0)
			{
				findNotReplace = true;
			}
			else
			{
				findNotReplace = false;
			}
			DefineFindWindow(findNotReplace);
		
		} //SelectedIndexChanged


		// replace a given text string with the replace value
		private void bReplace_Click(object sender, System.EventArgs e)
		{
			// find and replace the given text
			if (!this.FindReplaceOne(findText, replaceText, this.optionMatchWhole.Checked, this.optionMatchCase.Checked)) 
			{
				MessageBox.Show("All occurrences have been replaced!", "Find and Replace");
			}
		
		} //ReplaceClick


		// replace all occurences of a string with the replace value
		private void bReplaceAll_Click(object sender, System.EventArgs e)
		{
			int found = this.FindReplaceAll(findText, replaceText, this.optionMatchWhole.Checked, this.optionMatchCase.Checked);

			// indicate the number of replaces found
			MessageBox.Show(string.Format("{0} occurrences replaced", found), "Find and Replace");
		
		} // ReplaceAllClick


		// find the next occurence of the given string
		private void bFindNext_Click(object sender, System.EventArgs e)
		{
			// once find has completed indicate to the user success or failure
			if (!this.FindNext(findText, this.optionMatchWhole.Checked, this.optionMatchCase.Checked))
			{
				MessageBox.Show("No more occurrences found!", "Find and Replace");
			}
		
		} //FindNextClick


		// once the text has been changed reset the ranges to be worked with
		// initially defined by the set in the constructor
		private void textFind_TextChanged(object sender, System.EventArgs e)
		{
			ResetTextState();

		} //FindTextChanged


		// once the text has been changed reset the ranges to be worked with
		// initially defined by the set in the constructor
		private void textReplace_TextChanged(object sender, System.EventArgs e)
		{
			ResetTextState();

		} //TextChanged


		// sets the form state based on user input for Replace
		private void ResetTextState()
		{
			// reset the range being worked with
			this.FindReplaceReset();

			// determine the text values
			findText = this.textFind.Text.Trim();
			replaceText = this.textReplace.Text.Trim();

			// if no find text available disable find button
			if (findText != string.Empty)
			{
				this.bFindNext.Enabled = true;
			}
			else
			{
				this.bFindNext.Enabled = false;
			}

			// if no find text available disable replace button
			if (this.textFind.Text.Trim() != string.Empty && replaceText != string.Empty)
			{
				this.bReplace.Enabled = true;
				this.bReplaceAll.Enabled = true;
			}
			else
			{
				this.bReplace.Enabled = false;
				this.bReplaceAll.Enabled = false;
			}

		} //ResetTextReplace

	}
}
