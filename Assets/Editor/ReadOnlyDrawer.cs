using _00.Work.Resource.Scripts.UI;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // 편집 비활성화 -> 회색 처리
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;  // 다른 필드들 다시 활성화
    }
}
