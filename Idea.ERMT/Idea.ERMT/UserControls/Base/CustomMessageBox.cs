using System;
using System.Windows.Forms;
using Idea.Facade;

namespace Idea.ERMT.UserControls
{
    public enum CustomMessageBoxButtonType
    {
        YesNo, OKSkip, OKOnly
    }

    public enum CustomMessageBoxMessageType
    {
        Information, Error, Warning
    }

    public enum CustomMessageBoxReturnValue
    {
        Ok, Cancel
    }

    public partial class CustomMessageBox : Form
    {
        private static CustomMessageBox _newMessageBox;
        private static CustomMessageBoxReturnValue _returnValue;

        public CustomMessageBox()
        {
            InitializeComponent();
        }
        

        /// <summary>
        /// Displays an information message with OK button only.
        /// </summary>
        /// <param name="messageText"></param>
        /// <returns></returns>
        public static CustomMessageBoxReturnValue ShowMessage(String messageText)
        {
            return ShowMessage(messageText, CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);
        }

        public static CustomMessageBoxReturnValue ShowMessage(String messageText, CustomMessageBoxMessageType customMessageBoxMessageType)
        {
            _newMessageBox = new CustomMessageBox
            {
                StartPosition = FormStartPosition.CenterScreen,
                messageControl1 =
                {
                    MessageText = messageText,
                    CustomMessageBoxMessageType = customMessageBoxMessageType
                }
            };
            _newMessageBox.ShowDialog();
            return _returnValue;
        }

        public static CustomMessageBoxReturnValue ShowMessage(String messageText, string[] parameterValues)
        {
            _newMessageBox = new CustomMessageBox
                                {
                                    StartPosition = FormStartPosition.CenterScreen,
                                    messageControl1 =
                                        {
                                            MessageText = messageText,
                                            ParameterValues = parameterValues
                                        }
                                };
            _newMessageBox.ShowDialog();
            return _returnValue;
        }

        public static CustomMessageBoxReturnValue ShowMessage(String messageText, CustomMessageBoxMessageType customMessageBoxMessageType, string[] parameterValues)
        {
            _newMessageBox = new CustomMessageBox
            {
                StartPosition = FormStartPosition.CenterScreen,
                messageControl1 =
                {
                    MessageText = messageText,
                    CustomMessageBoxMessageType = customMessageBoxMessageType,
                    ParameterValues = parameterValues
                }
            };
            _newMessageBox.ShowDialog();
            return _returnValue;
        }

        public static CustomMessageBoxReturnValue ShowMessage(String messageText, CustomMessageBoxMessageType customMessageBoxMessageType, CustomMessageBoxButtonType customMessageBoxButtonType)
        {
            _newMessageBox = new CustomMessageBox
            {
                StartPosition = FormStartPosition.CenterScreen,
                messageControl1 =
                {
                    MessageText = messageText,
                    CustomMessageBoxMessageType = customMessageBoxMessageType
                }
            };

            switch (customMessageBoxButtonType)
            {
                case CustomMessageBoxButtonType.YesNo:
                    _newMessageBox.btnCancel.Text = ResourceHelper.GetResourceText("No");
                    _newMessageBox.btnOK.Text = ResourceHelper.GetResourceText("Yes");
                    break;
                case CustomMessageBoxButtonType.OKSkip:
                    _newMessageBox.btnCancel.Text = ResourceHelper.GetResourceText("Skip");
                    break;
                case CustomMessageBoxButtonType.OKOnly:
                    _newMessageBox.btnOK.Text = ResourceHelper.GetResourceText("OKText");
                    _newMessageBox.btnOK.Left = (_newMessageBox.Width / 2) - _newMessageBox.btnOK.Width / 2;
                    _newMessageBox.btnCancel.Visible = false;
                    break;
            }

            _newMessageBox.ShowDialog();
            return _returnValue;
        }

        public static CustomMessageBoxReturnValue ShowMessage(String messageText, CustomMessageBoxMessageType customMessageBoxMessageType, CustomMessageBoxButtonType customMessageBoxButtonType, string[] parameterValues)
        {
            _newMessageBox = new CustomMessageBox
            {
                StartPosition = FormStartPosition.CenterScreen,
                messageControl1 =
                {
                    MessageText = messageText,
                    CustomMessageBoxMessageType = customMessageBoxMessageType,
                    ParameterValues = parameterValues
                }
            };

            switch (customMessageBoxButtonType)
            {
                case CustomMessageBoxButtonType.YesNo:
                    _newMessageBox.btnCancel.Text = ResourceHelper.GetResourceText("No");
                    _newMessageBox.btnOK.Text = ResourceHelper.GetResourceText("Yes");
                    break;
                case CustomMessageBoxButtonType.OKSkip:
                    _newMessageBox.btnCancel.Text = ResourceHelper.GetResourceText("Skip");
                    break;
                case CustomMessageBoxButtonType.OKOnly:
                    _newMessageBox.btnCancel.Visible = false;
                    break;
            }

            _newMessageBox.ShowDialog();
            return _returnValue;
        }

        /// <summary>
        /// Displays the error message with the format of an error
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void ShowError(string errorMessage)
        {
            ShowMessage(errorMessage, CustomMessageBoxMessageType.Error, CustomMessageBoxButtonType.OKOnly);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _returnValue = CustomMessageBoxReturnValue.Ok;
            _newMessageBox.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _returnValue = CustomMessageBoxReturnValue.Cancel;
            _newMessageBox.Dispose();
        }
    }
}
