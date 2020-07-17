using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class ElectoralCycleBulletList : ERMTUserControl
    {
        private bool _orderChanged;
        int _lastId = -1;

        public bool HasChanged
        {
            get
            {
                if (_orderChanged) return true;

                bool changed = false;
                foreach (var item in bulletsListBox.Items)
                {
                    // TODO: Santiago: Terminar el has changed para informarlo!!!!

                    // Si id = 0 es nuevo, changed (Calculo que ni hace falta, que al cargarlo con un valor se edita.)
                    // Si alguno otro tiene el has changed = true, changed tambien!
                }



                return false;
            }
        }

        private List<PhaseBullet> _bullets;

        public List<PhaseBullet> Bullets
        {
            set
            {
                _bullets = value;
                bulletsListBox.Items.Clear();

                if (_bullets != null)
                {
                    foreach (PhaseBullet bullet in _bullets)
                    {
                        // Ignore if deleted. Shouldnt happen, cause it is filtered on the SQL, but as we can delete a bullet, 
                        // we can change it. Maybe in the future we could reload it for some reason and 
                        // it is better to have it filtered.
                        bulletsListBox.Items.Add(bullet);
                    }
                }
            }
            get
            {
                return _bullets;
            }
        }

        public List<int> PhaseBulletsIDsToDelete { get; set; }

        /// <summary>
        /// Used for new ones. Instead of ID zero, we are using a negative value.
        /// Later, when sending to save, we must remove it and insert zero.
        /// </summary>
        private int LastId
        {
            get
            {
                return _lastId - 1;
            }
        }



        public ElectoralCycleBulletList()
        {
            InitializeComponent();
            LoadControls();
            PhaseBulletsIDsToDelete = new List<int>();
        }

        private void LoadControls()
        {
            btnUp.Image = ResourceHelper.GetResourceImage("arrow_up_icon");
            btnDown.Image = ResourceHelper.GetResourceImage("arrow_down_icon");
        }

        private void MoveItem(int direction)
        {
            if (bulletsListBox.SelectedItem == null || bulletsListBox.SelectedIndex < 0)
            {
                return; // No selected item - nothing to do
            }

            int newIndex = bulletsListBox.SelectedIndex + direction;

            if (newIndex < 0 || newIndex >= bulletsListBox.Items.Count)
            {
                return;
            }

            object selected = bulletsListBox.SelectedItem;

            bulletsListBox.Items.Remove(selected);
            bulletsListBox.Items.Insert(newIndex, selected);
            bulletsListBox.SetSelected(newIndex, true);
            _orderChanged = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (ElectoralCycleEditBullet dialog = new ElectoralCycleEditBullet())
            {
                if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    // Create a new Bullet:
                    PhaseBullet newBullet = PhaseBulletHelper.GetNew();
                    newBullet.Text = dialog.BulletText;
                    newBullet.SortOrder = bulletsListBox.Items.Count + 1;
                    //newBullet.IDPhaseBullet = LastId;

                    // insert in the List and the collection:
                    bulletsListBox.Items.Add(newBullet);
                    _bullets.Add(newBullet);
                }
                else
                {
                    dialog.BulletText = string.Empty;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PhaseBulletDelete"), CustomMessageBoxMessageType.Information,
                CustomMessageBoxButtonType.YesNo) == CustomMessageBoxReturnValue.Ok)
            {

                // Mark as deleted in the 
                // Get the ID:
                int bulletId = ((PhaseBullet)bulletsListBox.SelectedItem).IDPhaseBullet;

                // Remove the item from the list
                bulletsListBox.BeginUpdate();
                bulletsListBox.Items.RemoveAt(bulletsListBox.SelectedIndex);
                bulletsListBox.EndUpdate();

                if (bulletId > 0)
                {
                    //PhaseBulletHelper.Delete(bulletId);
                    PhaseBulletsIDsToDelete.Add(bulletId);
                    _bullets.RemoveAll(s => s.IDPhaseBullet == bulletId);
                }
                else
                {
                    // It is negative, so it is a new one, we must actually remove it from the list.
                    _bullets.RemoveAll(s => s.IDPhaseBullet == bulletId);
                }
            }
        }

        private void bulletsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bulletsListBox.SelectedIndex >= 0)
            {
                btnDelete.Enabled = true;
                btnDown.Enabled = true;
                btnUp.Enabled = true;
                if (bulletsListBox.SelectedIndex == 0)
                    btnUp.Enabled = false;
                if (bulletsListBox.SelectedIndex == bulletsListBox.Items.Count - 1)
                    btnDown.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = false;
                btnDown.Enabled = false;
                btnUp.Enabled = false;
            }
        }

        private void bulletsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (bulletsListBox.SelectedIndex >= 0)
                using (ElectoralCycleEditBullet dialog = new ElectoralCycleEditBullet())
                {
                    dialog.BulletText = ((PhaseBullet)bulletsListBox.SelectedItem).Text;
                    if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                    {

                        // Update the exising bullet:
                        ((PhaseBullet)bulletsListBox.SelectedItem).Text = dialog.BulletText;


                        typeof(ListBox).InvokeMember("RefreshItems", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, bulletsListBox, new object[] { });

                    }
                    else
                    {
                        dialog.BulletText = string.Empty;
                    }
                }
        }

        protected internal void SortList()
        {
            // Reorder the values (this will ignore the deleted ones):
            int i = 0;
            foreach (var item in bulletsListBox.Items)
            {
                i = i + 1;
                foreach (PhaseBullet bullet in _bullets)
                {
                    if (bullet.IDPhaseBullet == ((PhaseBullet)item).IDPhaseBullet)
                    {
                        bullet.SortOrder = i;
                        break;
                    }
                }
            }
        }
    }
}
