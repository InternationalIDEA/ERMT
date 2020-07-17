using System;
using System.Configuration;
using System.IO;
using Idea.Utils;
using System.Xml.Serialization;
using System.Text;
using System.Runtime.Serialization;

namespace Idea.Entities
{
    public partial class ModelRiskAlertAttachment
    {
        [DataMember]
        private string _content;

        [DataMember]
        public string Content
        {
            get
            {
                // GEt the content:
                string path = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["RARDocumentsFolder"] + IDModelRiskAlertAttachment + "-" + AttachmentFile;
                if (File.Exists(path))
                {
                    _content = Convert.ToBase64String(File.ReadAllBytes(path));
                }
                return _content;
            }
            set
            {
                _content = value;
            }
        }
            
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }


        public bool Deleted { get; set; }
    }
}
