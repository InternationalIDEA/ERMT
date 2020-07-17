using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Idea.ERMT.Setup.UI
{
    public partial class SetupForm : Form
    {
        private readonly DirectoryInfo _baseDirectory = new DirectoryInfo(Application.StartupPath);
        private readonly FileInfo _serverExecutable;
        private readonly FileInfo _clientExecutable;
        public SetupForm()
        {
            InitializeComponent();
            _serverExecutable = new FileInfo(_baseDirectory + "\\Server\\setup.exe");
            _clientExecutable = new FileInfo(_baseDirectory + "\\Client\\setup.exe");
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Process.Start(_serverExecutable.ToString());
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Process.Start(_clientExecutable.ToString());
        }

        private void pbEN_Click(object sender, EventArgs e)
        {
            SetRightToLeft(false);
            lblInfo1.Text =
                "The server version of the tool should be installed before the client. When launching the server installation, this window will remain open and can be used to install the client version once the server setup is complete.";
            lblInfo2.Text = "Please select which version to install. Previous versions and data will be removed.";
            btnServer.Text = "SERVER";
            btnClient.Text = "CLIENT";
        }

        private void pbES_Click(object sender, EventArgs e)
        {
            SetRightToLeft(false);
            lblInfo1.Text =
                "El servidor debe ser instalado antes que el cliente. Al instalar el servidor esta ventana permanecerá abierta para poder instalar el cliente luego.";
            lblInfo2.Text = "Seleccione qué versión instalar. Versiones anteriores serán desinstaladas y la información eliminada.";
            btnServer.Text = "SERVIDOR";
            btnClient.Text = "CLIENTE";
        }

        private void pbFR_Click(object sender, EventArgs e)
        {
            SetRightToLeft(false);
            lblInfo1.Text =
                "La version du serveur de l'outil doit être installé avant le client. Lors du lancement de l'installation du serveur, cette fenêtre restera ouverte et peut être utilisé pour installer la version du client une fois la configuration du serveur est terminée";
            lblInfo2.Text =
                "S'il vous plaît sélectionner la version à installer. Les versions précédentes et données seront supprimées.";
            btnServer.Text = "SERVEUR";
            btnClient.Text = "CLIENT";
        }

        private void pbAR_Click(object sender, EventArgs e)
        {
            SetRightToLeft(true);
            lblInfo1.Text =
                "يجب تثبيت الإصدار خادم الأداة قبل العميل. عند بدء تركيب سيرفر، سوف تبقى هذه النافذة مفتوحة، ويمكن استخدامها لتثبيت إصدار عميل بمجرد أن إعداد ملقم كاملة.";
            lblInfo2.Text = "يرجى تحديد أي إصدار لتثبيت. سيتم إزالة الإصدارات والبيانات السابقة.";
            btnServer.Text = "الخادم";
            btnClient.Text = "زبون";
        }

        private void SetRightToLeft(Boolean rightToLeft)
        {
            lblInfo1.RightToLeft = (rightToLeft ? RightToLeft.Yes : RightToLeft.No);
            lblInfo2.RightToLeft = (rightToLeft ? RightToLeft.Yes : RightToLeft.No);
            btnServer.RightToLeft = (rightToLeft ? RightToLeft.Yes : RightToLeft.No);
            btnClient.RightToLeft = (rightToLeft ? RightToLeft.Yes : RightToLeft.No);
        }
    }
}
