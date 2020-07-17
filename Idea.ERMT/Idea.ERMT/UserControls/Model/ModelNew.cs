using System;
using System.Collections.Generic;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class ModelNew : ERMTUserControl
    {
        public ModelNew()
        {
            ResetModel();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("ModelAddTitle"));
        }

        private void ResetModel()
        {
            modelCRUD1.Initialize();
            modelCRUD1.Model = ModelHelper.GetNew();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model model = modelCRUD1.Model;
            try
            {
                if (!ModelHelper.Validate(model))
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("RequiredName"));
                    return;
                }
                List<ModelFactor> factors = modelCRUD1.ModelFactors;
                if (factors.Count == 0)
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("AtLeastOneFactor"));
                    return;
                }

                model = ModelHelper.Save(model);
                foreach (ModelFactor mf in factors)
                {
                    mf.IDModel = model.IDModel;
                    ModelFactorHelper.Save(mf);
                }
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelSavedOk"));
                ViewManager.ShowStart();
                ViewManager.LoadModelsMenu();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowError(ResourceHelper.GetResourceText("ModelSavingError") + exception.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }
    }
}
