using Dungeons.Infrastructure.LocalizationSystem;
using UnityEditor;
using UnityEngine;

namespace Dungeons.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(Item))]
    public class ItemDrawer : PropertyDrawer
    {
        private const string _idPropertyName = "_id";
        private const string _idPropertyLabel = "ID";
        private const string _tagsPropertyName = "_tags";
        private const string _tagsPropertyLabel = "Tags";
        private const string _valuePropertyName = "_value";
        private const string _valuePropertyLabel = "Value";
        private const float _space = 5f;
        private const float _idLabelWidth = 15f;
        private const float _idValueWidth = 80f;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect idLabelRect = new Rect(position.x, position.y, _idLabelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(idLabelRect, _idPropertyLabel);
            Rect idValueRect = new Rect(idLabelRect.x + _idLabelWidth + _space, position.y, _idValueWidth, EditorGUIUtility.singleLineHeight);
            property.FindPropertyRelative(_idPropertyName).intValue = 1234567890;
            EditorGUI.SelectableLabel(idValueRect, property.FindPropertyRelative(_idPropertyName).intValue.ToString());

            Rect tagsRect = new Rect(idValueRect.x + _idValueWidth + _space, position.y, position.width - idLabelRect.width - idValueRect.width - 2 * _space, EditorGUIUtility.singleLineHeight);
            SerializedProperty tag = property.FindPropertyRelative(_tagsPropertyName);
            tag.intValue = EditorGUI.MaskField(tagsRect, _tagsPropertyLabel, tag.intValue, tag.enumDisplayNames);

            EditorGUI.indentLevel = indent;
        }
    }
}
