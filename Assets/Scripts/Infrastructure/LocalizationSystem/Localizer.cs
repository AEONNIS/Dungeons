using Newtonsoft.Json;
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

        #region Unity
        private void OnEnable() => Init();
        #endregion

        public void Init()
        {
            _classifer = LoadClassifier();
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
