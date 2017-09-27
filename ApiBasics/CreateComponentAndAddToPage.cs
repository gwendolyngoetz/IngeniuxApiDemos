using System;
using Ingeniux.CMS;

namespace ApiBasics
{
    public class CreateComponentAndAddToPage
    {
        public static void Run(string parentPageId)
        {
            var contentStore = new ContentStore("http://dev.seattle.local/ContentStore", @"C:\igxsites\cms90\dev seattle local\site\App_Data\xml");
            var user = contentStore.GetStartingUser("DemoUser");

            using (var session = contentStore.OpenWriteSession(user))
            {
                var schemaToCreate = "DemoCopyComponent";

                var schema = session.SchemasManager.SchemaByRootName(schemaToCreate);
                var parentPage = session.Site.Page(parentPageId);

                var component = session.Site.CreatePage(schema, "Copy", parentPage);

                component.Element("Copy").Value = "Lorem ipsum dolar sit amet...";
                component.Save();

                parentPage.Element("DemoCopyComponent").Value = component.Id;
                parentPage.Save();

                Console.WriteLine($"{component.Id}\t{component.Name}");

            }
        }
    }
}