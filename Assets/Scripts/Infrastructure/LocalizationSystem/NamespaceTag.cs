using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Flags, JsonConverter(typeof(StringEnumConverter))]
    public enum NamespaceTag
    {
        Model = 1,
        Items = 2,
        Tiles = 4,
    }
}
