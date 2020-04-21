using Game.Operations.LocalizationSystem;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class LocalizedTextEditor : EditorWindow
    {
        private const string _menuItemName = "Window/Localized Text Editor";

        private static readonly string _windowTitle = "Localized Text Editor";

        private readonly string _saveButton = "Save localization";
        private readonly string _loadButton = "Load localization";
        private readonly string _createButton = "Create new localization";
        private readonly string _addButton = "+";
        private readonly string _selectTitle = "Select localization file";
        private readonly string _saveTitle = "Save localization file";
        private readonly string _directory = "Languages";
        private readonly string _defaultLanguage = "ru_RU";
        private readonly string _fileExtention = "json";

        [SerializeField] private LocalizationData _localization;

        private Vector2 _scrollPosition;

        [MenuItem(_menuItemName)]
        private static void Init()
        {
            GetWindow<LocalizedTextEditor>(_windowTitle).Show();
        }

        private void OnGUI()
        {
            if (_localization != null)
            {
                SerializedObject window = new SerializedObject(this);
                SerializedProperty localization = window.FindProperty(nameof(_localization));

                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
                EditorGUILayout.PropertyField(localization, true);
                window.ApplyModifiedProperties();
                EditorGUILayout.EndScrollView();

                if (GUILayout.Button(_addButton))
                    _localization.AddItemToList();

                if (GUILayout.Button(_saveButton))
                    SaveLocalization(_localization.LanguageDesignation);
            }

            if (GUILayout.Button(_loadButton))
                LoadLocalization();

            if (GUILayout.Button(_createButton))
                CreateNewLocalization(_defaultLanguage);
        }

        private void LoadLocalization()
        {
            string filePath = EditorUtility.OpenFilePanel(_selectTitle, Path.Combine(Application.streamingAssetsPath, _directory), _fileExtention);

            if (string.IsNullOrEmpty(filePath) == false)
                _localization = JsonUtility.FromJson<LocalizationData>(File.ReadAllText(filePath));
        }

        private void SaveLocalization(string defaultName)
        {
            string filePath = EditorUtility.SaveFilePanel(_saveTitle, Path.Combine(Application.streamingAssetsPath, _directory), defaultName, _fileExtention);

            if (string.IsNullOrEmpty(filePath) == false)
                File.WriteAllText(filePath, JsonUtility.ToJson(_localization, true));
        }

        private void CreateNewLocalization(string languageDesignation)
        {
            _localization = new LocalizationData(languageDesignation);
        }
    }
}
