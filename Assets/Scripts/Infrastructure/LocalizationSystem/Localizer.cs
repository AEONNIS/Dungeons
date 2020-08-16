using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "Dungeons/Infrastructure/LocalizationSystem/Localizer")]
    public class Localizer : ScriptableObject
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private List<Localization> _localizations;

        private Classifier _classifer = null;

        public Settings GetSettings => _settings;

        #region Unity
        //private void OnEnable() => Init();
        #endregion

        public void Init()
        {
            _settings = new Settings();
            _localizations = new List<Localization>();
            _classifer = LoadClassifier();
        }

        public void CreateLocalization(string localizationPath)
        {
            Localization localization = new Localization();
            _localizations.Add(localization);
            SaveLocalizationAsJson(localization, localizationPath);
        }

        private Classifier LoadClassifier()
        {
            if (File.Exists(_settings.ClassifierFullPath))
            {
                Debug.Log("Classifier file exists....");
                return LoadClassifierFromJson();
            }
            else
            {
                Debug.Log("Classifier file not exists");
                Classifier classifier = new Classifier();
                SaveClassifierAsJson(classifier);
                return classifier;
            }
        }

        private void SaveClassifierAsJson(Classifier classifier)
        {
            File.WriteAllText(_settings.ClassifierFullPath, JsonConvert.SerializeObject(classifier, Formatting.Indented));
            Debug.Log("Classifier Save as Json");
        }

        private Classifier LoadClassifierFromJson()
        {
            return JsonConvert.DeserializeObject<Classifier>(File.ReadAllText(_settings.ClassifierFullPath));
        }

        public void SaveLocalizationAsJson(Localization localization, string localizationPath)
        {
            File.WriteAllText(localizationPath, JsonConvert.SerializeObject(localization, Formatting.Indented));
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private string _directoryName = "Localizations";
            [SerializeField] private string _fileExtension = "json";
            [SerializeField] private string _classifierName = "Classifier";
            [SerializeField] private string _localizationDefaultName = "Localization";
            [SerializeField] private List<string> _localizationsNames;

            private readonly string _directoryPath;
            private readonly string _classifierFullPath;

            public Settings()
            {
                _directoryPath = Path.Combine(Application.streamingAssetsPath, _directoryName);
                _classifierFullPath = Path.Combine(_directoryPath, $"{_classifierName}.{_fileExtension}");
            }

            public string DirectoryPath => _directoryPath;
            public string FileExtention => _fileExtension;
            public string ClassifierFullPath => _classifierFullPath;
            public string LocalizationDefaultName => _localizationDefaultName;
        }
    }
}
