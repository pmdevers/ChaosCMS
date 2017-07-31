using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ChaosCMS.LiteDB
{
    public class ObjectIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ObjectId).GetTypeInfo().IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            return new ObjectId(token.ToObject<string>());
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
