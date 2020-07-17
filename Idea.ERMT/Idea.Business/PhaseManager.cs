using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Idea.DAL;
using Idea.Entities;
using System.IO;
using System.Threading;
using Idea.Utils;

namespace Idea.Business
{
    public static class PhaseManager
    {
        /// <summary>
        /// Returns a new Phase.
        /// </summary>
        /// <returns></returns>
        public static Phase GetNew()
        {
            return new Phase();
        }

        /// <summary>
        /// Returns all the Phases.
        /// </summary>
        /// <returns></returns>
        public static List<Phase> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return (from o in context.Phases select o).ToList();
            }
        }

        /// <summary>
        /// Saves the Phase.
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public static Phase Save(Phase phase)
        {
            phase.Column1Text = CleanFormat(phase.Column1Text);
            phase.Column2Text = CleanFormat(phase.Column2Text);
            phase.Column3Text = CleanFormat(phase.Column3Text);
            phase.PractitionersTips = CleanFormat(phase.PractitionersTips);
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.Phases.AddOrUpdate(phase);
                context.SaveChanges();
            }


            return phase;
        }

        public static Phase Get(int idPhase)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.Phases.FirstOrDefault(p => p.IDPhase == idPhase);
            }
        }

        public static void GenerateAllFiles(Phase phase, String culture)
        {
            //Generate server html file
            // GenerateFile("PPMFirstScreen", Phase);
            GenerateFile("PPMBulletTemplate", phase, culture);
            GenerateFile("PPMFullTextTemplate", phase, culture);
            GenerateFile("PPMFirstColumnTemplate", phase, culture);
            GenerateFile("PPMSecondColumnTemplate", phase, culture);
            GenerateFile("PPMThirdColumnTemplate", phase, culture);
            GenerateFile("PPMPractitionersTipsTemplate", phase, culture);
        }

        /// <summary>
        /// Generate the File with configValue and phase.
        /// </summary>
        /// <param name="configValue"></param>
        /// <param name="phase"></param>
        private static void GenerateFile(string configValue, Phase phase, String culture)
        {
            string content;
            string file = DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings[configValue];
            using (StreamReader streamReader = new StreamReader(file))
            {
                content = streamReader.ReadToEnd();
                streamReader.Close();
            }
            // SAves the template with the values, but replace template by the ID:
            DocumentManager.Save(DirectoryAndFileHelper.ServerAppDataFolder + ConfigurationManager.AppSettings[configValue].Replace("Template-", phase.IDPhase + "-"), FormatPhaseTemplate(content, phase, culture));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="phase"></param>
        /// <returns></returns>
        private static string FormatPhaseTemplate(string html, Phase phase, String culture)
        {
            CultureInfo uiCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = uiCulture;

            String _html = html.Replace("@Title@", phase.Title);
            _html = _html.Replace("@IDFase@", phase.IDPhase.ToString());
            _html = _html.Replace("@Column1Text@", phase.Column1Text);
            _html = _html.Replace("@Column2Text@", phase.Column2Text);
            _html = _html.Replace("@Column3Text@", phase.Column3Text);
            _html = _html.Replace("@PractitionersTips@", phase.PractitionersTips);
            _html = _html.Replace("@Text@", LanguageResourceManager.GetResourceText("Text"));

            //Bullets:
            _html = _html.Replace("@FaceBulletsColumn1@", GenerateBulletValues(PhaseBulletManager.GetByPhaseAndColumn(phase.IDPhase, 1).ToList()));
            _html = _html.Replace("@FaceBulletsColumn2@", GenerateBulletValues(PhaseBulletManager.GetByPhaseAndColumn(phase.IDPhase, 2).ToList()));
            _html = _html.Replace("@FaceBulletsColumn3@", GenerateBulletValues(PhaseBulletManager.GetByPhaseAndColumn(phase.IDPhase, 3).ToList()));

            //headers
            _html = _html.Replace("@ActionPointsHeader@", LanguageResourceManager.GetResourceText("ActionPointsHeader"));
            _html = _html.Replace("@ImprovedElectoralManagementHeader@", LanguageResourceManager.GetResourceText("ImprovedElectoralManagementHeader"));
            _html = _html.Replace("@ImprovedElectoralSecurityHeader@", LanguageResourceManager.GetResourceText("ImprovedElectoralSecurityHeader"));
            _html = _html.Replace("@ImprovedInfrastructurePeaceHeader@", LanguageResourceManager.GetResourceText("ImprovedInfrastructurePeaceHeader"));
            _html = _html.Replace("@PractitionersTipsHeader@", LanguageResourceManager.GetResourceText("PractitionersTipsHeader"));
            _html = _html.Replace("@FullText@", LanguageResourceManager.GetResourceText("FullText"));
            
            
            return _html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bullets"></param>
        /// <returns></returns>
        private static string GenerateBulletValues(List<PhaseBullet> bullets)
        {
            StringBuilder builder = new StringBuilder("");
            foreach (PhaseBullet bullet in bullets)
            {
                builder.AppendLine("<li>" + bullet.Text + "</li>");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Clean all formats.
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

    }
}
