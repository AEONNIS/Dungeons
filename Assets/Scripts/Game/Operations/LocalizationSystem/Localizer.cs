using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField] private string _directory = "Languages";
        [SerializeField] private string _fileExtention = "json";
        [SerializeField] private string _missingTextMessage = "Localized text not found";

        private List<LocalizationData> _localizations = new List<LocalizationData>();
        private LocalizationData _currentLocalization;

        #region Unity
        private void Awake()
        {
            LoadLocalizations();
            _currentLocalization = _localizations[0];
        }
        #endregion

        public void LoadLocalizedText(string localizationName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _directory, $"{localizationName}.{_fileExtention}");
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

        private void LoadLocalizations()
        {
            string[] fileNames = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, _directory), $"*.{_fileExtention}");

            foreach (var fileName in fileNames)
                _localizations.Add(ReadLocalization(fileName));
        }

        private LocalizationData ReadLocalization(string fileName)
        {
            return JsonUtility.FromJson<LocalizationData>(File.ReadAllText(fileName));
        }
    }
}
