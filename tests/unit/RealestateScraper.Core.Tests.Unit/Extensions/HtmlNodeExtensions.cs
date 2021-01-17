using HtmlAgilityPack;

namespace RealestateScraper.Core.Tests.Unit.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static HtmlNode AppendChild(this HtmlNode node, string html)
        {
            return node.AppendChild(HtmlNode.CreateNode(html));
        }
    }
}
