using System;
using System.Collections.Generic;
using System.Linq;
using Ingeniux.CMS;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Common.Extensions;
using IngeniuxApiDemos.Target.IngeniuxCms.Extensions;
using IngeniuxApiDemos.Target.IngeniuxCms.Model;
using IngeniuxApiDemos.Target.IngeniuxCms.Repository;
using NLog;
using Page = IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages.Page;

namespace IngeniuxApiDemos.Target.IngeniuxCms
{
    internal interface IPageGenerator
    {
        void Execute(IEnumerable<Page> models);
    }

    internal class PageGenerator : IPageGenerator
    {
        private readonly IContentStoreRepository _contentStore;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _config;
        private IIngeniuxRepository _repository;

        public PageGenerator(IContentStoreRepository contentStore, IConfiguration config)
        {
            _contentStore = contentStore;
            _config = config;
        }

        public void Execute(IEnumerable<Page> models)
        {
            models.Each(SaveModelGraph);
        }

        private void SaveModelGraph(Page model)
        {
            try
            {
                using (_repository = new IngeniuxRepository(_contentStore, _config))
                {
                    var pageContainerBuilder = new PageContainerBuilder(_repository);
                    var rootPageContainer = pageContainerBuilder.BuildPageTreeFromModel(model.ParentId, model);

                    BuildPageTreeFromChildPages(pageContainerBuilder, rootPageContainer, model.Pages);
                    ReloadPageContainerPages(rootPageContainer);
                    SyncListConnections(rootPageContainer);
                    _repository.SaveAll();
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, $"Could not completely save or create page for {model.Moniker}");
            }
        }

        private void BuildPageTreeFromChildPages(PageContainerBuilder pageContainerBuilder, PageContainer parent, IEnumerable<Page> pages)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            foreach (var page in pages)
            {
                page.ParentId = parent.Page.Id;



                var childPageContainer = pageContainerBuilder.BuildPageTreeFromModel(parent.Page.Id, page);
                parent.Pages.Add(childPageContainer);
                BuildPageTreeFromChildPages(pageContainerBuilder, childPageContainer, page.Pages);
            }
        }

        public void ReloadPageContainerPages(PageContainer pageContainer)
        {
            ReloadPageContainer(pageContainer, _repository.ReloadPages());
        }

        private void ReloadPageContainer(PageContainer pageContainer, Queue<IPage> loadedPages)
        {
            var reloadedPage = loadedPages.SingleOrDefault(x => x.Id == pageContainer.Page.Id);

            if (reloadedPage == null)
            {
                reloadedPage = _repository.GetPageById(pageContainer.Page.Id);
            }

            pageContainer.Page = reloadedPage;

            foreach (var siblingPageContainer in pageContainer.Pages)
            {
                ReloadPageContainer(siblingPageContainer, loadedPages);
            }

            foreach (var childPageContainer in pageContainer.Children)
            {
                ReloadPageContainer(childPageContainer, loadedPages);
            }
        }

        private void SyncListConnections(PageContainer parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }


            foreach (var sibling in parent.Pages)
            {
                SyncListConnections(sibling);
            }

            foreach (var child in parent.Children)
            {
                SyncListConnections(child);

                if (parent.Page.IsList(child.Name) && !parent.Page.AddToList(child.Name, child.Page))
                {
                    _logger.Warn($"Could not find element '{child.Name}' in '{parent.Page.Id} ({parent.Page.Page.Name})'");
                }
                else if (parent.Page.IsComponentElement(child.Name))
                {
                    parent.Page.Element(child.Name).Value = child.Page.Id;
                }
            }
        }
    }
}