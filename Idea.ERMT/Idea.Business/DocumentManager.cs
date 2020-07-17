using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Idea.Entities;
using System.Reflection;
using Idea.Utils;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


namespace Idea.Business
{
    public class DocumentManager
    {
        public const string IndexLinkTemplate = "<li><a href=\"./{0}\">{1}</a></li>";

        /// <summary>
        /// Saves the content to the path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void Save(string path, byte[] content)
        {

            bool addDocument = !File.Exists(path);
            File.WriteAllBytes(path, content);
        }

        /// <summary>
        /// Saves the content to the path.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        public static void Save(FileInfo file, byte[] content)
        {
            if (!Directory.Exists(file.DirectoryName))
            {
                Directory.CreateDirectory(file.DirectoryName);
            }

            File.WriteAllBytes(file.FullName, content);
        }

        /// <summary>
        /// Delete files by name(fileName) as a parameter
        /// </summary>
        /// <param name="fileName"></param>
        public static void Delete(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// Saves the html to the path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="html"></param>
        public static void Save(string path, string html)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write(html.Replace("contentEditable=true", "contentEditable=false"));
                streamWriter.Flush();
                streamWriter.Close();
            }
            GenerateIndex();
        }

        /// <summary>
        /// Generate the new Index.
        /// </summary>
        public static void GenerateIndex(CultureInfo uiCulture)
        {
            StringBuilder stringBuilderInt = new StringBuilder();
            StringBuilder stringBuilderExt = new StringBuilder();
            Thread.CurrentThread.CurrentUICulture = uiCulture;
            foreach (Factor factor in FactorManager.GetAllWithHtmlDocument())
            {
                if (factor.InternalFactor)
                    stringBuilderInt.AppendLine(string.Format(IndexLinkTemplate, factor.IdFactor + ".htm", factor.Name));
                else
                    stringBuilderExt.AppendLine(string.Format(IndexLinkTemplate, factor.IdFactor + ".htm", factor.Name));
            }

            String indexTemplate = DirectoryAndFileHelper.IndexTemplate;

            //switch (culture.Name.ToLower())
            //{
            //    case "es-es":
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplateSpanish;
            //            break;
            //        }
            //    case "fr":
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplateFrench;
            //            break;
            //        }
            //    default:
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplate;
            //            break;
            //        }
            //}

            using (StreamReader streamReader = new StreamReader(indexTemplate))
            {
                string template = streamReader.ReadToEnd();
                //factor data
                template = template.Replace("@InternalLinks@", stringBuilderInt.ToString());
                template = template.Replace("@ExternalLinks@", stringBuilderExt.ToString());

                //headers
                template = template.Replace("@KnowledgeResourcesHeader@", LanguageResourceManager.GetResourceText("KnowledgeResourcesHeader"));
                template = template.Replace("@InternalFactorsHeader@", LanguageResourceManager.GetResourceText("InternalFactorsHeader"));
                template = template.Replace("@ExternalFactorsHeader@", LanguageResourceManager.GetResourceText("ExternalFactorsHeader"));

                using (StreamWriter streamWriter2 = new StreamWriter(DirectoryAndFileHelper.Index, false))
                {
                    streamWriter2.Write(template);
                    streamWriter2.Flush();
                    streamWriter2.Close();
                }
                streamReader.Close();
            }
        }

        /// <summary>
        /// Generate de new Index.
        /// </summary>
        public static void GenerateIndex()
        {
            GenerateIndex(new CultureInfo("en"));
        }

        #region RESTORE

