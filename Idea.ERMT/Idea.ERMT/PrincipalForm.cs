using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Idea.Entities;
using Idea.ERMT.UserControls;
using Idea.Facade;
using Idea.Utils;
using System.Configuration;
using System.Reflection;

namespace Idea.ERMT
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
            LoadControls();
        }

        public void SetMainControl(UserControl control)
        {
            RemoveControls();
            control.Dock = DockStyle.Fill;
            gbPrincipal.Controls.Add(control);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //// Confirm user wants to close
            //switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            //{
            //    case DialogResult.No:
            //        e.Cancel = true;
            //        break;
            //    default:
            //        break;
            //}
            if (ControlCache.RiskMappingInstanceCreated)
            {
                ControlCache.RiskMappingInstance.SaveCurrentModelView();
            }
        }

        /// <summary>
        /// Removes all the controls in the main group box
        /// </summary>
        private void RemoveControls()
        {
            if (gbPrincipal.Controls.Count == 1 && gbPrincipal.Controls[0].GetType() == typeof(RiskMapping))
            {
                //it's the map. Let's check that the legends are NOT showing
                RiskMapping rm = (RiskMapping)gbPrincipal.Controls[0];
                rm.HideLegends();
            }

            gbPrincipal.Controls.Clear();
            lblTitle.Text = string.Empty;
        }

        public void LoadData()
        {
            User user = ERMTSession.Instance.CurrentUser;
            lblUser.Text = ResourceHelper.GetResourceText("User") + ": " + user.Name + " " + user.Lastname;
            msPrincipal.Visible = true;
            lblTitle.Text = string.Empty;
            tsmiAdmin.Enabled = (user.IDRole < 3);
            tsmiAdmin.DropDownItems[1].Enabled = (user.IDRole == 1);
            tsmiAdmin.DropDownItems[2].Enabled = (user.IDRole == 1);
            tsmiAdmin.DropDownItems[4].Enabled = (user.IDRole == 1);

            DocumentHelper.DownloadFiles();

            tsmiCreateNewModel.Enabled = (user.IDRole < 3);
            tsmiDeleteCurrentModel.Enabled = (user.IDRole < 3);
            tsmiImportModel.Enabled = (user.IDRole < 3);
            tsmiEditCurrentModel.Enabled = (user.IDRole < 3);

            //Load last used model
            string modelConfigFileName = DirectoryAndFileHelper.ModelViewConfigurationFile;
            if (File.Exists(modelConfigFileName))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(modelConfigFileName);
                }
                catch (Exception)
                {
                    //Last.ini tiene un id, versión vieja, borrar archivo
                    File.Delete(modelConfigFileName);
                    List<Model> models = ModelHelper.GetAll();
                    if (models.Count > 0)
                    {
                        ERMTSession.Instance.CurrentModel = models[0];
                    }
                    return;
                }

                ERMTSession.Instance.CurrentModel = ModelHelper.GetModel(int.Parse(doc.SelectSingleNode("/last/model").Attributes["id"].Value));

                EventManager.RaiseModelChanged();

                //Cargo el reporte
                if (doc.SelectSingleNode("/last/report") != null)
                {
                    ERMTSession.Instance.CurrentReport = ReportHelper.GetNew();
                    ERMTSession.Instance.CurrentReport.Parameters = doc.SelectSingleNode("/last/report").InnerXml;
                }

                if (ERMTSession.Instance.CurrentModel != null && ERMTSession.Instance.CurrentModel.IDModel == 0)
                {
                    List<Model> models = ModelHelper.GetAll();
                    if (models.Count > 0)
                    {
                        ERMTSession.Instance.CurrentModel = models[0];
                    }
                }
            }
            else
            {
                List<Model> models = ModelHelper.GetAll();
                if (models.Count > 0)
                {
                    ERMTSession.Instance.CurrentModel = models[0];
                }
            }
        }

        private void LoadControls()
        {
#if DEBUG
            tsmiShowTestControl.Visible = true;
#endif
            //tsmiCreateNewModel.Image = ResourceHelper.GetResourceImage("NewDocumentIcon");
            //tsmiEditCurrentModel.Image = ResourceHelper.GetResourceImage("EditDocumentIcon");
            //tsmiMapping.Image = ResourceHelper.GetResourceImage("MapIcon");
            //tsmiExportCurrentModel.Image = ResourceHelper.GetResourceImage("DocumentExportIcon");
            //tsmiDeleteCurrentModel.Image = ResourceHelper.GetResourceImage("DocumentDeleteIcon");
            //tsmiBackupDatabase.Image = ResourceHelper.GetResourceImage("DatabaseKeyIcon");
            //tsmiRestoreDatabase.Image = ResourceHelper.GetResourceImage("DatabaseEditIcon");
            //tsmiUserAdmin.Image = ResourceHelper.GetResourceImage("UserGroupIcon");
            //tsmiMarkerTypes.Image = ResourceHelper.GetResourceImage("PinIcon");
            LoadLeftButtons("Start");
        }

        public void LoadLeftButtons()
        {
            LoadLeftButtons(String.Empty);
        }

        public void LoadLeftButtons(string activeButton)
        {
            pbStart.Image = ResourceHelper.GetResourceImage("ERMThome");
            pbKnowledgeResources.Image = ResourceHelper.GetResourceImage("ERMTknowledgeresources");
            pbRiskMapping.Image = ResourceHelper.GetResourceImage("ERMTmaps");
            pbElectoralCycle.Image = ResourceHelper.GetResourceImage("ERMTelectoralcycle");
            pbRiskAndAction.Image = ResourceHelper.GetResourceImage("ERMTriskandaction");
            pbContact.Image = ResourceHelper.GetResourceImage("ERMTcontact");

            switch (activeButton.ToLower())
            {
                case "start":
                    {
                        pbStart.Image = ResourceHelper.GetResourceImage("ERMThome_active");
                        break;
                    }
                case "knowledgeresources":
                    {
                        pbKnowledgeResources.Image = ResourceHelper.GetResourceImage("ERMTknowledgeresources_active");
                        break;
                    }
                case "map":
                    {
                        pbRiskMapping.Image = ResourceHelper.GetResourceImage("ERMTmaps_active");
                        break;
                    }
                case "electoralcycle":
                    {
                        pbElectoralCycle.Image = ResourceHelper.GetResourceImage("ERMTelectoralcycle_active");
                        break;
                    }
                case "riskandaction":
                    {
                        pbRiskAndAction.Image = ResourceHelper.GetResourceImage("ERMTriskandaction_active");
                        break;
                    }
            }
        }

        public void SetLeftMenuButtonsVisibility(Boolean visible)
        {
            pbStart.Visible = visible;
            pbKnowledgeResources.Visible = visible;
            pbRiskMapping.Visible = visible;
            pbElectoralCycle.Visible = visible;
            pbRiskAndAction.Visible = visible;
            pbContact.Visible = visible;
        }

        public void LoadModelsMenu()
        {
            //tsmiModel.DropDownItems.Clear();
            List<ToolStripItem> itemsToRemove = new List<ToolStripItem>();
            foreach (var dropDownItem in tsmiModel.DropDownItems)
            {
                ToolStripMenuItem mi = dropDownItem as ToolStripMenuItem;

                if (mi != null && mi.Tag is int)
                {
                    itemsToRemove.Add(mi);
                }
            }

            foreach (ToolStripItem toolStripItem in itemsToRemove)
            {
                tsmiModel.DropDownItems.Remove(toolStripItem);
            }

            List<Model> models = ModelHelper.GetAll();

            foreach (Model model in models)
            {
                ToolStripItem tsi = new ToolStripMenuItem(model.Name);
                tsi.Tag = model.IDModel;
                tsi.Click += tsmiModel_Click;
                tsmiModel.DropDownItems.Add(tsi);
            }

            //If there is no current model selected, select the first one on the menu
            if (ERMTSession.Instance.CurrentModel == null && models.Count > 0)
            {
                ERMTSession.Instance.CurrentModel = models[0];
            }

            ControlCache.StartInstance.LoadModelsMenu();
            //if (gbPrincipal.Controls.Count > 0 && gbPrincipal.Controls[0].GetType() == typeof(Start))
            //{
            //    Start start = (Start)gbPrincipal.Controls[0];
            //    start.LoadModelsMenu();
            //}

        }

        public void DisableMenu()
        {
            flpLeftButtons.Enabled = false;
        }

        public void EnableMenu()
        {
            flpLeftButtons.Enabled = true;
        }

        public void ShowTitle(string title)
        {
            lblTitle.Text = title;
            if (ERMTSession.Instance.CurrentModel != null && ERMTSession.Instance.CurrentModel.IDModel != 0)
            {
                Text = ERMTSession.Instance.CurrentModel.Name + " - Electoral Risk Management Tool";
            }
            else
            {
                Text = "Electoral Risk Management Tool";
            }
        }

        private void GetFocusedControl(Control control, ref Control focusedControl)
        {
            if (focusedControl != null || control == null)
                return;
            foreach (Control c in control.Controls)
            {
                if (c.Focused)
                {
                    focusedControl = c;
                    return;
                }
                GetFocusedControl(c, ref focusedControl);
            }
        }

        #region Left Menu
        private void pbStart_Click(object sender, EventArgs e)
        {
            ViewManager.ShowStart();
        }

        private void pbKnowledgeResources_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.KnowledgeResources);
        }

        private void pbContact_Click(object sender, EventArgs e)
        {
            const string mailTo = "ERMTool@idea.int";
            string subject = ResourceHelper.GetResourceText("ContactSubject");
            string body = ResourceHelper.GetResourceText("ContactBody");
            try
            {
                Process.Start("mailto:" + mailTo + "?subject=" + subject + "&body=" + body);
            }
            catch (Exception)
            {
            }
            
        }

        private void pbElectoralCycle_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ElectoralCycle);
        }

        private void pbRiskAndAction_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.RiskActionRegister);
        }
        #endregion

        #region Tool Strip Menu Items events.
        private void tsmiAddFactor_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.FactorNew);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            //Call current control's print method
            if (this.gbPrincipal.Controls.Count > 0) //Si ya hay un control cargado
            {
                ((ERMTUserControl)gbPrincipal.Controls[0]).Print();
            }
        }

        private void tsmiServerAddress_Click(object sender, EventArgs e)
        {
            ServerSettings s = new ServerSettings();
            s.ShowDialog();
        }

        private void tsmiIndex_Click(object sender, EventArgs e)
        {
            Process.Start("file:///" + DirectoryAndFileHelper.HelpFilePath);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.About);
        }

        private void pbRiskMapping_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.RiskMapping);
        }

        private void tsmiModifyFactor_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.FactorModify);
        }

        private void tsmiReorderFactors_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.FactorReorder);
        }

        private void tsmiViewFactors_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.KnowledgeResources);
        }

        private void tsmiElectoralCycle_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ElectoralCycle);
        }

        private void tsmiModifyPhase_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ElectoralCycleModifyPhase);
        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.UserNew);
        }

        private void tsmiModifyExistingUser_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.UserModify);
        }

        private void tsmiMarkerTypes_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.MarkerTypeCRUD);
        }

        private void tsmiCreateNewModel_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ModelNew);
        }

        private void tsmiShowTestControl_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.TestUserControl);
        }

        private void tsmiEditCurrentModel_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ModelEdit);
        }

        private void tsmiModel_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;


            ERMTSession.Instance.CurrentModel = ModelHelper.GetModel((int)tsi.Tag);
            //if (File.Exists(DocumentHelper.GetClientAppDataFolder() + "\\Last.ini"))
            //{
            //    File.Delete(DocumentHelper.GetClientAppDataFolder() + "\\Last.ini");
            //}

            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<last><model id=\"" + ERMTSession.Instance.CurrentModel.IDModel + "\"/></last>");
            //doc.Save(DocumentHelper.GetClientAppDataFolder() + "\\Last.ini");
            ViewManager.ShowTitle(ERMTSession.Instance.CurrentModel.Name);
            ERMTSession.Instance.CurrentReport = null;
            EventManager.RaiseModelChanged(this);
        }

        private void tsmiDeleteCurrentModel_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DeleteModel"), CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo) == CustomMessageBoxReturnValue.Ok)
            {
                if (ERMTSession.Instance.CurrentModel != null)
                {
                    ModelHelper.Delete(ERMTSession.Instance.CurrentModel);
                    ERMTSession.Instance.CurrentModel = null;
                    ViewManager.LoadModelsMenu();

                    EventManager.RaiseModelChanged(this);

                    if (File.Exists(DirectoryAndFileHelper.ModelViewConfigurationFile + "\\Last.ini"))
                    {
                        File.Delete(DirectoryAndFileHelper.ModelViewConfigurationFile + "\\Last.ini");
                    }
                }
            }
        }

        private void tsmiReorderFactorsCurrentModel_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.ModelReorderFactors);
        }

        private void tsmiAdminRegions_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.EditRegion);
        }

        private void tsmiFeedback_Click(object sender, EventArgs e)
        {
            const string mailTo = "ERMTool@idea.int";
            string subject = ResourceHelper.GetResourceText("ContactSubject");
            string body = ResourceHelper.GetResourceText("ContactBody");
            try
            {
                Process.Start("mailto:" + mailTo + "?subject=" + subject + "&body=" + body);
            }
            catch (Exception)
            {
            }
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            ViewManager.SetMainControl(ERMTControl.UserResetPassword);
        }
        #endregion

        private void tsmiBackupDatabase_Click(object sender, EventArgs e)
        {
            DataBackupForm backupForm = new DataBackupForm { StartPosition = FormStartPosition.CenterParent };
            backupForm.ShowDialog();
        }

        private void tsmiExportCurrentModel_Click(object sender, EventArgs e)
        {
            Model currentModel = ERMTSession.Instance.CurrentModel;
            SaveFileDialog saveFileDialog = new SaveFileDialog() { FileName = currentModel.Name + ".xml" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                {
                    sw.Write(DocumentHelper.Export(currentModel.IDModel).Content);
                    sw.Flush();
                    sw.Close();
                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                    DirectoryInfo saveDirectoryInfo = new DirectoryInfo(fi.DirectoryName + "\\ModelShapefiles");
                    
                    //Saves shapes model in the same xml model folder.
                    ModelHelper.SaveAllShapefilesInModel(currentModel,saveDirectoryInfo); 
                }
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelExportOk"));
            }
        }

        private void tsmiImportModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = File.OpenText(openFileDialog.FileName);
                Document doc = new Document { Content = sr.ReadToEnd() };

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(doc.Content);

                int importVersion = 0;
                XmlNode versionNode = xmlDoc.SelectSingleNode("data/Version");
                if (versionNode != null)
                {
                    importVersion = int.Parse(versionNode.Attributes["Number"].Value.Substring(0, 1));
                }

                if (importVersion == 0 || importVersion < 7)
                {
                    CustomMessageBoxReturnValue customMessageBoxReturnValue = CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionsMustBeLoadedBeforeImport"),
                                                CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.YesNo);
                    if (customMessageBoxReturnValue == CustomMessageBoxReturnValue.Cancel)
                    {
                        return;
                    }
                }

                List<string> message = DocumentHelper.Import(doc);

                if (message[0] == "true")
                {
                    //import successfull.
                    if (message.Count > 2)
                    {
                        if (message[1] == "SomeRegionsWereNotImported")
                        {
                            CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportOk") + " " +
                                ResourceHelper.GetResourceText("SomeRegionsWereNotImported") + ": " +
                                message[2]);
                        }
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportOk"));
                    }
                    ViewManager.ShowStart();
                    ViewManager.LoadModelsMenu();
                }
                else
                {
                    //import failed.
                    if (message.Count == 1)
                    {
                        //it was an exception
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportNotOk"));
                        return;
                    }

                    if (message[1] == "ModelImportNoExistsRegionsInLocalDB")
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportNoExistsRegionsInLocalDB") + " " +
                            ResourceHelper.GetResourceText("ModelImportNotOk"));
                    }
                    else if (message[1] == "ModelImportNoExistsParentRegionInLocalDB")
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportNoExistsParentRegionInLocalDB") + " " +
                            ResourceHelper.GetResourceText("ModelImportNotOk"));
                    }
                    else if (message[1] == "ModelImportMainRegionDoesNotExits")
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportMainRegionDoesNotExits") + " " +
                            ResourceHelper.GetResourceText("ModelImportNotOk"));
                    }
                    else if (message[1] == "ModelImportErrorVersionUsesDundasAndWrongParent")
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportErrorVersionUsesDundasAndWrongParent") + " " +
                            ResourceHelper.GetResourceText("ModelImportNotOk"));
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ModelImportNotOk"));
                    }
                }
            }

        }

        private void tsmiRestoreDatabase_Click(object sender, EventArgs e)
        {
            DataRestoreForm restore = new DataRestoreForm { StartPosition = FormStartPosition.CenterParent };
            restore.ShowDialog();
        }

        private void tsmiLanguage_Click(object sender, EventArgs e)
        {
            CustomMessageBoxReturnValue exit = CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ExitToChangeLanguage"), CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo);
            if (exit == CustomMessageBoxReturnValue.Ok)
            {
                string culture = ((ToolStripMenuItem)sender).Tag.ToString();
                XmlDocument doc = new XmlDocument();
                String configFileName = DirectoryAndFileHelper.LanguageConfigurationFile;

                if (File.Exists(configFileName))
                {
                    doc.Load(configFileName);
                    doc.DocumentElement.Attributes["culture"].Value = culture;
                }
                else
                {
                    doc.LoadXml("<Language culture=\"" + culture + "\" />");
                }

                doc.Save(configFileName);
                try
                {
                    Process.Start(Application.ExecutablePath);
                    Process.GetCurrentProcess().Kill();
                }
                catch
                { }
            }
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            UserHelper.SignOutUser();
            SetLeftMenuButtonsVisibility(false);
            msPrincipal.Visible = false;
            lblUser.Text = string.Empty;
            ViewManager.SetMainControl(ERMTControl.Login);
        }

        private void tsmiERMTWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.idea.int/elections/ermtool/");
        }

        private void pbTopLeftLogo_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.idea.int");
        }
    }
}
