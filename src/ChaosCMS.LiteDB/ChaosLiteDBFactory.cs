using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.LiteDB
{
    public class ChaosLiteDBFactory
    {
        [ThreadStatic]
        private static LiteDatabase liteDatabase;

        private ChaosLiteDBStoreOptions options;
        private readonly HttpContext context;

        public ChaosLiteDBFactory(IOptions<ChaosLiteDBStoreOptions> optionsAccessor, IServiceProvider services)
        {
            options = optionsAccessor?.Value ?? new ChaosLiteDBStoreOptions();

            if (services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }
        }

        public LiteDatabase GetInstance()
        {
            if(context == null)
            {
                if(liteDatabase == null)
                {
                    liteDatabase = new LiteDatabase(options.ConnectionString);
                }
            }

            var instance = context.Items["liteDb"] as LiteDatabase;

            if(instance == null)
            {
                instance = new LiteDatabase(options.ConnectionString);
                context.Items["liteDb"] = instance;
            }

            return instance;
        }
    }
}
