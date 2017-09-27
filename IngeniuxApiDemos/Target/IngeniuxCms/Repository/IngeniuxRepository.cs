using System;
using System.Collections.Generic;
using System.Linq;
using Ingeniux.CMS;
using IngeniuxApiDemos.Common.Configuration;
using NLog;
using IngeniuxApiDemos.Common.Extensions;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Repository
{
    public interface IIngeniuxRepository : IDisposable
    {
        IPage GetPageById(string id, bool doNotCache = false);
        IPage GetPageByName(string parentPageId, string name, bool doNotCache = false);
        IEnumerable<IPage> GetChildPagesByParentId(string parentPageId, bool doNotCache = false, Func<IPage, bool> filter = null);
        ISchema GetSchemaByRootName(string schemaRootName);
        IPage CreatePage(ISchema schema, string pageName, string parentId);
        Queue<IPage> ReloadPages();
        void SaveAll();
        void Save(IPage page);
    }

    internal class IngeniuxRepository : IIngeniuxRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IContentStoreRepository _contentStoreRepository;
        private readonly IConfiguration _config;
        private IUserSession _session;
        private Queue<IPage> _loadedPages = new Queue<IPage>();

        public IngeniuxRepository(IContentStoreRepository contentStoreRepository, IConfiguration config)
        {
            _contentStoreRepository = contentStoreRepository;
            _config = config;
            CreateSession();
        }

        private void QueuePage(IPage page, bool doNotCache = false)
        {
            if (page == null || doNotCache)
            {
                return;
            }

            if (_loadedPages.Any(x => x.Id == page.Id))
            {
                return;
            }

            _loadedPages.Enqueue(page);
        }

        private void CreateSession()
        {
            _session?.Dispose();
            _session = _contentStoreRepository.CreateWriteSession();
        }

        public IPage GetPageById(string id, bool doNotCache = false)
        {
            var page = _session.Site.Page(id);
            QueuePage(page, doNotCache);
            return page;
        }

        public IPage GetPageByName(string parentPageId, string name, bool doNotCache = false)
        {
            var rootPage = _session.Site.Page(parentPageId);
            int childPageCount;

            var page = rootPage.Children(out childPageCount).FirstOrDefault(x => x.Name == name);
            
            QueuePage(page, doNotCache);
            return page;
        }

        public IEnumerable<IPage> GetChildPagesByParentId(string parentPageId, bool doNotCache = false, Func<IPage, bool> filter = null)
        {
            int count;
            var pages = _session.Site.Page(parentPageId).Children(out count).ToList();

            if (filter != null)
            {
                pages = pages.Where(filter).ToList();
            }

            pages.Each(x => QueuePage(x, doNotCache));
            return pages;
        }

        public ISchema GetSchemaByRootName(string schemaRootName)
        {
            return _session.SchemasManager.SchemaByRootName(schemaRootName);
        }

        public IPage CreatePage(ISchema schema, string pageName, string parentId)
        {
            var page = _session.Site.CreatePage(schema, pageName, _session.Site.Page(parentId));
            QueuePage(page);
            CreateSession();
            return page;
        }

        public Queue<IPage> ReloadPages()
        {
            var idsToLoad = _loadedPages.Select(x => x.Id);
            _loadedPages = _session.Site.Pages(idsToLoad).ToQueue();
            return _loadedPages;
        }

        public void SaveAll()
        {
            _loadedPages.Each(Save);
        }

        public void Save(IPage page)
        {
            _logger.Debug($"Saving: {page.Id} ({page.Name})");
            page.Save();
        }

        public void Dispose()
        {
            _loadedPages.Clear();
            _session.Dispose();
        }
    }
}
