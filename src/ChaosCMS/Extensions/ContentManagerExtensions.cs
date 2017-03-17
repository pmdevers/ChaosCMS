using System;
using System.Threading.Tasks;
using ChaosCMS.Managers;
using Newtonsoft.Json;

namespace ChaosCMS.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContentManagerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<TResult> GetValueAsync<TResult, TContent>(this ContentManager<TContent> manager, TContent content)
            where TContent : class
            where TResult : class , new()
        {
            var value = await manager.GetValueAsync(content);
            var result = JsonConvert.DeserializeObject<TResult>(value);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task SetValueAsync<TContent>(this ContentManager<TContent> manager, TContent content, object value)
            where TContent : class
        {
            var serialized = JsonConvert.SerializeObject(value);
            await manager.SetValueAsync(content, serialized);
        }
    }
}
