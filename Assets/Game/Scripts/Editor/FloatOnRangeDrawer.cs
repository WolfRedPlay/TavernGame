using UnityEditor;
using Core.Shared;
using UnityEngine;

namespace Editor
{

    [CustomPropertyDrawer(typeof(FloatAreaOnRange))]
    public class FloatOnRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var minProp = property.FindPropertyRelative("Min");
            var maxProp = property.FindPropertyRelative("Max");


            float min = minProp.floatValue;
            float max = maxProp.floatValue;

            Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);

            Rect sliderRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, 0f, 1f);

            min = Mathf.Round(min * 1000f) / 1000f;
            max = Mathf.Round(max * 1000f) / 1000f;

            minProp.floatValue = min;
            maxProp.floatValue = max;

            float elementWidth = position.width / 2;

            Rect minFieldRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), elementWidth - 5, EditorGUIUtility.singleLineHeight);
            Rect maxFieldRect = new Rect(position.x + elementWidth + 5, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), elementWidth - 5, EditorGUIUtility.singleLineHeight);

            EditorGUIUtility.labelWidth = 30;

            float newMin = EditorGUI.FloatField(minFieldRect, new GUIContent("min"), min);
            newMin = Mathf.Clamp(newMin, 0, max);
            minProp.floatValue = newMin;

            float newMax = EditorGUI.FloatField(maxFieldRect, new GUIContent("max"), max);
            newMax = Mathf.Clamp(newMax, min, 1f);
            maxProp.floatValue = newMax;

            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Высота под 3 строки: заголовок, слайдер, поля ввода
            return 3 * (EditorGUIUtility.singleLineHeight + 2);
        }
    }

}
