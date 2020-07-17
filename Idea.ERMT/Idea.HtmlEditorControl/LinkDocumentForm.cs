using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Idea.Facade;
using Idea.Utils;

namespace Microsoft.ConsultingServices
{
    public partial class LinkDocumentForm : Form
    {


        public LinkDocumentForm()
        {
            InitializeComponent();

            string[] files = Directory.GetFiles(DirectoryAndFileHelper.ClientDocumentsFolder);
            List<ShowFile> list = new List<ShowFile>();
            foreach (string s in files)
            {
                ShowFile sf = new ShowFile() { FileName = s.Split('\\')[s.Split('\\').Length - 1] };
                sf.GetImage();
                list.Add(sf);
            }
            dataGridView1.DataSource = list;

        }

        public string GetLinkToDocument()
        {
            return string.Empty;
        }

        public string HrefText
        {
            get { return this.txthref.Text; }
            set { txthref.Text = value; }
        }

        public string HrefLink
        {
            get { return hrefLink.Text; }
            set { hrefLink.Text = value; }
        }

        public class ShowFile
        {
            private const int SHGFI_ICON = 0x100;
            private const int SHGFI_SMALLICON = 0x1;
            private const int SHGFI_LARGEICON = 0x0;

            // This structure will contain information about the file
            public struct SHFILEINFO
            {
                // Handle to the icon representing the file
                public IntPtr hIcon;
                // Index of the icon within the image list
                public int iIcon;
                // Various attributes of the file
                public uint dwAttributes;
                // Path to the file
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string szDisplayName;
                // File type
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            };

            // The signature of SHGetFileInfo (located in Shell32.dll)
            [DllImport("Shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

            public string FileName { get; set; }
            public Image Image { get; set; }

            public void GetImage()
            {
                // Will store a handle to the small icon
                IntPtr hImgSmall;
                // Will store a handle to the large icon
                IntPtr hImgLarge;

                System.Drawing.Icon myIcon;
                SHFILEINFO shinfo = new SHFILEINFO();

                // Get a handle to the small icon
                hImgSmall = SHGetFileInfo(DirectoryAndFileHelper.ClientDocumentsFolder + FileName, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
                // Get the small icon from the handle
                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                // Display the small icon
                Image = myIcon.ToBitmap();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HrefLink = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.HrefLink = string.Empty;
            this.Close();
        }
    }
}
