using System.Collections.Generic;
using IngeniuxApiDemos.Source.Slickplan.Model;
using IngeniuxApiDemos.Source.Slickplan.XmlDeserialization;
using Shouldly;
using Xunit;

namespace SlickplanToIngeniux.Tests.Source.XmlDeserialization
{
    public class SlickplanXmlDeserializerTests
    {
        #region Xml Data

        private const string _xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<sitemap scheme=""2.0"">
    <title>Gwen's Sample Map</title>
    <version>1.1</version>
    <link></link>
    <pubDate>Thu, 09 Feb 2017 13:19:04 -0500</pubDate>
    <section id=""svgmainsection"">
        <options>
            <template>horizontal</template>
            <color_scheme>default</color_scheme>
            <id>svgmainsection</id>
            <design>flat</design>
            <text_shadow>false</text_shadow>
            <section>svgmainsection</section>
        </options>
        <cells>
            <cell id=""svgotrvqkzqsb0vp36u"">
                <level>home</level>
                <text>General Tree</text>
                <order>1000</order>
            </cell>
            <cell id=""svge5mfgzpcba4slw62"">
                <level>1</level>
                <text>Heroes</text>
                <order>1000</order>
                <contents>
                    <body>
                        <text>
                            <id>vckxwgz7bidi38ls</id>
                            <content>Page Header Name Here</content>
                            <options>
                                <tag>h1</tag>
                                <tag_class>page-header</tag_class>
                                <tag_id>pageHeader</tag_id>
                            </options>
                        </text>
                        <text>
                            <id>pg6tc3nqpd2laumh</id>
                            <content>Some kind of plain text box</content>
                            <options>
                                <tag>div</tag>
                                <tag_class>PlainText1</tag_class>
                                <tag_id>PlainTextId1</tag_id>
                            </options>
                        </text>
                        <text>
                            <id>zkpny3btbfeod612</id>
                            <content>Another box of plain text</content>
                        </text>
                    </body>
                    <meta_focus_keyword>MetaData Focus Keyword</meta_focus_keyword>
                    <meta_title>MetaData Seo Title</meta_title>
                    <meta_description>MetaData Description</meta_description>
                </contents>
            </cell>
            <cell id=""svgv4optsdgf0wujdyh"">
                <section>svgm73ficl72l2n3562</section>
                <level>1</level>
                <text>Family Tree</text>
                <order>2000</order>
            </cell>
            <cell id=""svgb0fl6bdty5m2h5xp"">
                <level>1</level>
                <text>Do not import</text>
                <order>3000</order>
                <archetype>ufyyekerys</archetype>
            </cell>
            <cell id=""svgt2kp7e44dffeoz5v"">
                <parent>svge5mfgzpcba4slw62</parent>
                <color>#205bf6</color>
                <textcolor>#ffffff</textcolor>
                <desc>Tony Stark is Iron Man</desc>
                <archetype>lcvxegamvob6pl</archetype>
                <level>2</level>
                <text>Iron Man</text>
                <order>1000</order>
            </cell>
            <cell id=""svgpnsmg9nbj1lw6mqg"">
                <parent>svgv4optsdgf0wujdyh</parent>
                <level>2</level>
                <text>SubPage1</text>
                <order>1000</order>
            </cell>
            <cell id=""svgx3460gr59q0zitz5"">
                <parent>svgb0fl6bdty5m2h5xp</parent>
                <level>2</level>
                <text>item 1</text>
                <order>1000</order>
            </cell>
            <cell id=""svgujkwksursta5f7yz"">
                <parent>svge5mfgzpcba4slw62</parent>
                <urls>
                    <url type=""external"" label=""Google"">http://google.com</url>
                    <url type=""external"" label=""Bing"">http://bing.com</url>
                </urls>
                <archetype>_file</archetype>
                <level>2</level>
                <text>Captain America</text>
                <order>2000</order>
            </cell>
            <cell id=""svgafvfrftd8wyas4mm"">
                <parent>svgb0fl6bdty5m2h5xp</parent>
                <level>2</level>
                <text>item 2</text>
                <order>2000</order>
            </cell>
            <cell id=""svgg6th6pxw4ay89exs"">
                <parent>svgt2kp7e44dffeoz5v</parent>
                <archetype>bv0jhusgtor</archetype>
                <level>3</level>
                <text>Jarvis</text>
                <order>1000</order>
                <contents>
                    <body>
                        <text>
                            <id>ozmsyrnx98tpk21w</id>
                            <content>Plain Text 1</content>
                        </text>
                        <wysiwyg>
                            <id>fy3izi57a6aeayas</id>
                            <content>&lt;p&gt;Rich Text 1&lt;/p&gt;</content>
                        </wysiwyg>
                    </body>
                </contents>
            </cell>
        </cells>
    </section>
    <section id=""svgm73ficl72l2n3562"">
        <options>
            <template>horizontal</template>
            <design>flat</design>
            <color_scheme>default</color_scheme>
            <text_shadow>false</text_shadow>
            <parent>svgv4optsdgf0wujdyh</parent>
            <name>Maternal</name>
            <id>svgm73ficl72l2n3562</id>
        </options>
        <cells>
            <cell id=""svglv053o6bpnwflefo"">
                <level>home</level>
                <text>Family Tree</text>
                <order>1000</order>
            </cell>
            <cell id=""svgqnarurimtamq1e31"">
                <level>1</level>
                <text>Grandmother</text>
                <order>1000</order>
            </cell>
            <cell id=""svguq4scwq7elar4r5n"" collapsed=""1"">
                <parent>svgqnarurimtamq1e31</parent>
                <level>2</level>
                <text>Aunt 1</text>
                <order>1000</order>
            </cell>
            <cell id=""svgx922h7qijz4xgpp3"" collapsed=""1"">
                <parent>svgqnarurimtamq1e31</parent>
                <level>2</level>
                <text>Mom</text>
                <order>2000</order>
            </cell>
            <cell id=""svgl8ye1qj2mvh29kto"" collapsed=""1"">
                <parent>svgqnarurimtamq1e31</parent>
                <level>2</level>
                <text>Uncle 2</text>
                <order>3000</order>
            </cell>
            <cell id=""svgnq2cho9ijo67srwh"" collapsed=""1"">
                <parent>svgqnarurimtamq1e31</parent>
                <level>2</level>
                <text>Uncle 3</text>
                <order>4000</order>
            </cell>
            <cell id=""svgrfwqool8m9rcc4af"">
                <parent>svguq4scwq7elar4r5n</parent>
                <level>3</level>
                <text>Cousin 1a</text>
                <order>1000</order>
            </cell>
            <cell id=""svgq8w6q8f5ltx99zxs"">
                <parent>svgx922h7qijz4xgpp3</parent>
                <level>3</level>
                <text>Me</text>
                <order>1000</order>
            </cell>
            <cell id=""svgpgfsl2dcfj4p215b"">
                <parent>svgl8ye1qj2mvh29kto</parent>
                <level>3</level>
                <text>Cousin 2a</text>
                <order>1000</order>
            </cell>
            <cell id=""svgjtakbt2xth8m2ysa"">
                <parent>svgnq2cho9ijo67srwh</parent>
                <level>3</level>
                <text>Cousin 3a</text>
                <order>1000</order>
            </cell>
            <cell id=""svgzz98cybu46xk46yt"">
                <parent>svgl8ye1qj2mvh29kto</parent>
                <level>3</level>
                <text>Cousin 2b</text>
                <order>2000</order>
            </cell>
            <cell id=""svgn460rnrmfka5miro"" collapsed=""1"">
                <parent>svgx922h7qijz4xgpp3</parent>
                <level>3</level>
                <text>Brother 1</text>
                <order>2000</order>
            </cell>
            <cell id=""svgpu5wdaelew9abgns"">
                <parent>svguq4scwq7elar4r5n</parent>
                <level>3</level>
                <text>Cousin 1b</text>
                <order>2000</order>
            </cell>
            <cell id=""svgn4a94rmphivknt31"">
                <parent>svgnq2cho9ijo67srwh</parent>
                <level>3</level>
                <text>Cousin 3b</text>
                <order>2000</order>
            </cell>
            <cell id=""svgneefc28dgx3iw91h"" collapsed=""1"">
                <parent>svgx922h7qijz4xgpp3</parent>
                <level>3</level>
                <text>Sister 2</text>
                <order>3000</order>
            </cell>
            <cell id=""svgtgwgvdjvkka6opix"">
                <parent>svgneefc28dgx3iw91h</parent>
                <level>4</level>
                <text>Nephew 2a</text>
                <order>1000</order>
            </cell>
            <cell id=""svgwxdidcsulp0q1ysv"">
                <parent>svgn460rnrmfka5miro</parent>
                <level>4</level>
                <text>Nephew 1a</text>
                <order>1000</order>
            </cell>
            <cell id=""svgkbbmfct7q2wyogjj"">
                <parent>svgn460rnrmfka5miro</parent>
                <level>4</level>
                <text>Niece 1a</text>
                <order>2000</order>
            </cell>
        </cells>
    </section>
</sitemap>";

