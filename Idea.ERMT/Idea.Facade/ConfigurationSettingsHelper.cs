using System;
using System.Xml;
using Idea.Entities;

namespace Idea.Facade
{
    public static class ConfigurationSettingsHelper
    {
        private const string NodePath = "/config/endpoint";

        public static void SetInstanceEndpointAddress()
        {
            ERMTSession.Instance.ServerAddress = GetEndpointAddress();
        }

        public static string GetEndpointAddress()
        {
            XmlNode node = LoadConfigDocument().SelectSingleNode(NodePath);

            return (node != null) ? node.Attributes["address"].Value : "localhost";
        }

        public static Boolean TestServer()
        {
            try
            {
                Region region = RegionHelper.GetWorld();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SaveEndpointAddress(string endpointAddress)
        {
            // load config document for current assembly
            XmlDocument doc = LoadConfigDocument();

            // retrieve appSettings node
            XmlNodeList nodes = doc.SelectNodes(NodePath);

            if (nodes.Count == 0)
            {
                XmlElement node = doc.CreateElement("endpoint");
                XmlAttribute att = doc.CreateAttribute("address");
                att.Value = endpointAddress;
                node.Attributes.Append(att);
                doc.DocumentElement.AppendChild(node);
                doc.Save(Utils.DirectoryAndFileHelper.ConfigFilePath);
            }
            else
            {
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        // select the 'add' element that contains the key
                        //XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                        node.Attributes["address"].Value = endpointAddress;
                        doc.Save(Utils.DirectoryAndFileHelper.ConfigFilePath);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        private static XmlDocument LoadConfigDocument()
        {
            XmlDocument doc;
            try
            {
                doc = new XmlDocument();
                doc.Load(Utils.DirectoryAndFileHelper.ConfigFilePath);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                doc = new XmlDocument();
                doc.LoadXml("<config/>");
                return doc;
            }
            catch (Exception ex)
            {
                doc = new XmlDocument();
                doc.LoadXml("<config/>");
                return doc;
            }
        }

        
    }
}
