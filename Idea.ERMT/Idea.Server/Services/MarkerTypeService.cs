using System.Collections.Generic;
using System.IO;
using System.Linq;
using Idea.Business;
using Idea.Entities;
using Idea.Utils;

namespace Idea.Server
{
    public class MarkerTypeService : IMarkerTypeService
    {
        public List<MarkerType> GetAll()
        {
            return(MarkerTypeManager.GetAll());
        }

        public MarkerType Get(int idMarkerType)
        {
            return (MarkerTypeManager.Get(idMarkerType));
        }

        public MarkerType Save(MarkerType p)
        {
            return (MarkerTypeManager.Save((p)));
        }

        public void Delete(MarkerType p, bool deleteMarkerImage)
        {
            MarkerTypeManager.Delete((p));
            string location = DirectoryAndFileHelper.ServerIconsFolder;
            List<MarkerType> markerTypeList = GetAll().Where(mt => mt.Symbol == p.Symbol).ToList();
            bool existAnother = markerTypeList.Count > 0;
            

            if (!existAnother && deleteMarkerImage)
            {
                string path = location + p.Symbol;
                if (File.Exists(path))
                {
                    DocumentManager.Delete(path);
                }
            }
        }

        public List<MarkerType> GetByName(string name)
        {
            return (MarkerTypeManager.GetByName(name));
        }

       
    }
}
