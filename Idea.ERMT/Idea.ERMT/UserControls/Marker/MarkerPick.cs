using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class MarkerPick : ERMTUserControl
    {
        private bool _validFormData = true;
        public string Title
        {
            get
            {
                return txtTitle.Text;
            }
            set
            {
                if (value != null)
                    txtTitle.Text = value;
            }
        }

        public Color TitleColor
        {
            get
            {
                return pnlMarkerTitleColor.BackColor;
            }
            set
            {
                if (value != null)
                {
                    pnlMarkerTitleColor.BackColor = value;
                }
            }
        }

        public string TextContent
        {
            get
            {
                return txtText.Text;
            }
            set
            {
                if (value != null)
                    txtText.Text = value;
            }
        }

        public MarkerType MarkerType
        {
            get
            {
                return (MarkerType)cbSelectTypeMarker.SelectedItem;
            }
            set
            {
                object a = cbSelectTypeMarker.Items.Cast<MarkerType>().FirstOrDefault(mt => mt.IDMarkerType == value.IDMarkerType);
                cbSelectTypeMarker.SelectedItem = a;
            }
        }

        private SymbolStruct GetSymbolStructByName(string symbol)
        {
            return new SymbolStruct();
        }

        public DateTime From
        {
            get
            {
                return dtpDateFrom.Value;
            }
            set
            {
                dtpDateFrom.Value = (value != null ? value : dtpDateFrom.Value);
            }
        }

        public DateTime To
        {
            get
            {
                return dtpDateTo.Value;
            }
            set
            {
                dtpDateTo.Value = (value != null ? value : dtpDateTo.Value);
            }
        }

        public decimal Latitude
        {
            get
            {
                return decimal.Parse(txtLatitude.Text);
            }
            set
            {
                txtLatitude.Text = (value != null ? value.ToString() : txtTitle.Text);
            }
        }

        public decimal Longitude
        {
            get
            {
                return decimal.Parse(txtLongitude.Text);
            }
            set
            {
                txtLongitude.Text = (value != null ? value.ToString() : txtLongitude.Text);
            }
        }

        public MarkerPick()
        {
            InitializeComponent();
            LoadCombos();
            SetContextMenuForText();
        }

        private void LoadCombos()
        {
            cbSelectTypeMarker.DisplayMember = "Name";
            cbSelectTypeMarker.DataSource = MarkerTypeHelper.GetAll();
            dtpDateFrom.Value = DateTime.Parse(DateTime.Now.ToShortDateString());
            dtpDateTo.Value = DateTime.Parse(DateTime.Now.ToShortDateString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Title == string.Empty || Latitude.ToString() == string.Empty ||
                Longitude.ToString() == string.Empty || MarkerType.IDMarkerType == 0)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerValidation"));
                return;
            }


            if (_validFormData)
            {
                ((Form)Parent).DialogResult = DialogResult.OK;
            }
            else
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("LatitudeLongitudeFormatError"));
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).DialogResult = DialogResult.Cancel;
        }

        private void cbSelectTypeMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectTypeMarker.SelectedValue == null) return;

            //TODO: if it's a predefined MarkerType, we should allow the user to select the color. Since ThinkGeo does not work as Dundas, this is temporarily disabled.
            //btnPickAColor.Enabled = MarkerTypeHelper.isPredefinedSymbol((((MarkerType)((ComboBox)(sender)).SelectedItem).Symbol));
            //pnlSelectedColor.Visible = btnPickAColor.Enabled;
            MarkerType mt = ((MarkerType)cbSelectTypeMarker.SelectedItem);
            try
            {
                pbSymbol.Load(DirectoryAndFileHelper.ClientIconsFolder + "\\" + mt.Symbol);
                pbSymbol.Image = pbSymbol.Image.GetThumbnailImage(29, 29, new Image.GetThumbnailImageAbort(GetThumbnailCallback), IntPtr.Zero);
            }
            catch
            { //Concurrency 
            }
        }

        private bool GetThumbnailCallback() { return false; }

        private void SetContextMenuForText()
        {
            ContextMenu cm = new ContextMenu();

            MenuItem mi = new MenuItem("Cut");
            mi.Click += mi_Cut;
            cm.MenuItems.Add(mi);
            mi = new MenuItem("Copy");
            mi.Click += mi_Copy;
            cm.MenuItems.Add(mi);
            mi = new MenuItem("Paste");
            mi.Click += mi_Paste;
            cm.MenuItems.Add(mi);

            txtText.ContextMenu = cm;
        }

        void mi_Cut(object sender, EventArgs e)
        {
            txtText.Cut();
        }
        void mi_Copy(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Rtf, txtText.SelectedText);
            Clipboard.Clear();
        }

        void mi_Paste(object sender, EventArgs e)
        {
            txtText.SelectedText = Clipboard.GetData(DataFormats.UnicodeText).ToString();
        }

        public static bool HasArabicGlyphs(string text)
        {
            char[] glyphs = text.ToCharArray();
            foreach (char glyph in glyphs)
            {
                if (glyph >= 0x600 && glyph <= 0x6ff) return true;
                if (glyph >= 0x750 && glyph <= 0x77f) return true;
                if (glyph >= 0xfb50 && glyph <= 0xfc3f) return true;
                if (glyph >= 0xfe70 && glyph <= 0xfefc) return true;
            }
            return false;
        }

        private void txtLatitude_Leave(object sender, EventArgs e)
        {
            try
            {
                float.Parse(txtLatitude.Text);
                _validFormData = true;
            }
            catch (Exception)
            {
                CustomMessageBox.ShowError(ResourceHelper.GetResourceText("LatitudeLongitudeFormatError"));
                _validFormData = false;
            }
        }

        private void txtLongitude_Leave(object sender, EventArgs e)
        {
            try
            {
                float.Parse(txtLongitude.Text);
                _validFormData = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceHelper.GetResourceText("LatitudeLongitudeFormatError"));
                _validFormData = false;
            }
        }

        private void rtbText_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrlV = e.Modifiers == Keys.Control && e.KeyCode == Keys.V;
            bool shiftIns = e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert;

            if (ctrlV || shiftIns)
            {
                e.Handled = true;
                mi_Paste(new object(), new EventArgs());
            }
        }

        private void pnlMarkerTitleColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pnlMarkerTitleColor.BackColor = colorDialog1.Color;
            }
        }
    }
}
