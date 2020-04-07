using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a very simple script that has two variables. The Name of the person attached to the dialogue, and an array of sentences the 
/// NPC has to show.
/// </summary>

[System.Serializable]
public class Dialogue
{
    public string[] Name;

    [TextArea(3,15)]
    public string[] Sentences;
}
