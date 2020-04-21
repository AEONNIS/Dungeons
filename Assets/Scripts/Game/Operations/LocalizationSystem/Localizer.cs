using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "Game/Operations/LocalizationSystem")]
    public class Localizer : ScriptableObject
    {
        [SerializeField] private string _directory = "Languages";
        [SerializeField] private string _fileExtention = "json";
        [TextArea(1, 2)]
        [SerializeField] private string _missingTextMessage = "Localized text not found";

        private List<LocalizationData> _localizations = new List<LocalizationData>();
        private Dictionary<LocalizationTextID, string> _currentLocalization;

        public event Action LanguageChanged;

        public void Init()
        {
            LoadLocalizations();
            SetCurrentLocalization(_localizations[0].LanguageDesignation);
        }

        public void SetCurrentLocalization(string languageDesignation)
        {
            _currentLocalization = _localizations.Find(localization => localization.LanguageDesignation == languageDesignation).ConvertToDictionary();
            LanguageChanged?.Invoke();
        }

        public string GetLocalizedText(LocalizationTextID key)
        {
            if (_currentLocalization.ContainsKey(key))
                return _currentLocalization[key];
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
