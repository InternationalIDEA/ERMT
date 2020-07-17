using System;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class FactorItem : ERMTUserControl
    {
        ModelFactor factor;
        bool _editing = false;
        bool _locked = false; // used for locking the control if it is checked at the time a model is edited (to disallow changin factor scales)
        bool _hasChanged = false;
        bool _loading = false;
        public FactorItem()
        {
            InitializeComponent();
        }

        public bool Checked
        {
            get { return chkFactor.Checked; }
            set
            {
                chkFactor.Checked = value;
            }
        }

        public string FactorName
        {
            set
            {
                chkFactor.Text = value;
            }
        }

        public ModelFactor Factor
        {
            get
            {
                if (factor == null)
                    return null;
                if (nuInt.Visible)
                {
                    factor.Interval = nuInt.Value;
                    factor.ScaleMin = (int)nuMin.Value;
                    factor.ScaleMax = (int)nuMax.Value;
                    factor.Weight = (int)nuWeight.Value;
                }
                else
                {
                    FactorHelper.Get(factor.IDFactor).CumulativeFactor = true;
                }
                return factor;
            }
            set
            {
                factor = value;
                Factor f = FactorHelper.Get(factor.IDFactor);
                chkFactor.Text = f.Name;
                _loading = true;
                if (!f.CumulativeFactor)
                {
                    nuInt.Value = factor.Interval > 0.1M ? factor.Interval : 0.1M;
                    nuMin.Value = factor.ScaleMin;
                    nuMax.Value = factor.ScaleMax;
                    nuWeight.Value = factor.Weight;
                }
                else
                {
                    chkFactor.Text += String.Format(" ({0})", ResourceHelper.GetResourceText("Cumulative"));
                    nuInt.Visible = false;
                    nuMin.Visible = false;
                    nuMax.Visible = false;
                    nuWeight.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                }
                _loading = false;
            }
        }

        public bool Editing
        { get { return _editing; } set { _editing = value; } }

        public bool HasChanged
        {
            get { return _hasChanged; }
        }

        private void chkFactor_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkFactor.Checked && _editing)
            {
                nuInt.Enabled = nuMax.Enabled = nuMin.Enabled = true;
            }
            else
            {
                if (_editing)
                    _locked = true;
            }
        }

        private void nuMin_ValueChanged(object sender, System.EventArgs e)
        {
            if (!this._hasChanged && !_loading)
            {
                MessageBox.Show(ResourceHelper.GetResourceText("FactorsChangedMessage2"), ResourceHelper.GetResourceText("DataChangedTitle"), MessageBoxButtons.OK);
                _hasChanged = true;
            }

        }

        private void nuWeight_ValueChanged(object sender, System.EventArgs e)
        {
            if (!this._hasChanged && !_loading)
                _hasChanged = true;
        }
    }
}
