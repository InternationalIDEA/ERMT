using System;
using System.Windows.Forms;
using System.IO;

namespace Idea.Facade
{
    public static class ExportCSVButton
    {
        public static string ExportToCSV(DataGridView source, string filename, bool exportAll)
        {
            filename = filename.Replace("/", "-");
            string listSeparator = Application.CurrentCulture.TextInfo.ListSeparator;
            string rows = string.Empty;
            foreach (DataGridViewColumn col in source.Columns)
            {
                if (col.Visible)
                    rows += col.HeaderText.Replace(",", "-") + listSeparator;
            }
            rows += Environment.NewLine;
            foreach (DataGridViewRow r in source.Rows)
            {
                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.Visible)
                        if (c.EditType.FullName != "System.Windows.Forms.DataGridViewComboBoxEditingControl")
                            rows += "\"" + (c.Value ?? string.Empty) + "\"" + listSeparator;
                        else
                            rows += "\"" + (c.FormattedValue ?? string.Empty) + "\"" + listSeparator;
                }

                rows += Environment.NewLine;

            }
            if (!exportAll)
            {
                SaveFileDialog sfd = new SaveFileDialog {FileName = filename + ".csv"};
                if (sfd.ShowDialog() == DialogResult.OK)
                    try
                    {
                        File.WriteAllText(sfd.FileName, rows);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ResourceHelper.GetResourceText("UnableToSave"));
                    }
                return sfd.FileName;
            }
            
            filename += ".csv";
            try
            {
                File.WriteAllText(filename, rows);
            }
            catch (Exception e)
            {
                MessageBox.Show(ResourceHelper.GetResourceText("UnableToSave"));
            }
            return filename;
        }
    }
}
