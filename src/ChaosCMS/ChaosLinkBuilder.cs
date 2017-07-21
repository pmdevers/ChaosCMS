using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public static class ChaosLinkBuilder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <typeparam name="TPage"></typeparam>
        /// <returns></returns>
        public static Link SelfLink<TPage>(this ControllerBase controller, PageManager<TPage> manager, TPage page) where TPage : class
        {
            return new Link("self", controller.Url.RouteUrl("page", new { id = manager.GetId(page) }));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <typeparam name="TPageType"></typeparam>
        /// <returns></returns>
        public static Link SelfLink<TPageType>(this ControllerBase controller, PageTypeManager<TPageType> manager, TPageType page) where TPageType : class
        {
            return new Link("self", controller.Url.RouteUrl("pagetype", new { id = manager.GetId(page) }));
        }

        /// <summary>
        /// /
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static HalResponse CreateEmbeddedResponse<TPage>(this ControllerBase controller, PageManager<TPage> manager, TPage page) 
            where TPage : class
        {
            var response =
                new HalResponse(
                    new
                    {
                        id = manager.GetId(page),
                        name = manager.GetName(page)
                    });
            response.AddLinks(new[] { controller.SelfLink(manager, page) });
            return response;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <typeparam name="TPageType"></typeparam>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static HalResponse CreateEmbeddedResponse<TPageType>(this ControllerBase controller, PageTypeManager<TPageType> manager, TPageType page) where TPageType : class
        {
            var response =
                new HalResponse(
                    new
                    {
                        id = manager.GetId(page),
                        name = manager.GetName(page)
                    });
            response.AddLinks(new[] { controller.SelfLink(manager, page) });
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Link[] GetPageLinks<TPage>(this ControllerBase controller, PageManager<TPage> manager, TPage page) where TPage : class
        {
            var id = manager.GetId(page);
            return new[]
            {
                controller.SelfLink(manager, page),
                new Link("children", controller.Url.RouteUrl("page-children", new {  id = id})),
                new Link("content", controller.Url.RouteUrl("page-content", new { id = id })),
                new Link("ac:copy", controller.Url.RouteUrl("copy-page", new { id = id })),
                new Link("ac:publish", controller.Url.RouteUrl("publish-page", new { id = id }))
            };
        }
    }
}