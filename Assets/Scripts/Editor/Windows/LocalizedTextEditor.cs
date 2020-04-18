using Game.Operations.LocalizationSystem;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class LocalizedTextEditor : EditorWindow
    {
        [SerializeField] private LocalizationData _data;
        private readonly string _directory = "Languages";
        private readonly string _fileExtention = "json";

        [MenuItem("Window/Localized Text Editor")]
        private static void Init()
        {
            GetWindow<LocalizedTextEditor>("Localized Text Editor").Show();
        }

        private void OnGUI()
        {
            if (_data != null)
            {
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("_data");
                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Save data"))
                    SaveGameData();
            }

            if (GUILayout.Button("Load data"))
                LoadGameData();

            if (GUILayout.Button("Create new data"))
                CreateNewData();
        }

        private void LoadGameData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select localization data file", Path.Combine(Application.streamingAssetsPath, _directory), _fileExtention);

            if (string.IsNullOrEmpty(filePath) == false)
            {
                string dataAsJson = File.ReadAllText(filePath);
                _data = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            }
        }

        private void SaveGameData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save localization data file", Path.Combine(Application.streamingAssetsPath, _directory), "", _fileExtention);

            if (string.IsNullOrEmpty(filePath) == false)
            {
                string dataAsJson = JsonUtility.ToJson(_data);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        private void CreateNewData()
        {
            _data = new LocalizationData();
        }
    }
}
