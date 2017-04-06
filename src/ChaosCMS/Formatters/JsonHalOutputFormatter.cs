using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace ChaosCMS.Formatters
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonHalOutputFormatter : IOutputFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        public const string HalJsonType = "application/hal+json";

        private readonly IEnumerable<string> _halJsonMediaTypes;
        private readonly JsonOutputFormatter _jsonFormatter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="halJsonMediaTypes"></param>
        public JsonHalOutputFormatter(IEnumerable<string> halJsonMediaTypes = null)
        {
            if (halJsonMediaTypes == null) halJsonMediaTypes = new string[] { HalJsonType };

            var serializerSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();

            this._jsonFormatter = new JsonOutputFormatter(serializerSettings, ArrayPool<Char>.Create());

            this._halJsonMediaTypes = halJsonMediaTypes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializerSettings"></param>
        /// <param name="halJsonMediaTypes"></param>
        public JsonHalOutputFormatter(JsonSerializerSettings serializerSettings, IEnumerable<string> halJsonMediaTypes = null)
        {
            if (halJsonMediaTypes == null) halJsonMediaTypes = new string[] { HalJsonType };

            this._jsonFormatter = new JsonOutputFormatter(serializerSettings, ArrayPool<Char>.Create());

            this._halJsonMediaTypes = halJsonMediaTypes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.ObjectType == typeof(HalResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            string mediaType = context.ContentType.HasValue ? context.ContentType.Value : null;

            object value = null;
            var halResponse = ((HalResponse)context.Object);

            // If it is a HAL response but set to application/json - convert to a plain response
            var serializer = JsonSerializer.Create();

            if (!halResponse.Config.ForceHal && !_halJsonMediaTypes.Contains(mediaType))
            {
                value = halResponse.ToPlainResponse(serializer);
            }
            else
            {
                value = halResponse.ToJObject(serializer);
            }

            var jsonContext = new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, value.GetType(), value);

            await _jsonFormatter.WriteAsync(jsonContext);
        }
    }
}
