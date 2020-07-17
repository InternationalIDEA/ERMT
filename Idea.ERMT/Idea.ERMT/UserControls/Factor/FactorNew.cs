using System;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class FactorNew : ERMTUserControl
    {
        private Factor Factor
        {
            set
            {
                factorCRUD.Factor = value;
            }
        }
    
        public FactorNew()
        {
            Factor = new Factor();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FactorSaved(this, factorCRUD.Factor);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel(this, EventArgs.Empty);
        }

        public override void Clear()
        {
            factorCRUD.Factor = null;
            base.Clear();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("AddNewFactor"));
        }

        public void FactorSaved(object sender, Factor factor)
        {
            try
            {
                FactorHelper.Validate(factor);
                
                FactorHelper.Save(factor);
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("FactorSavedOk"),
                    CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);

                ViewManager.ShowStart();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText(exception.Message), CustomMessageBoxMessageType.Error,
                    CustomMessageBoxButtonType.OKOnly);
            }
        }


        public void Cancel(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }
    }
}
