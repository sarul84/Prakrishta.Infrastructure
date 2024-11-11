using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prakrishta.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Prakrishta.Infrastructure.Test
{
    [TestClass]
    public class TypedClientDynamicResolverTest
    {
        [TestMethod]
        public void AddDynamic_HttpClient_DefaultClient()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<HttpClientFactoryOptions>("test", options =>
            {
                options.HttpClientActions.Add((c) => c.BaseAddress = new Uri("http://example.com"));
            });

            serviceCollection.Configure<HttpClientFactoryOptions>("test1", options =>
            {
                options.HttpClientActions.Add((c) => c.BaseAddress = new Uri("http://google.com"));
            });

            serviceCollection.AddHttpClient("test");
            serviceCollection.AddHttpClient("test1");
            serviceCollection.AddScoped<IHttpTypedClient, HttpTypedClient>();
            serviceCollection.AddSingleton<TypedClientResolver<HttpTypedClient>>();

            var services = serviceCollection.BuildServiceProvider();

            var dynamicResolver = services.GetRequiredService<TypedClientResolver<HttpTypedClient>>();
            var client = dynamicResolver.GetService("test");
            var client1 = dynamicResolver.GetService("test1");

            Assert.AreEqual("http://example.com/", client.BaseUri.AbsoluteUri);
            Assert.AreEqual("http://google.com/", client1.BaseUri.AbsoluteUri);
        }
    }

    public class HttpTypedClient(HttpClient client) : IHttpTypedClient
    {
        public Uri BaseUri => client.BaseAddress;
    }

    public interface IHttpTypedClient
    {
        Uri BaseUri { get; }
    }
}
