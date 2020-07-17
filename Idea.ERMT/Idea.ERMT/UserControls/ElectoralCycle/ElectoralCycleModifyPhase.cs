using System;
using System.Configuration;
using System.Linq;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public partial class ElectoralCycleModifyPhase : ERMTUserControl
    {
        private Phase _phase;

        private Phase Phase
        {
            set
            {
                _phase = value;
                if (value != null)
                {
                    txtPhaseName.Text = value.Title;
                    col1HtmlEditorControl.InnerHtml = value.Column1Text;
                    col2HtmlEditorControl.InnerHtml = value.Column2Text;
                    col3HtmlEditorControl.InnerHtml = value.Column3Text;
                    practitionersTipsHtmlEditorControl.InnerHtml = value.PractitionersTips;

                    column1BulletList.Bullets = PhaseBulletHelper.GetByPhaseAndColumn(_phase.IDPhase, 1);
                    column2BulletList.Bullets = PhaseBulletHelper.GetByPhaseAndColumn(_phase.IDPhase, 2);
                    column3BulletList.Bullets = PhaseBulletHelper.GetByPhaseAndColumn(_phase.IDPhase, 3);
                }
                else
                {
                    txtPhaseName.Text = string.Empty;
                    col1HtmlEditorControl.BodyHtml = string.Empty;
                    col2HtmlEditorControl.BodyHtml = string.Empty;
                    col3HtmlEditorControl.BodyHtml = string.Empty;
                    practitionersTipsHtmlEditorControl.BodyHtml = string.Empty;
                }
                col1HtmlEditorControl.LinkStyleSheet(DirectoryAndFileHelper.ClientHTMLFolder + "\\PMM\\style.css");
                col2HtmlEditorControl.LinkStyleSheet(DirectoryAndFileHelper.ClientHTMLFolder + "\\PMM\\style.css");
                col3HtmlEditorControl.LinkStyleSheet(DirectoryAndFileHelper.ClientHTMLFolder + "\\PMM\\style.css");
            }
        }

        public ElectoralCycleModifyPhase()
        {
            InitializeComponent();
            LoadCombo();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("ModifyPhase"));
        }

        private void LoadCombo()
        {
            cbPhases.DisplayMember = "Title";
            cbPhases.ValueMember = "IDPhase";
            cbPhases.DataSource = PhaseHelper.GetAll();
        }

        private void cbPhases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhases.SelectedIndex >= 0)
            {
                columnsTabControl.SelectedIndex = 0;
                Phase = (Phase)cbPhases.SelectedItem;

                // Get the bullets:
                //PhaseBulletHelper.GetAll();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _phase.Title = txtPhaseName.Text;
            _phase.Column1Text = col1HtmlEditorControl.InnerHtml;
            _phase.Column2Text = col2HtmlEditorControl.InnerHtml;
            _phase.Column3Text = col3HtmlEditorControl.InnerHtml;
            _phase.PractitionersTips = practitionersTipsHtmlEditorControl.InnerHtml;

            // Reorder bullets (they were ordereded in the control, but need to reorder the underlying list:
            column1BulletList.SortList();
            column2BulletList.SortList();
            column3BulletList.SortList();


            //delete phase bullets removed by the user.
            foreach (int idPhaseBullet in column1BulletList.PhaseBulletsIDsToDelete)
            {
                PhaseBulletHelper.Delete(idPhaseBullet);
            }

            foreach (int idPhaseBullet in column2BulletList.PhaseBulletsIDsToDelete)
            {
                PhaseBulletHelper.Delete(idPhaseBullet);
            }

            foreach (int idPhaseBullet in column3BulletList.PhaseBulletsIDsToDelete)
            {
                PhaseBulletHelper.Delete(idPhaseBullet);
            }

            foreach (PhaseBullet phaseBullet in column1BulletList.Bullets)
            {
                phaseBullet.ColumnNumber = 1;
                phaseBullet.IDPhase = _phase.IDPhase;
            }

            foreach (PhaseBullet phaseBullet in column2BulletList.Bullets)
            {
                phaseBullet.ColumnNumber = 2;
                phaseBullet.IDPhase = _phase.IDPhase;
            }

            foreach (PhaseBullet phaseBullet in column3BulletList.Bullets)
            {
                phaseBullet.ColumnNumber = 3;
                phaseBullet.IDPhase = _phase.IDPhase;
            }

            try
            {
                PhaseHelper.Validate(_phase);
                PhaseHelper.Save(_phase);

                PhaseBulletHelper.SaveColumnBullets(column1BulletList.Bullets);
                PhaseBulletHelper.SaveColumnBullets(column2BulletList.Bullets);
                PhaseBulletHelper.SaveColumnBullets(column3BulletList.Bullets);
                
                PhaseHelper.GenerateAllFiles(_phase);

                DocumentHelper.DownloadFilesAsync();

                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PhaseSavedOk"));

                LoadCombo();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowError(exception.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.CloseView();
        }
    }
}
