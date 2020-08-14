using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystemOLD2
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "Dungeons/Model/LocalizationSystemOLD2/Localizer")]
    public class Localizer : ScriptableObject
    {
        [SerializeField] private string _directory = "Languages";
        [SerializeField] private string _fileExtension = "json";
        [TextArea(1, 2)]
        [SerializeField] private string _missingTextMessage = "Localized text not found";

        private readonly List<LocalizationData> _localizations = new List<LocalizationData>();
        private Dictionary<LocalizationTextID, string> _currentLocalization;

        public event Action LanguageChanged;

        public void Init()
        {
            LoadLocalizations();
            SetCurrentLocalization(_localizations[0].LanguageDesignation);
        }

        public void SetCurrentLocalization(string languageDesignation)
        {
            _currentLocalization = _localizations.Find(localization => localization.LanguageDesignation == languageDesignation).GetDictionary();
            LanguageChanged?.Invoke();
        }

        public string GetLocalizedText(LocalizationTextID key) => _currentLocalization.ContainsKey(key) ? _currentLocalization[key] : _missingTextMessage;

        private void LoadLocalizations()
        {
            string[] fileNames = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, _directory), $"*.{_fileExtension}");

            foreach (var fileName in fileNames)
                _localizations.Add(ReadLocalization(fileName));
        }

        private LocalizationData ReadLocalization(string fileName) => JsonUtility.FromJson<LocalizationData>(File.ReadAllText(fileName));
    }
}
