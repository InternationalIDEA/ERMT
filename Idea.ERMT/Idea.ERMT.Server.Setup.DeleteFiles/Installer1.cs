using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Security.AccessControl;
using System.IO;
using System.Windows.Forms;
using Idea.Utils;

namespace CustomAction.DeleteFiles
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            try
            {
                string filesDirectory = Context.Parameters["filesDirectory"];
                DeleteFiles(new DirectoryInfo(filesDirectory));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + Context.Parameters["filesDirectory"]);
            }

            
            base.Uninstall(savedState);
        }

        public void DeleteFiles(DirectoryInfo filesDirectory)
        {
            try
            {
                FileSystem.ClearFolder(filesDirectory.FullName);
            }
            catch (Exception)
            {
            }
        }
    }
}
