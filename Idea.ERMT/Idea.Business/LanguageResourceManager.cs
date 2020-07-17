using System.Reflection;
using System.Resources;
using System.Threading;

namespace Idea.Business
{
    public static class LanguageResourceManager
    {
        public static string GetResourceText(string name)
        {
            ResourceManager resourceManager = new ResourceManager("Idea.Business.General", Assembly.GetExecutingAssembly());
            return resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
