﻿using System.IO;

namespace Idea.ERMT.UserControls
{
    partial class MarkerPickForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.markerPick1 = new MarkerPick();
            this.SuspendLayout();
            // 
            // markerPick1
            // 
            this.markerPick1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.markerPick1.Location = new System.Drawing.Point(0, 0);
            //this.markerPick1.MarkerType = Dundas.Maps.WinControl.MarkerStyle.Circle;
            this.markerPick1.Name = "markerPick1";
            this.markerPick1.Size = new System.Drawing.Size(323, 347);
            this.markerPick1.TabIndex = 0;
            this.markerPick1.TextContent = "";
            this.markerPick1.Title = "";
            // 
            // MarkerPickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 370);
            this.Controls.Add(this.markerPick1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Title = "Marker";
            this.ControlBox = false;
            this.Name = "Marker";
            this.Text = "Marker";
            this.ResumeLayout(false);

        }

        #endregion

        private MarkerPick markerPick1;
    }
}