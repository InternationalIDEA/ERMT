using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;
using Idea.Entities;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Idea.Utils;

namespace Idea.Business
{
    public static class FactorManager
    {
        /// <summary>
        /// Validate the fields by factor as a parameter.
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static bool ValidateRequiredFields(Factor factor)
        {
           
            if (string.IsNullOrEmpty(factor.Name))
                throw new ArgumentException("FactorNameRequired");  

            if (!factor.CumulativeFactor)
            {
                if (factor.ScaleMin < 0)
                {
                    throw new ArgumentException("FactorScaleMin");
                }
                if (factor.ScaleMax <= factor.ScaleMin)
                {
                    throw new ArgumentException("FactorScaleMax");
                }
                if (factor.Interval <= 0)
                {
                    throw new ArgumentException("FactorInterval");
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a new Factor.
        /// </summary>
        /// <returns></returns>
        public static Factor GetNew()
        {
            return new Factor();
        }

        public static void Save(Factor factor)
        {
            Save(factor, "en");
        }

        /// <summary>
        /// Saves a new factor, first deletes all the fields.
        /// </summary>
        /// <param name="factor"></param>
        public static void Save(Factor factor, String culture)
        {
            factor.Introduction = CleanFormat(factor.Introduction);
            factor.EmpiricalCases = CleanFormat(factor.EmpiricalCases);
            factor.DataCollection = CleanFormat(factor.DataCollection);
            factor.Questionnaire = CleanFormat(factor.Questionnaire);
            factor.ObservableIndicators = CleanFormat(factor.ObservableIndicators);

            if (factor.Description == null)
            {
                factor.Description = string.Empty;
            }

            if (factor.HtmlDocument == null)
            {
                factor.HtmlDocument = string.Empty;
            }

            factor.HtmlDocument = factor.IdFactor + ".htm";
            
            GenerateFactorHTML(factor, culture);

            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.Factors.AddOrUpdate(factor);
                context.SaveChanges();
            }
        }

        public static void GenerateAllFactorsHTML(String culture)
        {

            foreach (Factor factor in GetAll())
            {
                Save(factor, culture);
            }
        }

        public static void GenerateFactorHTML(Factor factor, String culture)
        {
            //Generate server html file   
            CultureInfo uiCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = uiCulture;


            string content;
            string file = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HtmlTemplate"];
            using (StreamReader streamReader = new StreamReader(file))
            {
                content = streamReader.ReadToEnd();
                streamReader.Close();
            }

            //Updates html with database data            
            string html = content.Replace("@Introduction@", CleanFormat(factor.Introduction));
            html = html.Replace("@Empirical Cases and Correlation@", CleanFormat(factor.EmpiricalCases));
            html = html.Replace("@Data Collection and Analysis methodologies@", CleanFormat(factor.DataCollection));
            html = html.Replace("@Questionnaire@", CleanFormat(factor.Questionnaire));
            html = html.Replace("@Observable indicators@", CleanFormat(factor.ObservableIndicators));
            html = html.Replace("@Title@", factor.Name);

            //headers
            html = html.Replace("@IntroductionHeader@", LanguageResourceManager.GetResourceText("IntroductionHeader"));
            html = html.Replace("@EmpiricalCasesHeader@", LanguageResourceManager.GetResourceText("EmpiricalCasesHeader"));
            html = html.Replace("@ObservableIndicatorsHeader@", LanguageResourceManager.GetResourceText("ObservableIndicatorsHeader"));
            html = html.Replace("@DataCollectionHeader@", LanguageResourceManager.GetResourceText("DataCollectionHeader"));
            html = html.Replace("@QuestionnaireHeader@", LanguageResourceManager.GetResourceText("QuestionnaireHeader"));

            DocumentManager.Save(DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings["HTMLFolder"] + factor.IdFactor + ".htm", html);
        }

        /// <summary>
        /// Clean the format of factor.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string CleanFormat(string p)
        {
            string cleaned = p ?? string.Empty;
            while (Regex.IsMatch(cleaned, "(<[^>]*)style\\s*=\\s*('|\")[^\\2]*?\\2([^>]*>)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline))
                cleaned = Regex.Replace(cleaned, "(<[^>]*)style\\s*=\\s*('|\")[^\\2]*?\\2([^>]*>)", "$1$3", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            while (Regex.IsMatch(cleaned, "<\\s*font[^>]*>(.*?)<\\s*/\\s*font>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline))
                cleaned = Regex.Replace(cleaned, "<\\s*font[^>]*>(.*?)<\\s*/\\s*font>", "$1", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            return cleaned;
        }

        /// <summary>
        /// Returns the list of all Factors.
        /// </summary>
        /// <returns></returns>
        public static List<Factor> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.Factors select o).ToList();
            }
        }

        /// <summary>
        /// Returns the factor with IdFactor = idFactor.
        /// </summary>
        /// <param name="idFactor"></param>
        /// <returns></returns>
        public static Factor GetById(int idFactor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                var ox = from o in context.Factors where o.IdFactor == idFactor select o;
                return ox.FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns the factor with name = factorName.
        /// </summary>
        /// <param name="factorName"></param>
        /// <returns></returns>
        public static Factor GetByName(String factorName)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Factors.FirstOrDefault(f => f.Name.ToLower() == factorName.ToLower());
            }
        }

        /// <summary>
        /// Returns the list of all html Documents. 
        /// </summary>
        /// <returns></returns>
        public static List<Factor> GetAllWithHtmlDocument()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.Factors
                        where o.HtmlDocument.Length > 0
                        select o).OrderBy(f=>f.SortOrder).ToList();
            }
        }

        /// <summary> 
        /// Deletes a Factor
        /// </summary>
        /// <param name="factor"></param>
        public static void Delete(Factor factor)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                Factor f = (from o in context.Factors where o.IdFactor == factor.IdFactor select o).FirstOrDefault();
                context.Factors.Remove(f);
                context.SaveChanges();
            }
        }
    }
}