        /// <summary>
        /// Restore the Application by fileContent as a parameter
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="restoreDB"></param>
        /// <param name="restoreFiles"></param>
        /// <param name="restoreShapeFiles"></param>
        public static string RestoreApplicationData(string fileContent, bool restoreDB, bool restoreFiles, bool restoreShapeFiles)
        {
            string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;
            DirectoryInfo backupFolder = new DirectoryInfo(commonDataFolder + "BackupAuxFolder");

            if (!Directory.Exists(backupFolder.FullName))
            {
                Directory.CreateDirectory(backupFolder.FullName);
            }

            Utils.FileSystem.ClearFolder(backupFolder.FullName);

            string fileName = backupFolder.FullName + @"\" + DateTime.Now.Ticks + ".gz";
            string noCorrespondingRestorations = string.Empty;

            try
            {
                //try to restore asuming the backup is from this version.
                File.WriteAllBytes(fileName, Convert.FromBase64String(fileContent));

                GZip.DecompressToDirectory(fileName, backupFolder.FullName);

                RestoreFiles(backupFolder.FullName, restoreFiles, restoreShapeFiles, ref noCorrespondingRestorations);

                if (restoreDB)
                {
                    if (File.Exists(backupFolder.FullName + "\\databaseBackup.bak"))
                    {
                        noCorrespondingRestorations += RestoreDatabase(backupFolder.FullName + "\\databaseBackup.bak");
                    }
                    else
                    {
                        noCorrespondingRestorations = noCorrespondingRestorations + "Database - ";
                    }
                }
            }
            //catch (OutOfMemoryException ex)
            //{
            //    //means the backup file is older than V6. Try to use the old method.
            //    RestoreDatabaseOld(fileContent, backupFolder.FullName);
            //    return noCorrespondingRestorations;
            //}
            catch (Exception ex)
            {
                //means the backup file is older than V6. Try to use the old method.
                //RestoreDatabaseOld(fileContent, backupFolder.FullName);
                return noCorrespondingRestorations;
            }
            finally
            {
                Utils.FileSystem.ClearFolder(backupFolder.FullName);
            }
            return noCorrespondingRestorations;
        }

        /// <summary>
        /// Used to restore databases older than 6.0
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="serverAppFolder"></param>
        /// <returns></returns>
        //public static string RestoreDatabaseOld(string fileContent, string serverAppFolder)
        //{
        //    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Start restore"));
        //    string fileName = serverAppFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak.gz";
        //    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Restore temp file: " + fileName));
        //    File.WriteAllBytes(fileName, Convert.FromBase64String(fileContent));
        //    // Get the stream of the source file.
        //    FileInfo fi = new FileInfo(fileName);
        //    using (FileStream inFile = fi.OpenRead())
        //    {
        //        // Get original file extension, for example "doc" from report.doc.gz.
        //        string curFile = fi.FullName;
        //        string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

        //        //Create the decompressed file.
        //        using (FileStream outFile = File.Create(origName))
        //        {
        //            using (GZipStream decompress = new GZipStream(inFile,
        //                    CompressionMode.Decompress))
        //            {
        //                //Copy the decompression stream into the output file.
        //                byte[] buffer = new byte[4096];
        //                int numRead;
        //                while ((numRead = decompress.Read(buffer, 0, buffer.Length)) != 0)
        //                {
        //                    outFile.Write(buffer, 0, numRead);
        //                }
        //            }
        //        }

        //        Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Decompress finished."));

        //        Restore sqlRestore = new Restore();

        //        BackupDeviceItem deviceItem = new BackupDeviceItem(origName, DeviceType.File);

        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Idea"].ConnectionString);
        //        conn.Open();
        //        string databaseName = conn.Database;
        //        ServerConnection connection = new ServerConnection(conn);
        //        Server sqlServer = new Server(connection);
        //        sqlRestore.Devices.Add(deviceItem);
        //        sqlRestore.Database = conn.Database;
        //        DataTable dt = sqlRestore.ReadBackupHeader(sqlServer);

        //        //Check db is for the same app version
        //        //Check db is for the same app version
        //        if (dt.Rows[0]["BackupDescription"] == DBNull.Value ||
        //            ((string)dt.Rows[0]["BackupDescription"] != AppVersion.Version.ToString(2) &&
        //            //this awful hardcoded line is to allow backups from Version 5.5.6 in version 6
        //                !((string)dt.Rows[0]["BackupDescription"] == "5.5" && AppVersion.Version.Major == 6)))
        //        {
        //            //invalid db
        //            sqlRestore.Abort();
        //            string alter2 = @"ALTER DATABASE [" + databaseName + "] SET Multi_User";
        //            SqlCommand alter2Cmd = new SqlCommand(alter2, conn);
        //            alter2Cmd.ExecuteNonQuery();
        //            return string.Format("Backups from {0} version are not supported.", dt.Rows[0]["BackupDescription"] == System.DBNull.Value ? "an older" : (string)dt.Rows[0]["BackupDescription"]);
        //        }

