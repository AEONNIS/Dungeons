using System;
using System.IO;
using UnityEngine;

namespace DungeonsEditor.Windows
{
    public partial class LocalizationsCreatorWindow
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] private string _directoryName = "Localizations";
            [SerializeField] private string _fileExtension = "json";
            [SerializeField] private string _classifierName = "Classifier";
            [SerializeField] private string _localizationName = "Localization";

            private readonly string _directoryPath;
            private readonly string _classifierFullPath;
            private readonly string _localizationFullPath;

            public Settings()
            {
                _directoryPath = Path.Combine(Application.streamingAssetsPath, _directoryName);
                _classifierFullPath = Path.Combine(_directoryPath, $"{_classifierName}.{_fileExtension}");
                _localizationFullPath = Path.Combine(_directoryPath, $"{_localizationName}.{_fileExtension}");
            }

            public string DirectoryPath => _directoryPath;
            public string FileExtention => _fileExtension;
            public string ClassifierName => _classifierName;
            public string ClassifierFullPath => _classifierFullPath;
            public string LocalizationName => _localizationName;
            public string LocalizationFullPath => _localizationFullPath;
        }
    }
}
