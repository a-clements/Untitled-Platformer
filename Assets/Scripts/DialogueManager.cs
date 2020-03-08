using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script types a dialogue letter by letter at a rate determined by the WaitTimer, which is defined during design time. This script
/// will enqueue and dequeue dialogue and load the next scene once all dialogue has been displayed.
/// </summary>

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Dialogue = new Queue<string>();
    // Start is called before the first frame update

    [SerializeField] private Text DialogueText;
    [SerializeField] private string SceneName;
    [SerializeField] private float WaitTimer = 0.5f;

    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue.Clear();

        foreach(string Sentence in dialogue.Sentences)
        {
            Dialogue.Enqueue(Sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        else
        {
            StopAllCoroutines();
            StartCoroutine(Typing(Dialogue.Dequeue()));
        }
    }

    void EndDialogue()
    {
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
    }

    IEnumerator Typing(string dialogue)
    {
        DialogueText.text = "";

        yield return new WaitForSeconds(WaitTimer);

        foreach (char letter in dialogue.ToCharArray())
        {
            DialogueText.text += letter;

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    void Start()
    {

    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
    }
}
