using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using System.IO;
using Idea.Utils;


namespace Idea.ERMT.UserControls
{
    public partial class RiskActionRegister : ERMTUserControl
    {
        List<ModelRiskAlert> _alerts;
        private const bool EnableSearch = true;

        public RiskActionRegister()
        {
            InitializeComponent();
        }

        private void LoadRisksDataGrid()
        {
            if (EnableSearch)
            {
                // Load List:
                // Clear and create columns:
                dgvAlerts.Rows.Clear();

                _alerts = ModelRiskAlertHelper.GetWithFilter(null, new List<int> (), (rbActive.Checked) ? true : (rbInactive.Checked ? (bool?)false : null));
                //List<ModelRiskAlert> modelRiskAlertDistinctIDsList = new List<ModelRiskAlert>();
                foreach (ModelRiskAlert alert in _alerts)
                {
                    //if (IsAlertInList(alert, modelRiskAlertDistinctIDsList)) continue;

                    int i = 0;
                    StringBuilder electoralPhase = new StringBuilder("");

                    foreach (ModelRiskAlertPhase phase in alert.ModelRiskAlertPhases)
                    {
                        if (i > 0)
                            electoralPhase.Append(", ");
                        electoralPhase.Append(phase.Phase.Title);
                        i = i + 1;
                    }

                    i = dgvAlerts.Rows.Add(alert.Code, alert.Title, alert.Model.Name, electoralPhase.ToString() != "" ? electoralPhase.ToString() : "--", alert.DateCreated, alert.DateModified, (alert.Active) ? "Active" : "Inactive", "+");
                    DataGridViewRow r = dgvAlerts.Rows[i];
                    r.Tag = alert;
                    if (electoralPhase.ToString() != "")
                        r.Cells[3].ToolTipText = electoralPhase.ToString().Replace(",", Environment.NewLine);

                    if (alert.Active)
                    {
                        r.Cells[6].Style.BackColor = Color.Red;
                        r.Cells[6].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        r.Cells[6].Style.BackColor = Color.Green;
                        r.Cells[6].Style.ForeColor = Color.White;
                    }

                }
            }
        }

        //private bool IsAlertInList(ModelRiskAlert alert, List<ModelRiskAlert> modelRiskAlertDistinctIDsList)
        //{
        //    foreach (ModelRiskAlert mra in modelRiskAlertDistinctIDsList)
        //    {
        //        if (mra.IDModelRiskAlert == alert.IDModelRiskAlert)
        //        {
        //            return true;
        //        }
        //    }
        //    modelRiskAlertDistinctIDsList.Add(alert);
        //    return false;
        //}

        private void rbAll_CheckedChanged(object sender, EventArgs e)
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

        private void btnView_Click(object sender, EventArgs e)
        {
            OpenView();
        }

        private void OpenView()
        {
            if (dgvAlerts.SelectedRows.Count > 0)
            {
                // Get the id:
                ModelRiskAlert selAlert = dgvAlerts.SelectedRows[0].Tag as ModelRiskAlert;

                if (selAlert != null)
                {
                    // Get the alert
                    foreach (ModelRiskAlert alert in _alerts)
                    {
                        if (alert.IDModelRiskAlert == selAlert.IDModelRiskAlert)
                        {
                            // Show this alert!!!!

                            // View / Print the grid:
                            CopyAttachmentFilesToDir(alert);
                            ModelRiskAlertHelper.GenerateRARHtml(GetAlertTitle(selAlert), GetAlertText(selAlert), ModelRiskAlertHelper.GetRARHtmlAlarmFile());
                            var win = new HtmlPopup(DirectoryAndFileHelper.ClientAppDataFolder + ModelRiskAlertHelper.GetRARHtmlAlarmFile())
                            {
                                ShowBackForward = false
                            };
                            win.ShowDialog();

                            break;
                        }
                    }
                }
            }

        }

        private void CopyAttachmentFilesToDir(ModelRiskAlert alert)
        {
            // Delete all files on attachment directory:
            string folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"];
            DirectoryInfo downloadedMessageInfo = new DirectoryInfo(folder);

            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                file.Delete();
            }

            // Generate the files for the attachments:
            foreach (ModelRiskAlertAttachment attachment in alert.ModelRiskAlertAttachments)
            {
                string path = folder + attachment.IDModelRiskAlertAttachment + "-" + attachment.AttachmentFile;
                File.WriteAllBytes(path, Convert.FromBase64String(attachment.Content));
            }
        }

        private string GetAlertTitle(ModelRiskAlert alert)
        {
            return "Alert " + alert.Code.Trim() + " - " + alert.Title.Trim();
        }

        private string GetAlertText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder("<div align='left'><table>");

            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Code"), alert.Code.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Title"), alert.Title.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Model"), alert.Model.Name.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("DateFrom"), alert.DateFrom.ToString("d", CultureInfo.InstalledUICulture)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("DateTo"), alert.DateTo.ToString("d", CultureInfo.InstalledUICulture)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Regions"), GetRegionsValueText(alert)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("RiskDescription"), alert.RiskDescription.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Action"), alert.Action.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Result"), alert.Result.Trim()));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Status"), alert.Active ? ResourceHelper.GetResourceText("Active") : ResourceHelper.GetResourceText("Inactive")));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Phases"), GetPhaseValueText(alert)));
            returnText.Append(GetAlertPropertyText(ResourceHelper.GetResourceText("Analysis"), GetAttachmentText(alert)));

            returnText.AppendLine("</table></div>");

            return returnText.ToString();
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

        private string GetRegionsValueText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder();
            foreach (var region in alert.ModelRiskAlertRegions)
            {
                returnText.Append(region.Region.RegionName.Trim() + ",");
            }
            string retVal =  returnText.ToString();
            if (retVal.Length > 0)
            {
                return retVal.Substring(0, retVal.Length - 1);
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetPhaseValueText(ModelRiskAlert alert)
        {
            StringBuilder returnText = new StringBuilder();
            foreach (var phase in alert.ModelRiskAlertPhases)
            {
                returnText.Append(phase.Phase.Title + "<br/>");
            }
            return returnText.ToString();
        }

        private string GetAttachmentText(ModelRiskAlert alert)
        {
            StringBuilder returnImages = new StringBuilder();
            StringBuilder returnFiles = new StringBuilder();

            foreach (var att in alert.ModelRiskAlertAttachments)
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

        private void dgvAlerts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlerts.SelectedRows.Count > 0)
            {
                btnView.Enabled = true;
            }
            else
            {
                btnView.Enabled = false;
            }
        }

        private void dgvAlerts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == 6) return;
            OpenView();
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("RiskAndActionTitle"));
        }
    }
}
