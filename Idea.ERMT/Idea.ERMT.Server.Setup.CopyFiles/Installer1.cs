using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Security.AccessControl;
using System.IO;
using System.Windows.Forms;


namespace Idea.CopyFiles
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            try
            {
                string sourceDir = Context.Parameters["Source"];
                string targetDir = Context.Parameters["Target"];
                CopyFiles(new DirectoryInfo(sourceDir), new DirectoryInfo(targetDir), false, "*.*");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + Context.Parameters["Source"] + "\r\n" + Context.Parameters["Target"]);
            }

            base.Install(stateSaver);
        }

        public void CopyFiles(DirectoryInfo source, DirectoryInfo destination, bool overwrite, string searchPattern)
        {
            FileInfo[] files = source.GetFiles(searchPattern);
            foreach (FileInfo file in files)
            {
                if (File.Exists(destination.FullName + "\\" + file.Name))
                {
                    try
                    {
                        File.Delete(destination.FullName + "\\" + file.Name);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                file.CopyTo(destination.FullName + "\\" + file.Name, overwrite);
            }
        }
    }
}
