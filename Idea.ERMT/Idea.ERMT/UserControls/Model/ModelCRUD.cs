using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Idea.Facade;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class ModelCRUD : ERMTUserControl
    {
        private bool _editing = false;
        private Model _model;
        private List<Region> _allRegions; 

        public ModelCRUD()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            LoadData();
            LoadCombos();
        }

        public Boolean FactorsChanged()
        {
            if (_model == null)
            {
                //it's a new model, then factors MUST have been added.
                return true;
            }

            List<int> databaseFactors = ModelFactorHelper.GetByModel(_model).Select(mf=>mf.IDFactor).ToList();
            List<int> checkedFactors = (from FactorItem fi in flpFactors.Controls where fi.Checked select fi.Factor.IDFactor).ToList();
            databaseFactors.Sort();
            checkedFactors.Sort();
            return (!databaseFactors.SequenceEqual(checkedFactors));
        }

        private void LoadData()
        {
            _allRegions = RegionHelper.GetAll();
        }

        private void LoadCombos()
        {
            tvRegion.Nodes.Clear();
            Region world = RegionHelper.GetWorld();
            
            TreeNode node = new TreeNode { Text = ResourceHelper.GetResourceText("World"), Name = "1", Tag = world };
            tvRegion.Nodes.Add(node);
            AddChildRegions(tvRegion.Nodes[0], _allRegions.Where(r=>r.IDParent == 1).ToList());
            if (tvRegion.SelectedNode != null)
            {
                tvRegion.SelectedNode.EnsureVisible();
                tvRegion.SelectedNode.Expand();
            }

            if (tvRegion.Nodes.Count > 0)
            {
                tvRegion.ExpandAll();
            }
        }

        private void AddChildRegions(TreeNode treeNode, List<Region> regions)
        {
            foreach (Region r in regions)
            {
                if (r.IDParent.ToString() == treeNode.Name)
                {
                    TreeNode t = new TreeNode { Text = r.RegionName, Name = r.IDRegion.ToString() };
                    treeNode.Nodes.Add(t);
                    AddChildRegions(t, _allRegions.Where(r2=>r2.IDParent == r.IDRegion).ToList());
                }
            }
        }

        public bool Editing
        {
            set
            {
                _editing = value;
                tvRegion.Enabled = !_editing;
            }
            get { return _editing; }

        }

        public Model Model
        {
            get
            {
                if (_model == null)
                {
                    _model = ModelHelper.GetNew();
                }
                
                _model.Name = txtName.Text;
                _model.Description = txtDescription.Text;
                if (!_editing) //Region can't be changed.
                {
                    _model.IDRegion = int.Parse(tvRegion.SelectedNode.Name);
                }

                return _model;
            }
            set
            {
                if (value != null)
                {
                    _model = value;
                    txtName.Text = _model.Name;
                    txtDescription.Text = _model.Description;
                    LoadFactors(ModelFactorHelper.GetByModel(_model));
                    LoadCombos();
                    tvRegion.SelectedNode = _model.IDRegion != 0 ? tvRegion.Nodes.Find(_model.IDRegion.ToString(), true)[0] : tvRegion.Nodes.Find("1", true)[0];
                }
            }
        }

        public List<ModelFactor> ModelFactors
        {
            get
            {
                List<ModelFactor> factors = (_model != null
                    ? ModelFactorHelper.GetByModel(_model)
                    : new List<ModelFactor>());

                foreach (FactorItem fi in flpFactors.Controls)
                {
                    if (fi.Checked)
                    {
                        if (!(from a in factors select a.IDFactor).Contains(fi.Factor.IDFactor))
                        {
                            factors.Add(fi.Factor);
                        }
                        else if (fi.HasChanged)
                        {
                            ModelFactor mf = (from a in factors where a.IDFactor == fi.Factor.IDFactor select a).FirstOrDefault();
                            mf.Weight = fi.Factor.Weight;
                            mf.Interval = fi.Factor.Interval;
                            mf.ScaleMax = fi.Factor.ScaleMax;
                            mf.ScaleMin = fi.Factor.ScaleMin;
                        }
                    }
                    else
                    {
                        ModelFactor fac = (from a in factors where a.IDFactor == fi.Factor.IDFactor select a).FirstOrDefault();
                        if (fac != null && fac.IDFactor != 0)
                        {
                            factors.Remove(fac);
                            ModelFactorHelper.Delete(fac);
                        }
                    }
                }
                return factors;
            }
        }

        private void LoadFactors(List<ModelFactor> list)
        {
            List<Factor> allFactors = FactorHelper.GetAll();
            flpFactors.Controls.Clear();
            foreach (Factor factor in allFactors)
            {
                FactorItem control = new FactorItem();
                ModelFactor aux = ModelFactorHelper.GetNew();
                aux.IDFactor = factor.IdFactor;
                aux.IDModel = _model.IDModel;
                if (!factor.CumulativeFactor)
                {
                    aux.Interval = factor.Interval;
                    aux.ScaleMin = factor.ScaleMin;
                    aux.ScaleMax = factor.ScaleMax;
                    aux.Weight = 100;
                }
                foreach (ModelFactor modelFactor in list)
                {
                    if (factor.IdFactor == modelFactor.IDFactor)
                    {
                        control.Checked = true;
                        if (factor.CumulativeFactor) break;
                        aux.Interval = modelFactor.Interval;
                        aux.ScaleMin = modelFactor.ScaleMin;
                        aux.ScaleMax = modelFactor.ScaleMax;
                        aux.Weight = modelFactor.Weight;
                        break;
                    }
                }
                control.Factor = aux;
                control.Editing = _editing;
                flpFactors.Controls.Add(control);
            }
        }
    }
}
