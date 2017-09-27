using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Attributes;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Components;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Pages;
using IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation;
using Shouldly;
using Xunit;

namespace SlickplanToIngeniux.Tests.Target.Validation
{
    public class BaseObjectValidatorTests
    {
        public class StringOnlyTestPage : Page
        {
            public override string SchemaRootName => "StringOnlyTestPage";
            public override string SchemaFriendlyName => "";

            [Required]
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            [Required]
            public string LastName { get; set; }
        }

        public class StringAndComponentTestPage : StringOnlyTestPage
        {
            [Required]
            public StringOnlyTestComponent Component1 { get; set; } = new StringOnlyTestComponent();
            public StringOnlyTestComponent Component2 { get; set; } = new StringOnlyTestComponent();
        }

        public class StringOnlyTestComponent : Component
        {
            public override string SchemaRootName => "StringOnlyTestComponent";
            public override string SchemaFriendlyName => "";

            [Required]
            public string Title { get; set; }
        }

        public class ListComponent : Component
        {
            public override string SchemaRootName => "ListComponent";
            public override string SchemaFriendlyName => "";

            [AllowedComponents("ListItemComponent1", "ListItemComponent2")]
            public List<Component> List { get; set; } = new List<Component>();
        }

        public class ListItemComponent1 : Component
        {
            [Required]
            public string Value { get; set; }
            public override string SchemaRootName => "ListItemComponent1";
            public override string SchemaFriendlyName => "";
        }

        public class ListItemComponent2 : Component
        {
            [Required]
            public string Value { get; set; }
            public override string SchemaRootName => "ListItemComponent2";
            public override string SchemaFriendlyName => "";
        }

        public class ListItemNotAllowedComponent : Component
        {
            [Required]
            public string Value { get; set; }
            public override string SchemaRootName => "ListItemNotAllowedComponent";
            public override string SchemaFriendlyName => "";
        }

        [Fact]
        public void page_has_no_values_set_should_be_invalid()
        {
            var testPage = new StringOnlyTestPage();

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(false);
        }

        [Fact]
        public void page_has_one_value_set_should_be_invalid()
        {
            var testPage = new StringOnlyTestPage
            {
                FirstName = "Tony"
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(false);
        }

        [Fact]
        public void page_has_all_string_values_set_should_be_valid()
        {
            var testPage = new StringOnlyTestPage
            {
                FirstName = "Tony",
                LastName = "Stark"
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(true);
        }

        [Fact]
        public void page_has_string_values_set_but_not_component_should_be_invalid()
        {
            var testPage = new StringAndComponentTestPage
            {
                FirstName = "Tony",
                LastName = "Stark"
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(false);
        }

        [Fact]
        public void page_has_all_string_and_component_values_set_should_be_valid()
        {
            var testPage = new StringAndComponentTestPage
            {
                FirstName = "Tony",
                LastName = "Stark",
                Component1 = new StringOnlyTestComponent
                {
                    Title = "Ironman"
                }
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(true);
        }

        [Fact]
        public void page_list_set_should_be_valid()
        {
            var testPage = new ListComponent
            {
                List = new List<Component>
                {
                    new ListItemComponent1
                    {
                        Value = "123"
                    },
                    new ListItemComponent1
                    {
                        Value = "abc"
                    }
                }
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(true);
        }

        [Fact]
        public void page_list_set_should_be_invalid()
        {
            var testPage = new ListComponent
            {
                List = new List<Component>
                {
                    new ListItemComponent1
                    {
                        Value = ""
                    }
                }
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(false);
        }

        [Fact]
        public void page_list_component_on_allowed_list_should_be_valid()
        {
            var testPage = new ListComponent
            {
                List = new List<Component>
                {
                    new ListItemComponent1
                    {
                        Value = "asdf"
                    },
                    new ListItemComponent2
                    {
                        Value = "asdf"
                    }
                }
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(true);
        }

        [Fact]
        public void page_list_component_not_on_allowed_list_should_be_invalid()
        {
            var testPage = new ListComponent
            {
                List = new List<Component>
                {
                    new ListItemComponent1
                    {
                        Value = "asdf"
                    },
                    new ListItemNotAllowedComponent()
                    {
                        Value = "asdf"
                    }
                }
            };

            var validationResults = BaseObjectValidator.Validate(testPage);
            validationResults.IsValid.ShouldBe(false);
        }
    }
}
