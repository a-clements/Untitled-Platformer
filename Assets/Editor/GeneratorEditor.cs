using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[CustomEditor(typeof(Generator))] 
public class UIManagerEditor : Editor 
{
    public int MethodIndex = 0;

    public override void OnInspectorGUI() 
    {
        Generator Manager = (Generator)target; 

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        string[] Methods = new[] { "Method One", "Method Two", "Method Three" }; 
        MethodIndex = EditorGUILayout.Popup(MethodIndex, Methods, GUILayout.ExpandWidth(true)); 
        Manager.MethodIndex = MethodIndex;

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        base.OnInspectorGUI(); 
    }
}