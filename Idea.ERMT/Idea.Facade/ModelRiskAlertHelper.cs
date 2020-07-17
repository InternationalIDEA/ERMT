using System.Collections.Generic;
using System.Linq;
using Idea.Entities;
using System;
using System.ServiceModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using Idea.Utils;

namespace Idea.Facade
{
    public class ModelRiskAlertHelper
    {
        private static ModelRiskAlertService.IModelRiskAlertService _service;
        private static ModelRiskAlertService.IModelRiskAlertService GetService()
        {
            if (_service == null)
            {
                _service = new ModelRiskAlertService.ModelRiskAlertServiceClient();
                Uri uri = new Uri(((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new ModelRiskAlertService.ModelRiskAlertServiceClient();
                    Uri uri = new Uri(((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<ModelRiskAlertService.IModelRiskAlertService>)(_service)).Abort();
                    _service = new ModelRiskAlertService.ModelRiskAlertServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new ModelRiskAlertService.ModelRiskAlertServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new ModelRiskAlert (service).
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlert GetNew()
        {
            return new ModelRiskAlert();
        }

        /// <summary>
        /// Returns the list of all ModelRiskAlert (service).
        /// </summary>
        /// <returns></returns>
        public static List<ModelRiskAlert> GetAll()
        {
            return GetService().GetAll().ToList();
        }

        public static ModelRiskAlert Get(int idModelRiskAlert)
        {
            return GetService().Get(idModelRiskAlert);
        }

        /// <summary>
        /// Returns the list of ModelRiskAlert by idModel, regions and isActive (service).
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="regions"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static List<ModelRiskAlert> GetWithFilter(int? idModel, List<int> regions, bool? isActive)
        {
            return GetService().GetWithFilter(idModel, regions, isActive);
        }

        /// <summary>
        /// Make a validation for ModelRiskAlert
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        /// <returns></returns>
        public static bool Validate(ModelRiskAlert modelRiskAlert)
        {
            // TODO: Santiago: MAke a validation for ModelRiskAlert
            /*
            if (string.IsNullOrEmpty(modelRiskAlert.Title))
                throw new ArgumentException("ModelRiskAlert title cannot be null or empty.");
             * */

            return true;
        }

        public static List<Region> GetAllRegionsWithAlerts()
        {
            return GetService().GetAllRegionsWithAlerts();
        }

        /// <summary>
        /// Saves the ModelRiskAlert (service).
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        public static ModelRiskAlert Save(ModelRiskAlert modelRiskAlert)
        {
            return GetService().Save(modelRiskAlert);
        }

        /// <summary>
        /// Saves the ModelRiskAlertPhase (service).
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        public static void SavePhase(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            GetService().SavePhase(modelRiskAlertPhase);
        }

        /// <summary>
        /// Saves the ModelRiskAlertRegion (service).
        /// </summary>
        /// <param name="modelRiskAlert"></param>
        public static void SaveRegion(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            GetService().SaveRegion(modelRiskAlertRegion);
        }

        /// <summary>
        /// Saves the ModelRiskAlertAttachment (service).
        /// </summary>
        /// <param name="modelRiskAlertAttachment"></param>
        public static void SaveAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            GetService().SaveAttachment(modelRiskAlertAttachment);
        }

        /// <summary>
        /// Returns a new ModelRiskAlertAttachment (service).
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlertAttachment GetNewModelRiskAlertAttachment()
        {
            return GetService().GetNewModelRiskAlertAttachment();
        }

        /// <summary>
        /// Returns a new ModelRiskAlertPhase (service).
        /// </summary>
        /// <returns></returns>
        //public static ModelRiskAlertPhase GetNewModelRiskAlertFase()
        //{
        //    return GetService().GetNewModelRiskAlertFase();
        //}

        /// <summary>
        ///  Returns a new ModelRiskAlertRegion (service).
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlertRegion GetNewModelRiskAlertRegion()
        {
            return GetService().GetNewModelRiskAlertRegion();
        }


        // Generates the file with the title and text and file name provided
        public static void GenerateRARHtml(string title, string text, string fileName)
        {
            string file = DirectoryAndFileHelper.ClientAppDataFolder + ConfigurationManager.AppSettings["RARTemplate"];
            string content;
            using (StreamReader streamReader = new StreamReader(file))
            {
                content = streamReader.ReadToEnd();
                streamReader.Close();
            }
            // SAves the template with the values, but replace template by the ID:
            //Use folders where the user has writing permissions!!!!!!
            DocumentSave(DirectoryAndFileHelper.ClientAppDataFolder + fileName, FormatTemplate(content, title, text));
        }

        public static string GetClientLocation()
        {
            string location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return location;
        }

        /// <summary>
        /// Formtas the template to save.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string FormatTemplate(string html, string title, string text)
        {
            string aux = html.Replace("@Title@", title);
            
            aux = aux.Replace("@Text@", text);
            aux = aux.Replace("@RiskAlertSearch@", ResourceHelper.GetResourceText("RiskAlertSearch"));
            aux = aux.Replace("@@", ResourceHelper.GetResourceText(""));
            aux = aux.Replace("@@", ResourceHelper.GetResourceText(""));
            aux = aux.Replace("@@", ResourceHelper.GetResourceText(""));
            aux = aux.Replace("@@", ResourceHelper.GetResourceText(""));
            aux = aux.Replace("@@", ResourceHelper.GetResourceText(""));

            return aux;
        }

        /// <summary>
        ///  Saves the file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="html"></param>
        private static void DocumentSave(string path, string html)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write(html.Replace("contentEditable=true", "contentEditable=false"));
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        public static List<Region> GetRegions(ModelRiskAlert modelRiskAlert)
        {
            return GetService().GetRegions(modelRiskAlert);
        }

        public static List<ModelRiskAlertRegion> GetModelRiskAlertRegions(ModelRiskAlert modelRiskAlert)
        {
            return GetService().GetModelRiskAlertRegions(modelRiskAlert);
        }

        public static List<ModelRiskAlertPhase> GetPhases(ModelRiskAlert modelRiskAlert)
        {
            return GetService().GetPhases(modelRiskAlert);
        }
        public static string GetRARHtmlGridFile()
        {
            return ConfigurationManager.AppSettings["RARHtmlGridFile"];
        }

        public static string GetRARHtmlBulletsFile()
        {
            return ConfigurationManager.AppSettings["RARHtmlBulletsFile"];
        }

        public static string GetRARHtmlAlarmFile()
        {
            return ConfigurationManager.AppSettings["RARHtmlAlarmFile"];
        }

        public static void Delete(ModelRiskAlert alert)
        {
            GetService().Delete(alert);
        }

        public static List<ModelRiskAlertAttachment> GetModelRiskAlertAttachments(ModelRiskAlert modelRiskAlert)
        {
            return GetService().GetModelRiskAlertAttachments(modelRiskAlert);
        }

        public static void DeletePhase(ModelRiskAlertPhase modelRiskAlertPhase)
        {
            GetService().DeletePhase(modelRiskAlertPhase);
        }

        public static void DeleteRegion(ModelRiskAlertRegion modelRiskAlertRegion)
        {
            GetService().DeleteRegion(modelRiskAlertRegion);
        }

        public static void DeleteAttachment(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            GetService().DeleteAttachment(modelRiskAlertAttachment);
        }
    }
}
