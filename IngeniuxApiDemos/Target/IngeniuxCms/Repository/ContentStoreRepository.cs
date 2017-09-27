using Ingeniux.CMS;
using IngeniuxApiDemos.Common.Configuration;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Repository
{
    internal interface IContentStoreRepository
    {
        IConfiguration Config { get; }
        IUserSession CreateReadSession();
        IUserSession CreateWriteSession();
    }

    /// <summary>
    /// This is a heavy operation. Create it once and cache it.
    /// </summary>
    internal class ContentStoreRepository : IContentStoreRepository
    {
        private readonly IContentStore _contentStore;
        public IConfiguration Config { get; }

        public ContentStoreRepository(IConfiguration config)
        {
            Config = config;
            _contentStore = new ContentStore(Config.Target.Ingeniux.ContentStoreUri, Config.Target.Ingeniux.ContentStoreXmlPath);
        }

        public IUserSession CreateReadSession()
        {
            var currentUser = _contentStore.GetStartingUser(Config.Target.Ingeniux.UserId);
            return _contentStore.OpenReadSession(currentUser);
        }

        public IUserSession CreateWriteSession()
        {
            var currentUser = _contentStore.GetStartingUser(Config.Target.Ingeniux.UserId);
            return _contentStore.OpenWriteSession(currentUser);
        }
    }
}