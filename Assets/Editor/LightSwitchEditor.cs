using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightSwitch))]
[CanEditMultipleObjects]
public class LightSwitchEditor : Editor
{
    public int PanelNumber;
    SerializedProperty Method;

    private void OnEnable()
    {
        Method = serializedObject.FindProperty("MethodNumber");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Method.intValue = GUILayout.Toolbar(Method.intValue, new string[] { "Saturation", "Lights Out"});

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
