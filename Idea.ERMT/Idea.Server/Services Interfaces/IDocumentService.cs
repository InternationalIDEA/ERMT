using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using Idea.Entities;


namespace Idea.Server
{
    [ServiceContract]
    public interface IDocumentService
    {
        [OperationContract]
        List<Document> GetAll(String culture);

        [OperationContract]
        List<Document> GetCurrentModelFactors(int modelId, String culture);

        [OperationContract]
        Document GetDocumentByFactor(Factor factor);

        [OperationContract]
        void Save(Document document);

        [OperationContract]
        void Delete(Document document);

        [OperationContract]
        Document Export(int idModel);

        [OperationContract]
        List<string> Import(Document model);

        [OperationContract]
        Document BackupApplicationData(bool backupDB, bool backupFiles, bool backupShapeFiles);

        [OperationContract]
        string RestoreApplicationData(Document doc, bool restoreDB, bool restoreFiles, bool restoreShapeFiles);

        [OperationContract]
        Document GetDocumentTemplate();

        [OperationContract]
        List<Document> GetPMM(String culture);

        [OperationContract]
        String SaveShapeFileToServer(Document doc, Region region, ShapeFileERMTType shapeFileERMTType);

        [OperationContract]
        List<Document> GetRegionShapefilesFromServer(Region region);
    }
}
