ChaosCMS [![Stories in Ready](https://badge.waffle.io/pmdevers/ChaosCMS.png?label=ready&title=Ready)](https://waffle.io/pmdevers/ChaosCMS)
=

Travis : [![Build Status](https://travis-ci.org/pmdevers/ChaosCMS.svg?branch=develop)](https://travis-ci.org/pmdevers/ChaosCMS)
AppVeyor : [![Build status](https://ci.appveyor.com/api/projects/status/w6079k2coukjfq2q?svg=true)](https://ci.appveyor.com/project/pmdevers/chaoscms)

Chaos CMS is a core Content Management System.

How to install
====

install the `ChaosCMS.Site` 

The following folders are added.

* data
* macros
* src
  * fonts
  * images
  * js
  * scss
* templates


```csharp

   // This method gets called by the runtime. Use this method to add services to the container.
   // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
   public void ConfigureServices(IServiceCollection services)
   {

      // add this line in your startup.cs
      services.AddChaos<Page, Content, User, Role>()
        .AddJsonStores();
   }

   // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
   public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
   {
      app.UseChaos();
   }

```
