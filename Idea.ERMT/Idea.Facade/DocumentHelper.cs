using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;
using Idea.Entities;
using Idea.Facade.DocumentService;
using Idea.Utils;

namespace Idea.Facade
{
    public class DocumentHelper
    {
        public class HtmlLabel
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public string Html { get; set; }
        private const string ReplaceLabelTemplate = "<!-- {0} -->{1}<!-- /{0} -->";
        private const string ReplaceLabelTemplateRegex = @"\<\!-- {0} --\>{1}\<\!-- /{0} --\>";
        public const string IndexLinkTemplate = "<li><a href=\"./{0}\">{1}</a></li>";
        private List<HtmlLabel> ReplacedLabels = new List<HtmlLabel>();

        public DocumentHelper(string html)
        {
            Html = html;
            LoadReplacedLabels();
        }

        public static String SaveShapefile(FileInfo doc, Region region, ShapeFileERMTType shapeFileERMTType)
        {
            byte[] fileContent = File.ReadAllBytes(doc.FullName);
            Document document = new Document
            {
                Content = Convert.ToBase64String(fileContent),
                Filename = doc.Name,
                DocumentType = ERMTDocumentType.Shapefile
            };
            String fileName = GetService().SaveShapeFileToServer(document, region, shapeFileERMTType);

            string shapefileDirectory = DirectoryAndFileHelper.ClientShapefilesFolder + "\\" + region.RegionLevel + "\\";
            if (!Directory.Exists(shapefileDirectory))
            {
                Directory.CreateDirectory(shapefileDirectory);
            }

            File.Copy(doc.FullName, shapefileDirectory + fileName, true);

            return fileName;
        }

        private void LoadReplacedLabels()
        {
            foreach (Match m in Regex.Matches(Html, string.Format(ReplaceLabelTemplateRegex, "@.*@", ".*"), RegexOptions.None))
            {
                string key = Regex.Matches(m.Value, "@[\\w| ]*@", RegexOptions.None)[0].Value;
                string value = m.Value.Substring(m.Value.IndexOf("->", StringComparison.Ordinal) + 2, m.Value.IndexOf("<!-", m.Value.IndexOf("->", StringComparison.Ordinal), StringComparison.Ordinal) - m.Value.IndexOf("->", StringComparison.Ordinal) - 2);
                ReplacedLabels.Add(new HtmlLabel { Key = key, Value = value });
            }
        }

        /// <summary>
        /// Delete files by name(fileName) as a parameter
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public List<HtmlLabel> GetLabelsToReplace()
        {
            List<HtmlLabel> list = new List<HtmlLabel>();
            foreach (Match m in Regex.Matches(Html, "@[\\w| ]*@", RegexOptions.None))
            {
                bool wasAdded = false;
                foreach (HtmlLabel htmlLabel in list)
                {
                    if (htmlLabel.Key == m.Value)
                        wasAdded = true;
                }
                if (!wasAdded)
                {
                    string value = string.Empty;
                    foreach (HtmlLabel htmlLabel in ReplacedLabels)
                    {
                        if (htmlLabel.Key == m.Value)
                            value = htmlLabel.Value;
                    }
                    list.Add(new HtmlLabel { Key = m.Value, Value = value });
                }

            }
            return list;
        }

