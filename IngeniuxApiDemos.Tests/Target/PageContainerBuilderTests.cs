using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IngeniuxApiDemos.Target.IngeniuxCms;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Components;
using IngeniuxApiDemos.Target.IngeniuxCms.Repository;
using NLog;
using NSubstitute;
using Shouldly;
using SlickplanToIngeniux.Tests.Target.Fakes;
using Xunit;
using Page = IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages.Page;

namespace SlickplanToIngeniux.Tests.Target
{
    public class PageContainerBuilderTests
    {
        public class TestIngeniuxPage : IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages.Page
        {
            public override string SchemaRootName => "";
            public override string SchemaFriendlyName => "";

            [Required]
            public string Name { get; set; }
            public string Value { get; set; }
            public List<Component> Components { get; set; } = new List<Component>();
        }

        [Fact]
        public void should_build_page_container_with_no_child_components()
        {
            var repository = Substitute.For<IIngeniuxRepository>();
            var logger = Substitute.For<ILogger>();

            var testPage = new FakePage("x67890");

            repository.GetPageById(Arg.Any<string>()).Returns(testPage);

            var builder = new PageContainerBuilder(repository, logger);

            var model = new TestIngeniuxPage
            {
                Moniker = "moniker",
                Name = "name",
                Value = "value"
            };

            var result = builder.BuildPageTreeFromModel("x12345", model);
            result.ShouldNotBe(null);
            result.Page.ShouldNotBe(null);
            result.Page.Id.ShouldBe(testPage.Id);
            result.Children.Count.ShouldBe(0);
        }
    }
}
