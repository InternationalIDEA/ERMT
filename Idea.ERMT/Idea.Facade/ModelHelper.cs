using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;
using Idea.Utils;

namespace Idea.Facade
{
    public class ModelHelper
    {
        private static ModelService.IModelService _service;
        private static ModelService.IModelService GetService()
        {
            if (_service == null)
            {
                _service = new ModelService.ModelServiceClient();
                Uri uri = new Uri(((ClientBase<ModelService.IModelService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<ModelService.IModelService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<ModelService.IModelService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new ModelService.ModelServiceClient();
                    Uri uri = new Uri(((ClientBase<ModelService.IModelService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<ModelService.IModelService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }

            switch (((ClientBase<ModelService.IModelService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<ModelService.IModelService>)(_service)).Abort();
                    _service = new ModelService.ModelServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new ModelService.ModelServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new Model (service).
        /// </summary>
        /// <returns></returns>
        public static Model GetNew()
        {
            return new Model();
        }

        /// <summary>
        /// Returns the Model by id (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        public static Model GetModel(int idModel)
        {
            return (GetService().Get(idModel));
        }

        /// <summary>
        /// Returns the list of Model by region (service).
        /// </summary>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public static List<Model> GetByRegion(int idRegion)
        {
            return (GetService().GetByRegion(idRegion)).ToList();
        }

        /// <summary>
        /// Returns the list of models by regions.
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public static List<Model> GetByRegions(List<Region> regions)
        {
            return (GetService().GetByRegions(regions)).ToList();
        }

        /// <summary>
        /// Returns the list of all Model (service).
        /// </summary>
        /// <returns></returns>
        public static List<Model> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }

        /// <summary>
        /// Saves the Model (service).
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Model Save(Model model)
        {
            return (GetService().Save((model)));
        }

        /// <summary>
        /// Make a validation for Model (service).
        /// </summary>
        /// <param name="model"></param>
        public static Boolean Validate(Model model)
        {
            Boolean aux = true;
            {
                if (model.Name == string.Empty)
                {
                    aux = false;
                }
            }
            return aux;
        }

        /// <summary>
        /// Deletes the Model (service).
        /// </summary>
        /// <param name="model"></param>
        public static void Delete(Model model)
        {
            GetService().Delete((model));
        }

        /// <summary>
        /// Returns the list of Model by name (service).
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static List<Model> GetByName(string modelName)
        {
            return (GetService().GetByName(modelName)).ToList();
        }

        /// <summary>
        /// Saves the shapefiles related to the model to the destination folder.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="destinationFolder"></param>
        public static void SaveAllShapefilesInModel(Model model, DirectoryInfo destinationFolder)
        {
            List<Region> regionList = new List<Region>();
            List<string> shapeFileNameList = new List<string>();
            Region firstRegion = RegionHelper.Get(model.IDRegion);
            regionList.Add(firstRegion);
            regionList.AddRange(RegionHelper.GetAllChilds(firstRegion.IDRegion));

            foreach (Region region in regionList)
            {
                if (shapeFileNameList.Contains(region.ShapeFileName)) continue;
                shapeFileNameList.Add(region.ShapeFileName);
            }

            if (shapeFileNameList.Count == 0) return;

            if (!Directory.Exists(destinationFolder.FullName))
            {
                Directory.CreateDirectory(destinationFolder.FullName);
            }

            foreach (string shapeFileName in shapeFileNameList)
            {
                if (!File.Exists(DirectoryAndFileHelper.ClientShapefilesFolder + shapeFileName))
                {
                    continue;
                }

                int regionLevelInt = -1;

                if (!int.TryParse(shapeFileName.Substring(0, 1), out regionLevelInt))
                {
                    continue;
                }

                RegionLevel regionLevel = RegionHelper.GetRegionLevelFromNumber(regionLevelInt);

                if (!Directory.Exists(destinationFolder.FullName + "\\" + regionLevelInt + "-" + regionLevel))
                {
                    Directory.CreateDirectory(destinationFolder.FullName + "\\" + regionLevelInt + "-" + regionLevel);
                }

                FileInfo auxFileInfo = new FileInfo(DirectoryAndFileHelper.ClientShapefilesFolder + shapeFileName);
                //shp
                if (File.Exists(auxFileInfo.FullName))
                {
                    File.Copy(auxFileInfo.FullName, destinationFolder.FullName + "\\" + regionLevelInt + "-" + regionLevel + "\\" + auxFileInfo.Name, true);
                }

                //shx
                string shxFileName = shapeFileName.ToLower().Replace(".shp", ".shx");
                auxFileInfo = new FileInfo(DirectoryAndFileHelper.ClientShapefilesFolder + shxFileName);
                if (File.Exists(auxFileInfo.FullName))
                {
                    File.Copy(auxFileInfo.FullName, destinationFolder.FullName + "\\" + regionLevelInt + "-" + regionLevel + "\\" + auxFileInfo.Name, true);
                }

                //dbf
                string dbfFileName = shapeFileName.ToLower().Replace(".shp", ".dbf");
                auxFileInfo = new FileInfo(DirectoryAndFileHelper.ClientShapefilesFolder + dbfFileName);
                if (File.Exists(auxFileInfo.FullName))
                {
                    File.Copy(auxFileInfo.FullName, destinationFolder.FullName + "\\" + regionLevelInt + "-" + regionLevel + "\\" + auxFileInfo.Name, true);
                }
                
            }
        }
    }
}
