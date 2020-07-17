using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Idea.Entities;
using Idea.ERMT.UserControls;
using Idea.Facade;
using Idea.Utils;
using Region = Idea.Entities.Region;

namespace Idea.ERMT.UserControls
{
    public partial class EditRiskAndAction : Form
    {
        private ModelRiskAlert _alert;

        public ModelRiskAlert ModelRiskAlert
        {
            get { return _alert; }
        }


        public EditRiskAndAction(ModelRiskAlert alert)
        {
            InitializeComponent();
            _alert = alert;
        }

        public EditRiskAndAction()
        {
            InitializeComponent();
            _alert = ModelRiskAlertHelper.GetNew();
        }

        private void lbElectoralPhases_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbElectoralPhases.SelectedIndex >= 0 && e.KeyCode == Keys.Delete && MessageBox.Show("Do you want to remove the selected Electoral Phase?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                lbElectoralPhases.Items.RemoveAt(lbElectoralPhases.SelectedIndex);
        }

        private void EditRiskAndAction_Load(object sender, EventArgs e)
        {
            //TREEVIEW
            Model model = ERMTSession.Instance.CurrentModel;
            if (model != null)
            {
                tvRegions.Nodes.Clear();
                Region current = RegionHelper.Get(model.IDRegion);
                List<Region> regions = new List<Region>();
                regions.Add(RegionHelper.Get(1));
                if (current.IDParent.HasValue && current.IDParent != 1)
                    regions.Add(RegionHelper.Get(current.IDParent.Value));
                regions.Add(current);
                regions.AddRange(RegionHelper.GetAllChilds(current.IDRegion));

                TreeNode node = new TreeNode() { Text = current.RegionName, Name = current.IDRegion.ToString(), Tag = current };
                int mainRegionLevel = RegionHelper.GetLevel(current.IDRegion);

                tvRegions.Nodes.Add(node);
                AddChildRegions(tvRegions.Nodes[0], regions, mainRegionLevel);
                tvRegions.SelectedNode = tvRegions.Nodes[0];
                tvRegions.ExpandAll();
            }

            //Electoral Phase
            cbElectoralPhases.Items.Add(new ComboBoxItemFace(null));
            cbElectoralPhases.Items.AddRange(PhaseHelper.GetAll().Select(p => new ComboBoxItemFace(p)).ToArray());
            cbElectoralPhases.SelectedIndex = 0;

            // Add data from alert:
            if (_alert.IDModel != 0)
            {
                tbCode.Text = _alert.Code;
                tbTitle.Text = _alert.Title;
                dtpDateFrom.Value = _alert.DateFrom;
                dtpDateTo.Value = _alert.DateTo;
                tbDescription.Text = _alert.RiskDescription;
                tbAction.Text = _alert.Action;
                tbResult.Text = _alert.Result;
                rbStatusActive.Checked = _alert.Active;
                rbStatusInactive.Checked = !_alert.Active;

                // Phases:
                List<ModelRiskAlertPhase> modelRiskAlertPhaseList = ModelRiskAlertHelper.GetPhases(_alert);
                foreach (ModelRiskAlertPhase mrap in modelRiskAlertPhaseList)
                {
                    foreach (var cboPhase in cbElectoralPhases.Items)
                    {
                        if (((ComboBoxItemFace)cboPhase).IFase != null)
                            if (((ComboBoxItemFace)cboPhase).IFase.IDPhase == mrap.IDPhase)
                                lbElectoralPhases.Items.Add(cboPhase);
                    }
                }

                // Regions:
                List<ModelRiskAlertRegion> modelRiskAlertRegionList = ModelRiskAlertHelper.GetModelRiskAlertRegions(_alert);
                foreach (ModelRiskAlertRegion region in modelRiskAlertRegionList)
                {
                    foreach (TreeNode node in tvRegions.Nodes)
                        CheckRegion(node, region);
                }

                // Add analysis:
                List<ModelRiskAlertAttachment> modelRiskAlertAttachment = ModelRiskAlertHelper.GetModelRiskAlertAttachments(_alert);
                foreach (ModelRiskAlertAttachment att in modelRiskAlertAttachment)
                {
                    addUpdater(att);
                }

                // And a last button to add more:
                addUpdater();
            }
            else
            {
                Label newLabel = new Label
                {
                    Text = ResourceHelper.GetResourceText("RiskAndActionSaveWarning"),
                    ForeColor = Color.Red,
                    Width = 500
                };
                analysisPanel.Controls.Add(newLabel);
            }
        }