        //        const string UseMaster = "USE master";
        //        SqlCommand useMasterCommand = new SqlCommand(UseMaster, conn);
        //        useMasterCommand.ExecuteNonQuery();

        //        string alter1 = @"ALTER DATABASE [" + databaseName + "] SET Single_User WITH Rollback Immediate";
        //        SqlCommand alter1Cmd = new SqlCommand(alter1, conn);
        //        alter1Cmd.ExecuteNonQuery();

        //        Database db = sqlServer.Databases[databaseName];
        //        sqlRestore.Action = RestoreActionType.Database;
        //        String dataFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DATABASE\\" + databaseName + ".mdf";
        //        String logFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DATABASE\\" + databaseName + "_Log.ldf";

        //        sqlRestore.RelocateFiles.Add(new RelocateFile("IDEA", dataFileLocation));
        //        sqlRestore.RelocateFiles.Add(new RelocateFile("IDEA" + "_log", logFileLocation));
        //        sqlRestore.ReplaceDatabase = true;
        //        sqlRestore.PercentCompleteNotification = 10;
        //        try
        //        {
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Restoring"));
        //            sqlRestore.SqlRestore(sqlServer);
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Finished restoring"));
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //        finally
        //        {
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Executing SET Multi_User"));
        //            string alter2 = @"ALTER DATABASE [" + databaseName + "] SET Multi_User";
        //            SqlCommand alter2Cmd = new SqlCommand(alter2, conn);
        //            alter2Cmd.ExecuteNonQuery();
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "SET Multi_User executed"));
        //        }

        //        db = sqlServer.Databases[databaseName];
        //        db.SetOnline();
        //        sqlServer.Refresh();

        //        try
        //        {
        //            if (conn.State != ConnectionState.Closed)
        //            {
        //                conn.Close();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //        }

        //        try
        //        {
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Executing test SQL"));
        //            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Idea"].ConnectionString);
        //            const string SQLQueryTest = @"SELECT * FROM FACTOR";
        //            SqlCommand sqlQueryTestCmd = new SqlCommand(SQLQueryTest, conn);
        //            conn.Open();
        //            sqlQueryTestCmd.ExecuteNonQuery();
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Test SQL executed."));

        //            const string SQLAutoCloseOff = @"ALTER DATABASE idea SET AUTO_CLOSE OFF";
        //            sqlQueryTestCmd = new SqlCommand(SQLAutoCloseOff, conn);
        //            sqlQueryTestCmd.ExecuteNonQuery();

        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Auto_Close OFF executed."));

        //        }
        //        catch (Exception ex)
        //        {
        //            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Failed executing test SQL"));
        //        }
        //        finally
        //        {
        //            if (conn.State != ConnectionState.Closed)
        //            {
        //                conn.Close();
        //                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Connection closed"));
        //            }
        //        }
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Restore the Data Base of Idea
        /// </summary>
        /// <param name="databaseBackupFileName"></param>
        /// <returns></returns>
        private static string RestoreDatabase(string databaseBackupFileName)
        {
            Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Start restore"));

            Restore sqlRestore = new Restore();
            //  Restore - SqlDataBase;
            BackupDeviceItem deviceItem = new BackupDeviceItem(databaseBackupFileName, DeviceType.File);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Idea"].ConnectionString);
            conn.Open();
            string databaseName = conn.Database;
            ServerConnection connection = new ServerConnection(conn);
            Server sqlServer = new Server(connection);

            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.Database = conn.Database;

            DataTable dt = sqlRestore.ReadBackupHeader(sqlServer);

            //Check db is for the same app version
            if (dt.Rows[0]["BackupDescription"] == System.DBNull.Value || (dt.Rows[0]["BackupDescription"] != System.DBNull.Value && (string)dt.Rows[0]["BackupDescription"] != AppVersion.Version.ToString()))
            {
                //invalid db
                sqlRestore.Abort();
                string alter2 = @"ALTER DATABASE [" + databaseName + "] SET Multi_User";
                SqlCommand alter2Cmd = new SqlCommand(alter2, conn);
                alter2Cmd.ExecuteNonQuery();
                return string.Format("invaliddbversion");
            }


            const string useMaster = "USE master";
            SqlCommand useMasterCommand = new SqlCommand(useMaster, conn);
            useMasterCommand.ExecuteNonQuery();

            string alter1 = @"ALTER DATABASE [" + databaseName + "] SET Single_User WITH Rollback Immediate";
            SqlCommand alter1Cmd = new SqlCommand(alter1, conn);
            alter1Cmd.ExecuteNonQuery();

            Database db = sqlServer.Databases[databaseName];
            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DATABASE\\" + databaseName + ".mdf";
            String logFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DATABASE\\" + databaseName + "_Log.ldf";

            Trace.WriteLine(string.Format("IDEA.ERMT: dataFileLocation: {0}", dataFileLocation));
            Trace.WriteLine(string.Format("IDEA.ERMT: logFileLocation: {0}", logFileLocation));
            
            //sqlRestore.RelocateFiles.Add(new RelocateFile("IDEA", dataFileLocation));
            //sqlRestore.RelocateFiles.Add(new RelocateFile("IDEA_log", logFileLocation));
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.PercentCompleteNotification = 10;
            try
            {
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Restoring"));
                sqlRestore.SqlRestore(sqlServer);
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Restoring"));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("IDEA.ERMT: Exception restoring: {0}", ex.Message));
                return "errorrestoringdb";
            }
            finally
            {
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Executing SET Multi_User"));
                string alter3 = @"ALTER DATABASE [" + databaseName + "] SET Multi_User";
                SqlCommand alter3Cmd = new SqlCommand(alter3, conn);
                alter3Cmd.ExecuteNonQuery();
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "SET Multi_User executed"));
            }



            db = sqlServer.Databases[databaseName];
            db.SetOnline();
            sqlServer.Refresh();

            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception)
            {
            }

            try
            {
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Executing test SQL"));
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Idea"].ConnectionString);
                const string SQLQueryTest = @"SELECT * FROM FACTOR";
                SqlCommand sqlQueryTestCmd = new SqlCommand(SQLQueryTest, conn);
                conn.Open();
                sqlQueryTestCmd.ExecuteNonQuery();
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Test SQL executed."));

                const string SQLAutoCloseOff = @"ALTER DATABASE idea SET AUTO_CLOSE OFF";
                sqlQueryTestCmd = new SqlCommand(SQLAutoCloseOff, conn);
                sqlQueryTestCmd.ExecuteNonQuery();

                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Auto_Close OFF executed."));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Failed executing test SQL"));
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    Trace.WriteLine(string.Format("IDEA.ERMT: {0}", "Connection closed"));
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Restore the all files of Data Base of Idea
        /// </summary>
        /// <param name="backupFolder"></param>
        private static void RestoreFiles(string backupFolder, bool restoreFiles, bool restoreShapeFiles, ref string noCorrespondingRestorations)
        {
            string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;
            string documentsFolder = commonDataFolder + ConfigurationManager.AppSettings["DocumentFolder"];
            string rarDocumentsFolder = commonDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"];
            string iconsFolder = commonDataFolder + "icons";
            string shapefilesFolder = commonDataFolder + "shapefiles";

            if (restoreFiles)
            {
                //check if the backup has documents
                if (Directory.Exists(backupFolder + "\\documents"))
                {
                    Idea.Utils.FileSystem.CopyDirectory(backupFolder + "\\documents", documentsFolder, true);
                }
                else
                {
                    noCorrespondingRestorations = "Documents - ";
                }

                //check if the backup has rar documents
                if (Directory.Exists(backupFolder + "\\rarDocuments"))
                {
                    Idea.Utils.FileSystem.CopyDirectory(backupFolder + "\\rarDocuments", rarDocumentsFolder, true);
                }
                else
                {
                    noCorrespondingRestorations = noCorrespondingRestorations + "RAR Documents - ";
                }

                //check if the backup has icons
                if (Directory.Exists(backupFolder + "\\icons"))
                {
                    FileSystem.CopyDirectory(backupFolder + "\\icons", iconsFolder, true);
                }
                else
                {
                    noCorrespondingRestorations = noCorrespondingRestorations + "Icons - ";
                }
            }

            if (restoreShapeFiles)
            {
                //check if the backup has shapefiles
                if (Directory.Exists(backupFolder + "\\shapefiles"))
                {
                    FileSystem.CopyDirectory(backupFolder + "\\shapefiles", shapefilesFolder, true);
                }
                else
                {
                    noCorrespondingRestorations = noCorrespondingRestorations + "Shapefiles - ";
                }
            }

            if (File.Exists(backupFolder + "\\info.xml"))
            {
                File.Copy(backupFolder + "\\info.xml", commonDataFolder + "\\info.xml", true);
            }
        }
        #endregion

        #region BACKUP

        public static string BackupApplicationData(bool backupDB, bool backupFiles, bool backupShapeFiles)
        {
            //Create the database name
            string sqlBakName = string.Empty;
            if (backupDB)
            {
                sqlBakName = BackupDatabase();
            }
            else
            {
                string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;
                DirectoryInfo di = new DirectoryInfo(commonDataFolder);
                sqlBakName = di + "\\Idea-EC-DB-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak";
            }

            //Now, create the .gz file. It'll contain the DB .bak file and the files added within the application
            string fileName = CreateCompressedFile(sqlBakName, backupDB, backupFiles, backupShapeFiles);

            try
            {
                //Delete the .bak file.
                File.Delete(sqlBakName);
            }
            catch (Exception ex)
            {
            }

            return fileName;
        }

        private static string BackupDatabase()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Idea"].ConnectionString);
            ServerConnection connection = new ServerConnection(conn);

            //   Smo.Server smoServer = new Smo.Server(connection);
            Server sqlServer = new Server(connection);
            string databaseName = conn.Database;
            //Database db = sqlServer.Databases[databaseName];
            // Create a new backup operation
            Backup bkpDatabase = new Backup { Action = BackupActionType.Database, Database = databaseName };
            // Set the backup type to a database backup
            // Set the database that we want to perform a backup on
            string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;
            DirectoryInfo di = new DirectoryInfo(commonDataFolder);
            string name = di + "\\Idea-EC-DB-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak";
            // Set the backup device to a file
            BackupDeviceItem bkpDevice = new BackupDeviceItem(name, DeviceType.File);
            bkpDatabase.BackupSetDescription = AppVersion.Version.ToString();
            // Add the backup device to the backup
            bkpDatabase.Devices.Add(bkpDevice);
            // Perform the backup

            try
            {
                bkpDatabase.SqlBackup(sqlServer);
                //Compress backup
                //FileInfo fi = new FileInfo(name);
                //using (FileStream inFile = fi.OpenRead())
                //{
                //    using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                //    {
                //        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                //        {
                //             Copy the source file into the compression stream.
                //            byte[] buffer = new byte[4096];
                //            int numRead;
                //            while ((numRead = inFile.Read(buffer, 0, buffer.Length)) != 0)
                //            {
                //                Compress.Write(buffer, 0, numRead);
                //            }
                //        }
                //        return fi.FullName + ".gz";
                //    }
                //}
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }
        /// <summary>
        /// This method will create a single .gz file that contains every file that needs to be backed up.
        /// </summary>
        /// <param name="databaseBackupFileName"></param>
        /// <returns></returns>
        private static string CreateCompressedFile(string databaseBackupFileName)
        {
            FileInfo databaseBackupFile = new FileInfo(databaseBackupFileName);
            string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;

            DirectoryInfo di = new DirectoryInfo(commonDataFolder + "BackupAuxFolder\\");

            Idea.Utils.FileSystem.ClearFolder(di.FullName);

            //copy the database
            File.Copy(databaseBackupFile.FullName, di.FullName + "databaseBackup.bak");

            //copy the documents to the auxiliary folder.
            string[] documents = Directory.GetFiles(commonDataFolder + ConfigurationManager.AppSettings["DocumentFolder"]);
            if (documents.Length > 0)
            {
                di.CreateSubdirectory("documents");
                foreach (string document in documents)
                {
                    FileInfo fi = new FileInfo(document);
                    File.Copy(document, di.FullName + "\\documents\\" + fi.Name);
                }
            }


            //copy rar documents to the auxiliary folder.
            string[] rarDocuments = Directory.GetFiles(commonDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"]);
            if (rarDocuments.Length > 0)
            {
                di.CreateSubdirectory("rarDocuments");
                //copy the documents to the auxiliary folder.
                foreach (string rarDocument in rarDocuments)
                {
                    FileInfo fi = new FileInfo(rarDocument);
                    File.Copy(rarDocument, di.FullName + "\\rarDocuments\\" + fi.Name);
                }
            }

            //copy icons to the auxiliary folder.
            string[] icons = Directory.GetFiles(commonDataFolder + "\\icons");
            if (icons.Length > 0)
            {
                di.CreateSubdirectory("icons");
                //copy the documents to the auxiliary folder.
                foreach (string icon in icons)
                {
                    FileInfo fi = new FileInfo(icon);
                    File.Copy(icon, di.FullName + "\\icons\\" + fi.Name);
                }
            }


            Idea.Utils.GZip.CompressDirectory(di.FullName, databaseBackupFile.FullName + ".gz");

            return databaseBackupFile.FullName + ".gz";
        }

        /// <summary>
        /// This method will create a single .gz file that contains every file that needs to be backed up.
        /// </summary>
        /// <param name="databaseBackupFileName"></param>
        /// <returns></returns>
        private static string CreateCompressedFile(string databaseBackupFileName, bool backupDataBase, bool backupFiles, bool backupShapeFiles)
        {

            string commonDataFolder = DirectoryAndFileHelper.ServerAppDataFolder;

            FileInfo databaseBackupFile = new FileInfo(databaseBackupFileName);
            DirectoryInfo di = new DirectoryInfo(commonDataFolder + "BackupAuxFolder\\");

            Idea.Utils.FileSystem.ClearFolder(di.FullName);

            //copy the database
            if (backupDataBase)
            {
                File.Copy(databaseBackupFile.FullName, di.FullName + "databaseBackup.bak");
            }

            //copy the documents to the auxiliary folder.
            if (backupFiles)
            {
                if (Directory.Exists(commonDataFolder + ConfigurationManager.AppSettings["DocumentFolder"]))
                {
                    string[] documents = Directory.GetFiles(commonDataFolder + ConfigurationManager.AppSettings["DocumentFolder"]);
                    if (documents.Length > 0)
                    {
                        di.CreateSubdirectory("documents");
                        foreach (string document in documents)
                        {
                            FileInfo fi = new FileInfo(document);
                            File.Copy(document, di.FullName + "\\documents\\" + fi.Name);
                        }
                    }
                }

                if (Directory.Exists(commonDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"]))
                {
                    //copy rar documents to the auxiliary folder.
                    string[] rarDocuments = Directory.GetFiles(commonDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"]);
                    if (rarDocuments.Length > 0)
                    {
                        di.CreateSubdirectory("rarDocuments");
                        //copy the documents to the auxiliary folder.
                        foreach (string rarDocument in rarDocuments)
                        {
                            FileInfo fi = new FileInfo(rarDocument);
                            File.Copy(rarDocument, di.FullName + "\\rarDocuments\\" + fi.Name);
                        }
                    }
                }

                if (Directory.Exists(commonDataFolder + "\\icons"))
                {
                    //copy icons to the auxiliary folder.
                    string[] icons = Directory.GetFiles(commonDataFolder + "\\icons");
                    if (icons.Length > 0)
                    {
                        di.CreateSubdirectory("icons");
                        //copy the documents to the auxiliary folder.
                        foreach (string icon in icons)
                        {
                            FileInfo fi = new FileInfo(icon);
                            File.Copy(icon, di.FullName + "\\icons\\" + fi.Name);
                        }
                    }
                }
            }

            if (backupShapeFiles)
            {
                //copy shapefiles to the auxiliary folder.
                int shapeLevel = 0;
                bool lastShapeLevel = false;
                while (!lastShapeLevel)
                {
                    bool existsShapeLevel = Directory.Exists(commonDataFolder + "\\shapefiles\\" + shapeLevel.ToString());
                    if (existsShapeLevel)
                    {
                        string[] shapefiles = Directory.GetFiles(commonDataFolder + "\\shapefiles\\" + shapeLevel.ToString());
                        if (shapefiles.Length > 0)
                        {
                            di.CreateSubdirectory("shapefiles\\" + shapeLevel.ToString());
                            //copy the documents to the auxiliary folder.
                            foreach (string shapefile in shapefiles)
                            {
                                FileInfo fi = new FileInfo(shapefile);
                                File.Copy(shapefile, di.FullName + "\\shapefiles\\" + shapeLevel.ToString() + "\\" + fi.Name);
                            }
                        }
                        shapeLevel++;
                    }
                    else
                    {
                        lastShapeLevel = true;
                    }
                }
            }

            XmlDocument doc = CreateXMLInfoBackup(di.FullName, backupDataBase, backupFiles, backupShapeFiles);
            using (StreamWriter sw = File.CreateText(di.FullName + "\\info.xml"))
            {
                sw.Write(doc.OuterXml);
                sw.Flush();
                sw.Close();
            }

            Idea.Utils.GZip.CompressDirectory(di.FullName, databaseBackupFile.FullName.Replace(".bak", string.Empty) + ".gz");

            return databaseBackupFile.FullName.Replace(".bak", string.Empty) + ".gz";
        }

        private static XmlDocument CreateXMLInfoBackup(string directory, bool backupDataBase, bool backupFiles, bool backupShapeFiles)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<info/>");
            XmlNode node = doc.DocumentElement.AppendChild(doc.CreateElement("Version"));
            XmlAttribute at = node.Attributes.Append(doc.CreateAttribute("Number"));
            at.Value = version;
            node = doc.DocumentElement.AppendChild(doc.CreateElement("BackupFiles"));
            if (backupDataBase)
            {
                node.AppendChild(doc.CreateElement("DataBase"));
            }
            if (backupFiles)
            {
                node.AppendChild(doc.CreateElement("Files"));
            }
            if (backupShapeFiles)
            {
                node.AppendChild(doc.CreateElement("Shapefiles"));
            }

            return doc;
        }



        #endregion

        /// <summary>
        /// Generate the Model Index by modelId.
        /// </summary>
        /// <param name="modelId"></param>
        public static void GenerateModelIndex(int modelId, CultureInfo uiCulture)
        {
            StringBuilder stringBuilderInt = new StringBuilder();
            StringBuilder stringBuilderExt = new StringBuilder();
            
            Thread.CurrentThread.CurrentUICulture = uiCulture;

            List<ModelFactor> modelFactors = ModelFactorManager.GetByModel(modelId);
            foreach (Factor factor in FactorManager.GetAllWithHtmlDocument())
            {
                bool found = modelFactors.Any(f => f.IDFactor == factor.IdFactor);
                if (found)
                {
                    if (factor.InternalFactor)
                        stringBuilderInt.AppendLine(string.Format(IndexLinkTemplate, factor.IdFactor + ".htm", factor.Name));
                    else
                        stringBuilderExt.AppendLine(string.Format(IndexLinkTemplate, factor.IdFactor + ".htm", factor.Name));
                }
            }
            string location = DirectoryAndFileHelper.ServerAppDataFolder;

            String indexTemplate = DirectoryAndFileHelper.IndexTemplate;

            //switch (culture.Name.ToLower())
            //{
            //    case "es-es":
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplateSpanish;
            //            break;
            //        }
            //    case "fr":
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplateFrench;
            //            break;
            //        }
            //    default:
            //        {
            //            indexTemplate = DirectoryAndFileHelper.IndexTemplate;
            //            break;
            //        }
            //}

            using (StreamReader streamReader = new StreamReader(indexTemplate))
            {
                string template = streamReader.ReadToEnd();
                //factor data
                template = template.Replace("@InternalLinks@", stringBuilderInt.ToString());
                template = template.Replace("@ExternalLinks@", stringBuilderExt.ToString());

                //headers
                template = template.Replace("@KnowledgeResourcesHeader@", LanguageResourceManager.GetResourceText("KnowledgeResourcesHeader"));
                template = template.Replace("@InternalFactorsHeader@", LanguageResourceManager.GetResourceText("InternalFactorsHeader"));
                template = template.Replace("@ExternalFactorsHeader@", LanguageResourceManager.GetResourceText("ExternalFactorsHeader"));
                using (StreamWriter streamWriter2 = new StreamWriter(location + "\\" + ConfigurationManager.AppSettings["HTMLFolder"] + "\\IndexModel.htm", false))
                {
                    streamWriter2.Write(template);
                    streamWriter2.Flush();
                    streamWriter2.Close();
                }
                streamReader.Close();
            }
        }
    }
}