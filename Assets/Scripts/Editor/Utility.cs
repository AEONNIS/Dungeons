using Dungeons.Infrastructure;
using UnityEditor;

namespace DungeonsEditor
{
    public static class Utility
    {
        public static T CreateUsingFilePanel<T>(T obj, string panelTitle, string directory, string defaultName, string fileExtention) where T : class
        {
            string path = EditorUtility.SaveFilePanel(panelTitle, directory, defaultName, fileExtention);
            return string.IsNullOrEmpty(path) ? null : JsonConverter.SaveAsJson(path, obj);
        }

        public static T OpenUsingFilePanel<T>(string panelTitle, string directory, string fileExtention) where T : class
        {
            string path = EditorUtility.OpenFilePanel(panelTitle, directory, fileExtention);
            return string.IsNullOrEmpty(path) ? null : JsonConverter.LoadFromJson<T>(path);
        }
    }
}
