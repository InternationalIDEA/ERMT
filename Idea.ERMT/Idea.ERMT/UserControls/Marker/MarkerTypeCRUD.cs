using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class MarkerTypeCRUD : ERMTUserControl
    {
        MarkerType _markerType;
        string _fileName = string.Empty;
        string _imagename = string.Empty;

        private MarkerType MarkerType
        {
            get
            {
                if (_markerType == null)
                {
                    _markerType = MarkerTypeHelper.GetNew();
                }

                _markerType.Name = txtName.Text;
                _markerType.Size = ((string)cbSize.SelectedValue);
                if (_imagename != string.Empty)
                {
                    _markerType.Symbol = _imagename + ".png";
                }

                return _markerType;
            }
        }

        public MarkerTypeCRUD()
        {
            InitializeComponent();
            ShowTitle();
            LoadData();
        }

        private void LoadData()
        {
            btnRemove.Enabled = false;
            LoadMarkerTypes();
            LoadCombos();
        }

        private void LoadMarkerTypes()
        {
            tvMarkerType.Nodes.Clear();
            List<MarkerType> markerTypes = MarkerTypeHelper.GetAll();
            foreach (MarkerType markerType in markerTypes)
            {
                TreeNode n = new TreeNode { Tag = markerType, Text = markerType.Name };
                tvMarkerType.Nodes.Add(n);
            }
            if (markerTypes.Count != 0) return;
            TreeNode node = new TreeNode { Text = ResourceHelper.GetResourceText("NoMarkerTypes") };
            tvMarkerType.Nodes.Add(node);
            txtName.Enabled = false;
            cbSymbol.Enabled = false;
            cbSize.Enabled = false;
        }

        public override sealed void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("MarkerTypes"));
        }

        private void tvMarkerType_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MarkerType mt = ((MarkerType)(e.Node.Tag));
            if (mt == null) return;
            txtName.Enabled = true;
            cbSymbol.Enabled = true;
            cbSize.Enabled = true;
            txtName.Text = mt.Name;
            cbSymbol.SelectedValue = Path.GetFileNameWithoutExtension(mt.Symbol);
            if (cbSymbol.SelectedValue == null)
            {
                cbSymbol.SelectedIndex = 0;
            }

            _imagename = Path.GetFileNameWithoutExtension(mt.Symbol);
            cbSize.SelectedIndex = GetSizeIndex(mt.Size);
            _markerType = mt;
            btnSave.Enabled = true;
            btnRemove.Enabled = true;
        }

        private int GetSizeIndex(string size)
        {
            switch (size.ToLower())
            {
                case "small":
                    return 0;
                case "medium":
                    return 1;
                case "large":
                    return 2;
                default: return 1;
            }
        }

        private void cbSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void LoadImage()
        {
            if (cbSymbol.SelectedValue != null && cbSymbol.SelectedIndex == 0)
            {
                btnLoadAImageFile.Enabled = true;
                return;
            }
            btnLoadAImageFile.Enabled = false;
            cbSize.Enabled = true;
            if (!File.Exists(DirectoryAndFileHelper.ClientIconsFolder + "\\" + cbSymbol.Text + ".png"))
            {
                pbSymbol.Image = null;
                return;
            }

            string filename = DirectoryAndFileHelper.ClientIconsFolder + "\\" + cbSymbol.Text + ".png";
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                pbSymbol.Image = Image.FromStream(stream);
                stream.Dispose();
            }
            _imagename = cbSymbol.Text;
            AdjustImage(cbSize.SelectedIndex);
        }

        private bool GetThumbnailCallback() { return false; }

        private void AdjustImage(int size)
        {
            switch (size)
            {
                case 0:
                    pbSymbol.Image = pbSymbol.Image.GetThumbnailImage(15, 15, GetThumbnailCallback, IntPtr.Zero);
                    break;
                case 1:
                    pbSymbol.Image = pbSymbol.Image.GetThumbnailImage(30, 30, GetThumbnailCallback, IntPtr.Zero);
                    break;
                case 2:
                    pbSymbol.Image = pbSymbol.Image.GetThumbnailImage(60, 60, GetThumbnailCallback, IntPtr.Zero);
                    break;
            }
            pbSymbol.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnLoadAImageFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Files PNG(*.png)|*.png",
                InitialDirectory = DirectoryAndFileHelper.ClientIconsFolder
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(ofd.FileName);
                pbSymbol.Image = image;
                _fileName = ofd.FileName;
                AdjustImage(cbSize.SelectedIndex);
                _imagename = Path.GetFileNameWithoutExtension(ofd.FileName);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            TreeNode n = tvMarkerType.SelectedNode;
            if (n != null)
            {
                if (
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ConfirmDeleteMarkerType"),
                        CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo) ==
                    CustomMessageBoxReturnValue.Ok)
                {
                    bool deleteImage =
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ConfirmDeleteMarkerTypeImage"),
                            CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo) ==
                        CustomMessageBoxReturnValue.Ok;
                    //delete the marker type and the server image
                    MarkerTypeHelper.Delete((MarkerType) (n.Tag), deleteImage);
                    //delete the image from the client.
                    if (deleteImage)
                    {
                        pbSymbol.Image = null;
                        DeleteMarkerTypeImageFromClient((MarkerType) (n.Tag));
                    }

                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerTypeDeleted"));
                    LoadData();
                    Clear();
                }
            }
            else
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("SelectMarkerType"));
            }
        }

        private void LoadCombos()
        {
            List<Document> documents = DocumentHelper.GetIcons();
            cbSymbol.ValueMember = "Filename";
            cbSymbol.DisplayMember = "Filename";
            documents.Insert(0, new Document { Filename = ResourceHelper.GetResourceText("MarkerTypeSelectImage") });
            cbSymbol.DataSource = documents;
            cbSymbol.SelectedIndex = 0;
            _imagename = string.Empty;
            List<string> sizes = MarkerTypeHelper.GetSizes();
            cbSize.DataSource = sizes;
        }

        public override void Clear()
        {
            tvMarkerType.SelectedNode = null;
            txtName.Text = string.Empty;
            txtName.Enabled = true;
            cbSymbol.SelectedIndex = 0;
            cbSymbol.Enabled = true;
            _imagename = string.Empty;
            cbSize.SelectedIndex = 0;
            cbSize.Enabled = true;
            pbSymbol.Image = null;
            _markerType = MarkerTypeHelper.GetNew();
            btnRemove.Enabled = false;
        }

        private void DeleteMarkerTypeImageFromClient(MarkerType markerType)
        {
            string location = DirectoryAndFileHelper.ClientIconsFolder;
            List<MarkerType> markerTypeList = MarkerTypeHelper.GetAll();
            bool existAnother = markerTypeList.Any(mt => mt.Symbol == markerType.Symbol);
            if (!existAnother)
            {
                string path = location + markerType.Symbol;
                if (File.Exists(path))
                {
                    DocumentHelper.DeleteFile(path);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MarkerType.Name != string.Empty && (cbSymbol.SelectedIndex != 0 || (cbSymbol.SelectedIndex == 0 && _fileName != string.Empty)))
            {
                MarkerTypeHelper.Save(MarkerType);
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerTypeSaved"));
                Clear();

                if (_fileName != string.Empty)
                {
                    Document doc = new Document
                    {
                        DocumentType = ERMTDocumentType.Icon,
                        Content = Convert.ToBase64String(File.ReadAllBytes(_fileName)),
                        Filename = Path.GetFileName(_fileName)
                    };
                    DocumentHelper.Save(doc);
                    DocumentHelper.DownloadFiles();
                }
                LoadData();
                Clear();
                btnRemove.Enabled = false;
            }
            else
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("Allfieldsrequired"));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
