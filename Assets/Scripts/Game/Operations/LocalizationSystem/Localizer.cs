using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField] private string _basePath = "Languages";
        [SerializeField] private string _fileExtention = "json";
        [SerializeField] private List<string> _localizations;
        [SerializeField] private string _missingTextMessage = "Localized text not found ";

        private Dictionary<string, string> _localizedText;

        public void LoadLocalizedText(string localizationName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _basePath, $"{localizationName}.{_fileExtention}");
            _localizedText = new Dictionary<string, string>();

            if (File.Exists(filePath))
            {
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(File.ReadAllText(filePath));
                loadedData.Items.ForEach(item => _localizedText.Add(item.Key, item.Value));
            }
            else
            {

            }
        }

        public string GetLocalizedValue(string key)
        {
            if (_localizedText.ContainsKey(key))
                return _localizedText[key];
            else
                return _missingTextMessage;
        }
    }
}
