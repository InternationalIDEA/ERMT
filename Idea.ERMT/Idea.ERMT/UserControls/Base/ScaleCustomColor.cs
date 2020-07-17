using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Idea.Facade;
using System.Xml.Linq;
using Idea.Entities;


namespace Idea.ERMT.UserControls
{
    public partial class ScaleCustomColor : Form
    {
        #region  Properties
        public List<ModelFactor> ListModelFactor { get; set; }
        #endregion


        public ScaleCustomColor()
        {
            InitializeComponent();
            CreateListScuards();
        }


        private void CreateListScuards()
        {
            List<XElement> listaElement = CustomFactorColorHelper.GetFactorColorsFromXML(ERMTSession.Instance.CurrentModel.IDModel);
            //Create and initialize a GroupBox and a Button control.
            Model model = ModelHelper.GetModel(ERMTSession.Instance.CurrentModel.IDModel);
            ListModelFactor = ModelFactorHelper.GetByModel(model);

            List<int> groupByTotals = new List<int>();

            foreach (ModelFactor listFactor in ListModelFactor)
            {                
                FlowLayoutPanel layout = new FlowLayoutPanel();

                int totalValue = (listFactor.ScaleMax - listFactor.ScaleMin + 1);

                //FActor que no sean acumulativos
                if (totalValue > 1)
                {
                    //Chequeo que renderize solo las escalas o rangos distintos
                    var factorsColors =
                       (from c in listaElement
                       from cColors in c.Descendants("Colors")
                       where (int)c.Element("TotalValues") == totalValue
                       select cColors).ToList();
                    if (!groupByTotals.Contains(totalValue))
                    {
                        groupByTotals.Add(totalValue);
                        layout.Name = "layout" + listFactor.IDFactor;
                        layout.Dock = DockStyle.None;
                        //layout.BorderStyle = BorderStyle.FixedSingle;
                        layout.Size = new Size(415, 49);

                        Label lbl = new Label
                                        {
                                            Text =
                                                listFactor.ScaleMin + " - " + listFactor.ScaleMax,
                                            Name =
                                                listFactor.ScaleMin + " - " + listFactor.ScaleMax,
                                            Visible = true
                                        };
                        layout.Controls.Add(lbl);

                        for (int i = 1; i <= totalValue; i++)
                        {
                            Label labelColorFactor = new Label
                            {
                                AutoSize = true,
                                BackColor =
                                    factorsColors.Count > 0
                                        ? ColorTranslator.FromHtml(factorsColors[0].Elements().ToList()[i - 1].Value)
                                        : Color.White,
                                BorderStyle = BorderStyle.Fixed3D,
                                Name = "lblColorFactor" + listFactor.IDFactor + i,
                                Size = new Size(18, 15)
                            };

                            labelColorFactor.Click += labelColorFactor_Click;
                            labelColorFactor.TabIndex = i;
                            labelColorFactor.Text = "  ";

                            layout.Controls.Add(labelColorFactor);
                        }
                        flowLayoutPanel1.Controls.Add(layout);
                    }
                }
            }
        }

        private void labelColorFactor_Click(object sender, EventArgs e)
        {
            if (colorDialogCustom.ShowDialog() == DialogResult.OK)
            {
                var lbl = ((Label)sender);
                lbl.BackColor = colorDialogCustom.Color;
            }
        }

        private void btnSaveCustomColors_Click(object sender, EventArgs e)
        {
            //Recorre todos los cuadraditos y recupera los backGround y los guarda
            Model model = ModelHelper.GetModel(ERMTSession.Instance.CurrentModel.IDModel);
            ListModelFactor = ModelFactorHelper.GetByModel(model);

            List<int> groupByTotals = new List<int>();

            List<CustomFactorColor> listaFactorColor = new List<CustomFactorColor>();

            foreach (ModelFactor modelFactor in ListModelFactor)
            {
                var factor = FactorHelper.Get(modelFactor.IDFactor);
                CustomFactorColor custom = new CustomFactorColor();

                int totalValue = (modelFactor.ScaleMax - modelFactor.ScaleMin + 1);

                //Chequeo que renderize solo las escalas o rangos distintos
                if (!groupByTotals.Contains(totalValue))
                {
                    groupByTotals.Add(totalValue);

                    custom.IdFactor = modelFactor.IDFactor;
                    custom.Name = modelFactor.ScaleMin + " - " + modelFactor.ScaleMax;
                    custom.MaxValue = modelFactor.ScaleMax;
                    custom.MinValue = modelFactor.ScaleMin;
                    custom.TotalValues = (modelFactor.ScaleMax - modelFactor.ScaleMin + 1);

                    var layOut = Controls.Find("layout" + factor.IdFactor, true);


                    if (layOut != null && layOut.Length > 0)
                    {
                        var lay = (FlowLayoutPanel)layOut[0];

                        bool primerLable = true;
                        foreach (var control in lay.Controls)
                        {
                            if (control.GetType() == typeof(Label) && !primerLable)
                            {
                                custom.ListColors.Add(((Label)control).BackColor);
                            }
                            else
                                primerLable = false;
                        }
                        listaFactorColor.Add(custom);
                    }
                }
            }
            CustomFactorColorHelper.SaveFactorColorsToXML(listaFactorColor, model.IDModel);

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
