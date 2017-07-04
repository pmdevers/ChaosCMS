using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChaosCMS.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="result"></param>
        public static void AddErrors(this ControllerBase controller, ChaosResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError(error.Code, error.Description);
            }
        }

       
        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IActionResult ChaosResults(this ControllerBase controller, ChaosResult result)
        {
            if (!result.Succeeded)
            {
                controller.AddErrors(result);
                return controller.BadRequest(controller.ModelState);
            }

            return controller.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static IActionResult ChaosResults(this ControllerBase controller, params ChaosError[] errors)
        {
            if (errors.Count() > 0)
            {
                return controller.ChaosResults(ChaosResult.Failed(errors));
            }
            return controller.ChaosResults(ChaosResult.Success);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="embeddedItem"></param>
        /// <param name="routeName"></param>
        /// <param name="statuscode"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IActionResult PagedHal<T>(this ControllerBase controller, ChaosPaged<T> model, Func<T, HalResponse> embeddedItem, string routeName, HttpStatusCode statuscode = HttpStatusCode.OK, IHalModelConfig config = null) where T : class
        {
            var responseModel = new PagedHalResponse<T>(model);
            var response = new HalResponse(responseModel, config);
            var selfLink = new Link("self", controller.Url.RouteUrl(routeName, new { page = model.CurrentPage, itemsPerPage = model.ItemsPerPage }));
            var firstLink = new Link("first", controller.Url.RouteUrl(routeName));
            var nextLink = new Link("next", controller.Url.RouteUrl(routeName, new { page = model.CurrentPage + 1, itemsPerPage = model.ItemsPerPage }));
            var prevLink = new Link("previous", controller.Url.RouteUrl(routeName, new { page = model.CurrentPage - 1, itemsPerPage = model.ItemsPerPage }));
            var lastLink = new Link("last", controller.Url.RouteUrl(routeName, new { page = model.TotalPages, itemsPerPage = model.ItemsPerPage }));

            response.AddLinks(selfLink);
            response.AddLinks(firstLink);
            if (model.CurrentPage < model.TotalPages)
            {
                response.AddLinks(nextLink);
            }
            if (model.CurrentPage > 1)
            {
                response.AddLinks(prevLink);
            }
            response.AddLinks(lastLink);

            response.AddEmbeddedCollection(routeName, model.Items.Select(embeddedItem));

            return response.ToActionResult(controller);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="links"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal(this ControllerBase controller, IEnumerable<Link> links, string relativeLinkBase = "~/", HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            var linkBase = GetLinkBase(controller, relativeLinkBase);

            var hyperMedia = new HalResponse(new HalModelConfig
            {
                ForceHal = true, // If we're only returning links, always return them
                LinkBase = linkBase
            });

            hyperMedia.AddLinks(links);

            return hyperMedia.ToActionResult(controller, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="hyperMedia"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal(this ControllerBase controller, HalResponse hyperMedia, HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            return hyperMedia.ToActionResult(controller, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="link"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="addSelfLinkIfNotExists"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal<T>(this ControllerBase controller, T model, Link link, string relativeLinkBase = "~/", bool addSelfLinkIfNotExists = true, HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            return controller.Hal(model, new Link[] { link }, relativeLinkBase, addSelfLinkIfNotExists, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="links"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="addSelfLinkIfNotExists"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal<T>(this ControllerBase controller, T model, IEnumerable<Link> links, string relativeLinkBase = "~/", bool addSelfLinkIfNotExists = true, HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            var linkBase = GetLinkBase(controller, relativeLinkBase);

            var response = new HalResponse(model, new HalModelConfig
            {
                LinkBase = linkBase
            })
            .AddLinks(links);

            if (addSelfLinkIfNotExists)
            {
                response.AddSelfLinkIfNotExists(controller.Request);
            }

            return response.ToActionResult(controller, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="modelLink"></param>
        /// <param name="embeddedName"></param>
        /// <param name="embeddedModel"></param>
        /// <param name="embeddedLink"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal<T, E>(this ControllerBase controller, T model, Link modelLink, string embeddedName, IEnumerable<E> embeddedModel, Link embeddedLink, string relativeLinkBase = "~/", HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            return controller.Hal(model, new Link[] { modelLink }, embeddedName, embeddedModel, new Link[] { embeddedLink }, relativeLinkBase, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="modelLink"></param>
        /// <param name="embeddedName"></param>
        /// <param name="embeddedModel"></param>
        /// <param name="embeddedLinks"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal<T, E>(this ControllerBase controller, T model, Link modelLink, string embeddedName, IEnumerable<E> embeddedModel, IEnumerable<Link> embeddedLinks, string relativeLinkBase = "~/", HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            return controller.Hal(model, new Link[] { modelLink }, embeddedName, embeddedModel, embeddedLinks, relativeLinkBase, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="modelLinks"></param>
        /// <param name="embeddedName"></param>
        /// <param name="embeddedModel"></param>
        /// <param name="embeddedLinks"></param>
        /// <param name="relativeLinkBase"></param>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        public static IActionResult Hal<T, E>(this ControllerBase controller, T model, IEnumerable<Link> modelLinks, string embeddedName, IEnumerable<E> embeddedModel, IEnumerable<Link> embeddedLinks, string relativeLinkBase = "~/", HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            var linkBase = GetLinkBase(controller, relativeLinkBase);

            var hyperMedia = new HalResponse(model, new HalModelConfig
            {
                LinkBase = linkBase
            });

            hyperMedia
                .AddLinks(modelLinks)
                .AddEmbeddedCollection(embeddedName, embeddedModel, embeddedLinks);

            return hyperMedia.ToActionResult(controller, statuscode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="relativeLinkBase"></param>
        /// <returns></returns>
        private static string GetLinkBase(ControllerBase controller, string relativeLinkBase)
        {
            string linkBase = null;

            if (!string.IsNullOrWhiteSpace(relativeLinkBase))
            {
                linkBase = controller.Url.Content(relativeLinkBase);
            }

            return linkBase;
        }
    }
}