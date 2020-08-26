using Dungeons.Infrastructure.LocalizationSystem;
using UnityEditor;
using UnityEngine;

namespace DungeonsEditor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(LocalizationItem))]
    public class LocalizationItemDrawer : PropertyDrawer
    {
        #region Unity
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect idLabelRect = new Rect(position.x, position.y, GetFieldWidth(Properties.IdLabel), EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(idLabelRect, Properties.IdLabel);

            SerializedProperty id = property.FindPropertyRelative(Properties.IdName);
            id.intValue = 1234567890;
            Rect idNameRect = new Rect(idLabelRect.x + GetFieldWidth(Properties.IdLabel) + Properties.Space, position.y, GetFieldWidth(id.intValue.ToString()), EditorGUIUtility.singleLineHeight);
            EditorGUI.SelectableLabel(idNameRect, id.intValue.ToString());

            Rect tagsRect = new Rect(idNameRect.x + GetFieldWidth(id.intValue.ToString()) + Properties.Space, position.y, position.width - idLabelRect.width - idNameRect.width - 2 * Properties.Space, EditorGUIUtility.singleLineHeight);
            SerializedProperty tags = property.FindPropertyRelative(Properties.TagsName);
            tags.intValue = EditorGUI.MaskField(tagsRect, Properties.TagsLabel, tags.intValue, tags.enumDisplayNames);

            EditorGUI.indentLevel = indent;
        }
        #endregion

        private static float GetFieldWidth(string fieldName) => fieldName.Length * Properties.CharacterWidth;

        private static class Properties
        {
            public const float Space = 5f;
            public const float CharacterWidth = 7.5f;
            public const string IdLabel = "ID";
            public const string IdName = "_id";
            public const string TagsLabel = "Tags";
            public const string TagsName = "_tags";
            public const string TextLabel = "Text";
            public const string TextName = "_text";
        }
    }
}
