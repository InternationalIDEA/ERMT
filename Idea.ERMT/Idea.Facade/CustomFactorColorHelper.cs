using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Drawing;
using Idea.Utils;

namespace Idea.Facade
{
    public class CustomFactorColorHelper
    {
        /// <summary>
        /// Guarda la lista de string en el XML
        /// </summary>
        /// <param name="listFactorColor">All Color Factors customized</param>
        /// <param name="idModel">The ID of the model</param>
        public static void SaveFactorColorsToXML(List<CustomFactorColor> listFactorColor, int idModel)
        {
            XmlDocument doc = new XmlDocument();


            XElement newElem = new XElement("CustomFactorColor",
                                            from l in listFactorColor
                                            select new XElement("Factor",
                                                                new XElement("IdFactor", l.IdFactor),
                                                                new XElement("Name", l.Name),
                                                                new XElement("MaxValue", l.MaxValue),
                                                                new XElement("MinValue", l.MinValue),
                                                                new XElement("TotalValues", l.TotalValues),
                                                                new XElement("Colors",
                                                                             from color in l.ListColors
                                                                             select new XElement("Color", ColorTranslator.ToHtml(color)))));


            XmlReader xmlReader = newElem.CreateReader();
            doc.Load(xmlReader);
            doc.PreserveWhitespace = true;
            doc.Save(DirectoryAndFileHelper.ClientAppDataFolder + "\\" + idModel + "CustomFactorColor.xml");

        }

        /// <summary>
        /// Devuelve todo el XML
        /// </summary>
        /// <param name="idModel">el ID del modelo a buscar</param>
        /// <returns></returns>
        public static List<XElement> GetFactorColorsFromXML(int idModel)
        {
            List<XElement> listFactorsColors = new List<XElement>();

            if (File.Exists(DirectoryAndFileHelper.ClientAppDataFolder + "\\" + idModel + "CustomFactorColor.xml"))
            {
                XElement customFactorColor = XElement.Load(DirectoryAndFileHelper.ClientAppDataFolder + "\\" + idModel + "CustomFactorColor.xml");

                var factorsColors = from c in customFactorColor.Descendants("Factor") select c;

                listFactorsColors = factorsColors.ToList();
            }
            return listFactorsColors;
        }
    }
}
