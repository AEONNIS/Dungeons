using Dungeons.Model.LocalizationSystemOLD1;
using UnityEditor;
using UnityEngine;

namespace Dungeons.Editor.CustomEditors
{
    [CustomEditor(typeof(Localization))]
    public class LocalizationEditor : UnityEditor.Editor
    {
        private Localization _localization;

        private void OnEnable()
        {
            _localization = target as Localization;
        }

        //public override void OnInspectorGUI()
        //{
        //    serializedObject.Update();
        //    EditorGUILayout.TextField(nameof(_localization.LanguageDesignation), _localization.LanguageDesignation);
        //    DrawDataList();

        //    if (GUILayout.Button("+ Items"))
        //        _localization.DataList.AddItems();

        //    if (GUILayout.Button("+ Data"))
        //        _localization.DataList.AddData();

        //    serializedObject.ApplyModifiedProperties();
        //}

        private void DrawDataList()
        {
            foreach (var data in _localization.DataList.Data)
            {
                if (data is LocalizationItems)
                    foreach (var item in (data as LocalizationItems).Items)
                        DrawItem(item);
            }
        }

        private void DrawItem(LocalizationItem item)
        {
        }
    }
}
