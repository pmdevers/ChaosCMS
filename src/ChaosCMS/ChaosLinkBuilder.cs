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
            return new Link("self", controller.Url.RouteUrl("page", new { id = manager.GetIdAsync(page).Result }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        /// <typeparam name="TContent"></typeparam>
        /// <returns></returns>
        public static Link SelfLink<TContent>(this ControllerBase controller, ContentManager<TContent> manager, TContent content) where TContent : class
        {
            return new Link("self", controller.Url.RouteUrl("content", new { id = manager.GetIdAsync(content).Result }));
        }

        /// <summary>
        /// /
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static HalResponse CreateEmbeddedResponse<TPage>(this ControllerBase controller, PageManager<TPage> manager, TPage page) where TPage : class
        {
            var response =
                new HalResponse(
                    new
                    {
                        id = manager.GetIdAsync(page).Result,
                        name = manager.GetNameAsync(page).Result
                    });
            response.AddLinks(new[] { controller.SelfLink(manager, page) });
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="controller"></param>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static HalResponse CreateEmbeddedResponse<TContent>(this ControllerBase controller, ContentManager<TContent> manager, TContent content) where TContent : class
        {
            var response =
                new HalResponse(
                    new
                    {
                        id = manager.GetIdAsync(content).Result,
                        name = manager.GetNameAsync(content).Result
                    });
            response.AddLinks(new[]
                                  {
                                      controller.SelfLink(manager, content),
                                      new Link("children", controller.Url.RouteUrl("children", new { id = manager.GetIdAsync(content).Result }))
                                  });
            return response;
        }
    }
}
