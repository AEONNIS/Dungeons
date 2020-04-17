using Attributes;
using UnityEngine;
using UnityEditor;

namespace Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ListAttribute))]
    public class ListAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}
