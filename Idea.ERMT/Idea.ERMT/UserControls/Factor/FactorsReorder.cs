using System;
using System.Collections.Generic;
using System.Linq;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class FactorsReorder : ERMTUserControl
    {
        public FactorsReorder()
        {
            InitializeComponent();
            LoadFactors();
            LoadControls();
        }

        private void LoadControls()
        {
            btnDownInternalFactors.Image = ResourceHelper.GetResourceImage("arrow_down_icon");
            btnUpInternalFactors.Image = ResourceHelper.GetResourceImage("arrow_up_icon");
            btnDownExternalFactors.Image = ResourceHelper.GetResourceImage("arrow_down_icon");
            btnUpExternalFactors.Image = ResourceHelper.GetResourceImage("arrow_up_icon");
        }

        public void LoadFactors()
        {
            lbInternalFactors.Items.Clear();
            lbInternalFactors.ValueMember = "IDFactor";
            lbInternalFactors.DisplayMember = "Name";

            lbExternalFactors.Items.Clear();
            lbExternalFactors.ValueMember = "IDFactor";
            lbExternalFactors.DisplayMember = "Name";

            foreach (Factor factor in FactorHelper.GetAll().Where(l => l.InternalFactor).OrderBy(f => f.SortOrder))
            {
                lbInternalFactors.Items.Add(factor);
            }


            foreach (Factor factor in FactorHelper.GetAll().Where(l => !l.InternalFactor).OrderBy(f => f.SortOrder))
            {
                lbExternalFactors.Items.Add(factor);
            }
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("ReorderFactors"));
        }

        public List<Factor> OrderedInternalFactors
        {
            get
            {
                List<Factor> factors = new List<Factor>();
                int i = 0;
                foreach (Factor f in lbInternalFactors.Items)
                {
                    f.SortOrder = i;
                    factors.Add(f);
                    i++;
                }
                return factors;
            }
        }

        public List<Factor> OrderedExternalFactors
        {
            get
            {
                List<Factor> factors = new List<Factor>();
                int i = 0;
                foreach (Factor f in lbExternalFactors.Items)
                {
                    f.SortOrder = i;
                    factors.Add(f);
                    i++;
                }
                return factors;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFactors(OrderedInternalFactors);
            SaveFactors(OrderedExternalFactors);
            DocumentHelper.DownloadFiles();
        }

        public void SaveFactors(List<Factor> factors)
        {
            try
            {
                foreach (Factor f in factors)
                {
                    FactorHelper.Save(f);
                }
                if (factors[0].InternalFactor) return;
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ReorderFactorsSavedOk"),
                    CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);
                ViewManager.ShowStart();
            }
            catch (Exception exception)
            {
                CustomMessageBox.ShowMessage(exception.Message, CustomMessageBoxMessageType.Error,
                    CustomMessageBoxButtonType.OKOnly);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }

        private void btnUpInternalFactors_Click(object sender, EventArgs e)
        {
            if (lbInternalFactors.SelectedItem != null && lbInternalFactors.SelectedIndex > 0)//&& ((lbInternalFactors.SelectedIndex + 1) < lbInternalFactors.Items.Count))
            {
                int index = lbInternalFactors.SelectedIndex;
                object a = lbInternalFactors.Items[index];
                lbInternalFactors.Items.RemoveAt(index);
                lbInternalFactors.Items.Insert(index - 1, a);
                lbInternalFactors.SelectedIndex = index - 1;
            }
        }

        private void btnDownInternalFactors_Click(object sender, EventArgs e)
        {
            if (lbInternalFactors.SelectedItem != null && lbInternalFactors.SelectedIndex < lbInternalFactors.Items.Count - 1)
            {
                int index = lbInternalFactors.SelectedIndex;
                object a = lbInternalFactors.Items[index];
                lbInternalFactors.Items.RemoveAt(index);
                lbInternalFactors.Items.Insert(index + 1, a);
                lbInternalFactors.SelectedIndex = index + 1;
            }
        }

        private void btnUpExternalFactors_Click(object sender, EventArgs e)
        {
            if (lbExternalFactors.SelectedItem != null && lbExternalFactors.SelectedIndex > 0)//&& ((lbInternalFactors.SelectedIndex + 1) < lbInternalFactors.Items.Count))
            {
                int index = lbExternalFactors.SelectedIndex;
                object a = lbExternalFactors.Items[index];
                lbExternalFactors.Items.RemoveAt(index);
                lbExternalFactors.Items.Insert(index - 1, a);
                lbExternalFactors.SelectedIndex = index - 1;
            }
        }

        private void btnDownExternalFactors_Click(object sender, EventArgs e)
        {
            if (lbExternalFactors.SelectedItem != null && lbExternalFactors.SelectedIndex < lbExternalFactors.Items.Count - 1)
            {
                int index = lbExternalFactors.SelectedIndex;
                object a = lbExternalFactors.Items[index];
                lbExternalFactors.Items.RemoveAt(index);
                lbExternalFactors.Items.Insert(index + 1, a);
                lbExternalFactors.SelectedIndex = index + 1;
            }
        }
    }
}
