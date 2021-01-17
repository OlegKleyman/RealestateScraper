using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using RealestateScraper.Core.Tests.Unit.Extensions;
using RichardSzalay.MockHttp;
using Xunit;

namespace RealestateScraper.Core.Tests.Unit
{
    public class RealestateServiceTests
    {
        public static IEnumerable<object[]> GetAllAsyncReturnsResultsFromClientCases()
        {
            return new[]
            {
                new object[]
                {
                    new[] {new RealestateResult(10.15m)},
                },
                new object[]
                {
                    new[]
                    {
                        new RealestateResult(10.15m),
                        new RealestateResult(1337m)
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetAllAsyncReturnsResultsFromClientCases))]
        public async Task GetAllAsyncReturnsResultsFromClient(RealestateResult[] results)
        {
            var document = new HtmlDocument();

            var html = HtmlNode.CreateNode("<html></html>")
                .AppendChild("<body></body>");

            foreach (var result in results)
            {
                html.AppendChild("<div></div>")
                    .AppendChild(@$"<div class=""price"">{result.Price}</div");
            }

            document.DocumentNode.AppendChild(html);

            var handler = new MockHttpMessageHandler();
            handler.When(HttpMethod.Get, "http://example.com").Respond("text/html",
                document.DocumentNode.OuterHtml);

            var service = CreateRealestateService(handler);

            var array = document.DocumentNode.QuerySelectorAll("div div")
                .Select(node => new
                {
                    Price = decimal.Parse(node.InnerText, CultureInfo.InvariantCulture)
                }).ToArray();

            (await service.GetAllAsync()).Should().BeEquivalentTo(array);
        }

        [Fact]
        public void GetAllAsyncThrowsExceptionWhenServerReturnsNonTwoHundredStatus()
        {
            var handler = new MockHttpMessageHandler();
            handler.When(HttpMethod.Get, "http://example.com").Respond(HttpStatusCode.NotFound);

            var service = CreateRealestateService(handler);

            Func<Task> getAllAsync = () => service.GetAllAsync();

            getAllAsync.Should().ThrowExactly<HttpRequestException>()
                .WithMessage("Response status code does not indicate success: 404 (Not Found).");
        }

        private RealestateService CreateRealestateService(HttpMessageHandler handler)
        {
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://example.com")
            };

            return new RealestateService(client);
        }
    }
}
