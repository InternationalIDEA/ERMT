using System;
using System.Collections.Generic;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class Start : ERMTUserControl
    {
        private Boolean _raiseModelChanged = true;
        public Start()
        {
            InitializeComponent();
            EventManager.OnModelChanged += OnModelChanged;
        }

        private void OnModelChanged(object sender, EventArgs e)
        {
            if (sender != null && sender.GetType() != typeof(Start))
            {
                _raiseModelChanged = false;

                Model model = ERMTSession.Instance.CurrentModel;
                if (model != null)
                {
                    cbModels.SelectedValue = model.IDModel;
                }
            }
        }

        private void Start_Load(object sender, EventArgs e)
        {
            ShowTitle();
            LoadModelsMenu();
            SetButtonsImages();
        }

        private void SetButtonsImages()
        {
            btnKnowledgeResources.Image = ResourceHelper.GetResourceImage("ERMTknowledgeresroucesbig");
            btnOpenModel.Image = ResourceHelper.GetResourceImage("ERMTmapsbig");
            btnPreventionAndMitigation.Image = ResourceHelper.GetResourceImage("ERMTelectoral_cyclebig");
        }

        private void btnKnowledgeResources_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.KnowledgeResources);
        }


        private void btnOpenModel_Click(object sender, EventArgs e)
        {
            Model model = (Model)cbModels.SelectedItem;
            ERMTSession.Instance.CurrentModel = model;
            ViewManager.SetMainControl(ERMTControl.RiskMapping);
        }

        private void btnPreventionAndMitigation_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ElectoralCycle);
            //((PrincipalController)((PrincipalForm)this.Parent.Parent.Parent).Controller).CallMenu("Responses.Prevention && Mitigation");
        }

        public void LoadModelsMenu()
        {
            try
            {
                //cbModels.SelectedIndexChanged -= cbModels_SelectedIndexChanged;
                int selectedIndex = cbModels.SelectedIndex;
                List<Model> models = ModelHelper.GetAll();
                cbModels.DataSource = models;
                cbModels.DisplayMember = "Name";
                cbModels.ValueMember = "IDModel";
                Model model = ERMTSession.Instance.CurrentModel;
                if (model != null)
                {
                    cbModels.SelectedValue = model.IDModel;
                    if (selectedIndex != -1)
                    {
                        cbModels.SelectedIndex = selectedIndex <= cbModels.Items.Count - 1 ? selectedIndex : 0;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            //finally
            //{
            //    cbModels.SelectedIndexChanged += cbModels_SelectedIndexChanged;
            //}

        }

        private void cbModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewManager.SetCurrentModel(((Model)cbModels.SelectedItem).IDModel);
                if (_raiseModelChanged)
                {
                    EventManager.RaiseModelChanged(this);
                }
                _raiseModelChanged = true;
                //((PrincipalController)((PrincipalForm)this.Parent.Parent.Parent).Controller).CallMenu("Model." + ((IModel)comboBox1.SelectedItem).Name);
            }
            catch { }

        }

        private void btnKnowledgeResources_MouseEnter(object sender, EventArgs e)
        {
            btnKnowledgeResources.Image = ResourceHelper.GetResourceImage("ERMTknowledgeresroucesbig_active");
        }

        private void btnKnowledgeResources_MouseLeave(object sender, EventArgs e)
        {
            btnKnowledgeResources.Image = ResourceHelper.GetResourceImage("ERMTknowledgeresroucesbig");
        }

        private void btnOpenModel_MouseEnter(object sender, EventArgs e)
        {
            btnOpenModel.Image = ResourceHelper.GetResourceImage("ERMTmapsbig_active");
        }

        private void btnOpenModel_MouseLeave(object sender, EventArgs e)
        {
            btnOpenModel.Image = ResourceHelper.GetResourceImage("ERMTmapsbig");
        }

        private void btnPreventionAndMitigation_MouseEnter(object sender, EventArgs e)
        {
            btnPreventionAndMitigation.Image = ResourceHelper.GetResourceImage("ERMTelectoral_cyclebig_active");
        }

        private void btnPreventionAndMitigation_MouseLeave(object sender, EventArgs e)
        {
            btnPreventionAndMitigation.Image = ResourceHelper.GetResourceImage("ERMTelectoral_cyclebig");
        }

        public override void ShowTitle()
        {
            if (ERMTSession.Instance.CurrentModel != null)
            {
                ViewManager.ShowTitle(ERMTSession.Instance.CurrentModel.Name);
            }
        }


    }
}
