using System;
using System.Collections.Generic;
using Idea.Facade;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class ModelEdit : ERMTUserControl
    {
        public ModelEdit()
        {
            modelCRUD1.Initialize();
        }

        private void ModelEdit_Load(object sender, EventArgs e)
        {
            EditModel();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("ModelEdit"));
        }

        private void EditModel()
        {
            if (ERMTSession.Instance.CurrentModel != null)
            {
                modelCRUD1.Visible = true;
                modelCRUD1.Editing = true;
                modelCRUD1.Model = ERMTSession.Instance.CurrentModel;

                btnSave.Visible = true;
                btnCancel.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
            btnExport.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (modelCRUD1.FactorsChanged() && CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("FactorsChangedMessage"), CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo) == CustomMessageBoxReturnValue.Cancel)
            {
                return;
            }

            try
            {
                Model model = modelCRUD1.Model;
                if (!ModelHelper.Validate(model))
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("RequiredName"));
                    return;
                }
                List<ModelFactor> factors = modelCRUD1.ModelFactors;
                if (!factors.Any())
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("AtLeastOneFactor"));
                    return;
                }

                model = ModelHelper.Save(model);
                foreach (ModelFactor mf in factors)
                {
                    mf.IDModel = model.IDModel;
                    ModelFactorHelper.Save(mf);
                }
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelSavedOk"));
                ViewManager.LoadModelsMenu();
                ViewManager.ShowStart();
                EventManager.RaiseModelUpdated();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowError(exception.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = ERMTSession.Instance.CurrentModel.Name;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveFileDialog1.FileName))
                {
                    sw.Write(DocumentHelper.Export(ERMTSession.Instance.CurrentModel.IDModel).Content);
                    sw.Flush();
                    sw.Close();
                }
                MessageBox.Show(ResourceHelper.GetResourceText("ModelSavedOk"));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResourceHelper.GetResourceText("DeleteModel"), string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ModelHelper.Delete(ModelHelper.GetModel(ERMTSession.Instance.CurrentModel.IDModel));
                ViewManager.ShowStart();
            }
        }


    }
}
