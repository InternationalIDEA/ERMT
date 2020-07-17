using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;
using Region = Idea.Entities.Region;

namespace Idea.ERMT.UserControls
{
    public partial class RiskAndActionRegister : ERMTUserControl
    {
        public Boolean IsMasterRAR;
        private bool _enableSearch = true;

        public RiskAndActionRegister()
        {
            InitializeComponent();
        }

        public RiskAndActionRegister(Boolean isMasterRAR)
        {
            InitializeComponent();
            IsMasterRAR = isMasterRAR;

            if (isMasterRAR)
            {
                btnEdit.Text = ResourceHelper.GetResourceText("ViewText");
                btnAddNew.Visible = false;
                btnPrint.Visible = false;
            }
        }

        private void RiskAndActionRegister_Load(object sender, EventArgs e)
        {
            LoadModelAndRegions();
            LoadRisksDataGrid();
        }

        private void LoadModelAndRegions()
        {
            Model model = ERMTSession.Instance.CurrentModel;

            if (model == null)
            {
                return;
            }


            if (!IsMasterRAR)
            {
                // Load tree:
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
                // node.ContextMenuStrip = NodeMenu;

                tvRegions.Nodes.Add(node);
                AddChildRegions(tvRegions.Nodes[0], regions, mainRegionLevel);
                tvRegions.ExpandAll();
                tvRegions.SelectedNode = tvRegions.Nodes[0];
            }
            else
            {
                // Load tree:
                tvRegions.Nodes.Clear();
                //Region current = RegionHelper.Get(model.IDRegion);
                //List<Region> regions = ModelRiskAlertHelper.GetAllRegionsWithAlerts();
                List<Region> regions = RegionHelper.GetAll().OrderBy(r => r.RegionLevel).ToList();
                //regions.Add(RegionHelper.GetWorld());

                if (regions.Count == 0)
                {
                    return;
                }

                TreeNode node = new TreeNode { Text = regions[0].RegionName, Name = regions[0].IDRegion.ToString(), Tag = regions[0] };
                int mainRegionLevel = RegionHelper.GetLevel(regions[0].IDRegion);
                // node.ContextMenuStrip = NodeMenu;

                tvRegions.Nodes.Add(node);
                AddChildRegions(tvRegions.Nodes[0], regions, mainRegionLevel);
                tvRegions.ExpandAll();
                tvRegions.SelectedNode = tvRegions.Nodes[0];

            }
        }


