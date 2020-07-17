using System;
using System.IO;

namespace Idea.Utils
{
    public static class FileSystem
    {
        /// <summary>
        /// Deletes everything from the specified folder
        /// </summary>
        /// <param name="folderName"></param>
        public static void ClearFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        public static bool CopyDirectory(string sourcePath, string destinationPath, bool overwriteExisting)
        {
            bool ret = true;
            try
            {
                sourcePath = sourcePath.EndsWith(@"\") ? sourcePath : sourcePath + @"\";
                destinationPath = destinationPath.EndsWith(@"\") ? destinationPath : destinationPath + @"\";

                if (Directory.Exists(sourcePath))
                {
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    

                    foreach (string fls in Directory.GetFiles(sourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(destinationPath + flinfo.Name, overwriteExisting);
                    }
                    foreach (string drs in Directory.GetDirectories(sourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, destinationPath + drinfo.Name, overwriteExisting) == false)
                            ret = false;
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
}
