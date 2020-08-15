using System;
using System.IO;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "Dungeons/Infrastructure/LocalizationSystem/Localizer")]
    public class Localizer : ScriptableObject
    {
        [SerializeField] private Settings _settings = new Settings();

        private Classifier _classifer = null;

        public void Init()
        {
            _classifer = LoadClassifier();
        }

        private Classifier LoadClassifier()
        {
            if (File.Exists(_settings.ClassifierFullPath))
            {
                Debug.Log("Classifier file loaded");
                return null;
            }
            else
            {
                Debug.Log("Classifier file not exists");

                return new Classifier();
            }
        }

        private void SaveClassifierAsJson(Classifier classifier)
        {
            //File.WriteAllText(_settings.ClassifierFullPath, JsonConvert);
        }

        [Serializable]
        private class Settings
        {
            [SerializeField] private string _directoryName = "Localizations";
            [SerializeField] private string _fileExtension = "json";
            [SerializeField] private string _classifierName = "Classifier";

            private readonly string _classifierFullPath;

            public Settings()
            {
                _classifierFullPath = Path.Combine(Application.streamingAssetsPath, _directoryName, $"{_classifierName}.{_fileExtension}");
            }

            public string ClassifierFullPath => _classifierFullPath;
        }
    }
}