        private void LoadRisksDataGrid()
        {
            if (_enableSearch)
            {
                Model model = ERMTSession.Instance.CurrentModel;
                if (model != null)
                {

                    // Load List:
                    // Clear and create columns:
                    dgvAlerts.Rows.Clear();

                    // Create a list of integer:
                    List<int> regionsChecked = new List<int>();
                    foreach (TreeNode checkedNode in tvRegions.Nodes)
                    {
                        GetCheckedRegions(regionsChecked, checkedNode);
                    }

                    List<ModelRiskAlert> alerts = new List<ModelRiskAlert>();
                    if (IsMasterRAR)
                    {
                        dgvAlerts.Columns["Model"].Visible = true;
                        alerts = ModelRiskAlertHelper.GetWithFilter(null, regionsChecked,
                            (rbActive.Checked) ? true : (rbInactive.Checked ? (bool?) false : null));
                    }
                    else
                    {
                        dgvAlerts.Columns["Model"].Visible = false;
                        alerts = ModelRiskAlertHelper.GetWithFilter(model.IDModel, regionsChecked,
                            (rbActive.Checked) ? true : (rbInactive.Checked ? (bool?) false : null));
                    }
                    
                    if (alerts == null || alerts.Count == 0) return;
                    
                    List<ModelRiskAlert> modelRiskAlertDistinctIDsList = new List<ModelRiskAlert>();
                    foreach (ModelRiskAlert alert in alerts)
                    {
                        if (IsAlertInList(alert, modelRiskAlertDistinctIDsList)) continue;

                        List<ModelRiskAlertPhase> modelRiskAlertPhaseList = ModelRiskAlertHelper.GetPhases(alert);
                        int i = 0;
                        StringBuilder electoralPhase = new StringBuilder("");

                        foreach (ModelRiskAlertPhase mrap in modelRiskAlertPhaseList)
                        {
                            if (i > 0)
                                electoralPhase.Append(" - ");
                            electoralPhase.Append(PhaseHelper.Get(mrap.IDPhase).Title);
                            i = i + 1;
                        }

                        Model alertModel = IsMasterRAR ? ModelHelper.GetModel(alert.IDModel) : ERMTSession.Instance.CurrentModel;

                        i = dgvAlerts.Rows.Add(alert.Code, alertModel.Name, alert.Title, electoralPhase.ToString() != "" ? electoralPhase.ToString() : "--", alert.DateCreated, alert.DateModified, (alert.Active) ? "Active" : "Inactive", "+");
                        DataGridViewRow r = dgvAlerts.Rows[i];
                        r.Tag = alert;
                        if (electoralPhase.ToString() != "")
                            r.Cells[2].ToolTipText = electoralPhase.ToString().Replace(",", Environment.NewLine);

                        if (alert.Active)
                        {
                            r.Cells[5].Style.BackColor = Color.Red;
                            r.Cells[5].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            r.Cells[5].Style.BackColor = Color.Green;
                            r.Cells[5].Style.ForeColor = Color.White;
                        }

                    }
                }
            }
        }

        private bool IsAlertInList(ModelRiskAlert alert, List<ModelRiskAlert> modelRiskAlertDistinctIDsList)
        {
            foreach (ModelRiskAlert mra in modelRiskAlertDistinctIDsList)
            {
                if (mra.IDModelRiskAlert == alert.IDModelRiskAlert)
                {
                    return true;
                }
            }
            modelRiskAlertDistinctIDsList.Add(alert);
            return false;
        }

        /// <summary>
        /// Iterative function to get all the checked nodes
        /// </summary>
        /// <param name="regionsChecked">The list to add the IDs of regions checked</param>
        /// <param name="node">Node to check. It also checks the nodes node, making it iterative.</param>
        private void GetCheckedRegions(List<int> regionsChecked, TreeNode node)
        {
            if (node.Checked)
            {
                regionsChecked.Add(((Region)node.Tag).IDRegion);
            }
            foreach (TreeNode checkedNode in node.Nodes)
            {
                GetCheckedRegions(regionsChecked, checkedNode);
            }
        }

