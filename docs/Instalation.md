Getting started
===

This section wil guid you through the basic setup of the ChaosCMS.

Setup
=

Create a new `Web Project` in Visual Studio.
Install the `ChaosCMS.Starter` package via the nuget package explorer.

this will create the following directory.

- data
- macros
- model
- src
- templates


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