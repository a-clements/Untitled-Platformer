using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue GetDialogue;

    void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(GetDialogue);
    }

    private void Start()
    {
        TriggerDialogue();
    }
}