        private void AddChildRegions(TreeNode treeNode, List<Region> regions, int level)
        {
            foreach (Region r in regions)
            {
                if (r.IDParent.ToString() == treeNode.Name)
                {
                    //regionLevel = 0;
                    TreeNode t = new TreeNode() { Text = r.RegionName, Name = r.IDRegion.ToString(), Tag = r };

                    if (ERMTSession.Instance.CurrentModel == null)
                    {
                        if (r.RegionLevel == 0)
                            t.Checked = true;
                    }

                    // t.ContextMenuStrip = NodeMenu;

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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            EditRiskAndAction win = new EditRiskAndAction();
            win.ShowDialog();

            RefreshData();
        }

        private void RefreshData()
        {
            LoadRisksDataGrid();
        }

        private void rbAll_Click(object sender, EventArgs e)
        {
            LoadRisksDataGrid();
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadRisksDataGrid();
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            LoadRisksDataGrid();
        }

        private void tvRegions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            LoadRisksDataGrid();
        }

        private void dgvAlerts_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dgvAlerts.SelectedRows.Count > 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (IsMasterRAR)
            {
                OpenView();
            }
            else
            {
                EditRiskAndAction win = new EditRiskAndAction((ModelRiskAlert)dgvAlerts.SelectedRows[0].Tag);
                win.ShowDialog();
                RefreshData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Print the grid:
            ModelRiskAlertHelper.GenerateRARHtml(GetPrintTitle(), GetPrintText(), ModelRiskAlertHelper.GetRARHtmlGridFile());
            HtmlPopup win = new HtmlPopup(DirectoryAndFileHelper.ClientAppDataFolder + ModelRiskAlertHelper.GetRARHtmlGridFile(), true);
            win.ShowDialog();
        }


        private string GetPrintTitle()
        {
            // This could actually add in the future the filter parameters. That is why I make it a function.
            return ResourceHelper.GetResourceText("RiskAlertSearch");
        }

        private string GetPrintText()
        {
            // Used http://tablestyler.com/#   for the style)
            StringBuilder returnText = new StringBuilder("<div class='datagrid'><table>");
            returnText.AppendLine("<thead><tr><th>" + ResourceHelper.GetResourceText("Code") + "</th><th>" 
                + ResourceHelper.GetResourceText("Title") + "</th><th>" 
                + ResourceHelper.GetResourceText("Created") + "</th><th>" 
                + ResourceHelper.GetResourceText("Modified") + "</th><th>" 
                + ResourceHelper.GetResourceText("Status") + "</th></tr></thead><tbody>");

            int i = 1;

            foreach (DataGridViewRow row in dgvAlerts.Rows)
            {
                // if (i %  2 == 0)
                //     returnText.Append("<tr class='alt'><td>");
                // else
                returnText.Append("<tr><td>");
                returnText.Append(((ModelRiskAlert)row.Tag).Code);
                returnText.Append("</td><td>");
                returnText.Append(((ModelRiskAlert)row.Tag).Title);
                returnText.Append("</td><td>");
                returnText.Append(((ModelRiskAlert)row.Tag).DateCreated.ToString("d", GeneralHelper.GetDateFormat()));
                returnText.Append("</td><td>");
                returnText.Append(((ModelRiskAlert)row.Tag).DateModified.ToString("d", GeneralHelper.GetDateFormat()));
                returnText.Append("</td><td>");
                returnText.Append(((ModelRiskAlert)row.Tag).Active ? ResourceHelper.GetResourceText("Active"): ResourceHelper.GetResourceText("Inactive"));
                returnText.AppendLine("</td></tr>");
                i = i + 1;
            }

            returnText.AppendLine("</tbody></table></div>");

            return returnText.ToString();
        }

        private string GetAccessTitle(ModelRiskAlert alert)
        {
            return "Prevention and mitigation electoral cycle for " + alert.Code + " - " + alert.Title;
        }

        private string GetAccessText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder("<div align='left'><ul>");

            foreach (ModelRiskAlertPhase alertFase in alert.ModelRiskAlertPhases)
            {
                returnText.Append("<li>");
                returnText.Append("<a href='../../PMM/" + alertFase.Phase.IDPhase.ToString() + "-AP.htm'>");
                returnText.Append(alertFase.Phase.Title);
                returnText.Append("</a>");
                returnText.AppendLine("</li>");
            }

            returnText.AppendLine("</ul></div>");

            return returnText.ToString();
        }

        /// <summary>
        /// Refreshes the model. Loades everything again.
        /// </summary>
        internal void RefreshModel()
        {
            // Load the tree
            LoadModelAndRegions();

            // Unselect the round buttons.
            rbAll.Checked = true;

            // No change on round buttons, new search:
            LoadRisksDataGrid();
        }

        /// <summary>
        /// Opens the edit window with the selected (double clicked) alert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAlerts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == 6) return;
            if (IsMasterRAR)
            {
                OpenView();
            }
            else
            {
                var win = new EditRiskAndAction((ModelRiskAlert)dgvAlerts.Rows[e.RowIndex].Tag);
                win.ShowDialog();
                RefreshData();
            }
        }

