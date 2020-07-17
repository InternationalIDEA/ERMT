using System;
using System.Drawing;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public partial class MessageControl : UserControl
    {
        public string[] ParameterValues;
        public String MessageText { get; set; }
        public CustomMessageBoxMessageType CustomMessageBoxMessageType
        {
            set
            {
                switch (value)
                {
                    case CustomMessageBoxMessageType.Information:
                        {
                            gbInfo.Text = ResourceHelper.GetResourceText("Information");
                            gbInfo.ForeColor = Color.Blue;
                            break;
                        }
                    case CustomMessageBoxMessageType.Warning:
                        {
                            gbInfo.Text = ResourceHelper.GetResourceText("Warning");
                            gbInfo.ForeColor = Color.OrangeRed;
                            break;
                        }
                    case CustomMessageBoxMessageType.Error:
                        {
                            gbInfo.Text = ResourceHelper.GetResourceText("Error");
                            gbInfo.ForeColor = Color.Red;
                            break;
                        }
                }
            }
        }

        public MessageControl()
        {
            InitializeComponent();
        }

        private void MessageControl_Load(object sender, EventArgs e)
        {
            lblInformation.ForeColor = Color.CornflowerBlue;
            string msg = MessageText ?? ResourceHelper.GetResourceText("MessageNotFound");

            if (ParameterValues != null)
            {
                switch (ParameterValues.Length)
                {
                    case 0:
                        lblInformation.Text = MessageText;
                        break;
                    case 1:
                        lblInformation.Text = String.Format(msg, ParameterValues[0]);
                        break;
                    case 2:
                        lblInformation.Text = String.Format(msg, ParameterValues[0], ParameterValues[1]);
                        break;
                    case 3:
                        lblInformation.Text = String.Format(msg, ParameterValues[0], ParameterValues[1], ParameterValues[2]);
                        break;
                }
            }
            else
            {
                lblInformation.Text = msg;
            }
        }
    }
}
