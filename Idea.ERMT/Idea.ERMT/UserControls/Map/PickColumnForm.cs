using System;
using System.Data;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class PickColumnForm : Form
    {
        public DataTable Data { get; set; }
        public string SelectedColumn { get { return ((DataColumn)cbColumns.SelectedItem).ColumnName; } }
        public string SelectedParentColumn { get { return (cbParentColumn.SelectedItem.GetType() == typeof(DataColumn)) ? ((DataColumn)cbParentColumn.SelectedItem).ColumnName : (((string)cbParentColumn.SelectedItem)); } }
        private PickColumnForm()
        {
            InitializeComponent();
        }

        public PickColumnForm(DataTable data)
        {
            InitializeComponent();
            LoadData(data);
        }

        private void LoadData(DataTable data)
        {
            cbColumns.DisplayMember = "ColumnName";
            foreach (DataColumn c in data.Columns)
            {
                cbColumns.Items.Add(c);
            }
            
            dgvColumnData.DataSource = data;
            if (cbColumns.Items.Count > 0)
            {
                cbColumns.SelectedIndex = 0;
            }

            cbParentColumn.DisplayMember = "ColumnName";
            foreach (DataColumn c in data.Columns)
            {
                cbParentColumn.Items.Add(c);
            }
            
            cbParentColumn.Items.Insert(0,"No parent column");
            if (cbParentColumn.Items.Count > 0)
            {
                cbParentColumn.SelectedIndex = 0;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void dgvColumnData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbColumns.SelectedIndex = e.ColumnIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

    }
}