        private void OpenView()
        {
            if (dgvAlerts.SelectedRows.Count > 0)
            {
                // Get the id:
                ModelRiskAlert selAlert = dgvAlerts.SelectedRows[0].Tag as ModelRiskAlert;

                if (selAlert != null)
                {
                    ModelRiskAlert alert = ModelRiskAlertHelper.Get(selAlert.IDModelRiskAlert);
                    List<ModelRiskAlertAttachment> alertAttachments =
                        ModelRiskAlertHelper.GetModelRiskAlertAttachments(alert);
                    if (alert.IDModelRiskAlert == selAlert.IDModelRiskAlert)
                    {
                        // View / Print the grid:
                        CopyAttachmentFilesToDir(alertAttachments);
                        ModelRiskAlertHelper.GenerateRARHtml(GetAlertTitle(selAlert), GetAlertText(selAlert, alertAttachments), ModelRiskAlertHelper.GetRARHtmlAlarmFile());
                        var win = new HtmlPopup(DirectoryAndFileHelper.ClientAppDataFolder + ModelRiskAlertHelper.GetRARHtmlAlarmFile())
                        {
                            ShowBackForward = false
                        };
                        win.ShowDialog();
                    }
                }
            }

        }


        private string GetAlertTitle(ModelRiskAlert alert)
        {
            return "Alert " + alert.Code.Trim() + " - " + alert.Title.Trim();
        }

