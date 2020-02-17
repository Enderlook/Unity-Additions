using Enderlook.Unity.ScriptableObjects.PolySwitchers;
using Enderlook.Unity.Utils.UnityEditor;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [CustomEditor(typeof(PolySwitchMaster))]
    internal class PolySwitchMasterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            SerializedProperty indexes = serializedObject.FindPropertyOrBackingField("Indexes");
            EditorGUILayout.PropertyField(indexes);

            SerializedProperty index = serializedObject.FindPropertyOrBackingField("Index");
            int amount = EditorGUILayout.IntField(index.GetGUIContentOfBackingField(), index.intValue + 1);
            if (amount <= 1 || amount >= indexes.intValue)
                amount = Mathf.Clamp(amount, 1, indexes.intValue);
            index.intValue = amount - 1;

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}