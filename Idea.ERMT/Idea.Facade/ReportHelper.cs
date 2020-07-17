using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Idea.Entities;

namespace Idea.Facade
{
    public class ReportHelper
    {
        private static ReportService.IReportService _service;
        private static ReportService.IReportService GetService()
        {
            if (_service == null)
            {
                _service = new ReportService.ReportServiceClient();
                Uri uri = new Uri(((ClientBase<ReportService.IReportService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                ((ClientBase<ReportService.IReportService>)_service).Endpoint.Address = new EndpointAddress(uri);
            }
            else
            {
                try
                {
                    ((ClientBase<ReportService.IReportService>)(_service)).Close();
                }
                catch
                { }
                finally
                {
                    _service = new ReportService.ReportServiceClient();
                    Uri uri = new Uri(((ClientBase<ReportService.IReportService>)(_service)).Endpoint.Address.Uri.ToString().Replace("localhost", ERMTSession.Instance.ServerAddress));
                    ((ClientBase<ReportService.IReportService>)_service).Endpoint.Address = new EndpointAddress(uri);
                }

            }
            switch (((ClientBase<ReportService.IReportService>)(_service)).State)
            {
                case CommunicationState.Faulted:
                    ((ClientBase<ReportService.IReportService>)(_service)).Abort();
                    _service = new ReportService.ReportServiceClient();
                    break;
                case CommunicationState.Closed:
                case CommunicationState.Closing:
                    _service = new ReportService.ReportServiceClient();
                    break;
            }
            return _service;
        }

        /// <summary>
        /// Returns a new Report (service).
        /// </summary>
        /// <returns></returns>
        public static Report GetNew()
        {
            return new Report();
        }

        /// <summary>
        /// Returns the list of all Reports (service).
        /// </summary>
        /// <returns></returns>
        public static List<Report> GetAll()
        {
            return (GetService().GetAll()).ToList();
        }

        /// <summary>
        /// Returns the list of Reports by model name (service).
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Report> GetByModel(int model)
        {
            return (GetService().GetByModel(model)).ToList();
        }

        /// <summary>
        /// Returns a Report by idReport (service).
        /// </summary>
        /// <param name="idReport"></param>
        /// <returns></returns>
        public static Report Get(int idReport)
        {
            return (GetService().Get(idReport));
        }

        /// <summary>
        /// Saves the Report (service).
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static Report Save(Report report)
        {
            return (GetService().Save((report)));
        }

        /// <summary>
        ///  Deletes the Report (service).
        /// </summary>
        /// <param name="report"></param>
        public static void Delete (Report report)
        {
            GetService().Delete((report));
        }

        /// <summary>
        /// Returns the list of Reports by name (service).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Report> GetByName(string name)
        {
            return (GetService().GetByName(name)).ToList();
        }

        /// <summary>
        /// Returns the list of Reports by name and type (service).
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Report> GetByNameAndType(string name, int type)
        {
            return (GetService().GetByNameAndType(name, type)).ToList();
        }
    }
}
