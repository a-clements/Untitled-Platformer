using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script trigggers a dialogue tree on a cut scene.
/// </summary>

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue GetDialogue;

    private DialogueManager Dialoguemanager;

    private void Start()
    {
        Dialoguemanager = FindObjectOfType<DialogueManager>();

        Dialoguemanager.StartDialogue(GetDialogue);
    }
}
