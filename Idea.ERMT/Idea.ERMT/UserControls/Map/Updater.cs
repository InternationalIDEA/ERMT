using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Idea.ERMT
{
    public partial class Updater : UserControl
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }

        public bool HasFile { get; set; }
        public bool HasChange { get; set; }

        public delegate void DUpdater(object sender, UpdaterEventArgs e);
        public delegate byte[] DDownload(object sender, UpdaterEventArgs e);
        public delegate void DGotContent(object sender, UpdaterEventArgs e);

        public event DUpdater Delete;
        public event DDownload GetContent;
        public event DGotContent GotContent;

        public Updater()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            btUpload.Visible = !this.HasFile;
            pnlDownload.Visible = this.HasFile;
            lbDownload.Text = this.FileName;
        }

        private void lbDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.Delete != null)
            {
                UpdaterEventArgs arg = new UpdaterEventArgs(this.Id);
                this.Delete.Invoke(this, arg);
            }
        }

        private void lbDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.Content == null)
            {
                if (this.GetContent != null)
                {
                    UpdaterEventArgs arg = new UpdaterEventArgs(this.Id);
                    this.Content = this.GetContent(this, arg);
                }
            }

            saveFileDialog1.FileName = this.FileName;
            saveFileDialog1.OverwritePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(saveFileDialog1.FileName))
                    System.IO.File.Delete(saveFileDialog1.FileName);

                System.IO.File.WriteAllBytes(saveFileDialog1.FileName, this.Content);
            }
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var info = new System.IO.FileInfo(openFileDialog1.FileName);
                if (info.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("Maximun file size is 2MB", "File size", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.FileName = openFileDialog1.FileName.Split('\\')[openFileDialog1.FileName.Split('\\').Length - 1];
                this.Content = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                this.HasChange = true;
                this.HasFile = true;
                Refresh();

                UpdaterEventArgs arg = new UpdaterEventArgs(this.Id);
                this.GotContent.Invoke(this, arg);
            }
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            Refresh();
        }
    }

    public class UpdaterEventArgs : EventArgs
    {
        int _id = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public UpdaterEventArgs() 
            :  base()
        {

        }
        public UpdaterEventArgs(int id)
            : base()
        {
            _id = id;
        }
    }
}
