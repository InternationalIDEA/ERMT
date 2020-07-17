using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Idea.DAL;

using SystemParameter = Idea.Entities.SystemParameter;

namespace Idea.Business
{
    public class SystemParameterManager
    {
        /// <summary>
        /// Saves a SystemParameter.
        /// </summary>
        /// <param name="systemParameter"></param>
        /// <returns></returns>
        public static SystemParameter Save(SystemParameter systemParameter)
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                context.SystemParameters.AddOrUpdate(systemParameter);
                context.SaveChanges();
                return systemParameter;
            }
        }

        /// <summary>
        /// Returns the list of all SystemParameters.
        /// </summary>
        /// <returns></returns>
        public static List<SystemParameter> GetAll()
        {
            using (IdeaContext context = ContextManager.GetNewDataContext())
            {
                return context.SystemParameters.ToList();
            }
        }

        /// <summary>
        /// Create a SystemParameter for default.
        /// </summary>
        public static void CreateDefaults()
        {
            SystemParameter systemParameter = new SystemParameter {LastRiskCode = 1};
            // This needs to be done cause zero is the default, and if I dont set it to 1 first,
            // the internal object doesnt know it has changed, and wont save it. So set to 1, marked as has changed, and set to 0 to really save.
            systemParameter.LastRiskCode = 0;
            Save(systemParameter);
        }

        /// <summary>
        /// Convert the code intCode in a string.
        /// </summary>
        /// <param name="intCode"></param>
        /// <returns></returns>
        public static string ConvertToCodeString(int intCode)
        {
            if (intCode > 999999)
            {
                throw new Exception("CodeSixCaracters");
            }
            
            string retString = intCode.ToString();
            while (retString.Length < 6)
            {
                retString = "0" + retString;
            }
            return retString;
        }

        /// <summary>
        /// Gets the next code (does not update it. Use 
        /// </summary>
        /// <returns></returns>
        public static string GetNextCode()
        {
            List<SystemParameter> systemParameters = GetAll();
            if (systemParameters.Count == 0)
            {
                CreateDefaults();
                systemParameters = GetAll();
            }
            // Get the last code, increase it, and save the new one.
            string lastCode = ConvertToCodeString(systemParameters[0].LastRiskCode + 1);
            systemParameters[0].LastRiskCode = systemParameters[0].LastRiskCode + 1;
            Save(systemParameters[0]);

            return ConvertToCodeString(systemParameters[0].LastRiskCode);
        }

        /// <summary>
        /// Saves the Last Code with lastCode.
        /// </summary>
        /// <param name="lastCode"></param>
        public static void SaveLastCode(int lastCode)
        {
            if (lastCode > 999999)
                throw new Exception("CodeSixCaracters");

            List<SystemParameter> systemParameters = GetAll();
            if (systemParameters.Count == 0)
            {
                CreateDefaults();
                systemParameters = GetAll();
            }
            systemParameters[0].LastRiskCode = lastCode;
            Save(systemParameters[0]);

        }
    }
}