        #endregion

        #region Page Types

        private static readonly IEnumerable<PageType> _pageTypes = new List<PageType>
        {
            new PageType { Id = "_file", Name = "File" },
            new PageType { Id = "lcvxegamvob6pl", Name = "Custom Page1" },
            new PageType { Id = "bv0jhusgtor", Name = "Custom Page2" },
            new PageType { Id = "ufyyekerys", Name = "Do Not Import" }
        };

        #endregion

        [Fact]
        public void should_deserialize_primary_section()
        {
            var deserializer = new SlickplanXmlDeserializer(_pageTypes);
            var result = deserializer.Deserialize(_xml);
            result.ShouldNotBe(null);
            result.Text.ShouldBe("General Tree");
            result.Children[0].Text.ShouldBe("Heroes");

            var ironman = result.Children[0].Children[0];
            ironman.Id.ShouldBe("svgt2kp7e44dffeoz5v");
            ironman.Text.ShouldBe("Iron Man");
            ironman.PageType.ShouldBe("Custom Page1");

            var captainAmerica = result.Children[0].Children[1];
            captainAmerica.Text.ShouldBe("Captain America");
            captainAmerica.Order.ShouldBe(2000);
        }

        [Fact]
        public void should_deserialize_sections_to_components()
        {
            var deserializer = new SlickplanXmlDeserializer(_pageTypes);
            var result = deserializer.Deserialize(_xml);
            result.ShouldNotBe(null);
            result.Text.ShouldBe("General Tree");
            result.Children[1].Text.ShouldBe("Family Tree");
            result.Children[1].Contents[0].Id.ShouldBe("svgqnarurimtamq1e31");
            result.Children[1].Contents[0].Text.ShouldBe("Grandmother");
            result.Children[1].Contents[0].Contents[0].Text.ShouldBe("Aunt 1");
            result.Children[1].Contents[0].Contents[0].Contents[0].Text.ShouldBe("Cousin 1a");
        }

        [Fact]
        public void should_deserialize_without_excluded_content()
        {
            var deserializer = new SlickplanXmlDeserializer(_pageTypes);
            var result = deserializer.Deserialize(_xml);
            result.ShouldNotBe(null);
            result.Text.ShouldBe("General Tree");
            result.Children.Count.ShouldBe(2);
        }
    }
}
