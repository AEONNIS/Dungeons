using Dungeons.Infrastructure.LocalizationSystem;
using UnityEditor;
using UnityEngine;

namespace Dungeons.Editors.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(LocalizationItem))]
    public partial class ItemDrawer : PropertyDrawer
    {
        private const string _idLabel = "ID";
        private const string _idName = "_id";
        private const float _idLabelWidth = 15f;
        private const float _idValueWidth = 80f;

        private const string _namespacesLabel = "Namespaces";
        private const string _namespacesName = "_namespaces";

        private const string _valuePropertyName = "_value";
        private const string _valuePropertyLabel = "Value";

        private const float _space = 5f;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect idLabelRect = new Rect(position.x, position.y, _idLabelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(idLabelRect, _idLabel);
            Rect idValueRect = new Rect(idLabelRect.x + _idLabelWidth + _space, position.y, _idValueWidth, EditorGUIUtility.singleLineHeight);
            property.FindPropertyRelative(_idName).intValue = 1234567890;
            EditorGUI.SelectableLabel(idValueRect, property.FindPropertyRelative(_idName).intValue.ToString());

            Rect tagsRect = new Rect(idValueRect.x + _idValueWidth + _space, position.y, position.width - idLabelRect.width - idValueRect.width - 2 * _space, EditorGUIUtility.singleLineHeight);
            SerializedProperty tag = property.FindPropertyRelative(_namespacesName);
            tag.intValue = EditorGUI.MaskField(tagsRect, _namespacesLabel, tag.intValue, tag.enumDisplayNames);

            EditorGUI.indentLevel = indent;
        }

        private class Properties
        {

        }
    }
}