        private void addUpdater()
        {
            Updater newUpdater = new Updater();

            // Set properties:
            newUpdater.HasChange = false;
            newUpdater.HasFile = false;
            newUpdater.Id = 0;
            newUpdater.Margin = new Padding(5);
            newUpdater.Size = new Size(217, 31);

            // Set events:
            newUpdater.Delete += new Updater.DUpdater(DeleteAttachment);
            newUpdater.GotContent += new Updater.DGotContent(GotAttachmentContent);


            // Save
            analysisPanel.Controls.Add(newUpdater);
        }

        private void addUpdater(ModelRiskAlertAttachment att)
        {
            Updater newUpdater = new Updater();

            // Set properties:
            newUpdater.HasChange = false;
            newUpdater.HasFile = true;
            newUpdater.Id = att.IDModelRiskAlertAttachment;
            newUpdater.FileName = att.AttachmentFile;
            if (att.Content != null)
                newUpdater.Content = Convert.FromBase64String(att.Content);
            newUpdater.Margin = new Padding(5);
            newUpdater.Size = new Size(217, 31);


            // Set events:
            newUpdater.Delete += new Updater.DUpdater(DeleteAttachment);

            // Save
            analysisPanel.Controls.Add(newUpdater);
        }

        private void DeleteAttachment(object sender, UpdaterEventArgs e)
        {
            if (e.Id != 0)
            {
                List<ModelRiskAlertAttachment> modelRiskAlertAttachmentList = ModelRiskAlertHelper.GetModelRiskAlertAttachments(_alert);
                foreach (ModelRiskAlertAttachment att in modelRiskAlertAttachmentList)
                {
                    if (att.IDModelRiskAlertAttachment == e.Id)
                    {
                        //att.Deleted = true;
                        ModelRiskAlertHelper.DeleteAttachment(att);
                    }
                }
            }
            // and remove the control.
            analysisPanel.Controls.Remove((Control)sender);
            ((Control)sender).Dispose();
        }

        private void GotAttachmentContent(object sender, UpdaterEventArgs e)
        {
            addUpdater();
        }

        private void CheckRegion(TreeNode node, ModelRiskAlertRegion region)
        {
            if (((Region)node.Tag).IDRegion == region.IDRegion)
                node.Checked = true;
            foreach (TreeNode childNode in node.Nodes)
                CheckRegion(childNode, region);
        }

        private void AddChildRegions(TreeNode treeNode, List<Region> regions, int level)
        {
            foreach (Region r in regions)
            {
                if (r.IDParent.ToString() == treeNode.Name)
                {
                    TreeNode t = new TreeNode { Text = r.RegionName, Name = r.IDRegion.ToString(), Tag = r };
                    treeNode.Nodes.Add(t);
                    Application.DoEvents();
                    AddChildRegions(t, regions, level + 1);
                }
            }
        }

        private int GetRegionLevel(int? idParent, List<Region> regions, int regionLevel)
        {
            if (idParent == null)
                return regionLevel;
            else
            {
                regionLevel++;
                idParent = (from a in regions where a.IDRegion == idParent select a.IDParent).FirstOrDefault();
                if (idParent != null)
                    GetRegionLevel(idParent, regions, regionLevel);
                return regionLevel;
            }
        }

        private void cbElectoralPhases_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ComboBoxItemFace)cbElectoralPhases.SelectedItem;
            if (item.IFase == null) return;
            if (lbElectoralPhases.Items.OfType<ComboBoxItemFace>().Where(p => p.IFase.IDPhase == item.IFase.IDPhase).Any())
            {
                MessageBox.Show("The item already exists", "Existent item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            lbElectoralPhases.Items.Add(item);
            cbElectoralPhases.SelectedIndex = 0;
        }

        private void btnBackRiskAndAction_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvRegions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tbRegion.Text = string.Join(",", GetSelectedRegionNodes().Select(p => p.Text).ToArray());
        }

