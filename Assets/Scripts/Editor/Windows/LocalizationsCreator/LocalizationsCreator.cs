using Dungeons.Infrastructure.LocalizationSystem;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DungeonsEditor.Windows
{
    public partial class LocalizationsCreator : EditorWindow
    {
        [SerializeField] private Settings _settings = new Settings();
        [SerializeField] private Classifier _classifier;

        [MenuItem(UISettings.MenuItem)]
        private static void Init() => GetWindow<LocalizationsCreator>(UISettings.WindowTitle).Show();

        #region Unity
        private void OnGUI()
        {
            DrawSettings();
            EditorGUILayout.Space(20);

            if (GUILayout.Button(UISettings.CreateClassifierButton))
                CreateClassifier();

            if (GUILayout.Button(UISettings.LoadClassifierButton))
                LoadClassifier();
        }
        #endregion

        private void DrawSettings()
        {
            SerializedObject window = new SerializedObject(this);
            SerializedProperty settings = window.FindProperty(nameof(_settings));
            EditorGUILayout.PropertyField(settings);

            { //DBG
                SerializedProperty classifier = window.FindProperty(nameof(_classifier));
                EditorGUILayout.PropertyField(classifier);
            }

            window.ApplyModifiedProperties();
        }

        private void CreateClassifier()
        {
            _classifier = new Classifier();
            string classifierPath = EditorUtility.SaveFilePanel(UISettings.SaveClassifierAsJsonTitle, _settings.DirectoryPath, _settings.ClassifierName, _settings.FileExtention);

            if (string.IsNullOrEmpty(classifierPath) == false)
                File.WriteAllText(classifierPath, JsonConvert.SerializeObject(_classifier, Formatting.Indented));
        }

        private void LoadClassifier()
        {
            string classifierPath = EditorUtility.OpenFilePanel(UISettings.LoadJsonClassifierTitle, _settings.DirectoryPath, _settings.FileExtention);

            if (string.IsNullOrEmpty(classifierPath) == false)
                _classifier = JsonConvert.DeserializeObject<Classifier>(File.ReadAllText(classifierPath));
        }
    }
}
