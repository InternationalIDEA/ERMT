using System.Data.Entity.Core.EntityClient;
using System.Text;

namespace Idea.DAL
{
    public static class ContextManager
    {
        public static IdeaContext GetNewDataContext()
        {
            IdeaContext context = new IdeaContext();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            return context;
        }
    }
}
