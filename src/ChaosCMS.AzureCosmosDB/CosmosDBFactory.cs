using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents.Client;

namespace ChaosCMS.AzureCosmosDB
{
    public class CosmosDBFactory
    {
        [ThreadStatic]
        private static DocumentClient client;

        private CosmosDBOptions options;
        private object context;

        public CosmosDBFactory(IOptions<CosmosDBOptions> optionsAccessor, IServiceProvider services)
        {
            options = optionsAccessor?.Value ?? new CosmosDBOptions();

            if (services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }
        }

        public DocumentClient GetInstance()
        {
            
        }
    }
}
