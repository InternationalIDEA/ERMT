using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class FactorCRUD : ERMTUserControl
    {
        private Factor _factor;
        Document _document;
        string _cssFile;
        FactorPreview _previewForm;

        public Factor Factor
        {
            get
            {
                if (_factor == null)
                {
                    return null;
                }
                _factor.Name = txtFactorName.Text;
                if (!string.IsNullOrEmpty(nudScaleMin.Text))
                {
                    _factor.ScaleMin = int.Parse(nudScaleMin.Text);
                }
                if (!string.IsNullOrEmpty(nudScaleMax.Text))
                {
                    _factor.ScaleMax = int.Parse(nudScaleMax.Text);
                }
                if (!string.IsNullOrEmpty(nudScaleInterval.Text))
                {
                    _factor.Interval = decimal.Parse(nudScaleInterval.Text);
                }
                _factor.CumulativeFactor = cbScaleOrCumulative.SelectedIndex == 1;
                if (_factor.CumulativeFactor)
                {
                    _factor.Color = ColorTranslator.ToHtml(colorPickerPrimary.Value) + "," + ColorTranslator.ToHtml(colorPickerSecondary.Value);
                }
                _factor.InternalFactor = cbInternalExternal.SelectedIndex == 0;
                _factor.DataCollection = txtDataCollection.InnerHtml;
                _factor.EmpiricalCases = txtEmpiricalCases.InnerHtml;
                _factor.Introduction = txtIntroduction.InnerHtml;
                _factor.ObservableIndicators = txtObservableIndicators.InnerHtml;
                _factor.Questionnaire = txtQuestionnaire.InnerHtml;
                return _factor;
            }
            set
            {
                _document = !DesignMode ? DocumentHelper.GetDocumentTemplate() : new Document();

                _cssFile = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["DefaultCss"];
                _factor = value;
                cbFactorDescriptionField.SelectedIndex = 0;
                pnlIntroduction.Visible = true;
                if (!DesignMode)
                {
                    pnlDataCollection.Visible = pnlEmpiricalCases.Visible = pnlObservableIndicators.Visible = pnlQuestionnaire.Visible = false;
                }

                if (value != null && value.IdFactor != 0)
                {
                    txtFactorName.Text = _factor.Name;
                    nudScaleMin.Text = _factor.ScaleMin.ToString();
                    nudScaleMax.Text = _factor.ScaleMax.ToString();
                    nudScaleInterval.Text = _factor.Interval.ToString();
                    cbScaleOrCumulative.Enabled = false;
                    if (_factor.CumulativeFactor)
                    {
                        cbScaleOrCumulative.SelectedIndex = 1;
                        cbInternalExternal.SelectedIndex = _factor.InternalFactor ? 0 : 1;
                        gbCumulativeFactorColor.Visible = true;
                        if (_factor.Color != string.Empty)
                        {
                            SetColors(ColorTranslator.FromHtml(_factor.Color.Split(',')[0]),
                                      ColorTranslator.FromHtml(_factor.Color.Split(',')[1]));
                        }
                    }
                    else
                    {
                        SetColors(SystemColors.Control, SystemColors.ControlText);
                        cbInternalExternal.SelectedIndex = _factor.InternalFactor ? 0 : 1;
                        cbScaleOrCumulative.SelectedIndex = 0;
                        if (_factor.IdFactor != 0)
                        {
                            gbCumulativeFactorColor.Visible = false;
                        }
                    }
                    txtDataCollection.InnerHtml = _factor.DataCollection;
                    txtDataCollection.LinkStyleSheet(_cssFile);
                    txtEmpiricalCases.InnerHtml = _factor.EmpiricalCases;
                    txtEmpiricalCases.LinkStyleSheet(_cssFile);
                    txtIntroduction.InnerHtml = _factor.Introduction;
                    txtIntroduction.LinkStyleSheet(_cssFile);
                    txtObservableIndicators.InnerHtml = _factor.ObservableIndicators;
                    txtObservableIndicators.LinkStyleSheet(_cssFile);
                    txtQuestionnaire.InnerHtml = _factor.Questionnaire;
                    txtQuestionnaire.LinkStyleSheet(_cssFile);
                }
                else
                {
                    txtFactorName.Text = string.Empty;
                    txtDataCollection.InnerHtml = string.Empty;
                    txtEmpiricalCases.InnerHtml = string.Empty;
                    txtIntroduction.InnerHtml = string.Empty;
                    txtObservableIndicators.InnerHtml = string.Empty;
                    txtQuestionnaire.InnerHtml = string.Empty;
                    nudScaleMin.Value = 1;
                    nudScaleMax.Value = 5;
                    nudScaleInterval.Value = 1;
                    cbInternalExternal.SelectedIndex = 0;
                    cbScaleOrCumulative.SelectedIndex = 0;
                }
            }
        }

        public FactorCRUD()
        {
            InitializeComponent();
            LoadControls();
            LoadFactorDescriptionFields();
        }

        private void LoadFactorDescriptionFields()
        {
            cbFactorDescriptionField.Items.Clear();
            cbFactorDescriptionField.Items.Add(ResourceHelper.GetResourceText("FactorCRUDDescriptionField1"));
            cbFactorDescriptionField.Items.Add(ResourceHelper.GetResourceText("FactorCRUDDescriptionField2"));
            cbFactorDescriptionField.Items.Add(ResourceHelper.GetResourceText("FactorCRUDDescriptionField3"));
            cbFactorDescriptionField.Items.Add(ResourceHelper.GetResourceText("FactorCRUDDescriptionField4"));
            cbFactorDescriptionField.Items.Add(ResourceHelper.GetResourceText("FactorCRUDDescriptionField5"));
        }

        private void LoadControls()
        {
            btnAddDocument.Image = ResourceHelper.GetResourceImage("new_doc_icon");
            btnDeleteDocument.Image = ResourceHelper.GetResourceImage("new_doc_icon");
            btnLinkDocument.Image = ResourceHelper.GetResourceImage("page_white_link_icon");
            ofdAddDocument.AddExtension = true;
            ofdAddDocument.RestoreDirectory = true;
            ofdAddDocument.Title = ResourceHelper.GetResourceText("DocumentSelectAdd");
            ofdAddDocument.InitialDirectory = @"C:/";
        }

        private void cbFactorDescriptionField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode) return;

            pnlIntroduction.Visible = pnlEmpiricalCases.Visible = pnlObservableIndicators.Visible = pnlDataCollection.Visible = pnlQuestionnaire.Visible = false;
            switch (cbFactorDescriptionField.SelectedIndex)
            {
                case 0: pnlIntroduction.Visible = true; break;
                case 1: pnlEmpiricalCases.Visible = true; break;
                case 2: pnlObservableIndicators.Visible = true; break;
                case 3: pnlDataCollection.Visible = true; break;
                case 4: pnlQuestionnaire.Visible = true; break;
            }
        }

        private void ShowPreviewHTML()
        {
            //Updates html on preview control
            string html = _document.Content.Replace("@Introduction@", txtIntroduction.InnerHtml);
            html = html.Replace("@Empirical Cases and Correlation@", txtEmpiricalCases.InnerHtml);
            html = html.Replace("@Data Collection and Analysis methodologies@", txtDataCollection.InnerHtml);
            html = html.Replace("@Questionnaire@", txtQuestionnaire.InnerHtml);
            html = html.Replace("@Observable indicators@", txtObservableIndicators.InnerHtml);
            html = html.Replace("@Title@", txtFactorName.Text);
            html = html.Replace("graphical3.css", _cssFile);
            html = html.Replace("src=\"./images/", "src=\"" + DirectoryAndFileHelper.ClientHTMLFolder + "images\\");
            _previewForm = new FactorPreview { BodyHtml = html };


            if (!string.IsNullOrEmpty(_cssFile))
            {
                _previewForm.LinkStyleSheet(_cssFile);
            }
            _previewForm.Tag = html;
            _previewForm.ShowDialog();
        }

        private void btnLinkDocument_Click(object sender, EventArgs e)
        {
            switch (cbFactorDescriptionField.SelectedIndex)
            {
                case 0:
                    {
                        if (txtIntroduction.SelectedText.Length > 0)
                        {
                            txtIntroduction.InsertLinkToDocumentPrompt();
                        }

                        break;
                    }

                case 1: txtEmpiricalCases.InsertLinkToDocumentPrompt(); break;
                case 2: txtObservableIndicators.InsertLinkToDocumentPrompt(); break;
                case 3: txtDataCollection.InsertLinkToDocumentPrompt(); break;
                case 4: txtQuestionnaire.InsertLinkToDocumentPrompt(); break;
            }
        }

        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            string fileName = GetDocumentToLoad();
            if (!string.IsNullOrEmpty(fileName))
            {
                string newFileName = DirectoryAndFileHelper.ClientDocumentsFolder +
                                     fileName.Split('\\')[fileName.Split('\\').Length - 1];
                if (File.Exists(newFileName))
                {
                    if (CustomMessageBoxReturnValue.Ok !=
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DocumentExistsAlready"),
                           CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo))
                    {
                        return;
                    }
                    File.Delete(newFileName);
                }
                File.Copy(fileName, newFileName);

                byte[] fileContent = File.ReadAllBytes(newFileName);
                Document document = new Document
                {
                    Content = Convert.ToBase64String(fileContent),
                    Filename = newFileName,
                    DocumentType = ERMTDocumentType.Document
                };
                DocumentHelper.Save(document);
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DocumentAddedOk"));
            }
        }

        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                                {
                                                    InitialDirectory = DirectoryAndFileHelper.ClientDocumentsFolder,
                                                    CheckPathExists = true,
                                                    Multiselect = false,
                                                    Title = ResourceHelper.GetResourceText("DeleteDocumentDialogTitle"),

                                                };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DirectoryInfo diSelectedFile = new DirectoryInfo(openFileDialog.FileName);
            DirectoryInfo diInitialDirectory = new DirectoryInfo(openFileDialog.InitialDirectory);
            if (Path.GetDirectoryName(diSelectedFile.FullName) != Path.GetDirectoryName(diInitialDirectory.FullName))
            {
                //means they have changed the base directory. We can't allow that.
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DeleteDocumentUseDefaultDirectory"));
                return;
            }


            if (File.Exists(openFileDialog.FileName))
            {
                if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DeleteDocumentWarning"), CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) 
                    == CustomMessageBoxReturnValue.Ok)
                {
                    //delete the local copy of the file
                    File.Delete(openFileDialog.FileName);
                }
            }

            Document documentToDelete = new Document
                                    {
                                        Content = string.Empty,
                                        Filename = openFileDialog.FileName,
                                        DocumentType = ERMTDocumentType.Document
                                    };
            DocumentHelper.Delete(documentToDelete);
            CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DocumentSuccessfullyDeleted"));
        }

        public string GetDocumentToLoad()
        {
            if (ofdAddDocument.ShowDialog() == DialogResult.OK)
                return ofdAddDocument.FileName;
            return null;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            ShowPreviewHTML();
        }

        private void colorPickerPrimary_Click(object sender, ColorPickerEventArgs e)
        {
            colorDialog1.Color = colorPickerPrimary.Value;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SetPrimaryColor(colorDialog1.Color);
            }
        }

        private void colorPickerSecondary_Click(object sender, ColorPickerEventArgs e)
        {
            colorDialog1.Color = colorPickerSecondary.Value;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SetSecondaryColor(colorDialog1.Color);
            }
        }

        private void SetPrimaryColor(Color primary)
        {
            colorPickerPrimary.Value = primary;
            lblPreview.BackColor = primary;
        }

        private void SetSecondaryColor(Color secondary)
        {
            colorPickerSecondary.Value = secondary;
            lblPreview.ForeColor = secondary;
        }

        private void SetColors(Color primary, Color secondary)
        {
            SetPrimaryColor(primary);
            SetSecondaryColor(secondary);
        }

        private void cbScaleOrCumulative_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbScaleOrCumulative.SelectedIndex == 0)
            {
                gbScale.Visible = true;
                gbCumulativeFactorColor.Visible = false;
            }
            else
            {
                gbScale.Visible = false;
                gbCumulativeFactorColor.Visible = true;
            }
        }
    }
}
