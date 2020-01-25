﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightSwitch))]
public class LightSwitchEditor : Editor
{
    public int PanelNumber;

    public override void OnInspectorGUI()
    {
        LightSwitch Lightswitch = (LightSwitch)target;

        EditorUtility.SetDirty(target);

        PanelNumber = GUILayout.Toolbar(PanelNumber, new string[] { "Saturation", "Lights Out"});



        Lightswitch.MethodNumber = PanelNumber;

        base.OnInspectorGUI();
    }
}