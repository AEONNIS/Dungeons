using Dungeons.Infrastructure.LocalizationSystem;
using UnityEditor;
using UnityEngine;

namespace DungeonsEditor.CustomEditors
{
    [CustomEditor(typeof(Localizer))]
    public class LocalizerEditor : Editor
    {
        private Localizer _localizer;

        #region Unity
        private void OnEnable() => _localizer = target as Localizer;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button(Settings.CreateLocalizationButtonName))
            {
                string localizationPath = EditorUtility.SaveFilePanel(Settings.SaveLocalizationAsJsonTitle, _localizer.GetSettings.DirectoryPath,
                                                                      _localizer.GetSettings.LocalizationDefaultName, _localizer.GetSettings.FileExtention);

                if (string.IsNullOrEmpty(localizationPath) == false)
                    _localizer.CreateLocalization(localizationPath);
            }

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        private class Settings
        {
            public static string CreateLocalizationButtonName => "Create Localization";
            public static string SaveLocalizationAsJsonTitle => "Save Localization as JSON";
        }
    }
}
