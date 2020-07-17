using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using Region = Idea.Entities.Region;

namespace Idea.ERMT.UserControls
{
    public partial class TestUserControl : ERMTUserControl
    {
        public TestUserControl()
        {
            InitializeComponent();
        }
        private Point MouseDownLocation;



        public void LoadFactors()
        {





            //foreach (Factor f in FactorHelper.GetAll())
            //{
            //    FactorAux aux = new FactorAux { Factor = f, Name = f.Name };
            //    if (f.InternalFactor)
            //    {
            //        lbInternalFactors.Items.Add(aux);
            //    }
            //    else
            //    {
            //        lbExternalFactors.Items.Add(aux);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user;
            UserHelper.Login("admin", "123456", out user);
            if (user.Role != null)
            {
                Console.WriteLine(user.Role.Description);
            }

            Region reg = RegionHelper.GetWorld();

            List<Model> models = ModelHelper.GetAll();

            List<Region> regions2 = RegionHelper.GetAll();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

      
    }
}
