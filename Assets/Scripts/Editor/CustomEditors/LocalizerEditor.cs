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
        private void OnEnable()
        {
            _localizer = target as Localizer;
            _localizer.Init();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.Button(Settings.CreateLocalizationButtonName);

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        private class Settings
        {
            public static string CreateLocalizationButtonName => "Create Localization";
        }
    }
}
