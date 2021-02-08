using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MFL.Common.JsonConverters
{
    public class SingleOrManyConverter<T> : JsonConverter<IEnumerable<T>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return (typeToConvert == typeof(IEnumerable<T>));
        }

        public override IEnumerable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var elements = new List<T>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                var element = JsonSerializer.Deserialize<T>(ref reader, options);
                elements.Add(element);
            }
            else
            {
                elements = JsonSerializer.Deserialize<List<T>>(ref reader, options);
            }

            return elements;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