        private List<TreeNode> GetSelectedRegionNodes()
        {
            var list = new List<TreeNode>();
            foreach (var node in tvRegions.Nodes.OfType<TreeNode>())
            {
                if (node.Checked) list.Add(node);
                list.AddRange(GetSelectedRegionNodes(node));
            }
            return list;
        }

        private List<TreeNode> GetSelectedRegionNodes(TreeNode node)
        {
            var list = new List<TreeNode>();
            foreach (var child in node.Nodes.OfType<TreeNode>())
            {
                if (child.Checked) list.Add(child);
                list.AddRange(GetSelectedRegionNodes(child));
            }
            return list;
        }

        private void btnSaveRiskAndAction_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                // Save data:
                Model model = ERMTSession.Instance.CurrentModel;
                _alert.IDModel = model.IDModel;
                _alert.Code = tbCode.Text;
                _alert.Title = tbTitle.Text;
                _alert.DateFrom = Convert.ToDateTime(dtpDateFrom.Value);
                _alert.DateTo = Convert.ToDateTime(dtpDateTo.Value);
                _alert.RiskDescription = tbDescription.Text;
                _alert.Action = tbAction.Text;
                _alert.Result = tbResult.Text;
                _alert.Active = rbStatusActive.Checked;

                List<ModelRiskAlertPhase> modelRiskAlertPhaseListToSave = new List<ModelRiskAlertPhase>();
                ModelRiskAlertPhase modelRiskAlertPhase = null;
                List<ModelRiskAlertPhase> modelRiskAlertPhaseList = ModelRiskAlertHelper.GetPhases(_alert);
                foreach (var lbPhase in lbElectoralPhases.Items)
                {

                    bool found = false;
                    foreach (ModelRiskAlertPhase oPhase in modelRiskAlertPhaseList)
                    {
                        if (((ComboBoxItemFace)lbPhase).IFase.IDPhase == oPhase.IDPhase)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        modelRiskAlertPhase = new ModelRiskAlertPhase();
                        modelRiskAlertPhase.IDPhase = ((ComboBoxItemFace)lbPhase).IFase.IDPhase;
                        modelRiskAlertPhaseListToSave.Add(modelRiskAlertPhase);
                    }
                }

                List<ModelRiskAlertRegion> modelRiskAlertRegionList = ModelRiskAlertHelper.GetModelRiskAlertRegions(_alert);
                List<ModelRiskAlertRegion> modelRiskAlertRegionsToDelete = GetModelRiskAlertRegionsToDelete(modelRiskAlertRegionList);
                List<int> regionIDsToAdd = GetRegionIDsToAdd(modelRiskAlertRegionList);

                // Add new attachments:
                foreach (Control control in analysisPanel.Controls)
                {
                    if (control is Updater)
                    {
                        if (((Updater)control).HasFile && ((Updater)control).Id == 0)
                        {
                            ModelRiskAlertAttachment att = new ModelRiskAlertAttachment();
                            att.IDModelRiskAlert = _alert.IDModelRiskAlert;
                            att.AttachmentFile = ((Updater)control).FileName; // Nombre del archivo, sin el ID adelante.
                            att.Content = Convert.ToBase64String(((Updater)control).Content);

                            ModelRiskAlertHelper.SaveAttachment(att);
                        }
                    }
                }

