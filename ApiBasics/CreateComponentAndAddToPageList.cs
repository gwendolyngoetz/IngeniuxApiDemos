using System;
using Ingeniux.CMS;

namespace ApiBasics
{
    public class CreateComponentAndAddToPageList
    {
        public static void Run(string parentPageId)
        {
            var contentStore = new ContentStore("http://dev.seattle.local/ContentStore", @"C:\igxsites\cms90\dev seattle local\site\App_Data\xml");
            var user = contentStore.GetStartingUser("DemoUser");

            using (var session = contentStore.OpenWriteSession(user))
            {
                var schemaToCreate = "DemoTagComponent";

                var schema = session.SchemasManager.SchemaByRootName(schemaToCreate);
                var parentPage = session.Site.Page(parentPageId);

                CreateDemoTagComponent(session, schema, "Tag1", parentPage);
                CreateDemoTagComponent(session, schema, "Tag2", parentPage);

                parentPage.Save();

            }
        }

        private static void CreateDemoTagComponent(IUserWriteSession session, ISchema schema, string newPageName, IPage parentPage)
        {
            var component = session.Site.CreatePage(schema, newPageName, parentPage);

            component.Element("Name").Value = newPageName;
            component.Save();

            AddChildPageToParentList(parentPage, "DemoTagComponents", component.Id);

            Console.WriteLine($"{component.Id}\t{component.Name}");

        }

        private static void AddChildPageToParentList(IPage parentPage, string parentPageListElementName, string childPageId)
        {
            var listElement = parentPage.Element(parentPageListElementName) as ListElement;

            if (listElement == null)
            {
                return;
            }

            // Remove the blank placeholder which exists on empty lists
            if (listElement._Elements.Count == 1 && listElement._Elements[0].GetType() == typeof(ComponentElement) && string.IsNullOrWhiteSpace(listElement._Elements[0].Value))
            {
                listElement.ClearAllListItems();
            }

            listElement.AddListItem(new ComponentElement(listElement.ChildElementName)
            {
                Value = childPageId,
                Expanded = true
            });
        }
    }
}