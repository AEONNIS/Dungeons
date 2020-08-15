using Dungeons.Model.LocalizationSystemOLD1;
using UnityEditor;
using UnityEngine;

namespace Dungeons.Editors.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(LocalizationTextID))]
    public class LocalizationTextIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUILayout.BeginHorizontal();
            SerializedProperty index = property.FindPropertyRelative("_index");
            EditorGUILayout.EndHorizontal();
        }
    }
}