                // Save the Alert:
                _alert = ModelRiskAlertHelper.Save(_alert);
                if (modelRiskAlertPhaseListToSave.Count > 0)
                {
                    foreach (ModelRiskAlertPhase mrap in modelRiskAlertPhaseListToSave)
                    {
                        mrap.IDModelRiskAlert = _alert.IDModelRiskAlert;
                        ModelRiskAlertHelper.SavePhase(mrap);
                    }
                }
                if (regionIDsToAdd.Count > 0)
                {
                    foreach (int regionID in regionIDsToAdd)
                    {
                        ModelRiskAlertRegion mrar = new ModelRiskAlertRegion();
                        mrar.IDModelRiskAlert = _alert.IDModelRiskAlert;
                        mrar.IDRegion = regionID;
                        ModelRiskAlertHelper.SaveRegion(mrar);
                    }
                }
                if (modelRiskAlertRegionsToDelete.Count > 0)
                {
                    foreach (ModelRiskAlertRegion mrar in modelRiskAlertRegionsToDelete)
                    {
                        ModelRiskAlertHelper.DeleteRegion(mrar);
                    }
                }


                this.Close();
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RiskAlertSaved"));
            }
        }

        private List<int> GetRegionIDsToAdd(List<ModelRiskAlertRegion> modelRiskAlertRegionList)
        {
            List<int> regionIDsToAddList = new List<int>();
            List<TreeNode> regionNodes = GetSelectedRegionNodes();
            foreach (TreeNode regionNode in regionNodes)
            {
                bool addRegion = true;
                foreach (ModelRiskAlertRegion mrar in modelRiskAlertRegionList)
                {
                    if (((Region)regionNode.Tag).IDRegion == mrar.IDRegion)
                    {
                        addRegion = false;
                        break;
                    }
                }
                if (addRegion)
                {
                    regionIDsToAddList.Add(((Region)regionNode.Tag).IDRegion);
                }
            }

            return regionIDsToAddList;
        }

        private List<ModelRiskAlertRegion> GetModelRiskAlertRegionsToDelete(List<ModelRiskAlertRegion> modelRiskAlertRegionList)
        {
            List<ModelRiskAlertRegion> modelRiskAlertRegionsToDeleteList = new List<ModelRiskAlertRegion>();
            bool deleteRegion;
            foreach (ModelRiskAlertRegion mrar in modelRiskAlertRegionList)
            {
                List<TreeNode> regionNodes = GetSelectedRegionNodes();
                deleteRegion = true;
                foreach (TreeNode regionNode in regionNodes)
                {
                    if (((Region)regionNode.Tag).IDRegion == mrar.IDRegion)
                    {
                        deleteRegion = false;
                        break;
                    }
                }
                if (deleteRegion)
                {
                    modelRiskAlertRegionsToDeleteList.Add(mrar);
                }
            }

            return modelRiskAlertRegionsToDeleteList;
        }

        private bool markRegionAsDeleted(TreeNode node, ModelRiskAlertRegion region)
        {
            bool regionFound = false;
            if (((Region)node.Tag).IDRegion == region.IDRegion)
            {
                //if (!node.Checked)
                //    region.Deleted = true;
                return true;
            }
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    regionFound = markRegionAsDeleted(childNode, region);
                    if (regionFound)
                        return true;
                }
            }
            return false;
        }

        private void addNewCheckedRegion(TreeNode node)
        {
            bool regionFound = false;
            foreach (ModelRiskAlertRegion region in _alert.ModelRiskAlertRegions)
            {
                if (((Region)node.Tag).IDRegion == region.IDRegion)
                {
                    regionFound = true;
                    break;
                }
            }
            if (!regionFound)
            {
                if (node.Checked)
                {
                    ModelRiskAlertRegion region = ModelRiskAlertHelper.GetNewModelRiskAlertRegion();
                    region.IDRegion = ((Region)node.Tag).IDRegion;
                    _alert.ModelRiskAlertRegions.Add(region);
                }
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                addNewCheckedRegion(childNode);
            }
        }

        private bool isValid()
        {
            if (dtpDateFrom.Text == string.Empty)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RiskAndActionDateFrom"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
                dtpDateFrom.Focus();
                return false;
            }

            if (dtpDateTo.Text == string.Empty)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RiskAndActionDateTo"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
                dtpDateTo.Focus();
                return false;
            }


            // Check that it has at least one Phase
            if (lbElectoralPhases.Items.Count == 0)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RiskAndActionPhase"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
                cbElectoralPhases.Focus();
                return false;
            }

            bool retValue = false;

            // Check that it has at least one region (ignore the ones marked as deleted)
            foreach (TreeNode node in tvRegions.Nodes)
            {
                if (checkChecked(node))
                {
                    retValue = true;
                    break;
                }
            }
            if (!retValue)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RiskAndActionRegion"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.OKOnly);
                return false;
            }

            return true;
        }

        private bool checkChecked(TreeNode node)
        {
            if (node.Checked)
                return true;
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (checkChecked(childNode))
                        return true;
                }
            }
            return false;
        }

        private void btnViewRiskAndAction_Click(object sender, EventArgs e)
        {
            // View / Print the grid:
            CopyAttachmentFilesToDir();
            ModelRiskAlertHelper.GenerateRARHtml(getAlertTitle(), getAlertText(), ModelRiskAlertHelper.GetRARHtmlAlarmFile());
            HtmlPopup win = new HtmlPopup(DirectoryAndFileHelper.ClientAppDataFolder + ModelRiskAlertHelper.GetRARHtmlAlarmFile());
            win.ShowBackForward = false;
            win.ShowDialog();
        }

        private string getAlertTitle()
        {
            string returnTitle = tbCode.Text.Trim();
            if (returnTitle != string.Empty && tbTitle.Text.Trim() != string.Empty)
            {
                returnTitle = returnTitle + " - " + tbTitle.Text.Trim();
            }
            return ResourceHelper.GetResourceText("Alert") + " " + returnTitle;
        }

        private string getAlertText()
        {
            StringBuilder returnText = new StringBuilder("<div align='left'><table>");

            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Code"), tbCode.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Title"), tbTitle.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("DateFrom"), dtpDateFrom.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("DateTo"), dtpDateTo.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Regions"), tbRegion.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("RiskDescription"), tbDescription.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Action"), tbAction.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Result"), tbResult.Text.Trim()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Status"), rbStatusActive.Checked ? ResourceHelper.GetResourceText("Active") : ResourceHelper.GetResourceText("Inactive")));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Phases"), getPhaseValueText()));
            returnText.Append(getAlertPropertyText(ResourceHelper.GetResourceText("Analysis"), getAttachmentText()));


            returnText.AppendLine("</table></div>");

            return returnText.ToString();
        }

        private string getAlertPropertyText(string Label, string Value)
        {
            StringBuilder returnText = new StringBuilder();

            returnText.AppendLine(" <tr>");
            returnText.AppendLine("     <td style='text-align:right;vertical-align:top;'>");
            returnText.AppendLine("         <span style='font-weight:bold'>" + Label + ": </span>");
            returnText.AppendLine("     </td>");
            returnText.AppendLine("     <td>");
            returnText.AppendLine("         " + Value);
            returnText.AppendLine("     </td>");
            returnText.AppendLine(" <tr>");

            return returnText.ToString();
        }

        private string getPhaseValueText()
        {
            StringBuilder returnText = new StringBuilder();
            foreach (var lbPhase in lbElectoralPhases.Items)
            {
                returnText.Append(((ComboBoxItemFace)lbPhase).IFase.Title + "<br/>");
            }
            return returnText.ToString();
        }

        private void CopyAttachmentFilesToDir()
        {
            // Delete all files on attachment directory:
            string folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"];
            DirectoryInfo downloadedMessageInfo = new DirectoryInfo(folder);

            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                file.Delete();
            }

            // Generate the files form the attachments:
            foreach (Control control in analysisPanel.Controls)
            {
                if (control is Updater)
                {
                    if (((Updater)control).HasFile)
                    {
                        // SAve the file:
                        string path = folder + ((Updater)control).Id.ToString() + "-" + ((Updater)control).FileName;
                        File.WriteAllBytes(path, ((Updater)control).Content);
                    }
                }
            }
        }

        private string getAttachmentText()
        {
            StringBuilder returnImages = new StringBuilder();
            StringBuilder returnFiles = new StringBuilder();

            foreach (Control control in analysisPanel.Controls)
            {
                if (control is Updater)
                {
                    if (((Updater)control).HasFile)
                    {
                        if (Path.GetExtension(((Updater)control).FileName.ToLower()) == ".jpg"
                            || Path.GetExtension(((Updater)control).FileName.ToLower()) == ".jpeg"
                            || Path.GetExtension(((Updater)control).FileName.ToLower()) == ".gif"
                            || Path.GetExtension(((Updater)control).FileName.ToLower()) == ".bmp"
                            || Path.GetExtension(((Updater)control).FileName.ToLower()) == ".png")
                        {
                            // Is an image, add it to the image builder as an html image.
                            returnImages.Append("<img src='../Docs/");
                            returnImages.Append(((Updater)control).Id.ToString() + "-" + ((Updater)control).FileName);
                            returnImages.Append("' alt='" + ((Updater)control).Id.ToString() + "-" + ((Updater)control).FileName);
                            returnImages.AppendLine("' /><br /><br />");
                        }
                        else
                        {
                            // Is an attachment. Add it as a href.
                            returnFiles.Append("<a href='../Docs/");
                            returnFiles.Append(((Updater)control).Id.ToString() + "-" + ((Updater)control).FileName);
                            returnFiles.Append("'>" + ((Updater)control).Id.ToString() + "-" + ((Updater)control).FileName);
                            returnFiles.AppendLine("</a><br /><br />");
                        }
                    }
                }
            }

            return returnImages.ToString() + returnFiles.ToString();
        }

        private void selectAllChildRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAllChildRegionsToolStripMenuItem1_Click(sender, e);
        }

        private void deselectAllChildRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deselectAllChildRegionsToolStripMenuItem1_Click(sender, e);
        }

        private void selectAllRegionsOnThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAllRegionsOnThisLevelToolStripMenuItem1_Click(sender, e);
        }

        private void deselectAllRegionsOnThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deselectAllRegionsOnThisLevelToolStripMenuItem1_Click(sender, e);
        }

        private void selectAllChildRegionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                foreach (TreeNode checkedNode in tvRegions.SelectedNode.Nodes)
                {
                    TreeUtil.MarkRegion(checkedNode, true);
                }
            }
        }

        private void deselectAllChildRegionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                foreach (TreeNode checkedNode in tvRegions.SelectedNode.Nodes)
                {
                    TreeUtil.MarkRegion(checkedNode, false);
                }
            }
        }

        private void selectAllRegionsOnThisLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                if (tvRegions.SelectedNode.Parent != null)
                {
                    // Get the current level
                    int level = TreeUtil.GetLevelFromRoot(tvRegions.SelectedNode);
                    TreeUtil.MarkRegion(tvRegions.Nodes[0], true, level, 1);
                }
                else
                {
                    TreeUtil.MarkRegion(tvRegions.SelectedNode, true, false);
                }
            }
        }

        private void deselectAllRegionsOnThisLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                if (tvRegions.SelectedNode.Parent != null)
                {
                    // Get the current level
                    int level = TreeUtil.GetLevelFromRoot(tvRegions.SelectedNode);
                    TreeUtil.MarkRegion(tvRegions.Nodes[0], false, level, 1);
                }
                else
                {
                    TreeUtil.MarkRegion(tvRegions.SelectedNode, false, false);
                }
            }
        }

        private void tvRegions_MouseUp(object sender, MouseEventArgs e)
        {
            // only need to change selected note during right-click - otherwise tree does
            // fine by itself
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                tvRegions.PointToClient(pt);

                TreeNode Node = tvRegions.GetNodeAt(pt);
                if (Node != null)
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        tvRegions.SelectedNode = Node;
                        // ResetContextMenu();
                        NodeMenu.Show(tvRegions, pt);
                    }
                }
            }
        }

    }



    public class ComboBoxItemFace
    {
        public Phase IFase { get; set; }

        public ComboBoxItemFace(Phase ifase)
        {
            this.IFase = ifase;
        }

        public override string ToString()
        {
            return this.IFase == null ? ResourceHelper.GetResourceText("Add") : this.IFase.Title;
        }
    }
}

