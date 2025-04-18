using UnityEditor;
using UnityEngine;
using View;
[CustomEditor(typeof(ActionButton))]
public class UIButtonHandlerEditor : Editor
{
    SerializedProperty useScriptableObjectProp;
    SerializedProperty scriptableObjectActionProp;
    SerializedProperty unityEventActionProp;

    void OnEnable()
    {
        useScriptableObjectProp = serializedObject.FindProperty("useScriptableObject");
        scriptableObjectActionProp = serializedObject.FindProperty("scriptableObjectAction");
        unityEventActionProp = serializedObject.FindProperty("unityEventAction");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(useScriptableObjectProp, new GUIContent("Use Scriptable Object"));

        if (useScriptableObjectProp.boolValue)
        {
            EditorGUILayout.PropertyField(scriptableObjectActionProp);
        }
        else
        {
            EditorGUILayout.PropertyField(unityEventActionProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