        public string ReplaceLabels(List<HtmlLabel> labelList)
        {
            foreach (HtmlLabel newLabel in labelList)
            {
                bool labelFound = false;
                HtmlLabel oldLabel = null;
                foreach (HtmlLabel pair in ReplacedLabels)
                {
                    if (pair.Key == newLabel.Key)
                    {
                        labelFound = true;
                        oldLabel = pair;
                    }
                }
                if (!labelFound)
                {
                    Html = Html.Replace(newLabel.Key, string.Format(ReplaceLabelTemplate, newLabel.Key, newLabel.Value));
                    ReplacedLabels.Add(newLabel);
                }
                else
                {
                    Html = Html.Replace(string.Format(ReplaceLabelTemplate, oldLabel.Key, oldLabel.Value), string.Format(ReplaceLabelTemplate, newLabel.Key, newLabel.Value));
                    ReplacedLabels.Remove(oldLabel);
                    ReplacedLabels.Add(newLabel);
                }
            }
            return Html;
        }
        private static IDocumentService _service;
        private static IDocumentService GetService()
        {
            if (_service == null)
            {
                _service = new DocumentServiceClient();
                Uri uri = new Uri(((ClientBase<IDocumentService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<IDocumentService>)_service).Endpoint.Address = new EndpointAddress(uri);

            }
            else
            {
                try
                {
                    ((ClientBase<IDocumentService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new DocumentServiceClient();
                    Uri uri = new Uri(((ClientBase<IDocumentService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<IDocumentService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<IDocumentService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<IDocumentService>)(_service)).Abort();
                    _service = new DocumentServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new DocumentServiceClient();
                    break;
            }

            return _service;

        }

        public static void DownloadFilesAsync()
        {
            ThreadStart ts = DownloadFiles;
            Thread thread = new Thread(ts);
            thread.Start();
        }

        public static void DownloadFiles()
        {
            // HTML Folder
            string folder = DirectoryAndFileHelper.ClientHTMLFolder;
            DeleteFilesFromDirectory(folder, "*.htm");

            // Documents
            folder = DirectoryAndFileHelper.ClientDocumentsFolder;
            DeleteFilesFromDirectory(folder, string.Empty);

            List<Document> documents = GetService().GetAll(Thread.CurrentThread.CurrentUICulture.Name);
            foreach (Document document in documents)
            {
                if (document.DocumentType == ERMTDocumentType.Document)
                {
                    folder = DirectoryAndFileHelper.ClientDocumentsFolder;
                }
                else if (document.DocumentType == ERMTDocumentType.Icon)
                {
                    folder = DirectoryAndFileHelper.ClientIconsFolder;
                }
                else
                {
                    folder = DirectoryAndFileHelper.ClientHTMLFolder;
                }

                try
                {
                    File.WriteAllBytes(folder + document.Filename, Convert.FromBase64String(document.Content));
                }
                catch (Exception ex)
                {//file being used.  
                }
            }

            folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["HTMLPMMFolder"];
            DeleteFilesFromDirectory(folder, "*.htm");

            // TODO: Santiago: This could be part of the GetAll, as a new parameter (is PMM), or just copy all to 
            // the same directory, but use different names. For now, Im duplicating a bit of functionality.
            documents = GetService().GetPMM(Thread.CurrentThread.CurrentUICulture.Name);
            foreach (Document document in documents)
            {
                if (document.DocumentType == ERMTDocumentType.Document)
                    folder = DirectoryAndFileHelper.ClientDocumentsFolder;
                else if (document.DocumentType == ERMTDocumentType.Document)
                    folder = DirectoryAndFileHelper.ClientIconsFolder;
                else
                    folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["HTMLPMMFolder"];
                try
                {
                    File.WriteAllBytes(folder + document.Filename, Convert.FromBase64String(document.Content));
                }
                catch (Exception ex)
                {//file being used.  
                }
            }
        }

        private static bool DeleteFilesFromDirectory(string directory, string searchPattern)
        {
            string[] files = (searchPattern != string.Empty
                                  ? Directory.GetFiles(directory, searchPattern)
                                  : Directory.GetFiles(directory));
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        // TODO: Santiago: Isnt this the same as DownloadFiles() (the rpevious function). If so, delete it.
        public static void DownloadFilesCurrentModel()
        {
            string folder = DirectoryAndFileHelper.ClientHTMLFolder;
            foreach (string file in Directory.GetFiles(folder, "*.htm"))
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
            List<Document> documents = GetService().GetCurrentModelFactors(ERMTSession.Instance.CurrentModel.IDModel, Thread.CurrentThread.CurrentUICulture.Name);
            foreach (Document document in documents)
            {
                if (document.DocumentType == ERMTDocumentType.Document)
                    folder = DirectoryAndFileHelper.ClientDocumentsFolder;
                else if (document.DocumentType == ERMTDocumentType.Icon)
                    folder = DirectoryAndFileHelper.ClientIconsFolder;
                else
                    folder = DirectoryAndFileHelper.ClientHTMLFolder;
                try
                {
                    File.WriteAllBytes(folder + document.Filename, Convert.FromBase64String(document.Content));
                }
                catch (Exception ex)
                {//file being used.  
                }
            }

            // HTML/PMM folder (Phases)
            // TODO: Santiago: This could actually be done with a function and call it twice, once for thml and another for htmlpmm,
            //  but for now we are duplicating it.
            folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["HTMLPMMFolder"];
            foreach (string file in Directory.GetFiles(folder, "*.htm"))
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }

            // TODO: Santiago: This could be part of the GetAll, as a new parameter (is PMM), or just copy all to 
            // the same directory, but use different names. For now, Im duplicating a bit of functionality.
            documents = GetService().GetPMM(Thread.CurrentThread.CurrentUICulture.Name);
            foreach (Document document in documents)
            {
                if (document.DocumentType == ERMTDocumentType.Document)
                    folder = DirectoryAndFileHelper.ClientDocumentsFolder;
                else if (document.DocumentType == ERMTDocumentType.Icon)
                    folder = DirectoryAndFileHelper.ClientIconsFolder;
                else
                    folder = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["HTMLPMMFolder"];
                try
                {
                    File.WriteAllBytes(folder + document.Filename, Convert.FromBase64String(document.Content));
                }
                catch (Exception ex)
                {//file being used.  
                }
            }
        }

        public static List<Document> GetIcons()
        {
            List<Document> icons = new List<Document>();
            List<Document> documents = GetService().GetAll(Thread.CurrentThread.CurrentCulture.Name);
            foreach (Document document in documents)
            {
                if (document.DocumentType != ERMTDocumentType.Icon) continue;
                document.Filename = Path.GetFileNameWithoutExtension(document.Filename);
                icons.Add(document);
            }
            return icons;
        }

        /// <summary>
        /// Returns a Document by factor (service).
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Document GetDocumentByFactor(Factor factor)
        {
            return GetService().GetDocumentByFactor((factor));
        }

        /// <summary>
        /// Saves a Document (service).
        /// </summary>
        /// <param name="document"></param>
        public static void Save(Document document)
        {
            GetService().Save(document);
        }

        /// <summary>
        /// Deletes a Document (service).
        /// </summary>
        /// <param name="document"></param>
        public static void Delete(Document document)
        {
            GetService().Delete(document);
        }

        /// <summary>
        /// Export a Document by idModel (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static Document Export(int idModel)
        {
            //TODO: implement EXPORT in the service
            return GetService().Export(idModel);
            //return GetService().GetAll().FirstOrDefault();
        }

        /// <summary>
        /// Import a Document by doc (service), else returns false.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<String> Import(Document doc)
        {
            return GetService().Import(doc);
        }

        /// <summary>
        /// Returns string of Backup.
        /// </summary>
        /// <returns></returns>
        public static string Backup(bool backupDB, bool backupFiles, bool backupShapeFiles)
        {
            Document d = GetService().BackupApplicationData(backupDB, backupFiles, backupShapeFiles);

            if (d.Filename == "Error")
                throw new Exception(d.Content);

            return d.Content;
        }

        /// <summary>`p
        /// Restore a Document (service).
        /// </summary>
        /// <param name="compressedFileContent"></param>
        public static string Restore(string compressedFileContent, bool restoreDB, bool restoreFiles, bool restoreShapeFiles)
        {
            Document doc = new Document { Content = compressedFileContent };
            return GetService().RestoreApplicationData(doc, restoreDB, restoreFiles, restoreShapeFiles);
        }

        /// <summary>
        /// Returns a DocumentTemplate (service).
        /// </summary>
        /// <returns></returns>
        public static Document GetDocumentTemplate()
        {
            return GetService().GetDocumentTemplate();
        }

        /// <summary>
        /// Returns the shapefile and related DBF ans SHX files.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Boolean GetRegionShapefilesFromServer(Region region)
        {
            List<Document> aux = GetService().GetRegionShapefilesFromServer(region);
            if (aux.Count == 0)
            {
                return false;
            }

            foreach (Document document in aux)
            {
                String fileName = DirectoryAndFileHelper.ClientShapefilesFolder + region.RegionLevel + "\\" + document.Filename;
                File.WriteAllBytes(fileName, Convert.FromBase64String(document.Content));
            }
            return true;
        }
    }
}