        private string GetAlertText(ModelRiskAlert alert, List<ModelRiskAlertAttachment> alertAttachments)
        {
            StringBuilder returnText = new StringBuilder("<div align='left'><table>");
            Model model = ModelHelper.GetModel(alert.IDModel);
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Code"), alert.Code.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Title"), alert.Title.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Model"), model.Name.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("DateFrom"), alert.DateFrom.ToString("d", GeneralHelper.GetDateFormat())));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("DateTo"), alert.DateTo.ToString("d", GeneralHelper.GetDateFormat())));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Regions"), GetRegionsValueText(alert)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("RiskDescription"), alert.RiskDescription.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Action"), alert.Action.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Result"), alert.Result.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Status"), alert.Active ? ResourceHelper.GetResourceText("Active") : ResourceHelper.GetResourceText("Inactive")));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Phases"), GetPhaseValueText(alert)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Analysis"), GetAttachmentText(alertAttachments)));

            returnText.AppendLine("</table></div>");

            return returnText.ToString();
        }



        private string GetRegionsValueText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder();
            List<Region> regions = ModelRiskAlertHelper.GetRegions(alert);
            foreach (var region in regions)
            {
                returnText.Append(region.RegionName.Trim() + ",");
            }
            string retVal = returnText.ToString();
            if (retVal.Length > 0)
            {
                return retVal.Substring(0, retVal.Length - 1);
            }
            return string.Empty;
        }

        private string GetPhaseValueText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder();
            List<ModelRiskAlertPhase> modelRiskAlertPhases = ModelRiskAlertHelper.GetPhases(alert);
            foreach (var modelRiskAlertPhase in modelRiskAlertPhases)
            {
                Phase phase = PhaseHelper.Get(modelRiskAlertPhase.IDPhase);
                returnText.Append(phase.Title + "<br/>");
            }
            return returnText.ToString();
        }

        private string GetAttachmentText(List<ModelRiskAlertAttachment> alertAttachments)
        {
            StringBuilder returnImages = new StringBuilder();
            StringBuilder returnFiles = new StringBuilder();

            foreach (ModelRiskAlertAttachment att in alertAttachments)
            {
                if (Path.GetExtension(att.AttachmentFile.ToLower()) == ".jpg"
                    || Path.GetExtension(att.AttachmentFile.ToLower()) == ".jpeg"
                    || Path.GetExtension(att.AttachmentFile.ToLower()) == ".gif"
                    || Path.GetExtension(att.AttachmentFile.ToLower()) == ".bmp"
                    || Path.GetExtension(att.AttachmentFile.ToLower()) == ".png")
                {
                    // Is an image, add it to the image builder as an html image.
                    returnImages.Append("<img src='../Docs/");
                    returnImages.Append(att.IDModelRiskAlertAttachment + "-" + att.AttachmentFile);
                    returnImages.Append("' alt='" + att.IDModelRiskAlertAttachment + "-" + att.AttachmentFile);
                    returnImages.AppendLine("' /><br /><br />");
                }
                else
                {
                    // Is an attachment. Add it as a href.
                    returnFiles.Append("<a href='../Docs/");
                    returnFiles.Append(att.IDModelRiskAlertAttachment + "-" + att.AttachmentFile);
                    returnFiles.Append("'>" + att.IDModelRiskAlertAttachment + "-" + att.AttachmentFile);
                    returnFiles.AppendLine("</a><br /><br />");
                }
            }

            return returnImages.ToString() + returnFiles;
        }

        private string GetAlertPropertyText(string label, string value)
        {
            StringBuilder returnText = new StringBuilder();

            returnText.AppendLine(" <tr>");
            returnText.AppendLine("     <td style='text-align:right;vertical-align:top;'>");
            returnText.AppendLine("         <span style='font-weight:bold'>" + label + ": </span>");
            returnText.AppendLine("     </td>");
            returnText.AppendLine("     <td>");
            returnText.AppendLine("         " + value);
            returnText.AppendLine("     </td>");
            returnText.AppendLine(" <tr>");

            return returnText.ToString();
        }
        private void CopyAttachmentFilesToDir(List<ModelRiskAlertAttachment> alertAttachments)
        {
            // Delete all files on attachment directory:
            string folder = DirectoryAndFileHelper.RARDocumentsFolder;
            DirectoryInfo downloadedMessageInfo = new DirectoryInfo(folder);

            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                file.Delete();
            }

            // Generate the files for the attachments:
            foreach (ModelRiskAlertAttachment attachment in alertAttachments)
            {
                string path = folder + attachment.IDModelRiskAlertAttachment + "-" + attachment.AttachmentFile;
                File.WriteAllBytes(path, Convert.FromBase64String(attachment.Content));
            }
        }


        private void selectAllChildRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                _enableSearch = false;
                foreach (TreeNode checkedNode in tvRegions.SelectedNode.Nodes)
                {
                    TreeUtil.MarkRegion(checkedNode, true);
                }
                _enableSearch = true;
                LoadRisksDataGrid();
            }
        }

        private void deselectAllChildRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                _enableSearch = false;
                foreach (TreeNode checkedNode in tvRegions.SelectedNode.Nodes)
                {
                    TreeUtil.MarkRegion(checkedNode, false);
                }
                _enableSearch = true;
                LoadRisksDataGrid();
            }
        }

        private void selectAllRegionsOnThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                _enableSearch = false;
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
                _enableSearch = true;
                LoadRisksDataGrid();
            }
        }

        private void deselectAllRegionsOnThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvRegions.SelectedNode != null)
            {
                _enableSearch = false;
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
                _enableSearch = true;
                LoadRisksDataGrid();
            }
        }

        // This is a hack, cause if not, I cant know what the selected node is when right clicking
        // but not selecting. Doing this, when you rightclick it also selects, and I can get the 
        // current selected node.
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

        private void dgvAlerts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult digres = MessageBox.Show("Are you sure you want to delete the current alert?", "Confirm Delete", MessageBoxButtons.OKCancel);
                if (digres == DialogResult.OK)
                {
                    if (dgvAlerts.SelectedRows.Count > 0)
                    {
                        // Delete the alert.
                        ModelRiskAlertHelper.Delete((ModelRiskAlert)dgvAlerts.SelectedRows[0].Tag);
                        // delete the row.
                        dgvAlerts.Rows.Remove(dgvAlerts.SelectedRows[0]);
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
