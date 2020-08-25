using Newtonsoft.Json;
using System.IO;

namespace Dungeons.Infrastructure
{
    public static class JsonConverter
    {
        public static T SaveAsJson<T>(string path, T obj)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
            return obj;
        }

        public static T LoadFromJson<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}
