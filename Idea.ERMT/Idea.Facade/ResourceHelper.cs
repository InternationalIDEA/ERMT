using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace Idea.Facade
{
    public class ResourceHelper
    {
        public static string GetResourceText(string name)
        {
            ResourceManager resourceManager = new ResourceManager("Idea.Facade.General", Assembly.GetExecutingAssembly()); 
            return resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
        }

        public static Bitmap GetResourceImage(string resourceImageName)
        {
            ResourceManager resourceManager = new ResourceManager("Idea.Facade.General", Assembly.GetExecutingAssembly());
            return (Bitmap)(resourceManager.GetObject(resourceImageName));
        }
    }
}
