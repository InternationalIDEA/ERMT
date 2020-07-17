using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Idea.Business;
using System;
using System.Globalization;
using System.Linq;
using Idea.Entities;
using System.Xml;
using System.Diagnostics;
using Idea.Utils;

namespace Idea.Server
{
    public class DocumentService : IDocumentService
    {
        private bool newVersion = true;

        public List<Document> GetAll(String culture)
        {
            List<Document> documents = new List<Document>();

            FactorManager.GenerateAllFactorsHTML(culture);

            documents.AddRange(GetPMM(culture));

            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerHTMLFolder), documents, ERMTDocumentType.HTML);
            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerDocumentsFolder), documents, ERMTDocumentType.Document);
            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerIconsFolder), documents, ERMTDocumentType.Icon);

            return documents;
        }

        //public List<Document> GetPMM()
        //{
        //    return GetPMM("en");
        //}

        public List<Document> GetPMM(String culture)
        {
            List<Document> documents = new List<Document>();
            //Generate non existing files
            foreach (Phase phase in PhaseManager.GetAll())
            {
                //this saves only if the object has changed, if it has not, won't save, but will 
                //check if the html file exists and will update it
                PhaseManager.GenerateAllFiles(phase, culture);
            }
            CultureInfo cultureInfo = new CultureInfo(culture);
            DocumentManager.GenerateIndex(cultureInfo);

            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HTMLPMMFolder"]), documents, ERMTDocumentType.HTML);

            return documents;
        }

        public String SaveShapeFileToServer(Document doc, Region region, ShapeFileERMTType shapeFileERMTType)
        {
            string shapefilesFolder = DirectoryAndFileHelper.ServerShapefilesFolder + "\\" + region.RegionLevel + "\\";

            FileInfo file = new FileInfo(shapefilesFolder + doc.Filename);
            String fileExtension = file.Extension;
            switch (shapeFileERMTType)
            {
                case ShapeFileERMTType.Path:
                    {
                        shapefilesFolder = DirectoryAndFileHelper.ServerShapefilesFolder + "\\";
                        file = new FileInfo(shapefilesFolder + region.ShapeFileName.ToLower().Replace(".shp", "PATH" + fileExtension));
                        break;
                    }
                case ShapeFileERMTType.POI:
                    {
                        shapefilesFolder = DirectoryAndFileHelper.ServerShapefilesFolder + "\\";
                        file = new FileInfo(shapefilesFolder + region.ShapeFileName.ToLower().Replace(".shp", "POI" + fileExtension));
                        break;
                    }
            }

            DocumentManager.Save(file, Convert.FromBase64String(doc.Content));
            return file.Name;
        }

        public List<Document> GetCurrentModelFactors(int modelId, String culture)
        {
            List<Document> documents = new List<Document>();
            //Generate non existing files 
            //foreach (Factor f in FactorManager.GetAll())
            //{
            //    //this saves only if the object has changed, if it has not, won't save, but will 
            //    //check if the html file exists and will update it
            //    FactorManager.Save(f);
            //}
            FactorManager.GenerateAllFactorsHTML(culture);
            CultureInfo cultureInfo = new CultureInfo(culture);

            DocumentManager.GenerateModelIndex(modelId, cultureInfo);
            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerHTMLFolder), documents, ERMTDocumentType.HTML);
            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerDocumentsFolder), documents, ERMTDocumentType.Document);
            GetDocuments(Directory.GetFiles(DirectoryAndFileHelper.ServerIconsFolder), documents, ERMTDocumentType.Icon);

            return documents;
        }

        public Document GetDocumentByFactor(Factor factor)
        {
            Factor deserializedFactor = (factor);
            Document document = new Document();
            string file;
            if (!string.IsNullOrEmpty(deserializedFactor.HtmlDocument))
                file = DirectoryAndFileHelper.ServerHTMLFolder + deserializedFactor.HtmlDocument;
            else
                file = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HtmlTemplate"];
            if (!File.Exists(file))
                file = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HtmlTemplate"];
            using (StreamReader streamReader = new StreamReader(file))
            {
                document.Content = streamReader.ReadToEnd();
                streamReader.Close();
            }
            document.DocumentType = ERMTDocumentType.HTML;
            document.Filename = file.Split('\\')[file.Split('\\').Length - 1];

            return document;
        }

        public Document GetDocumentTemplate()
        {
            Document document = new Document();
            string file;
            file = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HtmlTemplate"];
            using (StreamReader streamReader = new StreamReader(file))
            {
                document.Content = streamReader.ReadToEnd();
                streamReader.Close();
            }
            document.DocumentType = ERMTDocumentType.HTML;
            document.Filename = file.Split('\\')[file.Split('\\').Length - 1];

            return document;
        }

        public void Save(Document document)
        {
            string folder = DirectoryAndFileHelper.ServerHTMLFolder;
            if (document.DocumentType == ERMTDocumentType.Document)
            {
                folder = DirectoryAndFileHelper.ServerDocumentsFolder;
                DocumentManager.Save(folder + Path.GetFileName(document.Filename), Convert.FromBase64String(document.Content));
            }
            else if (document.DocumentType == ERMTDocumentType.Icon)
            {
                folder = DirectoryAndFileHelper.ServerIconsFolder;
                DocumentManager.Save(folder + Path.GetFileName(document.Filename), Convert.FromBase64String(document.Content));
            }
            else DocumentManager.Save(folder + Path.GetFileName(document.Filename), document.Content);
        }

        public void Delete(Document document)
        {
            if (document.DocumentType == ERMTDocumentType.Document)
            {
                string folder = DirectoryAndFileHelper.ServerDocumentsFolder;
                DocumentManager.Delete(folder + Path.GetFileName(document.Filename));
            }
        }

        private void GetDocuments(string[] files, List<Document> documents, ERMTDocumentType documentType)
        {
            foreach (string s in files)
            {
                byte[] datos = File.ReadAllBytes(s);
                documents.Add(new Document() { Filename = s.Split('\\')[s.Split('\\').Length - 1], DocumentType = documentType, Content = Convert.ToBase64String(datos) });
            }
        }

        public Document Export(int idModel)
        {
            Document result = new Document();
            Model m = ModelManager.Get(idModel);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<data/>");

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            XmlNode versionlNode = doc.DocumentElement.AppendChild(doc.CreateElement("Version"));
            versionlNode.Attributes.Append(doc.CreateAttribute("Number")).Value = version;

            if (doc.DocumentElement != null)
            {
                XmlNode modelNode = doc.DocumentElement.AppendChild(doc.CreateElement("Model"));

                if (modelNode.Attributes != null)
                {
                    XmlAttribute at = modelNode.Attributes.Append(doc.CreateAttribute("Name"));
                    at.Value = m.Name;
                    at = modelNode.Attributes.Append(doc.CreateAttribute("Description"));
                    at.Value = m.Description;
                    at = modelNode.Attributes.Append(doc.CreateAttribute("IDRegion"));
                    at.Value = m.IDRegion.ToString();
                    at = modelNode.Attributes.Append(doc.CreateAttribute("ShapeFileName"));

                    List<ModelFactor> modelFactorList = ModelFactorManager.GetByModel(m.IDModel);
                    foreach (ModelFactor mf in modelFactorList)
                    {
                        XmlNode factorNode = modelNode.AppendChild(doc.CreateElement("Factor"));
                        if (factorNode.Attributes != null)
                        {
                            Factor factor = FactorManager.GetById(mf.IDFactor);
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Name"));
                            at.Value = factor.Name;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Description"));
                            at.Value = factor.Description;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("ScaleMax"));
                            at.Value = mf.ScaleMax.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("ScaleMin"));
                            at.Value = mf.ScaleMin.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Interval"));
                            at.Value = mf.Interval.ToString("0.0").Replace(",", ".");
                            at = factorNode.Attributes.Append(doc.CreateAttribute("InternalFactor"));
                            at.Value = factor.InternalFactor.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Questionnaire"));
                            at.Value = factor.Questionnaire;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Introduction"));
                            at.Value = factor.Introduction;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("EmpiricalCases"));
                            at.Value = factor.EmpiricalCases;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("DataCollection"));
                            at.Value = factor.DataCollection;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("ObservableIndicators"));
                            at.Value = factor.ObservableIndicators;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Cumulative"));
                            at.Value = factor.CumulativeFactor.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Orden"));
                            at.Value = mf.SortOrder.GetValueOrDefault(0).ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Weight"));
                            at.Value = mf.Weight.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("Color"));
                            at.Value = factor.Color ?? string.Empty;
                            at = factorNode.Attributes.Append(doc.CreateAttribute("ID"));
                            at.Value = mf.IDModelFactor.ToString();
                            at = factorNode.Attributes.Append(doc.CreateAttribute("IDFactor"));
                            at.Value = mf.IDFactor.ToString();
                            foreach (ModelFactorData mfd in ModelFactorDataManager.GetByModelFactor(mf.IDModelFactor))
                            {
                                XmlNode mfdnode = modelNode.AppendChild(doc.CreateElement("ModelData"));
                                if (mfdnode.Attributes != null)
                                {
                                    at = mfdnode.Attributes.Append(doc.CreateAttribute("IDRegion"));
                                    at.Value = mfd.IDRegion.ToString();
                                    at = mfdnode.Attributes.Append(doc.CreateAttribute("IDModelFactor"));
                                    at.Value = mfd.IDModelFactor.ToString();
                                    at = mfdnode.Attributes.Append(doc.CreateAttribute("Data"));
                                    at.Value = mfd.Data.ToString("#0.00").Replace(",", ".");
                                    at = mfdnode.Attributes.Append(doc.CreateAttribute("Date"));
                                }
                                at.Value = mfd.Date.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    XmlNode region = modelNode.AppendChild(doc.CreateElement("Region"));
                    if (region.Attributes != null)
                    {
                        Region firstRegion = RegionManager.Get(m.IDRegion);
                        at = region.Attributes.Append(doc.CreateAttribute("Name"));
                        at.Value = firstRegion.RegionName;
                        at = region.Attributes.Append(doc.CreateAttribute("Description"));
                        at.Value = firstRegion.RegionDescription;
                        at = region.Attributes.Append(doc.CreateAttribute("ID"));
                        at.Value = firstRegion.IDRegion.ToString();
                        at = region.Attributes.Append(doc.CreateAttribute("Parent"));
                        at.Value = firstRegion.IDParent.ToString();
                        at = region.Attributes.Append(doc.CreateAttribute("ShapeFileName"));
                        at.Value = firstRegion.ShapeFileName;
                        at = region.Attributes.Append(doc.CreateAttribute("ShapeFileIndex"));
                        at.Value = firstRegion.ShapeFileIndex.ToString();
                        at = region.Attributes.Append(doc.CreateAttribute("RegionLevel"));
                        at.Value = firstRegion.RegionLevel.ToString();

                        AppendChildRegions(ref region, firstRegion, doc);

                        List<Marker> Markers = MarkerManager.GetByModelId(m.IDModel);
                        List<int> markerTypesList = (from a in Markers select a.IDMarkerType).Distinct().ToList();
                        List<Marker> markers = MarkerManager.GetByModelId(m.IDModel);
                        foreach (int idMarkerType in markerTypesList)
                        {
                            MarkerType markerType = MarkerTypeManager.Get(idMarkerType);
                            XmlNode markerTypeNode = modelNode.AppendChild(doc.CreateElement("MarkerType"));
                            if (markerTypeNode.Attributes != null)
                            {
                                at = markerTypeNode.Attributes.Append(doc.CreateAttribute("ID"));
                                at.Value = markerType.IDMarkerType.ToString();
                                at = markerTypeNode.Attributes.Append(doc.CreateAttribute("Name"));
                                at.Value = markerType.Name.ToString();
                                at = markerTypeNode.Attributes.Append(doc.CreateAttribute("Symbol"));
                                at.Value = markerType.Symbol.ToString();
                                at = markerTypeNode.Attributes.Append(doc.CreateAttribute("Size"));
                                at.Value = markerType.Size.ToString();

                                foreach (Marker marker in markers)
                                {
                                    if (marker.IDMarkerType != idMarkerType || marker.IDModel != m.IDModel) continue;
                                    XmlNode markerNode = modelNode.AppendChild(doc.CreateElement("Marker"));
                                    if (markerNode.Attributes != null)
                                    {
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("ID"));
                                        at.Value = marker.IDMarker.ToString();
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("IDMarkerType"));
                                        at.Value = marker.IDMarkerType.ToString();
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("IDModel"));
                                        at.Value = marker.IDModel.ToString();
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("Name"));
                                        at.Value = marker.Name;
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("Latitude"));
                                        at.Value = marker.Latitude.ToString().Replace(",", ".");
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("Longitude"));
                                        at.Value = marker.Longitude.ToString().Replace(",", ".");
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("DateFrom"));
                                        at.Value = marker.DateFrom.ToString("yyyy-MM-dd");
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("DateTo"));
                                        at.Value = marker.DateTo.ToString("yyyy-MM-dd");
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("Color"));
                                        at.Value = marker.Color;
                                        at = markerNode.Attributes.Append(doc.CreateAttribute("Description"));
                                    }
                                    at.Value = marker.Description;
                                }
                            }
                        }

                        List<ModelRiskAlert> modelRiskAlertList = ModelRiskAlertManager.GetByModelID(idModel);

                        foreach (ModelRiskAlert mra in modelRiskAlertList)
                        {
                            XmlNode modelRiskAlert = modelNode.AppendChild(doc.CreateElement("ModelRiskAlert"));
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("IDModel"));
                            at.Value = mra.IDModel.ToString();
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("Code"));
                            at.Value = mra.Code;
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("Title"));
                            at.Value = mra.Title;
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("DateFrom"));
                            at.Value = mra.DateFrom.ToString();
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("DateTo"));
                            at.Value = mra.DateTo.ToString();
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("RiskDescription"));
                            at.Value = mra.RiskDescription;
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("Action"));
                            at.Value = mra.Action;
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("Result"));
                            at.Value = mra.Result;
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("Active"));
                            at.Value = mra.Active.ToString();
                            at = modelRiskAlert.Attributes.Append(doc.CreateAttribute("deleted"));
                            at.Value = mra.Deleted;

                            List<ModelRiskAlertAttachment> modelRiskAlertAttachmentList = ModelRiskAlertAttachmentManager.GetByModelRiskAlertID(mra.IDModelRiskAlert);

                            foreach (ModelRiskAlertAttachment mraa in modelRiskAlertAttachmentList)
                            {
                                XmlNode modelRiskAlertAttachment = modelRiskAlert.AppendChild(doc.CreateElement("modelRiskAlertAttachment"));
                                at = modelRiskAlertAttachment.Attributes.Append(doc.CreateAttribute("AttachmentFile"));
                                at.Value = mraa.AttachmentFile;
                                at = modelRiskAlertAttachment.Attributes.Append(doc.CreateAttribute("Content"));
                                at.Value = mraa.Content;
                            }

                            List<ModelRiskAlertPhase> modelRiskAlertFaseList = ModelRiskAlertPhaseManager.GetByModelRiskAlertID(mra.IDModelRiskAlert);

                            foreach (ModelRiskAlertPhase mraf in modelRiskAlertFaseList)
                            {
                                XmlNode modelRiskAlertFase = modelRiskAlert.AppendChild(doc.CreateElement("modelRiskAlertFase"));
                                at = modelRiskAlertFase.Attributes.Append(doc.CreateAttribute("IDFase"));
                                at.Value = mraf.IDPhase.ToString();
                                at = modelRiskAlertFase.Attributes.Append(doc.CreateAttribute("deleted"));
                                at.Value = mra.Deleted;
                            }

                            List<ModelRiskAlertRegion> modelRiskAlertRegionList = ModelRiskAlertRegionManager.GetByModelRiskAlertID(mra.IDModelRiskAlert);

                            foreach (ModelRiskAlertRegion mrar in modelRiskAlertRegionList)
                            {
                                XmlNode modelRiskAlertRegion = modelRiskAlert.AppendChild(doc.CreateElement("modelRiskAlertRegion"));
                                at = modelRiskAlertRegion.Attributes.Append(doc.CreateAttribute("IDRegion"));
                                at.Value = mrar.IDRegion.ToString();
                                at = modelRiskAlertRegion.Attributes.Append(doc.CreateAttribute("deleted"));
                                at.Value = mrar.Deleted.ToString();
                            }
                        }
                    }
                }
            }



            result.Content = doc.OuterXml;
            result.Filename = m.Name.Replace(" ", "-") + ".xml";
            return result;
        }

        public List<Document> GetRegionShapefilesFromServer(Region region)
        {
            String shapefileName = DirectoryAndFileHelper.ServerShapefilesFolder + region.ShapeFileName;
            String dbfFileName = Path.GetDirectoryName(shapefileName) + "\\" + Path.GetFileNameWithoutExtension(shapefileName) + ".dbf";
            String shxFileName = Path.GetDirectoryName(shapefileName) + "\\" + Path.GetFileNameWithoutExtension(shapefileName) + ".shx";
            List<Document> auxDocuments = new List<Document>();
            if (File.Exists(shapefileName))
            {
                Document shapefile = new Document();

                shapefile.Content = Convert.ToBase64String(File.ReadAllBytes(shapefileName));
                shapefile.Filename = Path.GetFileName(shapefileName);
                shapefile.DocumentType = ERMTDocumentType.Shapefile;
                auxDocuments.Add(shapefile);
            }


            if (File.Exists(dbfFileName))
            {
                Document dfbFile = new Document();

                dfbFile.Content = Convert.ToBase64String(File.ReadAllBytes(dbfFileName));
                dfbFile.Filename = Path.GetFileName(dbfFileName);
                dfbFile.DocumentType = ERMTDocumentType.Shapefile;
                auxDocuments.Add(dfbFile);
            }

            if (File.Exists(shxFileName))
            {
                Document shxFile = new Document();

                shxFile.Content = Convert.ToBase64String(File.ReadAllBytes(shxFileName));
                shxFile.Filename = Path.GetFileName(shxFileName);
                shxFile.DocumentType = ERMTDocumentType.Shapefile;
                auxDocuments.Add(shxFile);
            }

            return auxDocuments;
        }

        private void AppendChildRegions(ref XmlNode regionNode, Region region, XmlDocument doc)
        {
            List<Region> childRegions = RegionManager.GetAllChilds(region.IDRegion);
            foreach (Region r in childRegions)
            {
                XmlNode aux = regionNode.AppendChild(doc.CreateElement("Region"));
                XmlAttribute at = aux.Attributes.Append(doc.CreateAttribute("Name"));
                at.Value = r.RegionName;
                at = aux.Attributes.Append(doc.CreateAttribute("Description"));
                at.Value = r.RegionDescription;
                at = aux.Attributes.Append(doc.CreateAttribute("ID"));
                at.Value = r.IDRegion.ToString();
                at = aux.Attributes.Append(doc.CreateAttribute("ShapeFileName"));
                at.Value = r.ShapeFileName;
                at = aux.Attributes.Append(doc.CreateAttribute("ShapeFileIndex"));
                at.Value = r.ShapeFileIndex.ToString();
                at = aux.Attributes.Append(doc.CreateAttribute("RegionLevel"));
                at.Value = r.RegionLevel.ToString();

                AppendChildRegions(ref aux, r, doc);
            }
        }

        public List<String> Import(Document modelDoc)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(modelDoc.Content);

                List<String> auxReturn = new List<string>();

                XmlNode modelNode = doc.SelectSingleNode("/data/Model");
                bool exist = true;
                int i = 1;
                string name = modelNode.Attributes["Name"].Value;
                while (exist)
                {
                    List<Model> modelList = ModelManager.GetByName(name);
                    if (modelList.Count > 0)
                    {
                        name = modelNode.Attributes["Name"].Value + " (" + i + ")";
                        i++;
                    }
                    else
                        exist = false;
                }

                Model model = new Model { Name = name, Description = modelNode.Attributes["Description"].Value };

                XmlNode firstRegionNode = doc.SelectSingleNode("/data/Model/Region");
                int idParentRegion = int.Parse(firstRegionNode.Attributes["Parent"].Value);

                List<KeyValuePair<int, int>> importedAndLocalRegionIDList = new List<KeyValuePair<int, int>>();
                List<Region> regionsNotImported = new List<Region>();

                bool isVersionGreaterThan6 = false;
                int importVersion = 0;
                Boolean isParentWorldOrContinent = IsWorldOrContinent(idParentRegion);
                Boolean parentFound = false;

                XmlNode versionNode = doc.SelectSingleNode("data/Version");
                if (versionNode != null)
                {
                    importVersion = int.Parse(versionNode.Attributes["Number"].Value.Substring(0, 1));
                }
                if (importVersion > 6)
                {
                    isVersionGreaterThan6 = true;
                }

                if (!isVersionGreaterThan6 && !isParentWorldOrContinent)
                {
                    auxReturn.Add("false");
                    auxReturn.Add("ModelImportErrorVersionUsesDundasAndWrongParent");
                    return auxReturn;
                }


                Region firstRegion = new Region();
                firstRegion.IDRegion = int.Parse(firstRegionNode.Attributes["ID"].Value);
                firstRegion.RegionName = firstRegionNode.Attributes["Name"].Value;
                firstRegion.RegionDescription = firstRegionNode.Attributes["Description"].Value;
                if (isVersionGreaterThan6)
                {
                    firstRegion.ShapeFileName = firstRegionNode.Attributes["ShapeFileName"].Value;
                    if (firstRegionNode.Attributes["ShapeFileIndex"] != null 
                        && firstRegionNode.Attributes["ShapeFileIndex"].Value != string.Empty)
                    {
                        firstRegion.ShapeFileIndex = int.Parse(firstRegionNode.Attributes["ShapeFileIndex"].Value);
                    }
                }

                //so, now we need to find the parent. If the parent is a continent or the world, then it's easy, the IDs for those regions remain the same between versions of ERMT.
                if (isParentWorldOrContinent)
                {
                    firstRegion.IDParent = idParentRegion;
                    parentFound = true;
                }
                else
                {
                    //parent is not a continent or the world. Try to find the parent in the local DB.
                    //first, let's check if the parent has the same ID in the local DB.
                    Region probableParent = RegionManager.Get(idParentRegion);
                    if (probableParent != null)
                    {
                        //we have a region with the IDRegion in our local DB.
                        Region probableParentChild = RegionManager.GetAllChilds(idParentRegion).FirstOrDefault(r => r.RegionName.ToLower() == firstRegion.RegionName.ToLower());

                        if (probableParentChild != null)
                        {
                            //so, we have a Region with the same ID as the parent, and a child with the same same as our main region in the XML. BINGO.
                            firstRegion.IDParent = idParentRegion;
                            parentFound = true;
                        }
                    }
                    else
                    {
                        //couldn't find the parent by it's id, try to use it's level and child.
                        int parentRegionLevel = -1;
                        if (int.TryParse(firstRegionNode.Attributes["ShapeFileName"].Value[0].ToString(),
                            out parentRegionLevel))
                        {
                            parentRegionLevel--;
                            //we have the model's main region name and it's parent level. So, try to find the parent by level and child's name.
                            List<Region> allRegionsInParentLevel = RegionManager.GetAllByRegionLevel(parentRegionLevel);

                            foreach (Region region in allRegionsInParentLevel)
                            {
                                //we have a region with the IDRegion in our local DB.
                                Region probableParentChild = RegionManager.GetAllChilds(region.IDRegion).FirstOrDefault(r => r.RegionName.ToLower() == firstRegion.RegionName.ToLower());

                                if (probableParentChild != null)
                                {
                                    //so, we have a Region with the same ID as the parent, and a child with the same same as our main region in the XML. BINGO.
                                    idParentRegion = region.IDRegion;
                                    firstRegion.IDParent = idParentRegion;
                                    parentFound = true;
                                }
                            }
                        }
                    }

                }

                if (!parentFound && isVersionGreaterThan6)
                {
                    //we still have no parent and the XML should include shapefileindex and shapefilename. 
                    //We can try to find the region and it's parent by shapefilename and shapefileindex.
                    Region localMainRegion = RegionManager.GetRegionByShapeFileAndIndex(firstRegion.ShapeFileName,firstRegion.ShapeFileIndex);
                    if (localMainRegion != null)
                    {
                        firstRegion.IDParent = localMainRegion.IDParent;
                        parentFound = true;
                    }
                }

                if (!parentFound)
                {
                    auxReturn.Add("false");
                    auxReturn.Add("ModelImportNoExistsParentRegionInLocalDB");
                    return auxReturn;
                }

                //Resta verificar si corresponde cargar shape file o no.
                List<Region> importedRegions = new List<Region> {firstRegion};

                AddChildRegions(firstRegionNode, ref importedRegions, isVersionGreaterThan6);

                Region localParentRegion = RegionManager.Get(idParentRegion);
                Region localModelRegion = RegionManager.GetRegionByIDParentAndName(idParentRegion, firstRegion.RegionName);

                if (localModelRegion == null && isVersionGreaterThan6)
                {
                    //try to get it by shapefilename and index.
                    localModelRegion = RegionManager.GetRegionByShapeFileAndIndex(firstRegion.ShapeFileName,
                        firstRegion.ShapeFileIndex);
                }

                if (localModelRegion == null)
                {
                    auxReturn.Add("false");
                    auxReturn.Add("ModelImportMainRegionDoesNotExits");
                    return auxReturn;
                }

                List<Region> localRegionList = new List<Region> { localModelRegion };
                localRegionList.AddRange(RegionManager.GetAllChilds(localModelRegion.IDRegion).OrderBy(r => r.RegionLevel).ToList());

                model.IDRegion = localModelRegion.IDRegion;

                GetRegionsNotImported(importedRegions, localRegionList, ref regionsNotImported, ref importedAndLocalRegionIDList,isVersionGreaterThan6);

                if (localRegionList.Count == 0 || regionsNotImported.Count == importedRegions.Count)
                {
                    auxReturn.Add("false");
                    auxReturn.Add("ModelImportNoExistsRegionsInLocalDB");
                    return auxReturn;
                }

                model = ModelManager.Save(model);

                XmlNodeList factors = doc.GetElementsByTagName("Factor");
                if (factors != null)
                {
                    foreach (XmlNode factorNode in factors)
                    {
                        Factor factor = FactorManager.GetByName(factorNode.Attributes["Name"].Value);
                        //Suponemos que el factor existe en la base de datos.
                        if (factor == null)
                        {
                            factor = new Factor();
                            factor.Name = factorNode.Attributes["Name"].Value;
                            factor.Description = factorNode.Attributes["Description"].Value;
                            factor.InternalFactor = bool.Parse(factorNode.Attributes["InternalFactor"].Value);
                            factor.ScaleMax = int.Parse(factorNode.Attributes["ScaleMax"].Value);
                            factor.ScaleMin = int.Parse(factorNode.Attributes["ScaleMin"].Value);
                            factor.Interval = decimal.Parse(factorNode.Attributes["Interval"].Value.Replace(",", "."), new CultureInfo("en-US").NumberFormat);
                            if (factor.Interval == 0) factor.Interval = 1; //To avoid interface errors
                            factor.Questionnaire = factorNode.Attributes["Description"].Value; ;
                            factor.Introduction = factorNode.Attributes["Introduction"].Value;
                            factor.EmpiricalCases = factorNode.Attributes["EmpiricalCases"].Value;
                            factor.DataCollection = factorNode.Attributes["DataCollection"].Value;
                            factor.ObservableIndicators = factorNode.Attributes["ObservableIndicators"].Value;
                            factor.CumulativeFactor = bool.Parse(factorNode.Attributes["Cumulative"].Value);
                            factor.Color = factorNode.Attributes["Color"].Value;

                            if (factor.Name.Length > 500)
                            {
                                factor.Name = factor.Name.Substring(0, 500);
                            }
                            FactorManager.Save(factor);
                        }
                        //Agregamos el model Factor
                        ModelFactor modelfactor = ModelFactorManager.GetNew();
                        modelfactor.IDFactor = factor.IdFactor;
                        modelfactor.IDModel = model.IDModel;
                        modelfactor.ScaleMax = int.Parse(factorNode.Attributes["ScaleMax"].Value);
                        modelfactor.ScaleMin = int.Parse(factorNode.Attributes["ScaleMin"].Value);
                        modelfactor.Interval = decimal.Parse(factorNode.Attributes["Interval"].Value.Replace(",", "."), new CultureInfo("en-US").NumberFormat);
                        modelfactor.SortOrder = int.Parse(factorNode.Attributes["Orden"].Value);
                        modelfactor.Weight = int.Parse(factorNode.Attributes["Weight"].Value);

                        ModelFactorManager.Save(modelfactor);


                        //Agregamos los datos del modelo...
                        foreach (XmlNode mfdNode in modelNode.SelectNodes("ModelData[@IDModelFactor=" + factorNode.Attributes["ID"].Value + "]"))
                        {
                            ModelFactorData mfd = ModelFactorDataManager.GetNew();
                            mfd.Data = decimal.Parse(mfdNode.Attributes["Data"].Value.Replace(",", "."), new CultureInfo("en-US").NumberFormat);
                            mfd.Date = DateTime.Parse(mfdNode.Attributes["Date"].Value);
                            //A que region pertenece?
                            //Obtengo en el xml la region con ese id, y luego busco el match en la lista de regiones que importe a la base de datos.
                            XmlNode regionAux = modelNode.SelectSingleNode("descendant::Region[@ID=" + mfdNode.Attributes["IDRegion"].Value + "]");
                            if (regionAux != null) //No importamos datos para regiones que no existen-
                            {
                                int currentRegionID = int.Parse(regionAux.Attributes["ID"].Value);
                                int localRegionID = GetLocalRegionIDFromImportedRegionID(importedAndLocalRegionIDList, currentRegionID);
                                if (localRegionID != 0)
                                {
                                    mfd.IDModelFactor = modelfactor.IDModelFactor;
                                    mfd.IDRegion = localRegionID;
                                    ModelFactorDataManager.Save(mfd);
                                }
                                //mfd.IDRegion = GetRegionID(regionAux, region);
                                //modelfactor.ModelFactorDatas.Add(mfd);
                            }
                        }
                        //model.ModelFactors.Add(modelfactor);

                    }
                }



                bool isNew;
                XmlNodeList markersTypes = doc.GetElementsByTagName("MarkerType");
                if (markersTypes != null)
                {
                    foreach (XmlNode markerTypeNode in markersTypes)
                    {
                        isNew = false;
                        MarkerType markerType = MarkerTypeManager.Get(int.Parse(markerTypeNode.Attributes["ID"].Value));
                        XmlNodeList markerList = doc.SelectNodes("/data/Model/Marker[@IDMarkerType=" + markerTypeNode.Attributes["ID"].Value + "]");
                        if (markerType == null || markerType.Name.ToLower() != markerTypeNode.Attributes["Name"].Value.ToLower() || markerType.Symbol.ToLower() != markerTypeNode.Attributes["Symbol"].Value.ToLower() || markerType.Size.ToLower() != markerTypeNode.Attributes["Size"].Value.ToLower())
                        {
                            isNew = true;
                            markerType = new MarkerType
                            {
                                Name = markerTypeNode.Attributes["Name"].Value,
                                Symbol = markerTypeNode.Attributes["Symbol"].Value,
                                Size = markerTypeNode.Attributes["Size"].Value
                            };
                            MarkerTypeManager.Save(markerType);
                        }

                        if (markerList != null)
                        {
                            foreach (XmlNode m in markerList)
                            {
                                Marker marker = new Marker();
                                if (!isNew)
                                {
                                    if (m.Attributes["IDMarkerType"].Value != markerType.IDMarkerType.ToString()) continue;
                                    markerType = MarkerTypeManager.Get(int.Parse(m.Attributes["IDMarkerType"].Value));
                                }
                                marker.IDMarkerType = markerType.IDMarkerType;
                                marker.IDModel = model.IDModel;//int.Parse(m.Attributes["IDModel"].Value);
                                marker.Name = m.Attributes["Name"].Value;
                                marker.Latitude = decimal.Parse(m.Attributes["Latitude"].Value.Replace(",", "."), new CultureInfo("en-US").NumberFormat);
                                marker.Longitude = decimal.Parse(m.Attributes["Longitude"].Value.Replace(",", "."), new CultureInfo("en-US").NumberFormat);
                                marker.DateFrom = DateTime.Parse(m.Attributes["DateFrom"].Value);
                                marker.DateTo = DateTime.Parse(m.Attributes["DateTo"].Value);
                                marker.Color = m.Attributes["Color"].Value;
                                marker.Description = m.Attributes["Description"].Value;
                                MarkerManager.Save(marker);
                            }
                        }
                    }
                }

                XmlNodeList modelRiskAlertList = doc.GetElementsByTagName("ModelRiskAlert");
                if (modelRiskAlertList != null)
                {
                    foreach (XmlNode mra in modelRiskAlertList)
                    {
                        ModelRiskAlert modelRiskAlert = new ModelRiskAlert();
                        modelRiskAlert.IDModel = model.IDModel;
                        modelRiskAlert.Code = mra.Attributes["Code"].Value;
                        modelRiskAlert.Title = mra.Attributes["Title"].Value;
                        modelRiskAlert.DateFrom = DateTime.Parse(mra.Attributes["DateFrom"].Value);
                        modelRiskAlert.DateTo = DateTime.Parse(mra.Attributes["DateTo"].Value);
                        modelRiskAlert.RiskDescription = mra.Attributes["RiskDescription"].Value;
                        modelRiskAlert.Action = mra.Attributes["Action"].Value;
                        modelRiskAlert.Result = mra.Attributes["Result"].Value;
                        modelRiskAlert.Active = bool.Parse(mra.Attributes["Active"].Value);

                        ModelRiskAlertManager.Save(modelRiskAlert);

                        XmlNodeList modelRiskAlertAttachmentList = mra.SelectNodes("modelRiskAlertAttachment");

                        foreach (XmlNode mraa in modelRiskAlertAttachmentList)
                        {
                            ModelRiskAlertAttachment modelRiskAlertAttachment = ModelRiskAlertAttachmentManager.GetNew();
                            modelRiskAlertAttachment.IDModelRiskAlert = modelRiskAlert.IDModelRiskAlert;
                            modelRiskAlertAttachment.AttachmentFile = mraa.Attributes["AttachmentFile"].Value;
                            modelRiskAlertAttachment.Content = mraa.Attributes["Content"].Value;

                            ModelRiskAlertAttachmentManager.Save(modelRiskAlertAttachment);
                        }

                        XmlNodeList modelRiskAlertFaseList = mra.SelectNodes("modelRiskAlertFase");

                        foreach (XmlNode mraf in modelRiskAlertFaseList)
                        {
                            ModelRiskAlertPhase modelRiskAlertPhase = new ModelRiskAlertPhase
                            {
                                IDModelRiskAlert = modelRiskAlert.IDModelRiskAlert,
                                IDPhase = int.Parse(mraf.Attributes["IDFase"].Value),
                                Deleted = mraf.Attributes["deleted"].Value == string.Empty ? false : bool.Parse(mraf.Attributes["deleted"].Value)
                            };

                            ModelRiskAlertPhaseManager.Save(modelRiskAlertPhase);
                        }

                        //XmlNodeList modelRiskAlertRegionList = mra.SelectNodes("modelRiskAlertRegion");

                        //foreach (XmlNode mrar in modelRiskAlertRegionList)
                        //{
                        //    ModelRiskAlertRegion modelRiskAlertRegion = ModelRiskAlertRegionManager.GetNew();
                        //    modelRiskAlertRegion.IDModelRiskAlert = modelRiskAlert.IDModelRiskAlert;
                        //    modelRiskAlertRegion.IDRegion = int.Parse(mrar.Attributes["IDRegion"].Value);
                        //    modelRiskAlertRegion.Deleted = bool.Parse(mrar.Attributes["deleted"].Value);

                        //    ((Idea.Backend.ModelRiskAlertRegion)modelRiskAlertRegion).Save();
                        //}
                    }
                }

                if (regionsNotImported.Count > 0)
                {
                    string regionNames = string.Empty;
                    foreach (Region r in regionsNotImported)
                    {
                        regionNames = regionNames + r.RegionName + " - ";
                    }
                    regionNames = regionNames.Substring(0, regionNames.Length - 3);

                    auxReturn.Add("SomeRegionsWereNotImported");
                    auxReturn.Add(regionNames);
                }
                auxReturn.Insert(0, "true");
                return auxReturn;
            }
            catch (Exception ex)
            {
                return new List<string> { "false" };
            }
        }

        private int GetLocalRegionIDFromImportedRegionID(List<KeyValuePair<int, int>> importedAndLocalRegionIDList, int currentRegionID)
        {
            foreach (KeyValuePair<int, int> ir in importedAndLocalRegionIDList)
            {
                if (ir.Key != currentRegionID) continue;
                return ir.Value;
            }

            return 0;
        }

        private void GetRegionsNotImported(List<Region> importedRegions, List<Region> localRegionList, ref List<Region> regionsNotImported, ref List<KeyValuePair<int, int>> importedAndLocalRegionIDList, Boolean isVersionGreaterThan6)
        {
            foreach (Region ir in importedRegions)
            {
                if (!IsRegionInLocalDatabase(ir, localRegionList, ref importedAndLocalRegionIDList, isVersionGreaterThan6))
                {
                    regionsNotImported.Add(ir);
                }
            }
        }

        private bool IsRegionInLocalDatabase(Region importedRegion, List<Region> localRegionList, ref List<KeyValuePair<int, int>> importedAndLocalRegionIDList, Boolean isVersionGreaterThan6)
        {
            foreach (Region localRegion in localRegionList)
            {
                Boolean found = false;
                if (isVersionGreaterThan6)
                {
                    //try to get the region by shapfilename and index.
                    if (importedRegion.ShapeFileName.ToLower() == localRegion.ShapeFileName.ToLower()
                        && importedRegion.ShapeFileIndex == localRegion.ShapeFileIndex)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    //try to find the local region by name.
                    if (importedRegion.RegionName.ToLower() == localRegion.RegionName.ToLower())
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    continue;
                }

                if (importedRegion.RegionName.ToLower() != localRegion.RegionName.ToLower())
                {
                    //means that the imported region's name was changed. We need to update the local region's name.
                    localRegion.RegionName = importedRegion.RegionName;
                    RegionManager.Save(localRegion);
                }

                importedAndLocalRegionIDList.Add(new KeyValuePair<int, int>(importedRegion.IDRegion, localRegion.IDRegion));
                return true;
            }
            return false;
        }

        private bool IsWorldOrContinent(int idParentRegion)
        {
            Region parentRegion = RegionManager.Get(idParentRegion);
            if (parentRegion != null && (parentRegion.RegionLevel == 0 || parentRegion.RegionLevel == 1))
            {
                return true;
            }
            return false;
        }

        //private int GetRegionID(XmlNode regionAux, IRegion region)
        //{
        //    if (regionAux.SelectSingleNode("Shape").OuterXml == region.ShapeData)
        //        return region.ID;
        //    else
        //    {
        //        int aux = -1;
        //        foreach (IRegion r in region.Regions)
        //        {
        //            aux = GetRegionID(regionAux, r);
        //            if (aux != -1)
        //                break;
        //        }
        //        return aux;
        //    }
        //}

        private void AddChildRegions(XmlNode firstRegionNode, ref List<Region> importedRegions, bool isVersionGreater6)
        {
            foreach (XmlNode regionNode in firstRegionNode.SelectNodes("Region"))
            {
                Region region = new Region();
                region.IDRegion = int.Parse(regionNode.Attributes["ID"].Value);
                region.RegionName = regionNode.Attributes["Name"].Value;
                region.RegionDescription = regionNode.Attributes["Description"].Value;
                if (isVersionGreater6)
                {
                    region.ShapeFileName = regionNode.Attributes["ShapeFileName"].Value;
                    if (regionNode.Attributes["ShapeFileIndex"] != null && !String.IsNullOrEmpty(regionNode.Attributes["ShapeFileIndex"].Value))
                    {
                        region.ShapeFileIndex = int.Parse(regionNode.Attributes["ShapeFileIndex"].Value);
                    }
                }

                importedRegions.Add(region);
                AddChildRegions(regionNode, ref importedRegions, isVersionGreater6);
                //region.Regions.Add(region2);
            }
        }

        public Document BackupApplicationData(bool backupDB, bool backupFiles, bool backupShapeFiles)
        {
            try
            {
                Document backup = new Document();
                string filename = DocumentManager.BackupApplicationData(backupDB, backupFiles, backupShapeFiles);

                //FileStream fs = File.OpenRead(filename);
                backup.Content = Convert.ToBase64String(File.ReadAllBytes(filename));
                //backup.Content = Convert.ToBase64String(fs.);
                backup.Filename = filename;
                backup.DocumentType = ERMTDocumentType.Backup;
                //File.
                try
                {
                    //Cleanup the mess =D
                    File.Delete(filename);
                }
                catch (Exception ex)
                {
                }

                return backup;
            }
            catch (Exception ex)
            {
                return new Document() { Filename = "Error", Content = ex.Message };
            }

        }

        public string RestoreApplicationData(Document backup, bool restoreDB, bool restoreFiles, bool restoreShapeFiles)
        {
            return DocumentManager.RestoreApplicationData(backup.Content, restoreDB, restoreFiles, restoreShapeFiles);
        }
    }
}
