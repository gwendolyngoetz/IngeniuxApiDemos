using System;
using Ingeniux.CMS;

namespace ApiBasics
{
    public class CreatePage
    {
        public static void Run()
        {
            var contentStore = new ContentStore("http://dev.seattle.local/ContentStore", @"C:\igxsites\cms90\dev seattle local\site\App_Data\xml");
            var user = contentStore.GetStartingUser("DemoUser");

            using (var session = contentStore.OpenWriteSession(user))
            {
                var schemaToCreate = "DemoPage";
                var parentPageId = "x74963";

                var schema = session.SchemasManager.SchemaByRootName(schemaToCreate);
                var parentPage = session.Site.Page(parentPageId);

                var page = session.Site.CreatePage(schema, schemaToCreate, parentPage);

                page.Element("Heading").Value = "Heading 123";
                page.Save();

                Console.WriteLine($"{page.Id}\t{page.Name}");
            }
        }
    }
}
