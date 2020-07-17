using System.Windows.Forms;
using Idea.Entities;
using Idea.ERMT.UserControls;
using Idea.Facade;

namespace Idea.ERMT
{
    
    public static class ViewManager
    {
        private static PrincipalForm ApplicationPrincipalForm { get; set; }

        public static PrincipalForm CreatePrincipalForm()
        {
            PrincipalForm principalForm = new PrincipalForm();
            ApplicationPrincipalForm = principalForm;
            principalForm.LoadModelsMenu();
            return ApplicationPrincipalForm;
        }

        public static void LoadApplicationInitialState()
        {
            ApplicationPrincipalForm.LoadData();
            ApplicationPrincipalForm.SetLeftMenuButtonsVisibility(true);
        }

        public static void SetMainControl(ERMTControl view)
        {
            ERMTUserControl currentUserControl = new ERMTUserControl {Name = "new"};
            switch (view)
            {
                case ERMTControl.About:
                    {
                        About about = new About();
                        about.ShowDialog();
                        break;
                    }
                case ERMTControl.EditRegion:
                {
                    EditRegion editRegion = new EditRegion();
                    currentUserControl = editRegion;
                    SetMainControl(editRegion);
                    ApplicationPrincipalForm.LoadLeftButtons();
                    break;
                }
                case ERMTControl.ElectoralCycle:
                    {
                        IndexUserControl indexUserControl = new IndexUserControl
                        {
                            IndexContentType = IndexContentType.ElectoralCycle
                        };
                        currentUserControl = indexUserControl;
                        SetMainControl(indexUserControl);
                        ApplicationPrincipalForm.LoadLeftButtons("electoralcycle");
                        indexUserControl.ShowHtml();
                        break;
                    }
                case ERMTControl.ElectoralCycleModifyPhase:
                    {
                        ElectoralCycleModifyPhase electoralCycleModifyPhase = new ElectoralCycleModifyPhase();
                        currentUserControl = electoralCycleModifyPhase;
                        SetMainControl(electoralCycleModifyPhase);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.FactorModify:
                    {
                        FactorModify modifyFactor = new FactorModify();
                        currentUserControl = modifyFactor;
                        SetMainControl(modifyFactor);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.FactorNew:
                    {
                        FactorNew newFactor = new FactorNew();
                        currentUserControl = newFactor;
                        SetMainControl(newFactor);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.FactorReorder:
                    {
                        FactorsReorder reorderFactors = new FactorsReorder();
                        currentUserControl = reorderFactors;
                        SetMainControl(reorderFactors);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.KnowledgeResources:
                    {
                        IndexUserControl indexUserControl = new IndexUserControl
                        {
                            IndexContentType = IndexContentType.KnowledgeResources
                        };
                        currentUserControl = indexUserControl;
                        SetMainControl(indexUserControl);
                        ApplicationPrincipalForm.LoadLeftButtons("KnowledgeResources");
                        indexUserControl.ShowHtml();
                        break;
                    }
                case ERMTControl.Login:
                    {
                        LoginUserControl loginUserControl = new LoginUserControl();
                        currentUserControl = loginUserControl;
                        SetMainControl(loginUserControl);
                        break;
                    }
                case ERMTControl.MarkerTypeCRUD:
                    {
                        MarkerTypeCRUD markerTypeControl = new MarkerTypeCRUD();
                        currentUserControl = markerTypeControl;
                        SetMainControl(markerTypeControl);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.ModelEdit:
                    {
                        ModelEdit modelEdit = new ModelEdit();
                        currentUserControl = modelEdit;
                        SetMainControl(modelEdit);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.ModelNew:
                    {
                        ModelNew modelNew = new ModelNew();
                        currentUserControl = modelNew;
                        SetMainControl(modelNew);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.ModelReorderFactors:
                    {
                        ModelReorderFactors modelReorderFactors = new ModelReorderFactors();
                        currentUserControl = modelReorderFactors;
                        SetMainControl(modelReorderFactors);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.RiskActionRegister:
                    {
                        RiskAndActionRegister riskActionRegister = new RiskAndActionRegister(true);
                        currentUserControl = riskActionRegister;
                        SetMainControl(riskActionRegister);
                        ApplicationPrincipalForm.LoadLeftButtons("riskandaction");
                        break;
                    }
                case ERMTControl.RiskMapping:
                    {
                        //RiskMapping riskMapping = new RiskMapping();
                        //currentUserControl = riskMapping;
                        //SetMainControl(riskMapping);
                        currentUserControl = ControlCache.RiskMappingInstance;
                        SetMainControl(ControlCache.RiskMappingInstance);
                        ApplicationPrincipalForm.LoadLeftButtons("map");
                        break;
                    }
                case ERMTControl.Start:
                    {
                        SetMainControl(ControlCache.StartInstance);
                        currentUserControl = ControlCache.StartInstance;
                        ApplicationPrincipalForm.LoadLeftButtons("Start");
                        break;
                    }
                case ERMTControl.TestUserControl:
                    {
                        TestUserControl testUserControl = new TestUserControl();
                        SetMainControl(testUserControl);
                        break;
                    }
                case ERMTControl.UserModify:
                    {
                        UserModify userModify = new UserModify();
                        currentUserControl = userModify;
                        SetMainControl(userModify);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.UserNew:
                    {
                        UserNew userNew = new UserNew();
                        currentUserControl = userNew;
                        SetMainControl(userNew);
                        ApplicationPrincipalForm.LoadLeftButtons();
                        break;
                    }
                case ERMTControl.UserResetPassword:
                {
                    UserChangePassword ucp = new UserChangePassword();
                    ucp.ShowDialog();

                    break;
                }
                
            }
            if (currentUserControl.Name != "new")
            {
                currentUserControl.ShowTitle();
            }
        }

        private static void SetMainControl(UserControl control)
        {
            ApplicationPrincipalForm.SetMainControl(control);
        }

        /// <summary>
        /// Shows the START control.
        /// </summary>
        public static void ShowStart()
        {
            SetMainControl(ERMTControl.Start);
        }


        public static void CloseView()
        {
            //TODO: deberíamos meter inteligencia para cerrar las vistas? por ejemplo, cargar la anterior?
            ShowStart();
        }

        public static void ShowTitle(string title)
        {
            ApplicationPrincipalForm.ShowTitle(title);
        }

        #region Model

        public static void SetCurrentModel(int idModel)
        {
            ERMTSession.Instance.CurrentModel = ModelHelper.GetModel(idModel);
            //XmlDocument doc = new XmlDocument();
            //String configFileName = Utils.DirectoryAndFileHelper.ModelViewConfigurationFile;

            //if (File.Exists(configFileName))
            //{
            //    doc.Load(configFileName);
            //    doc.DocumentElement.ChildNodes[0].Attributes["id"].Value = idModel.ToString();
            //}
            //else
            //{
            //    doc.LoadXml("<last><model id=\"" + ERMTSession.Instance.CurrentModel.IDModel + "\"/></last>");
            //}

            
            //doc.Save(DocumentHelper.GetClientAppDataFolder() + "\\Last.ini");
            ShowTitle(ERMTSession.Instance.CurrentModel.Name);

            //TODO: do we really need to refresh when we change the model?
            //if (this.ContainerControl.Container.Controls.Count > 0)
            //{
            //    GeoSession.Instance.ForceFullRefresh = true;
            //    //Reset current report to avoid selected model loading last selected model settings
            //    Business.GeoSession.Instance.CurrentReport = null;
            //    ((ViewController.GeoUserControl)this.ContainerControl.Container.Controls[0]).Controller.ClearMap();
            //    ((ViewController.GeoUserControl)this.ContainerControl.Container.Controls[0]).Controller.RefreshModel();

            //}
        }

        public static void LoadModelsMenu()
        {
            ApplicationPrincipalForm.LoadModelsMenu();
        }

        #endregion


    }
}
