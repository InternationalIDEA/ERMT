using System;
using System.Collections.Generic;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class FactorModify : ERMTUserControl
    {
        public Factor Factor
        {
            set { factorCRUD.Factor = value; }
            get { return factorCRUD.Factor; }
        }

        public FactorModify()
        {
            LoadControls();
        }

        private void LoadControls()
        {
            LoadCombo();
        }

        public void LoadCombo()
        {
            List<Factor> factorlist = FactorHelper.GetAll();
            cbFactors.DisplayMember = "Name";
            cbFactors.ValueMember = "ID";
            cbFactors.DataSource = factorlist;
        }

        private void cbFactors_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbFactors.SelectedIndex >= 0)
            {
                FactorSelected(this, (Factor)cbFactors.SelectedItem);
            }
            else
                factorCRUD.Factor = null;
        }

        public override void Clear()
        {
            cbFactors.SelectedIndex = -1;
            factorCRUD.Factor = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FactorSaved(this, factorCRUD.Factor);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel(this, EventArgs.Empty);
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("ModifyFactor"));   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FactorDeleted(this, factorCRUD.Factor);
        }

        public void FactorSelected(object sender, Factor factor)
        {
            factorCRUD.Factor = factor;
        }

        public void FactorSaved(object sender, Factor factor)
        {
            try
            {
                FactorHelper.Validate(factor);

                FactorHelper.Save(factor);

                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("FactorSavedOk"),
                    CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);

                LoadCombo();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowMessage(exception.Message, CustomMessageBoxMessageType.Error,
                    CustomMessageBoxButtonType.OKOnly);
            }
        }

        public void FactorDeleted(object sender, Factor factor)
        {
            try
            {
                if (
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("AssociatedDataWillBeLost"),
                        CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) ==
                    CustomMessageBoxReturnValue.Ok)
                {
                    FactorHelper.Delete(factor);
                }

                LoadCombo();
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage(ex.Message);
            }
        }

        public void Cancel(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }

      }
}
