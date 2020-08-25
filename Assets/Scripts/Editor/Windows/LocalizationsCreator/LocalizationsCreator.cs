using Dungeons.Infrastructure;
using Dungeons.Infrastructure.LocalizationSystem;
using UnityEditor;
using UnityEngine;

namespace DungeonsEditor.Windows
{
    public partial class LocalizationsCreator : EditorWindow
    {
        [SerializeField] private Settings _settings = new Settings();
        [SerializeField] private Localization _localization;

        private Classifier _classifier = null;

        [MenuItem("Window/Localizations Creator")]
        private static void Init() => GetWindow<LocalizationsCreator>("Localizations Creator").Show();

        #region Unity
        private void Awake() => _classifier = JsonConverter.LoadFromJson<Classifier>(_settings.ClassifierFullPath);

        private void OnGUI()
        {
            SerializedObject window = new SerializedObject(this);
            DrawSettings(window);
            EditorGUILayout.Space(20);
            DrawClassifierButtons();

            if (_classifier != null)
            {
                DrawLocalizationButtons();
            }
        }
        #endregion

        private void DrawSettings(SerializedObject window)
        {
            SerializedProperty settings = window.FindProperty(nameof(_settings));
            EditorGUILayout.PropertyField(settings);
            window.ApplyModifiedProperties();
        }

        private void DrawClassifierButtons()
        {
            if (GUILayout.Button("Create New Classifier"))
                _classifier = Utility.CreateUsingFilePanel(new Classifier(), "Save Classifier as JSON", _settings.DirectoryPath, _settings.ClassifierName, _settings.FileExtention);

            if (GUILayout.Button("Open JSON Classifier"))
                _classifier = Utility.OpenUsingFilePanel<Classifier>("Open JSON Classifier", _settings.DirectoryPath, _settings.FileExtention);
        }

        private void DrawLocalizationButtons()
        {
            if (GUILayout.Button("Create New Localization"))
                _localization = Utility.CreateUsingFilePanel(new Localization(), "Save Localization as JSON", _settings.DirectoryPath, _settings.LocalizationName, _settings.FileExtention);

            if (GUILayout.Button("Open JSON Localization"))
                _localization = Utility.OpenUsingFilePanel<Localization>("Open JSON Localization", _settings.DirectoryPath, _settings.FileExtention);
        }

        private void DrawLocalization(SerializedObject window)
        {
            SerializedProperty localization = window.FindProperty(nameof(_localization));
            EditorGUILayout.PropertyField(localization);
            window.ApplyModifiedProperties();
        }
    }
}
