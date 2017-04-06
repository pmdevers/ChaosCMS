using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HalResponseExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool HasSelfLink(this HalResponse response)
        {
            return response.HasLink(Link.RelForSelf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="halModel"></param>
        /// <param name="links"></param>
        /// <returns></returns>
        public static HalResponse AddLinks(this HalResponse halModel, params Link[] links)
        {
            return halModel.AddLinks(links);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hyperMedia"></param>
        /// <param name="resourceName"></param>
        /// <param name="model"></param>
        /// <param name="links"></param>
        /// <returns></returns>
        public static HalResponse AddEmbeddedResource<T>(this HalResponse hyperMedia, string resourceName, T model, IEnumerable<Link> links = null)
        { 
            if (links == null)
            {
                links = Enumerable.Empty<Link>();
            }

            var embedded = new HalResponse(model, hyperMedia.Config).AddLinks(links);
            hyperMedia.AddEmbeddedResource(resourceName, embedded);

            return hyperMedia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hyperMedia"></param>
        /// <param name="collectionName"></param>
        /// <param name="model"></param>
        /// <param name="links"></param>
        /// <returns></returns>
        public static HalResponse AddEmbeddedCollection<T>(this HalResponse hyperMedia, string collectionName, IEnumerable<T> model, IEnumerable<Link> links = null)
        {
            if (links == null)
            {
                links = Enumerable.Empty<Link>();
            }

            var embedded = model
                            .Select(m => new HalResponse(m, hyperMedia.Config).AddLinks(links))
                            .ToArray();

            hyperMedia.AddEmbeddedCollection(collectionName, embedded);

            return hyperMedia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static HalResponse AddEmbeddedResources<T>(this HalResponse response, IEnumerable<KeyValuePair<string, T>> resources)
        {
            foreach (var resource in resources)
            {
                response.AddEmbeddedResource(resource.Key, resource.Value);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="embeddedCollections"></param>
        /// <returns></returns>
        public static HalResponse AddEmbeddedCollections<T>(this HalResponse response, IEnumerable<KeyValuePair<string, IEnumerable<T>>> embeddedCollections)
        {
            foreach (var embeddedCollection in embeddedCollections)
            {
                response.AddEmbeddedCollection(embeddedCollection.Key, embeddedCollection.Value);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="embeddedCollections"></param>
        /// <returns></returns>
        public static HalResponse AddEmbeddedCollections(this HalResponse response, IEnumerable<KeyValuePair<string, IEnumerable<HalResponse>>> embeddedCollections)
        {

            foreach (var embeddedCollection in embeddedCollections)
            {
                response.AddEmbeddedCollection(embeddedCollection.Key, embeddedCollection.Value);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HalResponse AddSelfLinkIfNotExists(this HalResponse response, HttpRequest request)
        {
            if (!response.HasSelfLink())
            {
                response.AddSelfLink(request);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HalResponse AddSelfLink(this HalResponse response, HttpRequest request)
        {
            var selfLink = new Link(Link.RelForSelf, request.Path, method: request.Method);
            response.AddLinks(selfLink);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="controller"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult(this HalResponse model, ControllerBase controller, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ObjectResult(model)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
