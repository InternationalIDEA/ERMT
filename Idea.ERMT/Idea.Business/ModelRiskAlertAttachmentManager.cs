using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;
using System;
using System.IO;
using Idea.Utils;
using System.Configuration;
using System.Text;

namespace Idea.Business
{
    public class ModelRiskAlertAttachmentManager
    {
        /// <summary>
        /// Returns a new ModelRiskAlertAttachment.
        /// </summary>
        /// <returns></returns>
        public static ModelRiskAlertAttachment GetNew()
        {
            return new ModelRiskAlertAttachment();
        }

        /// <summary>
        /// Returns the list of ModelRiskAlertAttachment by idModelRiskAlert.
        /// </summary>
        /// <param name="idModelRiskAlert"></param>
        /// <returns></returns>
        public static List<ModelRiskAlertAttachment> GetByModelRiskAlertID(int idModelRiskAlert)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return
                    context.ModelRiskAlertAttachments.Where(mraa => mraa.IDModelRiskAlert == idModelRiskAlert).ToList();
            }
        }

        /// <summary>
        /// Saves the ModelRiskAlertAttachment.
        /// </summary>
        /// <param name="modelRiskAlertAttachment"></param>
        /// <returns></returns>
        public static ModelRiskAlertAttachment Save(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.ModelRiskAlertAttachments.AddOrUpdate(modelRiskAlertAttachment);
                context.SaveChanges();
                //Document attachmentDocument = new Document();

               // attachmentDocument.Content = Convert.ToBase64String(Encoding.ASCII.GetBytes(modelRiskAlertAttachment.Content));
               
                string path = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"];
                string fileName = modelRiskAlertAttachment.IDModelRiskAlertAttachment + "-" + modelRiskAlertAttachment.AttachmentFile;
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //attachmentDocument.Filename = Path.GetFileName(path + fileName);
                //attachmentDocument.DocumentType = ERMTDocumentType.Document;



                DocumentManager.Save(path + fileName, Convert.FromBase64String(modelRiskAlertAttachment.Content));

                return modelRiskAlertAttachment;
            }
        }

        /// <summary>
        /// Delete the ModelRiskAlertAttachment.
        /// </summary>
        /// <param name="modelRiskAlertAttachment"></param>
        public static void Delete(ModelRiskAlertAttachment modelRiskAlertAttachment)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                ModelRiskAlertAttachment mraa =
                    context.ModelRiskAlertAttachments.FirstOrDefault(
                        mraa2 => mraa2.IDModelRiskAlertAttachment == modelRiskAlertAttachment.IDModelRiskAlertAttachment);
                context.ModelRiskAlertAttachments.Remove(mraa);
                context.SaveChanges();
            }
        }
    }
}
