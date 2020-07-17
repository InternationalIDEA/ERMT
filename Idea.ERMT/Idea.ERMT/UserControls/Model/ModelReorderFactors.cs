using System;
using System.Collections.Generic;
using System.Linq;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class ModelReorderFactors : ERMTUserControl
    {
        Model _model;

        private List<ModelFactor> OrderedModelFactors
        {
            get
            {
                List<ModelFactor> modelFactors= new List<ModelFactor>();
                int i = 0;
                foreach (var o in lbModelFactors.Items)
                {
                    ModelFactor m = ((ModelFactorAux)o).ModelFactor;
                    m.SortOrder = i;
                    modelFactors.Add(m);
                    i++;
                }
                return modelFactors;
            }
        }

        private void ModelReorderFactors_Load(object sender, EventArgs e)
        {
            LoadControls();
            EditModelFactorsOrder();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("CurrentModelReorderFactors"));
        }

        private void LoadControls()
        {
            btnDown.Image = ResourceHelper.GetResourceImage("arrow_down_icon");
            btnUp.Image = ResourceHelper.GetResourceImage("arrow_up_icon");
        }

        private void EditModelFactorsOrder()
        {
            if (ERMTSession.Instance.CurrentModel != null)
            {
                _model = ERMTSession.Instance.CurrentModel;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                lbModelFactors.Items.Clear();
                lbModelFactors.DisplayMember = "Name";
                foreach (ModelFactor mf in ModelFactorHelper.GetByModel(_model).OrderBy(mf => mf.SortOrder))
                {
                    ModelFactorAux aux = new ModelFactorAux { ModelFactor = mf, Name = FactorHelper.Get(mf.IDFactor).Name };
                    lbModelFactors.Items.Add(aux);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.CloseView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ModelFactor f in OrderedModelFactors)
                {
                    ModelFactorHelper.Save(f);
                }

                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelSavedOk"));
                ViewManager.CloseView();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowError(exception.Message);
            }
        }




        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbModelFactors.SelectedItem != null && lbModelFactors.SelectedIndex > 0)//&& ((listBox1.SelectedIndex + 1) < listBox1.Items.Count))
            {
                int index = lbModelFactors.SelectedIndex;
                object a = lbModelFactors.Items[index];
                lbModelFactors.Items.RemoveAt(index);
                lbModelFactors.Items.Insert(index - 1, a);
                lbModelFactors.SelectedIndex = index - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbModelFactors.SelectedItem != null && lbModelFactors.SelectedIndex < lbModelFactors.Items.Count - 1)
            {
                int index = lbModelFactors.SelectedIndex;
                object a = lbModelFactors.Items[index];
                lbModelFactors.Items.RemoveAt(index);
                lbModelFactors.Items.Insert(index + 1, a);
                lbModelFactors.SelectedIndex = index + 1;
            }

        }

        
    }
    public class ModelFactorAux
    {
        public ModelFactor ModelFactor { get; set; }
        public string Name { get; set; }
    }
}
