﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ChaosCMS.Extensions
{
    internal static class ObjectExtensions
    {
        internal static IDictionary<string, object> ToDictionary(this object obj)
        {
            IDictionary<string, object> vardic;

            if (obj is IDictionary<string, object>)
            {
                vardic = (IDictionary<string, object>)obj;
            }
            else if (obj is JObject)
            {
                vardic = ToDictionary((JObject)obj);
            }
            else
            {
                vardic = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                var properties = obj.GetType().GetTypeInfo().DeclaredProperties;

                foreach (var prop in properties)
                {
                    var objValue = prop.GetValue(obj, null);

                    vardic.Add(prop.Name, objValue);
                }
            }

            return vardic;
        }

        internal static IDictionary<string, object> ToDictionary(this JObject obj)
        {
            var vardic = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            var properties = obj.Properties();

            foreach (var prop in properties)
            {
                var objValue = prop.Value;
                vardic.Add(prop.Name, objValue);
            }

            return vardic;
        }
    }
}
